namespace CarWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    public partial class Arquivos
    {
        [Key]
        public int idarquivo { get; set; }

        [Required]
        public string caminho { get; set; }

        public int idcadastro { get; set; }

        public int? idlote { get; set; }

        public string cadastrador { get; set; }

        public string nomearquivo { get; set; }


        public IEnumerable<HttpPostedFileBase> Arquivoss { get; set; }
    }
}
