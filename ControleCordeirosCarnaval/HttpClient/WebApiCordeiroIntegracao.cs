using ControleCordeirosCarnaval.HttpClient.Interfaces;
using ControleCordeirosCarnaval.HttpClient.Refit;
using ControleCordeirosCarnaval.Models;

namespace ControleCordeirosCarnaval.HttpClient
{
    public class WebApiCordeiroIntegracao : IWebApiCordeiroIntegracao
    {
        private readonly IWebApiCordeiroIntegracaoRefit _WebApiCordeiroIntegracaoRefit;
        public WebApiCordeiroIntegracao(IWebApiCordeiroIntegracaoRefit IWebApiCordeiroIntegracaoRefit)
        {
            _WebApiCordeiroIntegracaoRefit = IWebApiCordeiroIntegracaoRefit;
        }
        public async Task<CordeiroModel> GetCordeiros()
        {
          var responseData  = await _WebApiCordeiroIntegracaoRefit.GetCordeiros();

            if (responseData != null && responseData.IsSuccessStatusCode) 
            {
                return responseData.Content;   
            }
            return null;
        }
    }
}
