using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AFILIADOS_JUAN_CARLOS_TEJEDA.Models
{
    public class Afiliados
    {
        [Key]
        public int Id { get; set; }

        public string Nombres{ get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Sexo { get; set; }
        public string Cedula { get; set; }
        public string Nss { get; set; }
        public DateTime FechaRegistro { get; set; }
        public decimal MontoConsumido { get; set; }
        public virtual Estatus Estatuses { get; set; }
        public virtual Planes Planes { get; set; }
    }
}