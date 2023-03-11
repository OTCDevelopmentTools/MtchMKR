using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MtchMKRAPI.Data;
using MtchMKRAPI.Data.Entities;

namespace MtchMKRAPI.Controllers
{


    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly MtckMKRDbContext _mtckMKRDbContextt;
        public GameController(MtckMKRDbContext mtckMKRDbContext)
        {
            _mtckMKRDbContextt = mtckMKRDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var games = await _mtckMKRDbContextt.games.ToListAsync();
            return Ok(games);
        }
       
        [HttpPost]
        public async Task<bool> PostAsync(Game game)
        {
            bool flag = false;
            int val;
            try
            {
                game.GameId = new Guid();
                _mtckMKRDbContextt.games.Add(game);

                val = await _mtckMKRDbContextt.SaveChangesAsync();
                if (val == 1)
                {
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                flag = false;
            }
            return flag;
        }

        [HttpPut]
        public async Task<bool> PutAsync(Game gameToUpdate)
        {
            bool flag = false;
            int val;
            try
            {
                _mtckMKRDbContextt.games.Update(gameToUpdate);
                val = await _mtckMKRDbContextt.SaveChangesAsync();
                if (val == 1)
                {
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                flag = false;
            }
            return flag;
        }

        [HttpDelete]
        public async Task<bool> DeleteAsync(Guid id)
        {
            bool flag = false;
            int val;
            try
            {
                var gameToDelete = await _mtckMKRDbContextt.games.FindAsync(id);
                if (gameToDelete == null)
                {
                    flag = false;
                }
                else
                {
                    _mtckMKRDbContextt.games.Remove(gameToDelete);
                    val = await _mtckMKRDbContextt.SaveChangesAsync();
                    if (val == 1)
                    {
                        flag = true;
                    }
                }
            }
            catch (Exception ex)
            {
                flag = false;
            }
            return flag;
        }
    }
}
