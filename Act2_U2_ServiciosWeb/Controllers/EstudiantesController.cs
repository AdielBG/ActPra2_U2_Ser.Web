using Act2_U2_ServiciosWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace Act2_U2_ServiciosWeb.Controllers
{
    [ApiController]
    [Route("api/estudiantes")]
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

        // Contador para generar IDs únicos al agregar nuevos estudiantes
        private static int contadorId = 6;


        // ENDPOINT 1: Obtener todos los estudiantes
        [HttpGet("estudiantes")]
        public IActionResult ObtenerTodos()
        {
            return Ok(listaEstudiantes);
        }


        // ENDPOINT 2: Obtener un estudiante por Id
        [HttpGet("{id}")]
        public IActionResult ObtenerPorId(int id)
        {
            // Buscar el estudiante en la lista
            Estudiante estudianteEncontrado = null;

            foreach (Estudiante e in listaEstudiantes)
            {
                if (e.Id == id)
                {
                    estudianteEncontrado = e;
                }
            }

            // Si no se encontró, devolver 404
            if (estudianteEncontrado == null)
            {
                return NotFound("No se encontró un estudiante con el Id " + id);
            }

            return Ok(estudianteEncontrado);
        }


        // ENDPOINT 3: Agregar un nuevo estudiante
        [HttpPost]
        public IActionResult Agregar([FromBody] Estudiante nuevoEstudiante)
        {
            if (nuevoEstudiante == null)
            {
                return BadRequest("Los datos del estudiante no son válidos.");
            }

            // Asignar un Id automático
            nuevoEstudiante.Id = contadorId;
            contadorId = contadorId + 1;

            // Agregar a la lista
            listaEstudiantes.Add(nuevoEstudiante);

            // 201 Created: devuelve el estudiante creado y la ruta para consultarlo
            return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevoEstudiante.Id }, nuevoEstudiante);
        }


        // ENDPOINT 4: Actualizar un estudiante completo
        [HttpPut("{id}")]
        public IActionResult Actualizar(int id, [FromBody] Estudiante estudianteActualizado)
        {
            // Buscar el estudiante a actualizar
            Estudiante estudianteEncontrado = null;

            foreach (Estudiante e in listaEstudiantes)
            {
                if (e.Id == id)
                {
                    estudianteEncontrado = e;
                }
            }

            if (estudianteEncontrado == null)
            {
                return NotFound("No se encontró un estudiante con el Id " + id);
            }

            // Reemplazar todos los campos
            estudianteEncontrado.Nombre = estudianteActualizado.Nombre;
            estudianteEncontrado.Apellido = estudianteActualizado.Apellido;
            estudianteEncontrado.Correo = estudianteActualizado.Correo;
            estudianteEncontrado.Carrera = estudianteActualizado.Carrera;
            estudianteEncontrado.Edad = estudianteActualizado.Edad;
            estudianteEncontrado.Promedio = estudianteActualizado.Promedio;
            estudianteEncontrado.Activo = estudianteActualizado.Activo;

            // 204 NoContent: actualización exitosa, no hay nada que devolver
            return NoContent();
        }


        // ENDPOINT 5: Eliminar un estudiante
        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            // Buscar el estudiante a eliminar
            Estudiante estudianteEncontrado = null;

            foreach (Estudiante e in listaEstudiantes)
            {
                if (e.Id == id)
                {
                    estudianteEncontrado = e;
                }
            }

            if (estudianteEncontrado == null)
            {
                return NotFound("No se encontró un estudiante con el Id " + id);
            }

            listaEstudiantes.Remove(estudianteEncontrado);

            // 204 NoContent: eliminación exitosa
            return NoContent();
        }


        // ENDPOINT 6: Buscar por nombre o apellido
        [HttpGet("buscar")]
        public IActionResult Buscar([FromQuery] string texto)
        {
            if (texto == null || texto == "")
            {
                return BadRequest("Debe ingresar un texto para buscar.");
            }

            List<Estudiante> resultado = new List<Estudiante>();
            string textoBusqueda = texto.ToLower();

            foreach (Estudiante e in listaEstudiantes)
            {
                // Convertir a minúsculas para que la búsqueda no distinga mayúsculas
                string nombreMinusculas = e.Nombre.ToLower();
                string apellidoMinusculas = e.Apellido.ToLower();

                if (nombreMinusculas.Contains(textoBusqueda) || apellidoMinusculas.Contains(textoBusqueda))
                {
                    resultado.Add(e);
                }
            }

            return Ok(resultado);
        }


        // ENDPOINT 7: Filtrar por carrera
        [HttpGet("carrera/{carrera}")]
        public IActionResult PorCarrera(string carrera)
        {
            List<Estudiante> resultado = new List<Estudiante>();
            string carreraBusqueda = carrera.ToLower();

            foreach (Estudiante e in listaEstudiantes)
            {
                if (e.Carrera.ToLower() == carreraBusqueda)
                {
                    resultado.Add(e);
                }
            }

            return Ok(resultado);
        }


        // ENDPOINT 8: Obtener aprobados por promedio mínimo
        [HttpGet("aprobados")]
        public IActionResult Aprobados([FromQuery] decimal promedioMinimo = 70)
        {
            List<Estudiante> resultado = new List<Estudiante>();

            foreach (Estudiante e in listaEstudiantes)
            {
                if (e.Promedio >= promedioMinimo)
                {
                    resultado.Add(e);
                }
            }

            return Ok(resultado);
        }


        // ENDPOINT 9: Ordenar la lista
        [HttpGet("ordenar")]
        public IActionResult Ordenar([FromQuery] string por, [FromQuery] string direccion)
        {
            // Copiar la lista para no modificar el orden original
            List<Estudiante> resultado = new List<Estudiante>(listaEstudiantes);

            // Ordenar según el campo indicado
            if (por == "nombre")
            {
                // Ordenamiento burbuja simple por nombre
                for (int i = 0; i < resultado.Count - 1; i++)
                {
                    for (int j = 0; j < resultado.Count - 1 - i; j++)
                    {
                        bool debeIntercambiar = false;

                        if (direccion == "asc")
                        {
                            debeIntercambiar = string.Compare(resultado[j].Nombre, resultado[j + 1].Nombre) > 0;
                        }
                        else
                        {
                            debeIntercambiar = string.Compare(resultado[j].Nombre, resultado[j + 1].Nombre) < 0;
                        }

                        if (debeIntercambiar)
                        {
                            Estudiante temporal = resultado[j];
                            resultado[j] = resultado[j + 1];
                            resultado[j + 1] = temporal;
                        }
                    }
                }
            }
            else if (por == "promedio")
            {
                for (int i = 0; i < resultado.Count - 1; i++)
                {
                    for (int j = 0; j < resultado.Count - 1 - i; j++)
                    {
                        bool debeIntercambiar = false;

                        if (direccion == "asc")
                        {
                            debeIntercambiar = resultado[j].Promedio > resultado[j + 1].Promedio;
                        }
                        else
                        {
                            debeIntercambiar = resultado[j].Promedio < resultado[j + 1].Promedio;
                        }

                        if (debeIntercambiar)
                        {
                            Estudiante temporal = resultado[j];
                            resultado[j] = resultado[j + 1];
                            resultado[j + 1] = temporal;
                        }
                    }
                }
            }
            else if (por == "edad")
            {
                for (int i = 0; i < resultado.Count - 1; i++)
                {
                    for (int j = 0; j < resultado.Count - 1 - i; j++)
                    {
                        bool debeIntercambiar = false;

                        if (direccion == "asc")
                        {
                            debeIntercambiar = resultado[j].Edad > resultado[j + 1].Edad;
                        }
                        else
                        {
                            debeIntercambiar = resultado[j].Edad < resultado[j + 1].Edad;
                        }

                        if (debeIntercambiar)
                        {
                            Estudiante temporal = resultado[j];
                            resultado[j] = resultado[j + 1];
                            resultado[j + 1] = temporal;
                        }
                    }
                }
            }
            else
            {
                return BadRequest("El campo de ordenamiento debe ser: nombre, promedio o edad.");
            }

            return Ok(resultado);
        }

    }
}
