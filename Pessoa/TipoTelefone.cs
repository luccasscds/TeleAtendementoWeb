using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Web;

namespace TeleAtendimentoWeb
{
    class TipoTelefone
    {
        protected long id;
        public string TIPO;

        public TipoTelefone(long id, string TIPO)
        {
            this.id = id;
            this.TIPO = TIPO;
        }
        public TipoTelefone() { }
        public long ID => id;
    }
}