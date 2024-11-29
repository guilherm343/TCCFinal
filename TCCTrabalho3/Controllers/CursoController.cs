using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TCCTrabalho3.Data;
using TCCTrabalho3.Models;

namespace TCCTrabalho3.Controllers
        {
    public class CursoController : Controller
    {
        private readonly Contexto _context;

        public CursoController(Contexto context)
        {
            _context = context;
        }


        public IActionResult AdicionarCurso()
        {
            return View();
        }

        public IActionResult RemoverCurso()
        {
            return View();
        }


        public IActionResult Index()
        {
            // Recupera a lista de cursos
            var cursos = _context.Cursos.ToList();
            return View(cursos);
        }

        public IActionResult Details(int id)
        {
            // Recupera os detalhes de um curso específico pelo ID
            var curso = _context.Cursos.FirstOrDefault(c => c.Id == id);
            if (curso == null)
            {
                return NotFound();
            }
            return View(curso);
        }
        [HttpPost]
        public IActionResult AdicionarCurso(CursosModel curso)
        {
            if (ModelState.IsValid)
            {
                _context.Cursos.Add(curso);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }



            return View(curso); // Se o modelo não for válido, retorna a view com os dados preenchidos
        }

       

    }
}
