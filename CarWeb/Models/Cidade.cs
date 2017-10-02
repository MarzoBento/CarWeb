namespace CarWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Cidade")]
    public partial class Cidade
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idcidade { get; set; }

        [Column("cidade")]
        [StringLength(80)]
        public string cidade1 { get; set; }

        [StringLength(2)]
        public string uf { get; set; }
    }
}
