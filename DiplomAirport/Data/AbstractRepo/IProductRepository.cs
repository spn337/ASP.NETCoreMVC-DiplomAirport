using DiplomAirport.Models;
using System.Collections.Generic;

namespace DiplomAirport.Data.AbstractRepo
{
    interface IProductRepository
    {
        IEnumerable<Product> Products { get; }
    }
}
