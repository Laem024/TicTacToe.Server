using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TicTacToe.Domain.Entities;
using TicTacToe.Shared.DTO.User;

namespace TicTacToe.Application.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Entity -> DTO
            CreateMap<User, UserFindedDTO>();

            //DTO -> Entity
            CreateMap<UserSignUpDTO, User>();
        }
    }
}