using AngleSharp.Browser.Dom;
using LesBook.Monitoring;
using LesBooks.Application.Services;
using LesBooks.Application.Services.Client;
using LesBooks.Application.Services.Interfaces;
using LesBooks.DAL;
using LesBooks.DAL.DAOs;
using LesBooks.DAL.Interfaces;
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
            services.AddSingleton<IClientService, ClientService>();
            services.AddSingleton<IAdressService, AdressService>();
            services.AddSingleton<ICardService, CardService>();
            services.AddSingleton<IBookService, BookService>();
            services.AddSingleton<IStockService, StockService>();
            services.AddSingleton<IOrderService, OrderService>();

            //Daos
            services.AddSingleton<IClientDAO, ClientDAO>();
            services.AddSingleton<IAdressDAO, AdressDAO>();
            services.AddSingleton<ICardDAO, CardDAO>();
            services.AddSingleton<IUserDAO, UserDAO>();
            services.AddSingleton<IFlagDAO, FlagDAO>();
            services.AddSingleton<IBookDAO, BookDAO>();
            services.AddSingleton<IBookCategoryDAO, BookCategoryDAO>();
            services.AddSingleton<IAuthorDAO, AuthorDAO>();
            services.AddSingleton<IPricingDAO, PricingDAO>();
            services.AddSingleton<IPublisherDAO, PublisherDAO>();
            services.AddSingleton<IStockDAO, StockDAO>();
            services.AddSingleton<IStockEntryHistoryDAO, StockEntryHistoryDAO>();
            services.AddSingleton<IActivationStatusReasonDAO, ActivationStatusReasonDAO>();
            services.AddSingleton<ICategoryStatusReasonDAO, CategoryStatusReasonDAO>();
            services.AddSingleton<ICouponDAO, CouponDAO>();
            services.AddSingleton<IPaymentDAO, PaymentDAO>();
            services.AddSingleton<IItemDAO, ItemDAO>();
            services.AddSingleton<IOrderPurchaseDAO, OrderPurchaseDAO>();
            services.AddSingleton<IOrderDAO, OrderDAO>();
            services.AddSingleton<IOrderHistoryStatusDAO, OrderHistoryStatusDAO>();

            //Redis
            services.AddSingleton<IStockRedis,StockRedis>();

            //monitoring 
            services.AddSingleton<IMonitoring, Monitoring>();

            return services;
        }
    }
}
