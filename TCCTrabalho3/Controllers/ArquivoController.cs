using Microsoft.AspNetCore.Mvc;

namespace TCCTrabalho3.Controllers
{
    public class ArquivoController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ArquivoController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile arquivo)
        {
            if (arquivo != null && arquivo.Length > 0)
            {
                // Caminho para salvar o arquivo
                var caminhoPasta = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                var caminhoArquivo = Path.Combine(caminhoPasta, Path.GetFileName(arquivo.FileName));

                // Criar pasta, se não existir
                if (!Directory.Exists(caminhoPasta))
                    Directory.CreateDirectory(caminhoPasta);

                // Salvar o arquivo
                using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
                {
                    await arquivo.CopyToAsync(stream);
                }

                ViewBag.Mensagem = "Arquivo enviado com sucesso!";
            }
            else
            {
                ViewBag.Mensagem = "Selecione um arquivo válido.";
            }

            return View();
        }
    }
}
