﻿using LesBooks.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.DAL.Interfaces
{
    public interface IItemDAO
    {
        public Item CreateItem(Item item, int order_id);

        public List<Item> GetAllItensByOrderId(int orders_id);
    }
}
