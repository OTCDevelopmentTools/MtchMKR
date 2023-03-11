using Microsoft.AspNetCore.Mvc;
using MtchMKRAPI.Data.Entities;
using MtchMKRAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace MtchMKRAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MatchScoreCardController : ControllerBase
    {

        private readonly MtckMKRDbContext _mtckMKRDbContextt;
        public MatchScoreCardController(MtckMKRDbContext mtckMKRDbContext)
        {
            _mtckMKRDbContextt = mtckMKRDbContext;
        }

        [HttpPost]
        public async Task<bool> PostAsync(Guid matchId )
        {

            bool flag = false;
            int val;
            try
            {
                var matchDetails = _mtckMKRDbContextt.matches.Where(p => p.MatchId == matchId).FirstOrDefault();
                var game = _mtckMKRDbContextt.games.Where(p => p.GameId == matchDetails.GameId).FirstOrDefault();
                var matchScoreCardData = _mtckMKRDbContextt.matchScoreCards.Where(p => p.MatchId == matchId).ToList();
                List<MatchScoreCard> matchScoreList = new List<MatchScoreCard>();
                MatchScoreCard matchScore = new MatchScoreCard();

                if (matchScoreCardData.Count == 0)
                {

                    for (int i = 1; i <= game.MaxFrame; i++)
                    {
                        matchScore = new MatchScoreCard();
                        matchScore.MatchId = matchId;
                        matchScore.MatchScoreCardId = new Guid();
                        matchScore.FrameNumber = i;
                        matchScore.FrameTotal = game.MaxFrame; 
                        matchScore.CreatedDate = DateTime.Now;
                        matchScoreList.Add(matchScore);
                    }
                    _mtckMKRDbContextt.matchScoreCards.AddRange(matchScoreList);
                    val = await _mtckMKRDbContextt.SaveChangesAsync();
                    if (val >= 1)
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

        [HttpPut]
        public async Task<bool> Put(Guid matchId, Guid FrameWinner, int FrameNumber, bool ConfirmedbyPlayer)
        {

            bool flag = false;
            int val;
            try
            {
                var matchScoreCards = _mtckMKRDbContextt.matchScoreCards.Where(p => (p.MatchId == matchId && p.FrameNumber == FrameNumber)).FirstOrDefault();
                var matchBooking = _mtckMKRDbContextt.matchBookings.Where(p => (p.MatchId == matchId)).ToList();
                var existing = matchBooking.SingleOrDefault(c => c.UserId == FrameWinner);

                if (matchScoreCards != null && existing != null)
                {
                    matchScoreCards.FrameWinner = FrameWinner;
                    matchScoreCards.ConfirmedByPlayer = ConfirmedbyPlayer;

                    _mtckMKRDbContextt.matchScoreCards.Update(matchScoreCards);
                    val = await _mtckMKRDbContextt.SaveChangesAsync();
                    if (val >= 1)
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

        [HttpGet]
        [Route("GetMatchScoreByMatchId")]
        public  IActionResult GetMatchScoreByMatchId(Guid matchId)
        {
            try
            {

                var matchDetails =  _mtckMKRDbContextt.matchScoreCardDetails.FromSqlRaw("Select u.UserId, u.Name,m.GameId As GameId, g.Name as GameName,msc.FrameTotal as GameFrame, " +
                    " Place,ImageData,Count(Framewinner) FrameWinners, Rating " +
                    " from Users u Left Join UserImage ui On  u.UserId = ui.UserId INNER JOIN MatchScoreCard msc on  u.UserId = msc.FrameWinner " +
                    " INNER JOIN match m on m.MatchID = msc.MatchId inner JOIN dbo.Profile ON dbo.Profile.UserId = u.UserId " +
                    " INNER JoIn Game g ON m.GameId = g.Gameid  WHERE  FrameWinner <> '00000000-0000-0000-0000-000000000000'" +
                    " AND msc.MatchId ='" + matchId + "' Group by g.Name, ImageData, Framewinner, Rating, u.UserId, msc.FrameTotal, u.Name, m.GameId, Place, g.MaxFrame").ToList();
                    return Ok(matchDetails);
            }
            catch (Exception)
            {
                
                throw;
            }

        }

        [HttpGet]
        [Route("GetFrameWinnerCountByUserId")]
        public  int GetFrameWinnerCountByUserId(Guid userId)
        {
            try
            {
                int winnerCount = _mtckMKRDbContextt.matchScoreCards.Where(p => p.FrameWinner == userId).Count();
              
                return winnerCount;
            }
            catch (Exception)
            {
                
                throw;
            }

        }
    }
}

