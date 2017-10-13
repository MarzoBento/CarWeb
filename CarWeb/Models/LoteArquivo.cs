using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarWeb.Models
{
    public class LoteArquivo
    {

        public virtual IList<Lotes> Lotes { get; set; }

        public virtual IList<Arquivos> Arquivos { get; set; }

    }
}