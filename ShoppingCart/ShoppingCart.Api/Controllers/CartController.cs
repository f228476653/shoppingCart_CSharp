using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.AppServices.Models;
using ShoppingCart.AppServices.Services;
using ShoppingCart.Domain.AggregatesModel;
using System;

namespace ShoppingCart.Api.Controllers
{
    [Route("api/shoppingCart")]
    public class CartController : Controller
    {
        private ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        [HttpGet("{id}", Name = "GetCar")]
        public IActionResult GetCar(Guid id)
        {

            /*if (!_cartRepository.CarExists(id))
            {
                return NotFound();
            }
            var carFromRepo = _cartRepository.GetCar(id);
            CarDto carToReturn = Mapper.Map<CarDto>(carFromRepo);*/
            //return Ok(carFromRepo);
            return Ok("ok");
        }
        [HttpGet()]
        public IActionResult GetCars()
        {
            //var carsFromRepo = _cartRepository.GetCars();
            //IEnumerable < CarDto > carsToReturn
            //    = Mapper.Map<IEnumerable<CarDto>>(carsFromRepo);
            //return Ok(carsFromRepo);
            return Ok("ok");
        }
        [HttpPost()]
        public IActionResult CreateCar([FromBody] CartDto car)
        {
            if (car == null)
            {
                return BadRequest();
            }
            //var carForCreation = Mapper.Map<CartDto, CartEntity>(car);
            _cartRepository.AddCar(car);

            if (!_cartRepository.Save())
            {
                throw new Exception(" Saving error in Database");
            }
            //var carToReturn = Mapper.Map<CartEntity>(carForCreation);
            //return Ok(carToReturn);
            return CreatedAtRoute("GetCar", new { id = car.Id }, car);
        }
        /*[HttpDelete("{id}")]
        public IActionResult DeleteCar(Guid id)
        {
            var carFromRepo = _carRepository.GetCar(id);
            if (carFromRepo == null)
            {
                return NotFound();
            }
            _carRepository.DeleteCar(carFromRepo);
            if (!_carRepository.Save())
            {
                throw new Exception("Error at Save");
            }
            return NoContent();
        }

        [HttpPut("{carId}")]
        public IActionResult UpdateCar(Guid carId, [FromBody] CarForUpdateDto car)
        {
            if (car == null)
            {
                return BadRequest();
            }
            if (!_carRepository.CarExists(carId))
            {
                return NotFound();
            }

            var carFromRepo = _carRepository.GetCar(carId);
            Mapper.Map(car, carFromRepo);



            if (!_carRepository.Save())
            {
                throw new Exception("Fail to save");
            }

            return NoContent();



        }

        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdateCar(Guid id, [FromBody] JsonPatchDocument<CarForUpdateDto> pathDoc)
        {
            if (pathDoc == null)
            {
                return BadRequest();
            }
            if (!_carRepository.CarExists(id))
            {
                return NotFound();
            }
            var carFromRepo = _carRepository.GetCar(id);
            var carForUpdate = Mapper.Map<CarForUpdateDto>(carFromRepo);
            pathDoc.ApplyTo(carForUpdate);
            Mapper.Map(carForUpdate, carFromRepo);
            if (!_carRepository.Save())
            {
                throw new Exception("Saving problem");
            }
            return NoContent();
        }
*/


    }
}
