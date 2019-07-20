using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Cliente
    {
        public int Id;
        public int Id_cidade;
        public string Nome;
        public string Cpf;
        public DateTime Data_Nascimento;
        public int Numero;
        public string Complemento;
        public string Logradouro;
        public string Cep;

        public Cidade cidade;
    }
}
