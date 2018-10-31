using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Farmacia.Models
{
    public class Presentacion
    {
        [Required, Display(Name = "Identificador", Prompt = "Identificador", Description = "Identificador")]
        public int Id { get; set; }

        [Required, Display(Name = "Presentación", Prompt = "Presentación", Description = "Presentación")]
        public string Nombre { get; set; }

        public ICollection<Medicamento> Medicamentos { get; set; }
    }
}