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
        public IActionResult Editar(int ? id) 
        {
           if(id == null || id == 0)
            {
                return NotFound();  
            }

            CordeiroModel cordeiro = _db.cordeiro.FirstOrDefault(x => x.Id == id);

           if(cordeiro == null) 
            {
                return NotFound();

            }

            return View(cordeiro);
        }

        [HttpGet]
        public IActionResult Excluir(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            CordeiroModel cordeiro = _db.cordeiro.FirstOrDefault(x => x.Id == id);

            if (cordeiro == null)
            {
                return NotFound();

            }

            return View(cordeiro);
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
                    return View(cordeiroAtualizado); // Retorna a view com o objeto cordeiroAtualizado para que o usuário possa corrigir os campos
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, $"Erro ao editar cordeiro: {ex.Message}");
                return View(cordeiroAtualizado); // Retorna a view com o objeto cordeiroAtualizado para que o usuário possa corrigir os campos
            }
        }



        //public IActionResult Editar (CordeiroModel cordeiro)
        //{
        //    if (ModelState.IsValid) 
        //    {
        //        _db.cordeiro.Update(cordeiro);
        //        _db.SaveChanges();

        //        return RedirectToAction("Index");
        //    }
        //    return View(cordeiro);
        //}

        [HttpPost]
        public IActionResult Excluir (CordeiroModel cordeiro)
        {
            if (cordeiro == null )
            {
                return NotFound();  
            }

            _db.cordeiro.Remove(cordeiro);
            _db.SaveChanges();  

            return RedirectToAction("Index");   
        }
    }
}
