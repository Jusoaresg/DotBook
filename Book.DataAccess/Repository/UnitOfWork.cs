﻿using Book.DataAccess.Data;
using Book.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategoryRepository Category { get; private set;}
        public IProductRepository Product { get; private set;}
        public IShoppingCartItem ShoppingCartItem { get; private set;}
       
        private ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Product = new ProductRepository(_db);
            ShoppingCartItem = new ShoppingCartItemRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
