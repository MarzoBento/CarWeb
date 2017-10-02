namespace CarWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Municipios
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idmunicipio { get; set; }

        [DisplayName("Lote")]
        public int? idlote { get; set; }

        [DisplayName("Municipio")]
        public string municipio { get; set; }

        [StringLength(10)]
        [DisplayName("UF")]
        public string uf { get; set; }
    }
}
