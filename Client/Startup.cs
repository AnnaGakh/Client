using Models;
using Client.Commands;
using Client.Validation;
using FluentValidation;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddTransient<IValidator<CreateStudentRequest>, ValidateCreateStudentRequest>();
            services.AddTransient<ICreateCommand<CreateStudentResponse,CreateStudentRequest>, CreateStudentCommand>();

            services.AddTransient<IValidator<CreateSchoolRequest>, ValidateCreateSchoolRequest>();
            services.AddTransient<ICreateCommand<CreateSchoolResponse,CreateSchoolRequest>, CreateSchoolCommand>();

            services.AddTransient<IValidator<CreateBookRequest>, ValidateCreateBookRequest>();
            services.AddTransient<ICreateCommand<CreateBookResponse,CreateBookRequest>, CreateBookCommand>();

            //services.AddTransient<IValidator<CreateStudentRequest>, ValidateCreateStudentRequest>();
            //services.AddTransient<ICreateCommand<CreateStudentResponse, CreateStudentRequest>, CreateStudentCommand>();

            //services.AddTransient<IValidator<CreateSchoolRequest>, ValidateCreateSchoolRequest>();
            //services.AddTransient<ICreateCommand<CreateSchoolResponse, CreateSchoolRequest>, CreateSchoolCommand>();
            services.AddTransient<IReadCommand, ReadBookCommand>();

            services.AddMassTransit(mt =>
            {
                mt.UsingRabbitMq((context, config) =>
                {
                    config.Host("localhost", "/", host =>
                    {
                        host.Username("guest");
                        host.Password("guest");
                    });
                });

                mt.AddRequestClient<CreateStudentRequest>(new Uri("rabbitmq://localhost/createstudent"));
                mt.AddRequestClient<CreateSchoolRequest>(new Uri("rabbitmq://localhost/createschool"));
                mt.AddRequestClient<CreateBookRequest>(new Uri("rabbitmq://localhost/createbook"));
                mt.AddRequestClient<ReadBookRequest>(new Uri("rabbitmq://localhost/readbook"));
            });
            services.AddMassTransitHostedService();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
