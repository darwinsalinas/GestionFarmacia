using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Farmacia.Models
{
    public class VentaDetalle
    {
        [Required, Display(Name = "Identificador", Prompt = "Identificador", Description = "Identificador")]
        public int Id { get; set; }

        [Required, Display(Name = "Medicamento", Prompt = "Medicamento", Description = "Medicamento")]
        public int MedicamentoId { get; set; }
        public Medicamento Medicamento { get; set; }

        [Required, Display(Name = "Cantidad", Prompt = "Cantidad", Description = "Cantidad")]
        public int Cantidad { get; set; }

        [Required, Display(Name = "Precio", Prompt = "Precio", Description = "Precio")]
        [DisplayFormat(DataFormatString = "C$ {0:0.00}", ApplyFormatInEditMode = false)]
        public float Precio { get; set; }

        [Required, Display(Name = "Venta", Prompt = "Venta", Description = "Venta")]
        public int VentaId { get; set; }

        public Venta Venta { get; set; }



        [Display(Name = "Sub Total")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "C$ {0:0.00}")]
        public virtual float SubTotal { get { return Cantidad * Precio; } }

    }
}