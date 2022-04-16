using FluentValidation;
using MassTransit;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Commands
{
    public class CreateStudentCommand : ICreateCommand<CreateStudentResponse,CreateStudentRequest>
    {
        private readonly IValidator<CreateStudentRequest> _validator;
        private readonly IRequestClient<CreateStudentRequest> _requestClient;
        public CreateStudentCommand(
            IValidator<CreateStudentRequest> validator,
            IRequestClient<CreateStudentRequest> requestClient)
        {
            _validator = validator;
            _requestClient = requestClient;
        }
        public async Task<CreateStudentResponse> ExecuteAsync(CreateStudentRequest studentRequest)
        {
            List<string> errors = new();
            bool isSuccess = default;

            var a = _validator.Validate(studentRequest);

            if (a.IsValid)
            {
                var r = await _requestClient.GetResponse<CreateStudentResponse>(studentRequest, default, default);
                isSuccess = r.Message.IsSuccess;
                errors = r.Message.Errors;
            }
            else
            {
                errors.AddRange(a.Errors.Select(x => x.ErrorMessage).ToList());
            }

            return new CreateStudentResponse() { IsSuccess = isSuccess, Errors = errors };
        }
    }
}
