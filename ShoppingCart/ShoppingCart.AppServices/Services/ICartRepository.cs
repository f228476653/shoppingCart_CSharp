using ShoppingCart.AppServices.Models;
using ShoppingCart.Domain.AggregatesModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.AppServices.Services
{
    public interface ICartRepository
    {
        
        IEnumerable<CartDto> GetCars();
        CartDto GetCar(Guid carID);
        void AddCar(CartDto car);
        bool CarExists(Guid carId);
        bool Save();
        void DeleteCar(CartDto car);
        void UpdateCar(CartDto car);
    
       }
}
