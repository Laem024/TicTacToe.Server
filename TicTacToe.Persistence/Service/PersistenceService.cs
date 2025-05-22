using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TicTacToe.Persistence.Context;
using TicTacToe.Shared.Enums;

namespace TicTacToe.Persistence.Service
{
    public static class PersistenceService
    {
        public static IServiceCollection AddPersistenceService(this IServiceCollection services, string connectionString, SupportedDbProvider dbProvider)
        {
            services.AddDbContext<TicTacToeDbContext>(opciones =>
            {
                switch (dbProvider)
                {
                    case SupportedDbProvider.SqlServer:
                        opciones.UseSqlServer(connectionString);
                        break;
                    default:
                        throw new NotSupportedException($"El proveedor de base de datos '{dbProvider}' no está soportado.");
                }
            });


            return services;
        }

    }
}