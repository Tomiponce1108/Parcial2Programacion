using AccesoDatos.Data;
using AccesoDatos.Models;
using AccesoDatos.Repositories;
using Microsoft.EntityFrameworkCore;

Console.OutputEncoding = System.Text.Encoding.UTF8;

// Aplica las migraciones: crea la base de datos y las tablas si no existen
using (var context = new ApplicationDbContext())
{
    context.Database.Migrate();
}

var artistaRepo = new ArtistaRepository();
var cancionRepo = new CancionRepository();
const int CantidadMasLargas = 5;

var salir = false;
while (!salir)
{
    Console.WriteLine();
    Console.WriteLine("===== PLATAFORMA DE MÚSICA =====");
    Console.WriteLine("1. Alta artista");
    Console.WriteLine("2. Alta canción");
    Console.WriteLine("3. Ver canciones");
    Console.WriteLine("4. Mostrar canciones más largas");
    Console.WriteLine("5. Cantidad total de canciones");
    Console.WriteLine("6. Mostrar canciones ordenadas alfabéticamente por título");
    Console.WriteLine("7. Verificar si existen canciones registradas");
    Console.WriteLine("0. Salir");
    Console.Write("Opción: ");

    switch (Console.ReadLine()?.Trim())
    {
        case "1": AltaArtista(); break;
        case "2": AltaCancion(); break;
        case "3": MostrarCanciones("Canciones registradas", cancionRepo.ObtenerTodasConArtista()); break;
        case "4": MostrarCanciones($"Las {CantidadMasLargas} canciones más largas", cancionRepo.ObtenerMasLargas(CantidadMasLargas)); break;
        case "5": Console.WriteLine($"\nCantidad total de canciones: {cancionRepo.Contar()}"); break;
        case "6": MostrarCanciones("Canciones ordenadas por título", cancionRepo.ObtenerOrdenadasPorTitulo()); break;
        case "7": VerificarCanciones(); break;
        case "0": salir = true; break;
        default: Console.WriteLine("Opción inválida."); break;
    }
}

// ================= Opciones =================

void AltaArtista()
{
    Console.WriteLine("\n--- Alta de artista ---");
    Console.Write("Nombre: ");
    var nombre = Console.ReadLine()?.Trim();

    if (string.IsNullOrWhiteSpace(nombre))
    {
        Console.WriteLine("El nombre no puede estar vacío.");
        return;
    }

    var artista = new Artista { Nombre = nombre };
    artistaRepo.Agregar(artista);
    Console.WriteLine($"Artista registrado (Id {artista.Id}): {artista.Nombre}");
}

void AltaCancion()
{
    Console.WriteLine("\n--- Alta de canción ---");

    var artistas = artistaRepo.ObtenerTodos();
    if (artistas.Count == 0)
    {
        Console.WriteLine("No hay artistas registrados. Primero dá de alta un artista (opción 1).");
        return;
    }

    Console.WriteLine("Artistas registrados:");
    foreach (var a in artistas)
        Console.WriteLine($"  [{a.Id}] {a.Nombre}");

    Console.Write("Título: ");
    var titulo = Console.ReadLine()?.Trim();
    if (string.IsNullOrWhiteSpace(titulo))
    {
        Console.WriteLine("El título no puede estar vacío.");
        return;
    }

    Console.Write("Duración en segundos: ");
    if (!int.TryParse(Console.ReadLine(), out var duracion) || duracion <= 0)
    {
        Console.WriteLine("La duración debe ser un número entero mayor a 0.");
        return;
    }

    Console.Write("Id del artista: ");
    if (!int.TryParse(Console.ReadLine(), out var artistaId) || !artistaRepo.ExisteArtista(artistaId))
    {
        Console.WriteLine("El artista indicado no existe.");
        return;
    }

    var cancion = new Cancion
    {
        Titulo = titulo,
        DuracionSegundos = duracion,
        ArtistaId = artistaId
    };
    cancionRepo.Agregar(cancion);
    Console.WriteLine($"Canción registrada (Id {cancion.Id}): {cancion.Titulo}");
}

void MostrarCanciones(string titulo, List<Cancion> canciones)
{
    Console.WriteLine($"\n--- {titulo} ---");
    if (canciones.Count == 0)
    {
        Console.WriteLine("No hay canciones registradas.");
        return;
    }

    foreach (var c in canciones)
        Console.WriteLine($"[{c.Id}] {c.Titulo} - {c.Artista.Nombre} ({FormatearDuracion(c.DuracionSegundos)})");
}

void VerificarCanciones()
{
    Console.WriteLine(cancionRepo.ExistenCanciones()
        ? "\nSí, existen canciones registradas."
        : "\nNo hay canciones registradas.");
}

static string FormatearDuracion(int segundos) => $"{segundos / 60}:{segundos % 60:D2}";
