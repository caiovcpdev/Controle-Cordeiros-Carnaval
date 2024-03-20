using ControleCordeirosCarnaval.Models;
using Refit;

namespace ControleCordeirosCarnaval.HttpClient.Interfaces
{
    public interface IWebApiCordeiroIntegracao
    {
        Task<CordeiroModel> GetCordeiro();
    }
}
