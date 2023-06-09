﻿using Complaint_API.Base;
using Complaint_API.Models;
using Complaint_API.Repository;
using Complaint_API.Repository.Contracts;
using Complaint_API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace Complaint_API.Controllers
{
    [Authorize]
    public class OrderController : BaseController<IOrderRepository, Order, int>
    {
        private readonly IUserRepository _userRepository;
        public OrderController(IOrderRepository repository, IUserRepository userRepository) : base(repository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public override async Task<ActionResult> GetAllAsync()
        {
            var results = await _repository.GetAllAsync();
            if(User.IsInRole("user"))
            {
                string email = User.FindFirstValue(ClaimTypes.Email);
                var user = await _userRepository.GetUserByEmailAsync(email);
                results = await _repository.GetMyAsync(user.Id);
            }
            if (results.Count() is 0)
            {
                return Ok(new
                {
                    statusCode = 404,
                    message = "Data Not Found!",
                    data = results
                });
            }

            return Ok(new ResultFormat
            {
                Data = results
            });
        }

        [HttpPost]
        public override async Task<ActionResult> InsertAsync(Order entity)
        {
            string email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userRepository.GetUserByEmailAsync(email);
            DateTime dateTime = DateTime.Now;
            var order = new Order
            {
                CustomerId = user.Id,
                OrderDate = dateTime,
            };

            await _repository.InsertAsync(order);

            return new ObjectResult(new ResultFormat
            {
                StatusCode = 201,
                Status = "Success",
                Message = "Data Saved Successfully!",
                Data = new
                {
                    order.OrderDate
                }
            })
            {
                StatusCode = StatusCodes.Status201Created,
            };
        }
    }
}
