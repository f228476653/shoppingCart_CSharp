using ShoppingCart.AppServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.AppServices.Services
{
    public class CartRepository : ICartRepository
    {
        public void AddCar(CartEntity car)
        {
            throw new NotImplementedException();
        }

        public bool CarExists(Guid carId)
        {
            throw new NotImplementedException();
        }

        public void DeleteCar(CartEntity car)
        {
            throw new NotImplementedException();
        }

        public CartEntity GetCar(Guid carID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CartEntity> GetCars()
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public void UpdateCar(CartEntity car)
        {
            throw new NotImplementedException();
        }
    }
}
