using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeleAtendimentoWeb
{
    class Telefone
    {
        protected long id;
        public long NUMERO;
        public long DDD;
        public TipoTelefone TIPO;

        public Telefone(long id, long NUMERO, long DDD, TipoTelefone TIPO)
        {
            this.id = id;
            this.NUMERO = NUMERO;
            this.DDD = DDD;
            this.TIPO = TIPO;
        }
        public Telefone() { }
        public long ID => id;
    }
}