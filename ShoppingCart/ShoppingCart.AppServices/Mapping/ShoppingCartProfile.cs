using AutoMapper;
using ShoppingCart.AppServices.Models;
using ShoppingCart.Domain.AggregatesModel;
using System;

namespace ShoppingCart.AppServices
{
    public class ShoppingCartProfile: Profile
    {
        public ShoppingCartProfile()
        {
            CreateMap<CartEntity, CartDto>().ReverseMap();
            // Use CreateMap... Etc.. here (Profile methods are the same as configuration methods)
        }
    }
}
