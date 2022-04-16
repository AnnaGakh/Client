using Models;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.Commands
{
    public class ReadBookCommand : IReadCommand
    {
        public IRequestClient<ReadBookRequest> _requestClient;

        public ReadBookCommand(
            IRequestClient<ReadBookRequest> requestClient)
        {
            _requestClient = requestClient;
        }
        public async Task<ReadBookResponse> ExecuteAsync(ReadBookRequest bookRequest)
        {
            var r = await _requestClient.GetResponse<ReadBookResponse>(_requestClient, default, default);
            ReadBookResponse response = new ReadBookResponse()
            {
                IsSuccess = r.Message.IsSuccess,
                Errors = r.Message.Errors,
                Books = r.Message.Books
            };

            return response;
        }
    }
}
