using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AFILIADOS_JUAN_CARLOS_TEJEDA.Models
{
    public class Planes
    {
        public int ID { get; set; }
        public string Plan { get; set; }
        public Decimal MontoCobertura { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int IdEstatus { get; set; }
        
    }
}