using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Farmacia.Models
{
    public class Venta
    {
        [Required, Display(Name = "Identificador", Prompt = "Identificador", Description = "Identificador")]
        public int Id { get; set; }

        [Required, Display(Name = "Código", Prompt = "Código", Description = "Código")]
        public string Codigo { get; set; }

        [Required, Display(Name = "Fecha", Prompt = "Fecha", Description = "Fecha")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }


        [Display(Name = "Total", Prompt = "Total", Description = "Total")]
        [DisplayFormat(DataFormatString = "C$ {0:0.00}", ApplyFormatInEditMode = false)]
        public virtual float Total { get; set; }


        public ICollection<VentaDetalle> VentaDetalles { get; set; }


    }
}