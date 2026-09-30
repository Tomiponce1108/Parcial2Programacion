namespace AccesoDatos.Models;

public class Cancion
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public int DuracionSegundos { get; set; }

    // Clave foránea: cada canción pertenece a un único artista
    public int ArtistaId { get; set; }

    // Propiedad de navegación
    public Artista Artista { get; set; } = null!;
}
