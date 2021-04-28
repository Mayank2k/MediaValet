using AutoMapper;
using MediaValet.OrderSupervisor.Repository.DTOs;
using MediaValet.OrderSupervisor.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaValet.OrderSupervisor.Repository
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<Order, OrderDTO>()
                .ForMember(d => d.MagicNumber, opt => opt.MapFrom(s => s.RandomNumber));
            CreateMap<OrderDTO, Order>()
                .ForMember(d => d.RandomNumber, opt => opt.MapFrom(s => s.MagicNumber));  
        }        
    }
}
