﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.Application.Responses
{
    public class ValidateStockByBookIdResponse : ResponseBase
    {
        public Boolean validate;

        public int quantity;
    }
}
