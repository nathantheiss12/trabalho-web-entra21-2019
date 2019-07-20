using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class TarefaController : Controller
    {
        private TarefaRepository repository;

        public TarefaController()
        {
            repository = new TarefaRepository();
        }

        public ActionResult Index()
        {
            List<Tarefa> tarefas = repository.ObterTodos();
            ViewBag.Tarefas = tarefas;

            return View();
        }

        public ActionResult Cadastro()  
        {
            UsuarioRepository usuarioRepository = new UsuarioRepository();
            List<Usuario> usuarios = usuarioRepository.ObterTodos();
            ViewBag.Usuarios = usuarios;

            ProjetoRepository projetoRepository = new ProjetoRepository();
            List<Projeto> projetos = projetoRepository.ObterTodos();
            ViewBag.Projetos = projetos;

            CategoriaRepository categoriaRepository = new CategoriaRepository();
            List<Categoria> categorias = categoriaRepository.ObterTodos();
            ViewBag.Categorias = categorias;

            return View();
        }

        public ActionResult Store(int usuario, int projeto
            , int categoria, string titulo, string descricao, DateTime duracao)
        {
            Tarefa tarefa = new Tarefa();
            tarefa.Id_Usuario_Responsavel = usuario;
            tarefa.Id_Projeto = projeto;
            tarefa.Id_Categoria = categoria;
            tarefa.Titulo = titulo;
            tarefa.Descricao = descricao;
            tarefa.Duracao = duracao;
            repository.Inserir(tarefa);
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            Tarefa tarefa = new Tarefa();
            tarefa = repository.ObterPeloId(id);

            UsuarioRepository usuarioRepository = new UsuarioRepository();
            List<Usuario> usuarios = usuarioRepository.ObterTodos();
            ViewBag.Usuarios = usuarios;

            ProjetoRepository projetoRepository = new ProjetoRepository();
            List<Projeto> projetos = projetoRepository.ObterTodos();
            ViewBag.Projetos = projetos;

            CategoriaRepository categoriaRepository = new CategoriaRepository();
            List<Categoria> categorias = categoriaRepository.ObterTodos();
            ViewBag.Categorias = categorias;

            return View();
        }

        public ActionResult Update(int id, int usuario, int projeto
            , int categoria, string titulo, string descricao, DateTime duracao)
        {
            Tarefa tarefa = new Tarefa();
            tarefa.Id = id;
            tarefa.Id_Usuario_Responsavel = usuario;
            tarefa.Id_Projeto = projeto;
            tarefa.Id_Categoria = categoria;
            tarefa.Titulo = titulo;
            tarefa.Descricao = descricao;
            tarefa.Duracao = duracao;

            repository.Alterar(tarefa);
            return RedirectToAction("Index");
        }

        public ActionResult Apagar(int id)
        {
            repository.Apagar(id);
            return RedirectToAction("Index");
        }
    }
}