using BlazorCRUD.Server.Modelos;

namespace BlazorCRUD.Server.Servicios
{
    public interface IUsuario
    {
        public List<Usuario> DatosUsuarios();

        public void NuevoUsuario(Usuario u);

        public void ActualizaUsuario(Usuario u);

        public Usuario DatosUsuario(int id);

        public void BorrarUsuario(int id);


    }
}
