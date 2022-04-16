using Models;
using FluentValidation;
using MassTransit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace Client.Commands
{
    public class CreateBookCommand: ICreateCommand<CreateBookResponse,CreateBookRequest>
    {
        private readonly IValidator<CreateBookRequest> _validator;
        private readonly IRequestClient<CreateBookRequest> _requestClient;
        public CreateBookCommand(
            IValidator<CreateBookRequest> validator,
            IRequestClient<CreateBookRequest> requestClient)
        {
            _validator = validator;
            _requestClient = requestClient;
        }
        public async Task<CreateBookResponse> ExecuteAsync(CreateBookRequest bookRequest)
        {
            List<string> errors = new();
            bool isSuccess = default;

            ValidationResult validationResult = await _validator.ValidateAsync(bookRequest, default);

            if (validationResult.IsValid)
            {
                var r = await _requestClient.GetResponse<CreateBookResponse>(bookRequest, default, default);
                isSuccess = r.Message.IsSuccess;
                errors = r.Message.Errors;
            }
            else
            {
                errors.AddRange(validationResult.Errors.Select(x => x.ErrorMessage).ToList());
            }

            return new CreateBookResponse() { IsSuccess = isSuccess, Errors = errors };
        }
    }
}
