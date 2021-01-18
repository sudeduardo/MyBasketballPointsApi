using System.Threading.Tasks;
using MyBasketballPointsApi.Models;

namespace MyBasketballPointsApi.Data
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();

        Task<Game[]> GetAllGamesAsync();        
    
        Task<Game> GetGameAsyncById(int gameId);
        Task<Results> GetResult();
        
    }
}