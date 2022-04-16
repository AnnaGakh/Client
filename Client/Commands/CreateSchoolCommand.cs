using Models;
using FluentValidation;
using MassTransit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Commands
{
    public class CreateSchoolCommand : ICreateCommand<CreateSchoolResponse,CreateSchoolRequest>
    {
        private readonly IValidator<CreateSchoolRequest> _validator;
        private readonly IRequestClient<CreateSchoolRequest> _requestClient;
        public CreateSchoolCommand(
            IValidator<CreateSchoolRequest> validator,
            IRequestClient<CreateSchoolRequest> requestClient)
        {
            _validator = validator;
            _requestClient = requestClient;
        }
        public async Task<CreateSchoolResponse> ExecuteAsync(CreateSchoolRequest schoolRequest)
        {
            List<string> errors = new();
            bool isSuccess = default;

            var a = _validator.Validate(schoolRequest);

            if (a.IsValid)
            {
                var r = await _requestClient.GetResponse<CreateSchoolResponse>(schoolRequest, default, default);
                isSuccess = r.Message.IsSuccess;
                errors = r.Message.Errors;
            }
            else
            {
                errors.AddRange(a.Errors.Select(x => x.ErrorMessage).ToList());
            }

            return new CreateSchoolResponse() { IsSuccess = isSuccess, Errors = errors };
        }
    }
}
