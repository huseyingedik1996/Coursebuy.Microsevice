﻿using Dapper;
using freeCourse.Shared.Dtos;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.Services.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;

        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
        }

        public async Task<Response<NoContent>> Delete(int id)
        {
            var status = await _dbConnection.ExecuteAsync("delete from discount where id=@id", new {id = id});

            return status > 0 ? Response<NoContent>.Success(204) : Response<NoContent>.Fail("discount not fount", 404);
        }

        public async Task<Response<NoContent>> Update(Model.Discount discount)
        {
            var status = await _dbConnection.ExecuteAsync("update discount set userid=@UserId, code=@Code,rate=@Rate where id=@id",
                new { id = discount.Id, UserId = discount.UserId, Code = discount.Code, Rate = discount.Code });

            if(status>0)
            {
                return Response<NoContent>.Success(204);
            }

            return Response<NoContent>.Fail("discount not found", 404);
        }

        public async Task<Response<List<Model.Discount>>> GetAll()
        {
            var discounts = await _dbConnection.QueryAsync<Model.Discount>("Select * from discount");
            return Response<List<Model.Discount>>.Success(discounts.ToList(),200);
        }

        public async Task<Response<Model.Discount>> GetByCodeAndUserId(string code, string userId)
        {
            var discount = await _dbConnection.QueryAsync<Model.Discount>("select * from discount where userid=@UserId and code=@Code", new
            {
                UserId = userId,
                Code = code
            });
            var hasDiscount = discount.FirstOrDefault();

            if (hasDiscount == null) { return Response<Model.Discount>.Fail("discount not fount", 404); }

            return Response<Model.Discount>.Success(hasDiscount, 200);
        }

        public async Task<Response<Model.Discount>> GetById(int id)
        {
            var discount = (await _dbConnection.QueryAsync<Model.Discount>("select * from discount where id =@id", new { id = id })).SingleOrDefault();
            if (discount == null) { return Response<Model.Discount>.Fail("Discount not found", 404); }

            return Response<Model.Discount>.Success(discount,200);


        }

        public async Task<Response<NoContent>> Save(Model.Discount discount)
        {
            var saveStatus = await _dbConnection.ExecuteAsync("INSERT INTO discount (userid,rate,code) VALUES(@UserId,@Rate,@Code", discount);

            if (saveStatus>0)
            {
                return Response<NoContent>.Success(204);
            }

            return Response<NoContent>.Fail("an error accured while adding", 500);
        }
    }
}
