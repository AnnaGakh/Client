using Moq.AutoMock;
using Client.Commands;
using Models;
using NUnit.Framework;
using FluentValidation;
using FluentValidation.Results;
using System.Threading.Tasks;
using System.Collections.Generic;
using MassTransit;

namespace Client.Tests
{
    public class CreateBookCommandTests
    {
        private AutoMocker _mocker;
        private ICreateCommand<CreateBookResponse, CreateBookRequest> _command;

        private CreateBookRequest _request;
        private string _bookName = "Example";
        private string _author = "Someone";
        private int _year = 1900;

        private CreateBookResponse _correctResponse;
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _request = new()
            {
                Name = _bookName,
                Author = _author,
                Year = _year
            };

            
        }

        [SetUp]
        public void SetUp()
        {
            _mocker = new AutoMocker();

            _mocker
                .Setup<IValidator<CreateBookRequest>, Task<ValidationResult>>(x => x.ValidateAsync(_request, default))
                .Returns(Task.FromResult(new ValidationResult() { }));

            _command = _mocker.CreateInstance<CreateBookCommand>();
            _correctResponse = _mocker.CreateInstance<CreateBookResponse>();
            _request = _mocker.CreateInstance<CreateBookRequest>();
        }

        [Test]
        public void FailedValidation()
        {
            _command = _mocker.CreateInstance<CreateBookCommand>();
            _correctResponse = new()
            {
                IsSuccess = true,
                Errors = new() { }
            };
            _mocker
                .Setup<IValidator<CreateBookRequest>, Task<ValidationResult>>(x => x.ValidateAsync(_request, default))
                .Returns(Task.FromResult(new ValidationResult(new List<ValidationFailure>() { new ValidationFailure("Name","Error")})));

            _correctResponse.Errors.Add("Error");
            _correctResponse.IsSuccess = false;

            Assert.AreEqual(_correctResponse, _command.ExecuteAsync(_request).Result);
        }

        [Test]
        public void SuccessTest()
        {
           _command = _mocker.CreateInstance<CreateBookCommand>();
           _correctResponse = new()
           {
               IsSuccess = true,
               Errors = new() { }
           };
            Assert.AreEqual(_correctResponse, _command.ExecuteAsync(_request).Result);
        }
    }
}
