namespace CarWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Cadastrador")]
    public partial class Cadastrador
    {
        [Key]
        public int idcadastrador { get; set; }

        [Column("cadastrador")]
        [DisplayName("Cadastrador")]
        public string cadastrador1 { get; set; }
    }
}
