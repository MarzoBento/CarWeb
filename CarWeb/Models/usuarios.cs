namespace CarWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class usuarios
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idusu { get; set; }

        [StringLength(100)]
        public string nome { get; set; }

        [StringLength(100)]
        public string email { get; set; }

        [StringLength(20)]
        public string login { get; set; }

        [StringLength(32)]
        public string senha { get; set; }

        [StringLength(50)]
        public string status { get; set; }

        [StringLength(50)]
        public string tipousuario { get; set; }
    }
}
