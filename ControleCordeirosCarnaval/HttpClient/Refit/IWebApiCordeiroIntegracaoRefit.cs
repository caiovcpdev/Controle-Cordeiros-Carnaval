using ControleCordeirosCarnaval.Models;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace ControleCordeirosCarnaval.HttpClient.Refit
{
    public interface IWebApiCordeiroIntegracaoRefit
    {
        [Get("/api/cordeiro")]
        Task<CordeiroModel> getCordeiro();
    }
}
