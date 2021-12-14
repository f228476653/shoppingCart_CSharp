using ShoppingCart.AppServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.AppServices.Services
{
    public interface ICartRepository
    {
        
        IEnumerable<CartEntity> GetCars();
        CartEntity GetCar(Guid carID);
        void AddCar(CartEntity car);
        bool CarExists(Guid carId);
        bool Save();
        void DeleteCar(CartEntity car);
        void UpdateCar(CartEntity car);
    
       }
}
