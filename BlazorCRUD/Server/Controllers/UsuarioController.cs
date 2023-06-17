using BlazorCRUD.Server.Servicios;

using BlazorCRUD.Server.Modelos;
using BlazorCRUD.Server.Clases;

using BlazorCRUD.Shared;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IO;
using System.Runtime.CompilerServices;

namespace BlazorCRUD.Server.Controllers
{
    [Route("api/Usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuario iUsuario;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileChecker oFileChecker = new FileChecker();

        public UsuarioController(IUsuario iUser, IWebHostEnvironment webHostEnvironment)
        {
            iUsuario = iUser;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<List<Modelos.Usuario>> ListaUsuarios()
        {
            return await Task.FromResult(iUsuario.DatosUsuarios());
        }


        [HttpPost]
        public void Post(Modelos.Usuario usuario)
        {
            iUsuario.NuevoUsuario(usuario);
        }

        [HttpGet("{id}")]
        public IActionResult MostrarUsuario(int id)
        {
            Modelos.Usuario u = iUsuario.DatosUsuario(id);
            if (u != null)
            {
                return Ok(u);
            }
            return NotFound();
        }

        [HttpPut]
        public void Modificar(Modelos.Usuario usuario)
        {
            iUsuario.ActualizaUsuario(usuario);
        }

        [HttpDelete("{id}")]
        public IActionResult Borrar(int id)
        {
            iUsuario.BorrarUsuario(id);
            return Ok();
        }

        ///Agregar Metodo Para El Login
        ///
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginClase login)
        {
            //Obtener datos del usuario a comparar en base datos
            Modelos.Usuario sesionDTO = iUsuario.DatosUsuario1(login.Correo);

            Modelos.Usuario sesionActual = new Modelos.Usuario();


            if (login.Correo.Equals(sesionDTO.Email) && login.Clave.Equals(sesionDTO.Password))
            {
                sesionActual.Nombre = sesionDTO.Nombre;
                sesionActual.Email = sesionDTO.Email;


            }
            else
            {
                sesionActual.Nombre = sesionDTO.Nombre;
                sesionActual.Email = sesionDTO.Email;

            }

            return StatusCode(StatusCodes.Status200OK, sesionActual); //Investigar
        }
    }
}
