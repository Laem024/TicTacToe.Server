using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TicTacToe.Application.Interfaces.Auth.Repositories;
using TicTacToe.Application.Interfaces.Auth.Services;
using TicTacToe.Domain.Entities;
using TicTacToe.Shared.DTO.User;
using TicTacToe.Shared.Enums;
using TicTacToe.Shared.Responses;

namespace TicTacToe.Application.Services.Auth
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(IMapper mapper, IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<Result<UserLoginDTO>> LoginUserAsync(UserLoginDTO userLoginDTO)
        {
            if (userLoginDTO == null || userLoginDTO.Email == null || userLoginDTO.Password == null)
            {
                return Result<UserLoginDTO>.fail(ResultType.BadRequest, "Email y contraseña son requeridos.");
            }

            var user = await _userRepository.GetUserByEmailAsync(userLoginDTO.Email);

            if (user == null)
            {
                return Result<UserLoginDTO>.fail(ResultType.NotFound, "Email o contraseña erroneos.");
            }

            if (!_passwordHasher.VerifyPassword(userLoginDTO.Password,user.Password))
            {
                return Result<UserLoginDTO>.fail(ResultType.NotFound, "Email o contraseña erroneos.");
            }

            return Result<UserLoginDTO>.ok("¡Login exitoso!");
        }

        public async Task<Result<UserSignUpDTO>> SignUpUserAsync(UserSignUpDTO userSignUpDTO)
        {
            if (userSignUpDTO == null || userSignUpDTO.Email == null || userSignUpDTO.Password == null)
            {
                return Result<UserSignUpDTO>.fail(ResultType.BadRequest, "Email y contraseña son requeridos.");
            }

            if (await _userRepository.EmailExistente(userSignUpDTO.Email))
            {
                return Result<UserSignUpDTO>.fail(ResultType.BadRequest, "Ya existe un usuario con ese correo.");
            }

            if (await _userRepository.UserNameExistente(userSignUpDTO.UserName))
            {
                return Result<UserSignUpDTO>.fail(ResultType.BadRequest, "El nombre de usuario ya está en uso.");
            }

            userSignUpDTO.Password = _passwordHasher.HashPassword(userSignUpDTO.Password);

            var user = _mapper.Map<User>(userSignUpDTO);
            await _userRepository.AddUserAsync(user);
            return Result<UserSignUpDTO>.ok("Registro exitoso!");
        }
    }
}