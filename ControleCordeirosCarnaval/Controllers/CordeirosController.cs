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
        /*public IActionResult Index()
        {
            IEnumerable<CordeiroModel> cordeiro = _db.cordeiro;
            return View(cordeiro);
        }*/
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
                return null;// Trate o erro de alguma forma adequada, como exibindo uma mensagem de erro
            }

            ViewData["cordeiros"] = cordeiros;
            return View();
        }



        public IActionResult Cadastrar()
        {
            return View();
        }

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
        public IActionResult Cadastrar(CordeiroModel cordeiro)
        {
            if (ModelState.IsValid)
            {
                _db.cordeiro.Add(cordeiro);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Editar (CordeiroModel cordeiro)
        {
            if (ModelState.IsValid) 
            {
                _db.cordeiro.Update(cordeiro);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(cordeiro);
        }

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
