using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TCCTrabalho3.Data;
using TCCTrabalho3.Models;

public class CarrinhoController : Controller
{
    private readonly Contexto _context;

    public CarrinhoController(Contexto context)
    {
        _context = context;
    }

    // Método para obter o carrinho da sessão
    private Carrinho GetCarrinhoDaSessao()
    {
        var carrinho = HttpContext.Session.GetObject<Carrinho>("Carrinho");
        if (carrinho == null)
        {
            carrinho = new Carrinho { Items = new List<CarrinhoItem>() };
            HttpContext.Session.SetObject("Carrinho", carrinho); // Inicializa o carrinho na sessão
        }
        return carrinho;
    }   

    // Exibe o conteúdo do carrinho
    public IActionResult Index()
    {
        var carrinho = GetCarrinhoDaSessao();
        return View(carrinho);  
    }

    // Adiciona um curso ao carrinho
    public IActionResult AdcionarParaCarrinho(int id)
    {
        var curso = _context.Cursos.FirstOrDefault(c => c.Id == id);
        if (curso == null)
        {
            return NotFound(); // Curso não encontrado
        }

        var carrinho = GetCarrinhoDaSessao();

        // Verifica se o item já está no carrinho
        var carrinhoItem = carrinho.Items.FirstOrDefault(c => c.CursoId == id);
        if (carrinhoItem == null)
        {
            carrinho.Items.Add(new CarrinhoItem
            {
                CursoId = id,
                Cursos = curso, 
                Quantidade = 1
            });
        }
        else
        {
            carrinhoItem.Quantidade++;
        }

        HttpContext.Session.SetObject("Carrinho", carrinho); // Atualiza o carrinho na sessão

        return RedirectToAction("Index");
    }



    // Remove ou diminui a quantidade de um item no carrinho
    public IActionResult RemoverDoCarrinho(int Cursoid)
    {
        try
        {
            var carrinho = GetCarrinhoDaSessao();
            var carrinhoItem = carrinho.Items.FirstOrDefault(c => c.CursoId == Cursoid);

            if (carrinhoItem != null)
            {
                if (carrinhoItem.Quantidade > 1)
                {
                    carrinhoItem.Quantidade--; // Diminui a quantidade
                }
                else
                {
                    carrinho.Items.Remove(carrinhoItem); // Remove o item
                }

                HttpContext.Session.SetObject("Carrinho", carrinho);  // Atualiza a sessão
            }

            return RedirectToAction("Index");  // Redireciona para a página do carrinho
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);  // Para depuração
            return StatusCode(500, "Ocorreu um erro ao remover o curso do carrinho.");
        }
    }

    public IActionResult Checkout()
    {
        HttpContext.Session.Remove("Carrinho");
        TempData["MensagemSucesso"] = "Compra realizada com sucesso! Obrigado por adquirir nossos cursos.";
        return RedirectToAction("Index", "Home");
    }

    public IActionResult Relatorio()
    {
        var vendas = _context.Vendas.Select(v => new VendasModel
        {
            Id = v.Id,
            Recebedor = v.Recebedor,
            Fornecedor = v.Fornecedor,
            CursoVendido = v.CursoVendido,
            dataUltimaAtualizacao = v.dataUltimaAtualizacao
        }).ToList();

        return View(vendas);
    }


}
