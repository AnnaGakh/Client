using Models;
using System.Threading.Tasks;

namespace Client.Commands
{
    public interface ICreateCommand<Tresponse,Trequest>
    {
        Task<Tresponse> ExecuteAsync(Trequest request);
    }
}
