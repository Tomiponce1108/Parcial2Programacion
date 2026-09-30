namespace AccesoDatos.Models;

public class Artista
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;

    // Un artista tiene muchas canciones (lado "uno" de la relación 1:N)
    public List<Cancion> Canciones { get; set; } = new();
}
