using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyBasketballPointsApi.Models;

namespace MyBasketballPointsApi.Data
{
    public class Repository : IRepository
    {
        private readonly MyBasketballPointsContext _context;

        public Repository(MyBasketballPointsContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<Game[]> GetAllGamesAsync()
        {
            IQueryable<Game> query = _context.Game;
            query = query.AsNoTracking()
                         .OrderBy(c => c.Id);

            return await query.ToArrayAsync();
        }
        public async Task<Game> GetGameAsyncById(int gameId)
        {
            IQueryable<Game> query = _context.Game;

            query = query.AsNoTracking()
                         .OrderBy(game => game.Id)
                         .Where(game => game.Id == gameId);

            return await query.FirstOrDefaultAsync();
        }
        public async Task<Results> GetResult(){

            var amountRecordBreaking = new List<int>();
            var games = _context.Game.OrderBy(x => x.Date).ToList();
            int max = 0;
            for (int i = 0; i < games.Count(); i++)
            {
                if(i != 0 && games[i].Points > max){
                    max =  (int)games[i].Points;
                    amountRecordBreaking.Add((int)games[i].Points);
                }
                
            }

            var result =  new Results{
                DataStart = (DateTime) _context.Game.Min(x => x.Date),
                DataEnd = (DateTime) _context.Game.Max(x => x.Date),
                AmountGame  = _context.Game.Count(),
                MaxPointInGames= _context.Game.Max(x => x.Points).GetValueOrDefault(0),
                MinPointInGames = _context.Game.Min(x => x.Points).GetValueOrDefault(0),
                AvgPointPerGame = _context.Game.Average(x => x.Points).GetValueOrDefault(0.0),
                CountAllPoints  = _context.Game.Sum(x => x.Points).GetValueOrDefault(0),
                AmountRecordBreaking = amountRecordBreaking.Count()
            };
            return result;
        }

    }
}