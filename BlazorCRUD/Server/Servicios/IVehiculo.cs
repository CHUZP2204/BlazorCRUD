using BlazorCRUD.Server.Modelos;

namespace BlazorCRUD.Server.Servicios
{
    public interface IVehiculo
    {

        public List<TblVehiculo> ListVehiculos();

        public void AddVehiculo(TblVehiculo vehiculoNuevo);

        public void RemoveVehiculo(int id_Vehiculo);

        public void UpadateVehiculo(TblVehiculo vehiculoEditar);

        public TblVehiculo DataVehiculo(int id_Vehiculo);

    }
}
