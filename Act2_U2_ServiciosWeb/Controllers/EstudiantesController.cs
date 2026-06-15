using Act2_U2_ServiciosWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace Act2_U2_ServiciosWeb.Controllers
{
    public class EstudiantesController: ControllerBase
    {
        // Lista estática en memoria: todos los endpoints compartiran esta misma lista
        private static List<Estudiante> listaEstudiantes = new List<Estudiante>
        {
            new Estudiante { Id = 1, Nombre = "Ana", Apellido = "Pérez", Correo = "ana.perez@ufhec.edu.do", Carrera = "Ingeniería de Sistemas", Edad = 20, Promedio = 85.5m, Activo = true },
            new Estudiante { Id = 2, Nombre = "Luis", Apellido = "García", Correo = "luis.garcia@ufhec.edu.do", Carrera = "Ingeniería de Software", Edad = 22, Promedio = 72.0m, Activo = true },
            new Estudiante { Id = 3, Nombre = "María", Apellido = "López", Correo = "maria.lopez@ufhec.edu.do", Carrera = "Ingeniería de Sistemas", Edad = 23, Promedio = 60.0m, Activo = false },
            new Estudiante { Id = 4, Nombre = "Adiel", Apellido = "Batista", Correo = "adiel.batista@ufhec.edu.do", Carrera = "Ingeniería de Software", Edad = 21, Promedio = 90.0m, Activo = true },
            new Estudiante { Id = 5, Nombre = "Sofía", Apellido = "Martínez", Correo = "sofia.martinez@ufhec.edu.do", Carrera = "Ingeniería de Sistemas", Edad = 19, Promedio = 78.5m, Activo = true }
        };
    }
}
