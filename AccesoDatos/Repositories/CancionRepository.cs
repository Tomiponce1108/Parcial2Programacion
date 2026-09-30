using AccesoDatos.Models;
using Microsoft.EntityFrameworkCore;

namespace AccesoDatos.Repositories
{
    public class CancionRepository : GenericRepository<Cancion>
    {
        // Ver canciones (incluye el artista para poder mostrarlo)
        public List<Cancion> ObtenerTodasConArtista()
        {
            return _context.Cancion
                           .Include(c => c.Artista)
                           .ToList();
        }

        // Canciones más largas: las de mayor duración primero
        public List<Cancion> ObtenerMasLargas(int cantidad)
        {
            return _context.Cancion
                           .Include(c => c.Artista)
                           .OrderByDescending(c => c.DuracionSegundos)
                           .Take(cantidad)
                           .ToList();
        }

        // Orden alfabético por título (en memoria, para ignorar mayúsculas/minúsculas)
        public List<Cancion> ObtenerOrdenadasPorTitulo()
        {
            return _context.Cancion
                           .Include(c => c.Artista)
                           .ToList()
                           .OrderBy(c => c.Titulo, StringComparer.CurrentCultureIgnoreCase)
                           .ToList();
        }

        public bool ExistenCanciones()
        {
            return _context.Cancion.Any();
        }
    }
}
