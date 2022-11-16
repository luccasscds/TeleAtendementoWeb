using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeleAtendimentoWeb
{
    class Pessoa : PessoaDAO
    {
        protected long id;
        public string NOME { get; set; }
        public long CPF { get; set; }
        public Endereco ENDERECO;
        public List<Telefone> TELEFONES;

        public Pessoa(long id, string NOME, long CPF, Endereco ENDERECO, List<Telefone> TELEFONES)
        {
            this.id = id;
            this.NOME = NOME;
            this.CPF = CPF;
            this.ENDERECO = ENDERECO;
            this.TELEFONES = TELEFONES;
        }
        public long ID => id;
    }
}