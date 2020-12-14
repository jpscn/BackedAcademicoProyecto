using System;
using System.Collections.Generic;
using System.Text;
using Backend.core.Entidades;
using Backend.core.Interfaces;
using System.Linq;
using Oracle.ManagedDataAccess.Client;
using Microsoft.Extensions.Configuration;

namespace BackendInfraestructura.Repositorios
{
   public class EstudianteRepository : IEstudianteREpository
    {
        private readonly IConfiguration _configuracion;
        public EstudianteRepository(IConfiguration configuracion)
        {
            _configuracion = configuracion;
        }
     
        public IEnumerable<Estudiante> GetEstudiantesDB()
        {
             string strCon = _configuracion.GetConnectionString("OracleDBConnection");
            List<Estudiante> listaEstudiantes = new List<Estudiante>();
            using (OracleConnection conexion = new OracleConnection(strCon))
            {

                using (OracleCommand comando = conexion.CreateCommand())
                {
                    
                    try
                    {
                        conexion.Open();
                        comando.CommandText = "select * from estudiante";

                        OracleDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            listaEstudiantes.Add(new Estudiante()
                            {
                                idEstudiante = reader.GetInt32(0),
                                ci = reader.GetString(1),
                                nombres = reader.GetString(2),
                                apellidos = reader.GetString(3),
                                fechaNacimiento = reader.GetDateTime(4)

                            });



                        }
                        reader.Dispose();

                    }
                    catch (Exception error)
                    {

                        throw new Exception(error.Message);
                    }  
                }
            }
            return listaEstudiantes;
        }
        public IEnumerable<Estudiante>GenerarDatosEstudiantes()
        {
            
            List<Estudiante>listaEstudiante = new List<Estudiante>();
            bool existeDatos = false;
            int numRegistros = 0;
            var ramdomNombres = new string[] { "Juan", "Maria", "Rodrigo", "Jose", "Marco", "Osvaldo", "Juana", "Rocio", "Lucia", "Mariel", "Tito", "Andres", "Roxana", "Leticia", "Ruth", "Mario", "Miriam", "Ruben", "Daniel", "Omar", "Carlos" };
            var ramdomApellidos = new string[] { "Carvajal", "Lopez", "Conde", "Roque", "Rocha", "Martinez", "Lucana", "Arce", "Quintana", "Rodriguez", "Alvarez", "Quispe", "Mamani", "Rojas", "Vaca", "Barba", "Soria", "Gonzales", "Palacios", "Reyes" };
            try
            {
                while(numRegistros<10)
                {
                    existeDatos = false;
                    Estudiante nuevoEstudiante = new Estudiante()
                    {
                        idEstudiante = numRegistros,
                        nombres = ramdomNombres[new Random().Next(ramdomNombres.Length)],
                        apellidos = ramdomApellidos[new Random().Next(ramdomApellidos.Length)],
                        ci = new Random().Next(1000000, 9999999).ToString(),
                        fechaNacimiento = new DateTime(new Random().Next(1984, 2000),
                                         new Random().Next(1, 12),
                                         new Random().Next(1, 31)),
                    };
                    foreach(Estudiante actual in listaEstudiante)
                    {
                        if(actual.nombres == nuevoEstudiante.nombres || actual.apellidos== nuevoEstudiante.apellidos || actual.ci == nuevoEstudiante.ci || actual.fechaNacimiento == nuevoEstudiante.fechaNacimiento) { existeDatos = true; break; }
                    }
                    if(!existeDatos)
                    {
                        listaEstudiante.Add(nuevoEstudiante);
                        numRegistros++;
                    }
                    
                }
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
            return listaEstudiante;
        }
        public bool AddEstudiantes(Estudiante nuevo)
        {

            string strCon = _configuracion.GetConnectionString("OracleDBConnection");
            int result = 0;
            using (OracleConnection connection = new OracleConnection(strCon))
            {
                using (OracleCommand command = connection.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        command.BindByName = true;
                        command.CommandText = "insert into estudiante (NOMBRE, APELLIDO, CI, FECHA_NACIMIENTO) " +
                            "values(:nombres, :apellidos, :ci, :fechaNacimiento)";

                        OracleParameter nombres = new OracleParameter();
                        nombres.ParameterName = "nombres";
                        nombres.Value = nuevo.nombres;
                        command.Parameters.Add(nombres);

                        OracleParameter apellidos = new OracleParameter();
                        apellidos.ParameterName = "apellidos";
                        apellidos.Value = nuevo.apellidos;
                        command.Parameters.Add(apellidos);

                        OracleParameter ci = new OracleParameter();
                        ci.ParameterName = "ci";
                        ci.Value = nuevo.ci;
                        command.Parameters.Add(ci);

                        OracleParameter fechaNacimiento = new OracleParameter();
                        fechaNacimiento.ParameterName = "fechaNacimiento";
                        fechaNacimiento.Value = nuevo.fechaNacimiento;
                        fechaNacimiento.DbType = System.Data.DbType.Date;
                        command.Parameters.Add(fechaNacimiento);

                        result = command.ExecuteNonQuery();
                    }
                    catch (Exception err)
                    {
                        throw new Exception(err.Message);
                    }
                }
            }
            return result > 0 ? true : false;
        }
        public bool InsertarDatosLista()
        {
            bool exito = true;
            try
            {
                IEnumerable<Estudiante> miLista = GenerarDatosEstudiantes();
                foreach(Estudiante actual in miLista)
                {
                    exito = AddEstudiantes(actual);
                    if (!exito) break;
                }

            }
            catch(Exception error)
            {
                throw new Exception(error.Message);
            }
            return exito;
        }
    }
}
