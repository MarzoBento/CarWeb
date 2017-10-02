namespace CarWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Lotes
    {
        [Key]
        public int idlote { get; set; }

        [StringLength(10)]
        [DisplayName("Lote")]
        public string lote { get; set; }
    }
}
