﻿using LesBooks.Application.Requests;
using LesBooks.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.Application.Services.Interfaces
{
    public interface IOrderService
    {
        public Task<CreateOrderPurchaseResponse> CreateOrderPurchase(CreateOrderPurchaseRequest request);
    }
}