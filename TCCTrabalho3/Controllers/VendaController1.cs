using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TCCTrabalho3.Data;
using TCCTrabalho3.Models;

namespace TCCTrabalho3.Controllers
{
    public class VendaController1 : Controller
    {
        readonly private Contexto _db;

        public VendaController1(Contexto db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<VendasModel> vendas = _db.Vendas;


            return View(vendas);
        }
        

        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Editar(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            VendasModel venda = _db.Vendas.FirstOrDefault(x => x.Id == id);

            if (venda == null)
            {
                return NotFound();
            }


            return View(venda); 
        }
        [HttpGet]
        public IActionResult Excluir(int? id)
        {
            if (id == null || null == 0)
            {
                return NotFound();
            }
            VendasModel venda = _db.Vendas.FirstOrDefault(x => x.Id == id);

            if (venda == null)
            {
                return NotFound();
            }

            return View(venda);
        }

        [HttpPost]
        public IActionResult Cadastrar(VendasModel venda)
        {   
            if (ModelState.IsValid)
            {
                _db.Vendas.Add(venda);
                _db.SaveChanges();

                TempData["MensagemSucesso"] = "Cadastro realizado com sucesso!";

                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Editar(VendasModel venda)
        {
            if (!ModelState.IsValid)
            {
                _db.Vendas.Update(venda);
                _db.SaveChanges();

                TempData["MensagemSucesso"] = "Edição realizado com sucesso!";


                return RedirectToAction("Index");
            }

            TempData["MensagemErro"] = "Algum erro ocorreu ao realizar edição!";

            return View(venda);

        }
        [HttpPost]
        public IActionResult Excluir(VendasModel venda)
        {
            if (venda == null)
            {
                return NotFound();
            }

            TempData["MensagemSucesso"] = "Exclusão realizado com sucesso!";


            _db.Vendas.Remove(venda);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        

    }
}
