using AccesoDatos.Data;
using Microsoft.EntityFrameworkCore;

namespace AccesoDatos.Repositories
{
    public class GenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository()
        {
            _context = new ApplicationDbContext();
            _dbSet = _context.Set<T>();
        }

        public List<T> ObtenerTodos()
        {
            return _dbSet.ToList();
        }

        public T? ObtenerPorId(int id)
        {
            return _dbSet.Find(id);
        }

        public void Agregar(T entidad)
        {
            _dbSet.Add(entidad);
            _context.SaveChanges();
        }

        public int Contar()
        {
            return _dbSet.Count();
        }
    }
}
