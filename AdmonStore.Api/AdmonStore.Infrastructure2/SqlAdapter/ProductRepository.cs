﻿using AdmonStore.Domain2.Gateway.Repositories;
using AdmonStore.Entities2.Commands;
using AdmonStore.Entities2.Entities;
using AdmonStore.Infrastructure2.Gateway;
using AutoMapper;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace AdmonStore.Infrastructure2.SqlAdapter
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnectionBuilder _dbConnectionBuilder;
        private readonly IMapper _mapper;
        private readonly string _tableName = "Produts";        

        public ProductRepository(IDbConnectionBuilder dbConnectionBuilder, IMapper mapper)
        {
            _dbConnectionBuilder = dbConnectionBuilder;
            _mapper = mapper;
        }

        public async Task<NewProduct> CreateProductAsync(Product product)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();

            //verify that the store exists and it balance
            //var storeSql = $"SELECT COUNT(*) FROM Stores WHERE Store_Id = @idStore;";
            //var storeCount = await connection.ExecuteScalarAsync<int>(storeSql, new { idStore = product.Id_Store });

            //if (storeCount == 0)
            //{
            //    throw new Exception("The store doesn't exist.");
            //}

            var producToCreate = new Product
            {
                Id_Store = product.Id_Store,
                Names = product.Names,
                Description = product.Description,
                Stock = product.Stock,
                Price = product.Price,
                AdmissionDate = DateTime.Now,
                DepartureDate = null,
                State = product.State
            };

            Product.Validate(producToCreate);

            string sqlQuery = $"INSERT INTO {_tableName} (Id_Store, Names, Description, Stock, Price, AdmissionDate, DepartureDate, State)" +
                $"VALUES (@Id_Store, @Names, @Description, @Stock, @Price, @AdmissionDate, @DepartureDate, @State)";

            var result = await connection.ExecuteScalarAsync(sqlQuery, producToCreate);
            connection.Close();
            return _mapper.Map<NewProduct>(producToCreate);
        }

    
        public async Task<List<Product>> GetProductAsync()
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            string sqlQuery = $"SELECT * FROM {_tableName}";
            var products = await connection.QueryAsync<Product>(sqlQuery);            

            connection.Close();
            return products.ToList();

        }
    }
}
