using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto;
using Application.Dto.VehicleDto;
using AutoMapper;
using Domain.Models;
using GoCargo.Application.Dto.AssingmentDto;
using GoCargo.Application.Dto.BookingDto;
using GoCargo.Application.Dto.DriverRequestDto;
using GoCargo.Application.Dto.VehicleDto;
using GoCargo.Domain.Models;

namespace Application.Mapper
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper() 
        {
            CreateMap<RegisterDto, User>().ReverseMap();
            CreateMap<LoginDto, User>().ReverseMap();
            CreateMap<GetUserDto, User>().ReverseMap();
            CreateMap<UpdateUserDto, User>().ReverseMap();
            CreateMap<CreateVehicleDto, Vehicle>().ReverseMap();
            CreateMap<GetVehicleDto, Vehicle>().ReverseMap();
            CreateMap<UpdateAvailableDto, Vehicle>().ReverseMap();
            CreateMap<CreateBookingDto, Booking>().ReverseMap();
            CreateMap<AssignDriverDto, DriverAssignment>().ReverseMap();
            CreateMap<UpdateDeliveryStatusDto, DriverAssignment>().ReverseMap();
            CreateMap<CreateDriverRequestDto, DriverRequest>().ReverseMap();








        }
    }
}
