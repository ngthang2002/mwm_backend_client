using System.Threading.Tasks;

namespace Project.App.DesignPatterns.Reponsitories
{
    public partial interface IRepositoryBaseMariaDB<T> 
    {
        Task<T> GetByIdAsync(params object[] keys);
    }
    public partial class RepositoryBaseMariaDB<T> : IRepositoryBaseMariaDB<T> where T : class
    {
        public async Task<T> GetByIdAsync(params object[] keys)
        {
            return await DbContext.Set<T>().FindAsync(keys);
        }
    }
}