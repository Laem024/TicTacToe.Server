using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.API.Common;
using TicTacToe.Application.Interfaces.Auth.Services;
using TicTacToe.Shared.DTO.User;
using TicTacToe.Shared.Enums;
using TicTacToe.Shared.Responses;

namespace TicTacToe.API.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ApiControllerBase
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService, ILogger<ApiControllerBase> logger) : base(logger)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO userLoginDTO)
        {
            var result = await _userService.LoginUserAsync(userLoginDTO);

            return HandleResult(result);
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] UserSignUpDTO userSignUpDTO)
        {
            var result = await _userService.SignUpUserAsync(userSignUpDTO);

            return HandleResult(result);
        }
    }
}