using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ClienteRepository
    {
        public int Inserir(Cliente cliente)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"INSERT INTO clientes (nome, cpf, data_nascimento, numero, complemento, logradouro, cep) OUTPUT INSERTED.ID VALUES (@NOME, @CPF, @DATA_NASCIMENTO, @NUMERO, @COMPLEMENTO, @LOGRADOURO, @CEP)";
            comando.Parameters.AddWithValue("@NOME", cliente.Nome);
            comando.Parameters.AddWithValue("@CPF", cliente.Cpf);
            comando.Parameters.AddWithValue("@DATA_NASCIMENTO", cliente.Data_Nascimento);
            comando.Parameters.AddWithValue("@NUMERO", cliente.Numero);
            comando.Parameters.AddWithValue("@COMPLEMENTO", cliente.Complemento);
            comando.Parameters.AddWithValue("@LOGRADOURO", cliente.Logradouro);
            comando.Parameters.AddWithValue("@CEP", cliente.Cep);
            int id = Convert.ToInt32(comando.ExecuteScalar());
            comando.Connection.Close();

            return id;
        }

        public bool Apagar(int id)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"DELETE FROM clientes WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            int quantidadeafetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeafetada == 1;
        }

        public bool Alterar(Cliente cliente)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"UPDATE clientes SET id_cidade = @ID_CIDADE, nome = @NOME, cpf = @CPF, data_nascimento = @DATA_NASCIMENTO, numero = @NUMERO, complemento = @COMPLEMENTO, logradouro = @LOGRADOURO, cep = @CEP WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID_CIDADE", cliente.Id_Cidade);
            comando.Parameters.AddWithValue("@NOME", cliente.Nome);
            comando.Parameters.AddWithValue("@CPF", cliente.Cpf);
            comando.Parameters.AddWithValue("@DATA_NASCIMENTO", cliente.Data_Nascimento);
            comando.Parameters.AddWithValue("@NUMERO", cliente.Numero);
            comando.Parameters.AddWithValue("@COMPLEMENTO", cliente.Complemento);
            comando.Parameters.AddWithValue("@LOGRADOURO", cliente.Logradouro);
            comando.Parameters.AddWithValue("@CEP", cliente.Cep);
            int quantidadeAfetada = comando.ExecuteNonQuery();
            comando.Connection.Close();

            return quantidadeAfetada == 1;
        }

        public List<Cliente> ObterTodos()
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"SELECT cliente.id AS 'ClienteId',
            cliente.Id_Cidade AS 'ClienteIdCidade',
            cliente.Nome AS 'ClienteNome',
            cliente.Cpf AS 'ClienteCpf',
            cliente.Data_Nascimento AS 'ClienteDataNascimento',
            cliente.Numero AS 'ClienteNumero',
            cliente.Complemento AS 'ClienteComplemento',
            cliente.Logradouro AS 'ClienteLogradouro',
            cliente.Cep AS 'ClienteCep'
            FROM clientes 
            INNER JOIN cidades ON (cliente.id_cidade = cidade.id";

            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();

            List<Cliente> clientes = new List<Cliente>();
            foreach(DataRow linha in tabela.Rows)
            {
                Cliente cliente = new Cliente();
                cliente.Id_Cidade = Convert.ToInt32(linha["ClienteIdCidade"]);
                cliente.Nome = linha["ClienteNome"].ToString();
                cliente.Cpf = linha["ClienteCpf"].ToString();
                cliente.Data_Nascimento = Convert.ToDateTime(linha["ClienteDataNascimento"]);
                cliente.Numero = Convert.ToInt32(linha["ClienteNumero"]);
                cliente.Complemento = linha["ClienteComplemento"].ToString();
                cliente.Logradouro = linha["ClienteLogradouro"].ToString();
                cliente.Cep = linha["ClienteCep"].ToString();
                cliente.cidade = new Cidade;
                cliente.cidade.Nome = linha["CidadeNome"].ToString();
                clientes.Add(cliente);
            }
            return clientes;
        }
    }
}
