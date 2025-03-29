using InternIntellegence_Portfolio.DbHelper.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace InternIntellegence_Portfolio.DbHelper.Repos
{
	public class GenericRepo<T> : IGenericRepo<T> where T : class
	{
		private readonly ApplicationContext _context;
        public GenericRepo(ApplicationContext context)
        {
            _context = context;
        }

		public void Add(T entity)
		{
			_context.Set<T>().Add(entity);
		}

		public void Delete(T entity)
		{
			_context.Set<T>().Remove(entity);
		}

		public IQueryable<T> GetAll()
		{
			return _context.Set<T>().AsQueryable();
		}

		public T GetById(int id)
		{
			return _context.Set<T>().Find(id)!;
		}

		public void Save()
		{
			_context.SaveChanges();
		}

		public void Update(T entity)
		{
			_context.Set<T>().Update(entity);
		}
	}
}
