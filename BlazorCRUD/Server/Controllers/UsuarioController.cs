using BlazorCRUD.Server.Servicios;

using BlazorCRUD.Server.Modelos;
using BlazorCRUD.Server.Clases;
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
        public async Task<List<Usuario>> ListaUsuarios()
        {
            return await Task.FromResult(iUsuario.DatosUsuarios());
        }


        [HttpPost]
        public void Post(Usuario usuario)
        {
            iUsuario.NuevoUsuario(usuario);
        }

        [HttpGet("{id}")]
        public IActionResult MostrarUsuario(int id)
        {
            Usuario u = iUsuario.DatosUsuario(id);
            if (u != null)
            {
                return Ok(u);
            }
            return NotFound();
        }

        [HttpPut]
        public void Modificar(Usuario usuario)
        {
            iUsuario.ActualizaUsuario(usuario);
        }

        [HttpDelete("{id}")]
        public IActionResult Borrar(int id)
        {
            iUsuario.BorrarUsuario(id);
            return Ok();
        }
    }
}
