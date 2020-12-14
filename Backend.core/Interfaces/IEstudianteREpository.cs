using System;
using System.Collections.Generic;
using System.Text;
using Backend.core.Entidades;

namespace Backend.core.Interfaces
{
    public interface IEstudianteREpository
    {
        IEnumerable<Estudiante> GetEstudiantesDB();
        IEnumerable<Estudiante> GenerarDatosEstudiantes();
        bool AddEstudiantes(Estudiante nuevo);
        bool InsertarDatosLista();
    }
}
