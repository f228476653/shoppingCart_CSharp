using ShoppingCart.AppServices.Models;
using ShoppingCart.Domain.AggregatesModel;
using ShoppingCart.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.AppServices.Services
{
    public class CartRepository : ICartRepository
    {
        private ShoppingCartContext _context;

        public CartRepository(ShoppingCartContext context)
        {
            _context = context;
        }
        public void AddCar(CartDto car)
        {
            try
            {
                _context.ShoppingCart.Add(car);
                _context.SaveChanges();
            }
            catch (Exception e) {
                throw e;
            }
        }

        public bool CarExists(Guid carId)
        {
            throw new NotImplementedException();
        }

        public void DeleteCar(CartDto car)
        {
            throw new NotImplementedException();
        }

        public CartDto GetCar(Guid carID)
        {
            return _context.ShoppingCart.FirstOrDefault(c => c.Id == carID);
        }

        public IEnumerable<CartDto> GetCars()
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            return true;
        }

        public void UpdateCar(CartDto car)
        {
            throw new NotImplementedException();
        }
    }
}
