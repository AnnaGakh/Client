using Models;
using System;
using System.Threading.Tasks;

namespace Client.Commands
{
    public interface IReadCommand
    {
        Task<ReadBookResponse> ExecuteAsync(ReadBookRequest request);
    }
}
