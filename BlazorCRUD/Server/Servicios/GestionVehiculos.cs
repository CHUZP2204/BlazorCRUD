using BlazorCRUD.Server.Modelos;
using Microsoft.EntityFrameworkCore;

namespace BlazorCRUD.Server.Servicios
{
    public class GestionVehiculos : IVehiculo
    {
        readonly PruebablazorContext BaseDatos = new PruebablazorContext();

        public void AddVehiculo(TblVehiculo vehiculoNuevo)
        {
            try
            {
                BaseDatos.Add(vehiculoNuevo);
                BaseDatos.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());

            };
        }

        public TblVehiculo DataVehiculo(int id_Vehiculo)
        {

            TblVehiculo? v = BaseDatos.TblVehiculos.Find(id_Vehiculo);
            try
            {
                return (v);
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException();
            }
        }

        public List<TblVehiculo> ListVehiculos()
        {


            try
            {
                return BaseDatos.TblVehiculos.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public void RemoveVehiculo(int id_Vehiculo)
        {
            try
            {
                TblVehiculo? v = BaseDatos.TblVehiculos.Find(id_Vehiculo);
                if (v != null)
                {
                    BaseDatos.TblVehiculos.Remove(v);
                    BaseDatos.SaveChanges();
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public void UpadateVehiculo(TblVehiculo vehiculoEditar)
        {
            try
            {
                BaseDatos.Entry(vehiculoEditar).State = EntityState.Modified;
                BaseDatos.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
