using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Repository
{
    public class CidadeRepository
    {
        public List<Cidade> ObterTodos()
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"SELECT * FROM cidades";
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            List<Cidade> cidades = new List<Cidade>();

            foreach (DataRow linha in tabela.Rows)
            {
               Cidade cidade = new Cidade();
                cidade.id = Convert.ToInt32(linha["id"]);
                cidade.nome = linha["nome"].ToString();
              cidades.Add(cidade);
            }
            comando.Connection.Close();
            return cidades;
        }
        public int Inserir(Cidade cidade)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"INSERT INTO cidades (nome,numerohabitacao) OUTPUT INSERTED.ID VALUES (@NOME,@NUMHABITANTES)";
            comando.Parameters.AddWithValue("@NOME", cidade.nome);
            comando.Parameters.AddWithValue("@NUMHABITANTES", cidade.NumeroHabitantes);
            int id = Convert.ToInt32(comando.ExecuteScalar());
            comando.Connection.Close();
            return id;
        }
        public Cidade ObterPeloId(int id)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"SELECT * FROM cidades WHERE id = @Id";
            comando.Parameters.AddWithValue("@ID", id);

            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());

            comando.Connection.Close();
            if (tabela.Rows.Count == 0)
            {
                return null;
            }

            DataRow linha = tabela.Rows[0];
            Cidade cidade = new Cidade();
           cidade.id = Convert.ToInt32(linha["id"]);
           cidade.nome = linha["nome"].ToString();
           cidade.NumeroHabitantes = Convert.ToInt32(linha["NumeroHabitantes"]);
            return cidade;
        }

        public bool Alterar(Cidade cidade)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"UPDATE cidades SET nome = @NOME WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", cidade.id);
            comando.Parameters.AddWithValue("@NOME", cidade.nome);
            int quantidadeAfetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeAfetada == 1;
        }
        public bool Apagar(int id)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"DELETE FROM cidades WHERE id = @ID";
            comando.Parameters.AddWithValue("@Id", id);
            int quantidadeAfetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeAfetada == 1;
        }



    }
}
