﻿using ControleCordeirosCarnaval.Data;
using ControleCordeirosCarnaval.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControleCordeirosCarnaval.Controllers
{
    public class CordeirosController : Controller
    {
        readonly private AppDBContext _db;
        public CordeirosController(AppDBContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<CordeiroModel> cordeiro = _db.cordeiro;
            return View(cordeiro);
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