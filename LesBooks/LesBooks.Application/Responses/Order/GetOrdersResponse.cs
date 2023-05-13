﻿using LesBooks.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.Application.Responses
{
    public class GetOrdersResponse: ResponseBase
    {
        public List<OrderPurchase> orders;

        public string filter;
    }
}