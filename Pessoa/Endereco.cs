using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeleAtendimentoWeb
{
    class Endereco
    {
        protected long id;
        public string LOGRADOURO;
        public long NUMERO;
        public long CEP;
        public string BAIRRO;
        public string CIDADE;
        public string ESTADO;

        public Endereco(long id, string LOGRADOURO, long NUMERO, long CEP, string BAIRRO, string CIDADE, string ESTADO)
        {
            this.id = id;
            this.LOGRADOURO = LOGRADOURO;
            this.NUMERO = NUMERO;
            this.CEP = CEP;
            this.BAIRRO = BAIRRO;
            this.CIDADE = CIDADE;
            this.ESTADO = ESTADO;
        }
        public Endereco() { }

        public long ID => id;
    }
}