namespace InternIntellegence_Portfolio.DbHelper.Repos
{
	public interface IGenericRepo<T> where T : class
	{
		T GetById(int id);
		IQueryable<T> GetAll();
		void Add(T entity);
		void Update(T entity);
		void Delete(T entity);
		void Save();
	}
}
