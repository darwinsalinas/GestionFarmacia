using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Farmacia.Models
{
    public class Medicamento
    {
        [Required, Display(Name = "Identificador", Prompt = "Identificador", Description = "Identificador")]
        public int Id { get; set; }

        [Required, Display(Name = "Medicamento", Prompt = "Medicamento", Description = "Medicamento")]
        public string Nombre { get; set; }

        [Required]
        [Display(Name = "Presentación", Prompt = "Presentación", Description = "Presentación")]
        public int PresentacionId { get; set; }
        [Display(Name = "Presentación", Prompt = "Presentación", Description = "Presentación")]
        public Presentacion Presentacion { get; set; }


        [Required, Display(Name = "Precio", Prompt = "Precio", Description = "Precio")]
        public float Precio { get; set; }

        [Required, Display(Name = "Existencia", Prompt = "Existencia", Description = "Existencia")]
        public float Existencia { get; set; }

        public  ICollection<VentaDetalle> Ventas { get; set; }
    }
}