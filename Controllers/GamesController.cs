// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using MyBasketballPointsApi.Data;
// using MyBasketballPointsApi.Models;

// namespace MyBasketballPointsApi.Controllers
// {
//     public class GamesController : ControllerBase
//     {
//         private readonly MyBasketballPointsContext _context;

//         public GamesController(MyBasketballPointsContext context)
//         {
//             _context = context;
//         }

//         // GET: Games
//         public async Task<IActionResult> Index()
//         {
//             return View(await _context.Game.ToListAsync());
//         }

//         // GET: Games/Details/5
//         public async Task<IActionResult> Details(int? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }

//             var game = await _context.Game
//                 .FirstOrDefaultAsync(m => m.Id == id);
//             if (game == null)
//             {
//                 return NotFound();
//             }

//             return View(game);
//         }

//         // GET: Games/Create
//         public IActionResult Create()
//         {
//             return View();
//         }

//         // POST: Games/Create
//         // To protect from overposting attacks, enable the specific properties you want to bind to, for 
//         // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Create([Bind("Id")] Game game)
//         {
//             if (ModelState.IsValid)
//             {
//                 _context.Add(game);
//                 await _context.SaveChangesAsync();
//                 return RedirectToAction(nameof(Index));
//             }
//             return View(game);
//         }

//         // GET: Games/Edit/5
//         public async Task<IActionResult> Edit(int? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }

//             var game = await _context.Game.FindAsync(id);
//             if (game == null)
//             {
//                 return NotFound();
//             }
//             return View(game);
//         }

//         // POST: Games/Edit/5
//         // To protect from overposting attacks, enable the specific properties you want to bind to, for 
//         // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Edit(int id, [Bind("Id")] Game game)
//         {
//             if (id != game.Id)
//             {
//                 return NotFound();
//             }

//             if (ModelState.IsValid)
//             {
//                 try
//                 {
//                     _context.Update(game);
//                     await _context.SaveChangesAsync();
//                 }
//                 catch (DbUpdateConcurrencyException)
//                 {
//                     if (!GameExists(game.Id))
//                     {
//                         return NotFound();
//                     }
//                     else
//                     {
//                         throw;
//                     }
//                 }
//                 return RedirectToAction(nameof(Index));
//             }
//             return View(game);
//         }

//         // GET: Games/Delete/5
//         public async Task<IActionResult> Delete(int? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }

//             var game = await _context.Game
//                 .FirstOrDefaultAsync(m => m.Id == id);
//             if (game == null)
//             {
//                 return NotFound();
//             }

//             return View(game);
//         }

//         // POST: Games/Delete/5
//         [HttpPost, ActionName("Delete")]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> DeleteConfirmed(int id)
//         {
//             var game = await _context.Game.FindAsync(id);
//             _context.Game.Remove(game);
//             await _context.SaveChangesAsync();
//             return RedirectToAction(nameof(Index));
//         }

//         private bool GameExists(int id)
//         {
//             return _context.Game.Any(e => e.Id == id);
//         }
//     }
// }

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyBasketballPointsApi.Data;
using MyBasketballPointsApi.Models;

namespace MyBasketballPointsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly IRepository _repo;

        public GamesController(IRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _repo.GetAllGamesAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        [HttpGet("{gameId}")]
        public async Task<IActionResult> GetByGameId(int gameId)
        {
            try
            {
                var result = await _repo.GetGameAsyncById(gameId);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> post(Game model)
        {
            try
            {
                _repo.Add(model);

                if(await _repo.SaveChangesAsync())
                {
                    return Ok(model);
                }                
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }

            return BadRequest();
        }

        [HttpPut("{gameId}")]
        public async Task<IActionResult> put(int gameId, Game model)
        {
            try
            {
                var game = await _repo.GetGameAsyncById(gameId);
                if(game == null) return NotFound();

                if(!model.Date.Equals(null)){
                    game.Date = model.Date;
                }

                if(!model.Points.Equals(null)){
                     game.Points = model.Points;
                }

                _repo.Update(game);

                if(await _repo.SaveChangesAsync())
                {
                    return Ok(game);
                }                
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }

            return BadRequest();
        }

        [HttpDelete("{gameId}")]
        public async Task<IActionResult> delete(int gameId)
        {
            try
            {
                var game = await _repo.GetGameAsyncById(gameId);
                if(game == null) return NotFound();

                _repo.Delete(game);

                if(await _repo.SaveChangesAsync())
                {
                    return Ok();
                }                
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }

            return BadRequest();
        }
        
        [HttpGet("results")]
         public async Task<IActionResult> GetResult(){ 
             var result =  await _repo.GetResult();
            return Ok(result);
         }
    }
}