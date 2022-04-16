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
    public class StudentController : ControllerBase
    {
        [HttpGet]
        public void Get()
        {
            
        }

        [HttpPost("create")]
        public async Task<CreateStudentResponse> CreateAsync(
            [FromServices] ICreateCommand<CreateStudentResponse,CreateStudentRequest> command,
            [FromBody] CreateStudentRequest request)
        { 
            return await command.ExecuteAsync(request);
        }
    }
}
