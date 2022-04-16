using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using FluentValidation;
using MassTransit;
using Client.Commands;

namespace Client.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController: ControllerBase
    {

        [HttpGet("read")]
        public async Task<ReadBookResponse> GetAsync(
            [FromServices] IReadCommand command)
        {
            ReadBookRequest request = new ReadBookRequest();
            return await command.ExecuteAsync(request);
        }

        [HttpPost("create")]
        public async Task<CreateBookResponse> CreateAsync(
            [FromServices] ICreateCommand<CreateBookResponse,CreateBookRequest> command,
            [FromBody] CreateBookRequest request)
        {
            return await command.ExecuteAsync(request);
        }
    }
}
