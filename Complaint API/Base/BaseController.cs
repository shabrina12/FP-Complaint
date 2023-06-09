﻿using Complaint_API.Repository.Contracts;
using Complaint_API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Complaint_API.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<IRepository, Entity, Key> : ControllerBase
    where Entity : class
    where IRepository : IGeneralRepository<Entity, Key>
    {
        protected readonly IRepository _repository;

        public BaseController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public virtual async Task<ActionResult> GetAllAsync()
        {
            var results = await _repository.GetAllAsync();
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

        [HttpGet("{key}")]
        public async Task<ActionResult> GetByIdAsync(Key key)
        {
            var result = await _repository.GetByIdAsync(key);
            if (result is null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Data Not Found!"
                });
            }
            return Ok(new ResultFormat
            {
                Data = result
            });
        }

        [HttpPost]
        public virtual async Task<ActionResult> InsertAsync(Entity entity)
        {
            var result = await _repository.InsertAsync(entity);
            if (result is 0)
            {
                return Conflict(new
                {
                    statusCode = HttpStatusCode.Conflict,
                    message = "Data Fail to Insert!"
                });
            }
            return new ObjectResult(new ResultFormat
            {
                StatusCode = 201,
                Status = "Success",
                Message = "Data Saved Successfully!",
                Data = result
            })
            {
                StatusCode = 201
            };
        }

        [HttpPut("{key}")]
        public async Task<ActionResult> UpdateAsync(Entity entity, Key key)
        {
            var result = await _repository.IsExist(key);
            if (!result)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Data Not Found!"
                });
            }

            var update = await _repository.UpdateAsync(entity);
            if (update is 0)
            {
                return Conflict(new
                {
                    statusCode = 409,
                    message = "Data Fail to Update!"
                });
            }

            return Ok(new ResultFormat
            {
                Message = "Data Updated!",
                Data = update
            });
        }

        [HttpDelete("{key}")]
        public async Task<ActionResult> Delete(Key key)
        {
            var result = await _repository.DeleteAsync(key);

            if (result is 0)
            {
                return Conflict(new
                {
                    statusCode = 409,
                    message = "Data Fail to Delete!"
                });
            }

            return Ok(new ResultFormat
            {
                Message = "Data Deleted!",
                Data= result
            });
        }
    }
}
