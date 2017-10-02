namespace CarWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ImportacaoSeia")]
    public partial class ImportacaoSeia
    {
        [Key]
        public int idseia { get; set; }

        public string nomeimovel { get; set; }

        public string municipio { get; set; }

        public string statuscadastro { get; set; }

        public string requerente { get; set; }

        public decimal? area { get; set; }

        [Column(TypeName = "date")]
        public DateTime? dtprimeirafinalizacao { get; set; }
    }
}
