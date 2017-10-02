namespace CarWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Propietario")]
    public partial class Propietario
    {
        [Key]
        public int idpropietario { get; set; }

        [Column("propietario")]
        [DisplayName("Proprietário")]
        public string propietario1 { get; set; }

        [StringLength(22)]
        [DisplayName("Cpf")]
        public string cpf { get; set; }
    }
}
