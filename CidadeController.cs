using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Repository;
using Model;

namespace View.Controllers
{
    public class CidadeController : Controller
    {
        // GET: Cidade
        private CidadeRepository repository;

        public CidadeController()
        {
            repository = new CidadeRepository();
        }

        // GET: Categoria
        public ActionResult Index()
        {
            List<Cidade> cidades = repository.ObterTodos();
            ViewBag.Estados = cidades;

            return View();
        }
        public ActionResult Cadastro()
        {
            return View();
        }



        public ActionResult Editar(int id)
        {
            Cidade cidade = repository.ObterPeloId(id);
            ViewBag.Estado = cidade;
            return View();
        }

        public ActionResult Update(int id, string nome, int numhabitacao)
        {
            Cidade cidade = new Cidade();
            cidade.nome = nome;
            cidade.NumeroHabitantes = numhabitacao;

            repository.Alterar(cidade);
            return RedirectToAction("Index");
        }

        public ActionResult Apagar(int id)
        {
            repository.Apagar(id);
            return RedirectToAction("Index");
        }
    }
}