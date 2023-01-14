﻿using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Strategy.Models;

namespace WebApp.Strategy.Repositories
{
    public class ProductRepositoryFomMongoDb : IProductRepository
    {
        private readonly IMongoCollection<Product> _products;

        public ProductRepositoryFomMongoDb(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MongoDb");
            var client = new MongoClient(connectionString);
            var databse = client.GetDatabase("ProductDb");
            _products = databse.GetCollection<Product>("Products");
        }

        public async Task Delete(Product product)
        {
            await _products.DeleteOneAsync(x=>x.Id== product.Id);
        }

        public async Task<List<Product>> GetAllByUserId(string userId)
        {
            return await _products.Find(x => x.UserId == userId).ToListAsync();
        }

        public async Task<Product> GetByID(string id)
        {
            return await _products.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Product> Save(Product product)
        {
            await _products.InsertOneAsync(product);
            return product;
        }

        public async Task Update(Product product)
        {
            await _products.FindOneAndReplaceAsync(x=>x.Id==product.Id,product);
        ;
        }
    }
}
