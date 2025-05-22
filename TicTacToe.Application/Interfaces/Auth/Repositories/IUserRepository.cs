using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Domain.Entities;


namespace TicTacToe.Application.Interfaces.Auth.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task<bool> EmailExistente(string email);
        Task<bool> UserNameExistente(string userName);
        Task AddUserAsync(User user);
    }
}