﻿using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class TarefaRepository
    {
        public int Inserir(Tarefa tarefa)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"INSERT INTO tarefas (id_usuario_responsavel, id_projeto, id_categoria, titulo, descricao, duracao) OUTPUT INSERTED.ID VALUES (@ID_USUARIO_RESPONSAVEL, @ID_PROJETO, @ID_CATEGORIA, @TITULO, @DESCRICAO, @DURACAO)";
            comando.Parameters.AddWithValue("@ID_USUARIO_RESPONSAVEL", tarefa.Id_Usuario_Responsavel);
            comando.Parameters.AddWithValue("@ID_PROJETO", tarefa.Id_Projeto);
            comando.Parameters.AddWithValue("@ID_CATEGORIA", tarefa.Id_Categoria);
            comando.Parameters.AddWithValue("@TITULO", tarefa.Titulo);
            comando.Parameters.AddWithValue("@DESCRICAO", tarefa.Descricao);
            comando.Parameters.AddWithValue("@DURACAO", tarefa.Duracao);
            int id = Convert.ToInt32(comando.ExecuteScalar());
            comando.Connection.Close();
            return id;
        }

        public bool Apagar(int id)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"DELETE FROM tarefas WHERE id = @ID";
            comando.Parameters.AddWithValue("ID", id);
            int quantidadeAfetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeAfetada == 1;
        }

        public bool Alterar(Tarefa tarefa)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"UPDATE tarefas SET id_usuario_responsavel = @ID_USUARIO_RESPONSAVEL, id_projeto = @ID_PROJETO,  id_categoria = @ID_CATEGORIA, titulo = @TITULO, descricao = @DESCRICAO, duracao = @DURACAO WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", tarefa.Id);
            comando.Parameters.AddWithValue("@ID_USUARIO_RESPONSAVEL", tarefa.Id_Usuario_Responsavel);
            comando.Parameters.AddWithValue("@ID_PROJETO", tarefa.Id_Projeto);
            comando.Parameters.AddWithValue("@ID_CATEGORIA", tarefa.Id_Categoria);
            comando.Parameters.AddWithValue("@TITULO", tarefa.Titulo);
            comando.Parameters.AddWithValue("@DESCRICAO", tarefa.Descricao);
            comando.Parameters.AddWithValue("@DURACAO", tarefa.Duracao);
            int quantidadeAfetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeAfetada == 1;
        }

        public List<Tarefa> ObterTodos()
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"SELECT tarefas.id AS 'TarefaId',
            tarefas.id_usuario_responsavel AS 'TarefaIdUsuarioResponsavel',
            usuarios.nome AS 'UsuarioNome',
            tarefas.id_projeto AS 'TarefaIdProjeto',
            projetos.nome AS 'ProjetoTitulo',
            tarefas.id_categoria AS 'TarefaIdCategoria',
            categorias.nome As 'CategoriaNome',
            tarefas.titulo AS 'TarefaTitulo',
            tarefas.descricao AS 'TarefaDescricao',
            tarefas.duracao AS 'TarefaDuracao'
            FROM tarefas
            INNER JOIN usuarios ON (tarefas.id_usuario_responsavel = usuarios.id)
            INNER JOIN projetos ON (tarefas.id_projeto = projetos.id)
            INNER JOIN categorias ON (tarefas.id_categoria = categorias.id);";

            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();

            List<Tarefa> tarefas = new List<Tarefa>();
            foreach (DataRow linha in tabela.Rows)
            {
                Tarefa tarefa = new Tarefa();
                tarefa.Id_Usuario_Responsavel = Convert.ToInt32(linha["TarefaIdUsuarioResponsavel"]);
                tarefa.Id_Projeto = Convert.ToInt32(linha["TarefaIdProjeto"]);
                tarefa.Id_Categoria = Convert.ToInt32(linha["TarefaIdCategoria"]);
                tarefa.Titulo = linha["TarefaTitulo"].ToString();
                tarefa.Descricao = linha["TaredaDescricao"].ToString();
                tarefa.Duracao = Convert.ToDateTime(linha["TarefaDuracao"]);
                tarefa.usuario = new Usuario();
                tarefa.projeto = new Projeto();
                tarefa.categoria = new Categoria();
                tarefa.usuario.Nome = linha["UsuarioNome"].ToString();
                tarefa.projeto.Titulo = linha["ProjetoTitulo"].ToString();
                tarefa.categoria.Nome = linha["CategoriaNome"].ToString();
                tarefas.Add(tarefa);
            }
            return tarefas;
        }

        public Tarefa ObterPeloId(int id)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"SELECT tarefas.id AS 'TarefaId',
            tarefas.id_usuario_responsavel AS 'TarefaIdUsuarioResponsavel',
            usuarios.nome AS 'UsuarioNome',
            tarefas.id_projeto AS 'TarefaIdProjeto',
            projetos.nome AS 'ProjetoTitulo',
            tarefas.id_categoria AS 'TarefaIdCategoria',
            categorias.nome As 'CategoriaNome',
            tarefas.titulo AS 'TarefaTitulo',
            tarefas.descricao AS 'TarefaDescricao',
            tarefas.duracao AS 'TarefaDuracao'
            FROM tarefas
            INNER JOIN usuarios ON (tarefas.id_usuario_responsavel = usuarios.id)
            INNER JOIN projetos ON (tarefas.id_projeto = projetos.id)
            INNER JOIN categorias ON (tarefas.id_categoria = categorias.id)
            WHERE id = @ID;";

            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();

            List<Tarefa> tarefas = new List<Tarefa>();
            if (tabela.Rows.Count == 0)
            {
                return null;
            }

            DataRow linha = tabela.Rows[0];
            Tarefa tarefa = new Tarefa();
            tarefa.Id_Usuario_Responsavel = Convert.ToInt32(linha["TarefaIdUsuarioResponsavel"]);
            tarefa.Id_Projeto = Convert.ToInt32(linha["TarefaIdProjeto"]);
            tarefa.Id_Categoria = Convert.ToInt32(linha["TarefaIdCategoria"]);
            tarefa.Titulo = linha["TarefaTitulo"].ToString();
            tarefa.Descricao = linha["TaredaDescricao"].ToString();
            tarefa.Duracao = Convert.ToDateTime(linha["TarefaDuracao"]);
            tarefa.usuario = new Usuario();
            tarefa.projeto = new Projeto();
            tarefa.categoria = new Categoria();
            tarefa.usuario.Nome = linha["UsuarioNome"].ToString();
            tarefa.projeto.Titulo = linha["ProjetoTitulo"].ToString();
            tarefa.categoria.Nome = linha["CategoriaNome"].ToString();

            return tarefa;
        }
    }
}
