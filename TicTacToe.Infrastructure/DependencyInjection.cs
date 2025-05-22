using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TicTacToe.Application.Interfaces.Auth.Repositories;
using TicTacToe.Application.Interfaces.Auth.Services;
using TicTacToe.Application.Services.Auth;
using TicTacToe.Infrastructure.Repositories.Auth;
using TicTacToe.Infrastructure.Services.Auth;

namespace TicTacToe.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}