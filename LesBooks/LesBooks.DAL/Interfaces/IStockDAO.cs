﻿using LesBooks.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.DAL.Interfaces
{
    public interface IStockDAO
    {
        public Stock GetStockByBookId(int id);

        public void UpdateQuantityStockByBookId(int quantity, int book_id,bool remove);

        public void EntryStockBookId(int book_id, int quantity, Double costValue);

        public Task<Stock> CreateStockByBookId(int book_id, Stock stock);
    }
}
