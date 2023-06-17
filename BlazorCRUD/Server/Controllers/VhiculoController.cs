
using BlazorCRUD.Server.Servicios;
using BlazorCRUD.Server.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace BlazorCRUD.Server.Controllers
{
    [Route("api/Vhiculo")]
    [ApiController]
    public class VhiculoController : ControllerBase
    {
        private readonly IVehiculo iVehiculo;


        public VhiculoController(IVehiculo iVeh)
        {
            iVehiculo = iVeh;
        }
        [HttpGet]
        public async Task<List<TblVehiculo>> ListaVehiculos()
        {
            return await Task.FromResult(iVehiculo.ListVehiculos());
        }

        [HttpGet("{id}")]
        public IActionResult MostrarVehiculo(int id)
        {
            TblVehiculo v = iVehiculo.DataVehiculo(id);
            if (v != null)
            {
                return Ok(v);
            }
            return NotFound();
        }

        [HttpPost]
        public void nuevoVEhiculo(TblVehiculo vehiculo)
        {
            iVehiculo.AddVehiculo(vehiculo);
        }

        [HttpPut]
        public void modificarVehiculo(TblVehiculo vehiculo)
        {
            iVehiculo.UpadateVehiculo(vehiculo);
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarVehiculo(int id)
        {
            iVehiculo.RemoveVehiculo(id);
            return Ok();
        }

    }
}
