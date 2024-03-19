using ControleCordeirosCarnaval.Models;
using Refit;

namespace ControleCordeirosCarnaval.HttpClient.Interfaces
{
    public interface IWebApiCordeiroIntegracao
    {
        [Get("/api/cordeiro")]
        Task<CordeiroModel> GetCordeiros();
    }
}
