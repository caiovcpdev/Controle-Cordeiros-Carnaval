using ControleCordeirosCarnaval.HttpClient.Interfaces;
using ControleCordeirosCarnaval.HttpClient.Refit;
using ControleCordeirosCarnaval.Models;
using Newtonsoft.Json;
using System.Net.Http; // Importe este namespace para acessar a classe HttpContent

namespace ControleCordeirosCarnaval.HttpClient
{
    public class WebApiCordeiroIntegracao : IWebApiCordeiroIntegracao
    {
        private readonly IWebApiCordeiroIntegracaoRefit _webApiCordeiroIntegracaoRefit;

        public WebApiCordeiroIntegracao(IWebApiCordeiroIntegracaoRefit webApiCordeiroIntegracaoRefit)
        {
            _webApiCordeiroIntegracaoRefit = webApiCordeiroIntegracaoRefit;
        }

        public async Task<CordeiroModel> GetCordeiro()
        {
            var response = await _webApiCordeiroIntegracaoRefit.getCordeiro();

            if (response != null && response.IsSuccessStatusCode)
            {
                // Obtém o conteúdo da resposta como uma string
                string content = await response.Content.ReadAsStringAsync();

                // Deserializa a string content para um objeto CordeiroModel
                CordeiroModel cordeiro = JsonConvert.DeserializeObject<CordeiroModel>(content);

                return cordeiro;
            }

            return null;
        }
    }
}
