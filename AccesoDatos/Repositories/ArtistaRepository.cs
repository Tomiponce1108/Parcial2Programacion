using AccesoDatos.Models;

namespace AccesoDatos.Repositories
{
    public class ArtistaRepository : GenericRepository<Artista>
    {
        public bool ExisteArtista(int id)
        {
            return _context.Artista.Any(a => a.Id == id);
        }
    }
}
