using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MtchMKRAPI.Data;
using MtchMKRAPI.Data.Entities;


namespace MtchMKRAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MatchBookingController : ControllerBase
    {
        private readonly MtckMKRDbContext _mtckMKRDbContextt;
        public MatchBookingController(MtckMKRDbContext mtckMKRDbContext)
        {
            _mtckMKRDbContextt = mtckMKRDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var Matchs = await _mtckMKRDbContextt.matchBookings.ToListAsync();
                return Ok(Matchs);
            }
            catch (Exception)
            {

                throw;
            }
           
        }
        //[HttpGet]
        //[Route("get-MatchBooking-by-id")]
        //public async Task<IActionResult> GetMatchByIdAsync(Guid id)
        //{
        //    try
        //    {
        //        var MatchBooking = await _mtckMKRDbContextt.matchBookings.FindAsync(id);
        //        return Ok(MatchBooking);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
            
        //}

        [HttpPost]
        public async Task<ObjectResults> PostAsync(Guid matchId ,Guid UserId)
        {
            
            ObjectResults objectResults = new ObjectResults();
            
            int val;
            try
            {
                               
                var plyerCount = _mtckMKRDbContextt.matchBookings.Where(x => x.MatchId == matchId).GroupBy(x => x.MatchId)
                      .Select(g => g.Count());
                           
                int noOfPlayer = plyerCount.FirstOrDefault();
                           
                 if (noOfPlayer < 2)
                {
                    MatchBooking matchBooking = new MatchBooking();
                    matchBooking.MatchBookingId = new Guid();
                    matchBooking.MatchId = matchId;
                    matchBooking.UserId = UserId;
                    
                    matchBooking.CreatedDate = DateTime.Now;
                    _mtckMKRDbContextt.matchBookings.Add(matchBooking);
                    val = await _mtckMKRDbContextt.SaveChangesAsync();
                    objectResults.status = true;
                    objectResults.message = "You have successfully booked the Match";
                }
                
                else
                {
                    objectResults.status = false;
                    objectResults.message = "This match have been already booked by two players";
                }
            }
            catch (Exception)
            {
                objectResults.status = false;
                objectResults.message = "You have already booked this match";
                throw;
            }
            return objectResults; 
        }

        [HttpPut]
        public async Task<bool> PutAsync(MatchBooking MatchToUpdate)
        {
            bool flag = false;
            int val;
            try
            {
                _mtckMKRDbContextt.matchBookings.Update(MatchToUpdate);
                val = await _mtckMKRDbContextt.SaveChangesAsync();
                if (val > 0)
                {
                    flag = true;
                }
            }
            catch (Exception)
            {
                flag = false;
                throw;
                
            }
            return flag;
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                var MatchToDelete = await _mtckMKRDbContextt.matchBookings.FindAsync(id);
                if (MatchToDelete == null)
                {
                    return NotFound();
                }
                _mtckMKRDbContextt.matchBookings.Remove(MatchToDelete);
                await _mtckMKRDbContextt.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        
    }
}

