using BlazorCRUD.Server.Modelos;

namespace BlazorCRUD.Server.Servicios
{
    public interface IUsuario
    {
        public List<Usuario> DatosUsuarios();

        public void NuevoUsuario(Usuario u);

        public void ActualizaUsuario(Usuario u);

        public Usuario DatosUsuario(int id);

        public Usuario DatosUsuario1(string Email);

        public void BorrarUsuario(int id);


    }
}
