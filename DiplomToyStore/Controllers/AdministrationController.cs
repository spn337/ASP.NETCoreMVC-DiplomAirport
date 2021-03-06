﻿using DiplomToyStore.Domain.AbstractRepo;
using DiplomToyStore.Models;
using DiplomToyStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomToyStore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AdministrationController> _logger;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public AdministrationController(
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            ILogger<AdministrationController> logger,
            IProductRepository productRepository,
            ICategoryRepository categoryRepository
            )
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _logger = logger;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        #region Products manage
        [HttpGet]
        public ViewResult ListProducts() => View(_productRepository.Products);

        [HttpGet]
        public ViewResult CreateProduct()
        {
            ViewBag.Categories = new SelectList(_categoryRepository.Categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult CreateProduct(Product model)
        {
            _productRepository.AddProduct(model);
            return RedirectToAction("ListProducts", "Administration");
        }

        [HttpGet]
        public ViewResult EditProduct(int id)
        {
            ViewBag.Categories = new SelectList(_categoryRepository.Categories, "Id", "Name");
            return View(_productRepository.GetProductById(id));
        }

        [HttpPost]
        public IActionResult EditProduct(Product model)
        {
            if (ModelState.IsValid)
            {
                _productRepository.UpdateProduct(model);
                return RedirectToAction("ListProducts", "Administration");

            }
            return View(model);
        }

        [HttpPost]
        public IActionResult DeleteProduct(int id)
        {
            var model = _productRepository.GetProductById(id);
            if(model != null)
            {
                _productRepository.DeleteProduct(model);
                return RedirectToAction("ListProducts", "Administration");
            }       
            return View(model);
        }
        #endregion


        #region Categories manage
        [HttpGet]
        public ViewResult ListCategories() => View(_categoryRepository.Categories);

        [HttpGet]
        public ViewResult CreateCategory() => View();

        [HttpPost]
        public IActionResult CreateCategory(Category model)
        {
            _categoryRepository.AddCategory(model);
            return RedirectToAction("ListCategories", "Administration");
        }

        [HttpGet]
        public ViewResult EditCategory(int id)
            => View(_categoryRepository.Categories.SingleOrDefault(x => x.Id == id));

        [HttpPost]
        public IActionResult EditCategory(Category model)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.UpdateCategory(model);
                return RedirectToAction("ListCategories", "Administration");

            }
            return View(model);
        }

        [HttpPost]
        public IActionResult DeleteCategory(int id)
        {
            var model = _categoryRepository.Categories.SingleOrDefault(x => x.Id == id);

            if (model == null)
            {
                ViewBag.ErrorMessage = $"Category with Id = {id} cannot be found";
                return View("NotFound");
            }

            try
            {
                _categoryRepository.DeleteCategory(model);
                return RedirectToAction("ListCategories", "Administration");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Exception Occured: {ex}");
                ViewBag.ErrorTitle = $"{model.Name} category is in role";
                ViewBag.ErrorMessage = $"{model.Name} category cannot be deleted as there are products in this category";
                return View("Error");
            }
        }
        #endregion


        #region Roles manage
        [HttpGet]
        public ViewResult ListRoles() => View(_roleManager.Roles);


        [HttpGet]
        public ViewResult CreateRole() => View();


        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole
                {
                    Name = model.RoleName
                };

                var result = await _roleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NotFound");
            }

            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };

            foreach (var user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found";
                return View("NotFound");
            }

            role.Name = model.RoleName;

            var result = await _roleManager.UpdateAsync(role);

            if (result.Succeeded)
            {
                return RedirectToAction("ListRoles");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.roleId = roleId;

            var role = await _roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View("NotFound");
            }

            var modelList = new List<UsersInRoleViewModel>();

            foreach (var user in _userManager.Users)
            {
                var model = new UsersInRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                model.IsSelected = await _userManager.IsInRoleAsync(user, role.Name) ? true : false;

                modelList.Add(model);
            }

            return View(modelList);
        }


        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UsersInRoleViewModel> modelList, string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View("NotFound");
            }

            for (int i = 0; i < modelList.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(modelList[i].UserId);
                var isUserInRole = await _userManager.IsInRoleAsync(user, role.Name);

                IdentityResult result = null;

                if (modelList[i].IsSelected && !(isUserInRole))
                {
                    result = await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!(modelList[i].IsSelected) && isUserInRole)
                {
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if (i < (modelList.Count - 1))
                    {
                        continue;
                    }
                }
            }

            return RedirectToAction("EditRole", new { Id = roleId });
        }


        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NotFound");
            }

            try
            {
                var result = await _roleManager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View("ListRoles");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Exception Occured: {ex}");
                ViewBag.ErrorTitle = $"{role.Name} role is in use";
                ViewBag.ErrorMessage = $"{role.Name} role cannot be deleted as there are users in this role";
                return View("Error");
            }
        }
        #endregion


        #region Users manage
        [HttpGet]
        public ViewResult ListUsers() => View(_userManager.Users);


        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View("NotFound");
            }

            var userClaims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            var model = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender,
                Country = user.Country,
                Claims = userClaims.Select(c => c.Value).ToList(),
                Roles = userRoles
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {model.Id} cannot be found";
                return View("NotFound");
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Gender = model.Gender;
            user.Email = model.Email;
            user.UserName = model.Email;
            user.Country = model.Country;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction("ListUsers");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View("NotFound");
            }

            try
            {
                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View("ListUsers");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Exception Occured: {ex}");
                ViewBag.ErrorTitle = $"{user.UserName} user is in role";
                ViewBag.ErrorMessage = $"{user.UserName} user cannot be deleted as there are roles in this user";
                return View("Error");
            }
        }


        [HttpGet]
        public async Task<IActionResult> EditRolesInUser(string userId)
        {
            ViewBag.userId = userId;

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }

            var modelList = new List<RolesInUserViewModel>();

            foreach (var role in _roleManager.Roles)
            {
                var model = new RolesInUserViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };

                model.IsSelected = await _userManager.IsInRoleAsync(user, role.Name) ? true : false;

                modelList.Add(model);
            }

            return View(modelList);
        }


        [HttpPost]
        public async Task<IActionResult> EditRolesInUser(List<RolesInUserViewModel> modelList, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }

            for (int i = 0; i < modelList.Count; i++)
            {
                var role = await _roleManager.FindByIdAsync(modelList[i].RoleId);
                var isRoleInUser = await _userManager.IsInRoleAsync(user, role.Name);

                IdentityResult result = null;

                if (modelList[i].IsSelected && !(isRoleInUser))
                {
                    result = await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!(modelList[i].IsSelected) && isRoleInUser)
                {
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if (i < (modelList.Count - 1))
                    {
                        continue;
                    }
                }
            }

            return RedirectToAction("EditUser", new { Id = userId });
        }

        #endregion


    }
}
