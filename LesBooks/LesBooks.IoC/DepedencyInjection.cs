using AngleSharp.Browser.Dom;
using LesBook.Monitoring;
using LesBooks.Application.Services;
using LesBooks.Application.Services.Client;
using LesBooks.Application.Services.Interfaces;
using LesBooks.DAL;
using LesBooks.DAL.DAOs;
using LesBooks.DAL.Interfaces;
using LesBooks.EmailSender;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<IOrderService, OrderService>();

            //Daos
            services.AddScoped<IClientDAO, ClientDAO>();
            services.AddScoped<IAdressDAO, AdressDAO>();
            services.AddScoped<ICardDAO, CardDAO>();
            services.AddScoped<IUserDAO, UserDAO>();
            services.AddScoped<IFlagDAO, FlagDAO>();
            services.AddScoped<IBookDAO, BookDAO>();
            services.AddScoped<IBookCategoryDAO, BookCategoryDAO>();
            services.AddScoped<IAuthorDAO, AuthorDAO>();
            services.AddScoped<IPricingDAO, PricingDAO>();
            services.AddScoped<IPublisherDAO, PublisherDAO>();
            services.AddScoped<IStockDAO, StockDAO>();
            services.AddScoped<IStockEntryHistoryDAO, StockEntryHistoryDAO>();
            services.AddScoped<IActivationStatusReasonDAO, ActivationStatusReasonDAO>();
            services.AddScoped<ICategoryStatusReasonDAO, CategoryStatusReasonDAO>();
            services.AddScoped<ICouponDAO, CouponDAO>();
            services.AddScoped<IPaymentDAO, PaymentDAO>();
            services.AddScoped<IItemDAO, ItemDAO>();
            services.AddScoped<IOrderPurchaseDAO, OrderPurchaseDAO>();
            services.AddScoped<IOrderDAO, OrderDAO>();
            services.AddScoped<IOrderHistoryStatusDAO, OrderHistoryStatusDAO>();

            //Redis
            services.AddScoped<IStockRedis,StockRedis>();

            //monitoring 
            services.AddSingleton<IMonitoring, Monitoring>();

            //Email Sender
            services.AddScoped<ISender, Sender>();

            return services;
        }
    }
}
