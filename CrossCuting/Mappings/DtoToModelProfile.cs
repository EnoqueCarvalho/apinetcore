using AutoMapper;
using Domain.Dtos.User;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrossCuting.Mappings
{
    public class DtoToModelProfile : Profile
    {
        public DtoToModelProfile()
        {
            CreateMap<UserModel, UserDtoCreate>()
                .ReverseMap();

            CreateMap<UserModel, UserDtoCreate>()
                .ReverseMap();

            CreateMap<UserModel, UserDtoUpdate>()
                .ReverseMap();
        }
    }
}
