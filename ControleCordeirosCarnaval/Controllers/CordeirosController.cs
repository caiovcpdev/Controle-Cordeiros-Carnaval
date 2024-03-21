using ControleCordeirosCarnaval.Data;
using ControleCordeirosCarnaval.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;

namespace ControleCordeirosCarnaval.Controllers
{
    public class CordeirosController : Controller
    {
        readonly private AppDBContext _db;
   
        private readonly  HttpClient _httpClient;  
        public CordeirosController(AppDBContext db, IHttpClientFactory httpClientFactory)
        {
            _db = db;
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7025");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<CordeiroModel> cordeiros = null;

            HttpResponseMessage response = await _httpClient.GetAsync("/api/cordeiro");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                cordeiros = JsonConvert.DeserializeObject<IEnumerable<CordeiroModel>>(data);
            }
            else
            {
                return null;
            }

            ViewData["cordeiros"] = cordeiros;
            return View(cordeiros);
        }


        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"/api/cordeiro/{id}");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                CordeiroModel cordeiro = JsonConvert.DeserializeObject<CordeiroModel>(data);
                return View(cordeiro);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]

        public async Task<IActionResult> Excluir(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"/api/cordeiro/{id}");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                CordeiroModel cordeiro = JsonConvert.DeserializeObject<CordeiroModel>(data);
                return View(cordeiro);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(CordeiroModel novoCordeiro)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/api/cordeiro", novoCordeiro);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index"); 
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Erro ao cadastrar cordeiro. Por favor, tente novamente mais tarde.");
                return View(novoCordeiro); // Retorna a view com o objeto novoCordeiro para que o usuário possa corrigir os campos
            }
        }

        [HttpPost]
        public async Task<IActionResult> Editar(int id, CordeiroModel cordeiroAtualizado)
        {
            try
            {
                cordeiroAtualizado.Id = id;
                HttpResponseMessage response = await _httpClient.PutAsJsonAsync("/api/cordeiro", cordeiroAtualizado);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Erro ao editar cordeiro. Por favor, tente novamente mais tarde.");
                    return View(cordeiroAtualizado);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, $"Erro ao editar cordeiro: {ex.Message}");
                return View(cordeiroAtualizado);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Excluir(int id, CordeiroModel cordeiroAtualizado)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"/api/cordeiro/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Erro ao excluir cordeiro. Por favor, tente novamente mais tarde.");
                    return View(cordeiroAtualizado);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Erro ao excluir cordeiro: {ex.Message}");
                return View(cordeiroAtualizado);
            }
        }
    }
}
