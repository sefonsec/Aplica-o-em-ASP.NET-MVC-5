using System.Web.Mvc;
using System.Net;
using Modelo.Cadastros;
using Servico.Cadastros;

namespace Projeto01.Areas.Cadastros.Controllers
{
    public class ProdutosAreasController : Controller
    {
        private ProdutoServico produtoServico = new ProdutoServico();
        private CategoriaServico categoriaServico = new CategoriaServico();
        private FabricanteServico fabricanteServico = new FabricanteServico();

        public ActionResult Index()
        {
            return View(produtoServico.ObterProdutosClassificadosPorNome());
        }

        private ActionResult ObterVisaoProdutoPorId(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var produto = produtoServico.ObterProdutoPorId((long) id);

            if (produto == null)
            {
                return HttpNotFound();
            }

            return View(produto);
        }
       
        public ActionResult Details(long? id)
        {            
            return ObterVisaoProdutoPorId(id);
        }
        
        public ActionResult Delete(long? id)
        {            
            return ObterVisaoProdutoPorId(id);
        }

        [HttpPost]
        public ActionResult Delete(long id)
        {
            try
            {
                var produto = produtoServico.EliminarProdutoPorId(id);

                TempData["Message"] = "Produto " + produto.Nome.ToUpper() + " foi removido";

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private void PopularViewBag(Produto produto = null)
        {
            if (produto == null)
            {
                ViewBag.CategoriaId = new SelectList(categoriaServico.ObterCategoriasClassificadasPorNome(), "CategoriaId", "Nome");
                ViewBag.FabricanteId = new SelectList(fabricanteServico.ObterFabricantesClassificadosPorNome(), "FabricanteId", "Nome");
            }
            else
            {
                ViewBag.CategoriaId = new SelectList(categoriaServico.ObterCategoriasClassificadasPorNome(), "CategoriaId", "Nome", produto.CategoriaId);
                ViewBag.FabricanteId = new SelectList(fabricanteServico.ObterFabricantesClassificadosPorNome(), "FabricanteId", "Nome", produto.FabricanteId);
            }
        }

        public ActionResult Edit(long? id)
        {
            PopularViewBag(produtoServico.ObterProdutoPorId((long) id));
            return ObterVisaoProdutoPorId(id);
        }

        [HttpPost]
        public ActionResult Edit(Produto produto)
        {
            return GravarProduto(produto);
        }

        public ActionResult Create()
        {
            PopularViewBag();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Produto produto)
        {
            return GravarProduto(produto);
        }

        private ActionResult GravarProduto(Produto produto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    produtoServico.GravarProduto(produto);
                    return RedirectToAction("Index");
                }

                PopularViewBag(produto);

                return View(produto);
            }
            catch
            {
                PopularViewBag(produto);

                return View(produto);
            }
        }
    }
}
