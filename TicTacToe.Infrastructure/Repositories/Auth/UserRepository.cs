using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicTacToe.Application.Interfaces.Auth.Repositories;
using TicTacToe.Domain.Entities;
using TicTacToe.Persistence.Context;
using TicTacToe.Shared.DTO.User;

namespace TicTacToe.Infrastructure.Repositories.Auth
{
    public class UserRepository : IUserRepository
    {
        private readonly TicTacToeDbContext _dbContext;

        public UserRepository(TicTacToeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddUserAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> EmailExistente(string email)
        {
            return await _dbContext.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> UserNameExistente(string userName)
        {
            return await _dbContext.Users.AnyAsync(u => u.UserName == userName);
        }
    }
}