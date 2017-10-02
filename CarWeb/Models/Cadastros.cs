namespace CarWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    public partial class Cadastros
    {
       

        [Key]
        public int idcadastro { get; set; }

        [DisplayName("NºCadastro")]
        public Int64 ncadastro { get; set; }

        [DisplayName("Lote")]
        public int? idlote { get; set; }

        [DisplayName("Município")]
        public int? Idmunicipio { get; set; }

        [Required]
        [StringLength(22)]
        public string cpf { get; set; }

        [DisplayName("Proprietário")]
        public string propietario { get; set; }
        [DisplayName("Fone")]
        public string fonecontato { get; set; }

        [DisplayName("Propriedade")]
        public string propriedade { get; set; }

        [DisplayName("Comunidade")]
        public string comunidade { get; set; }

        [DisplayName("Área")]
        public decimal? area { get; set; }

        [StringLength(50)]
        [DisplayName("Status.Coordenadas")]
        public string statuscoodenada { get; set; }

        [StringLength(50)]
        [DisplayName("Google.Drive")]
        public string statusgoogle { get; set; }
        [DisplayName("Status.Serviço")]
        public string statusservico { get; set; }

        [DisplayName("Observação")]
        public string observacao { get; set; }

        [DisplayName("Cadastrador")]
        public string cadastrador { get; set; }

        [Column(TypeName = "date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Dt.Cadastro")]
        public DateTime? dtcadastro { get; set; }

        [StringLength(50)]
        [DisplayName("StatusEmissão")]
        public string statusemissao { get; set; }

        [DisplayName("NumeroCarba")]
        public string numerocarba { get; set; }

        [StringLength(50)]
        public string statusimpressao { get; set; }

        [Column(TypeName = "date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Dt.Entrega")]
        public DateTime? dtentrega { get; set; }


        // public IEnumerable<HttpPostedFileBase> Arquivos { get; set; }
        public virtual IList<Arquivos> Arquivos { get; set; }



        // public virtual Arquivos ArquivosTb { get; set; }

        public virtual Municipios Munic { get; set; }

        public IEnumerable<SelectListItem> MunicipioList { get; set; }

    }
}
