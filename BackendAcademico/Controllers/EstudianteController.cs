using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendInfraestructura.Repositorios;
using Backend.core.Interfaces;
using Backend.core.Entidades;

namespace BackendAcademico.Controllers
{
    //Https:port/api/GetEstudiantes
    [Route("api")]
    [ApiController]
    public class EstudianteController : ControllerBase
    {
        readonly IEstudianteREpository _estudianteREpository;
        public EstudianteController(IEstudianteREpository estudianteREpository)
        {
            _estudianteREpository = estudianteREpository;
        }
       
        [HttpGet]
        [Route("GetEstudiantesDB")]
        public IActionResult GetEstudiantesDB()
        {
            var estudiante = _estudianteREpository.GetEstudiantesDB();
            return Ok(estudiante);
        }
        [HttpPost]
        [Route("AddEstudiantes")]
        public IActionResult AddEstudiantes(Estudiante nuevo)
        {
            var estudiante  = _estudianteREpository.AddEstudiantes(nuevo);
            return Ok(estudiante);
        }
        [HttpGet]
        [Route ("InsertarDatosLista")]
        public IActionResult InsertarDatosLista()
        {
            var estudiante = _estudianteREpository.InsertarDatosLista();
            return Ok(estudiante);
        }
    }
}
