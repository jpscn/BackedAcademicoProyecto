using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.core.Entidades
{
    public class Estudiante
    {
       
            public int idEstudiante { set; get; }
            public string ci { set; get; }
            public string nombres { set; get; }
            public string apellidos { set; get; }
            public DateTime fechaNacimiento { get; set; }
        
    }
}
