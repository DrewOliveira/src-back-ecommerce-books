using LesBooks.Application.Services;
using LesBooks.Application.Services.Client;
using LesBooks.Application.Services.Interfaces;
using LesBooks.DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.IoC
{
    public static class DepedencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
           IConfiguration configuration)
        {
            //Services
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IAdressService, AdressService>();
            services.AddScoped<ICardService, CardService>();

            //Daos
            services.AddScoped<IClientDAO, ClientDAO>();
            services.AddScoped<IAdressDAO, AdressDAO>();
            services.AddScoped<ICardDAO, CardDAO>();
            services.AddScoped<IUserDAO, UserDAO>();

            return services;
        }
    }
}
