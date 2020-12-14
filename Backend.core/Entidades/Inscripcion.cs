using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.core.Entidades
{
   public  class Inscripcion
    {
        public int IdInscripcion { get; set; }
        public int IdMateria { get; set; }
        public int IdEstudiante { get; set; }
        public string descripcion { get; set; }
    }
}
