    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using TicTacToe.Shared.DTO.User;
    using TicTacToe.Shared.Responses;

    namespace TicTacToe.Application.Interfaces.Auth.Services
    {
        public interface IUserService
        {
            Task<Result<UserSignUpDTO>> SignUpUserAsync(UserSignUpDTO userSignUpDTO);
            Task<Result<UserLoginDTO>> LoginUserAsync(UserLoginDTO userLoginDTO);
        }
    }