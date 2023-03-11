using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MtchMKRAPI.Data;
using MtchMKRAPI.Data.Entities;
using MtchMKRAPI.Services;
using System.Data.Entity;
using System.Linq;

namespace MtchMKRAPI.Controllers
{


    [ApiController]
    [Route("[controller]")]
    public class MatchController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly MtckMKRDbContext _mtckMKRDbContextt;
        public MatchController(MtckMKRDbContext mtckMKRDbContext, INotificationService notificationService)
        {
            _mtckMKRDbContextt = mtckMKRDbContext;
            _notificationService = notificationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {

                var Matchs = _mtckMKRDbContextt.matches.ToList();
                return Ok(Matchs);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpGet]
        [Route("GetLocations")]
        public async Task<IActionResult> GetLocations()
        {
            try
            {

                var locs =  _mtckMKRDbContextt.locations.ToList();
                return Ok(locs);
            }
            catch (Exception)
            {

                throw;
            }

        }
        [HttpGet]
        [Route("GetMatchDetails")]
        public async Task<IActionResult> GetMatchDetails()
        {
            try
            {
                var matchDetails = await _mtckMKRDbContextt.matchDetails.ToListAsync();
                return Ok(matchDetails);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpGet]
        [Route("GetMatchDetailsByGame")]
        public async Task<IActionResult> GetMatchDetailsByGame(Guid gameID)
        {
            try
            {
                var matchDetails = await _mtckMKRDbContextt.matches.Where(p => p.GameId == gameID).ToListAsync();
                return Ok(matchDetails);
            }
            catch (Exception)
            {

                throw;
            }

        }

       


        [HttpGet]
        [Route("Showcase")]
        public async Task<IActionResult> Showcase(Guid userID)
        {
            try
            {
                var showcase =  _mtckMKRDbContextt.showCases.Where(p => p.RequestedToUser == userID && p.CreatedByUser != userID).ToList(); ;
                return Ok(showcase);
            }
            catch (Exception)
            {

                throw;
            }

        }
        [HttpGet]
        [Route("GetIsAgreedResult")] 
        public async Task<bool> GetIsAgreedResult(Guid matchId, bool isAgreed, Guid RequestedUserId)
        {
            bool flag = false;
            int val;
            try
            {

                var MatchToUpdate = _mtckMKRDbContextt.matchRequestDetails.Where(p => p.MatchId == matchId).FirstOrDefault();
                if (MatchToUpdate != null)
                {
                    MatchToUpdate.isAgreed = isAgreed;
                    _mtckMKRDbContextt.matchRequestDetails.Update(MatchToUpdate);
                    val = await _mtckMKRDbContextt.SaveChangesAsync();

                    var matchBookings = _mtckMKRDbContextt.matchBookings.Where(p => p.MatchId == matchId && p.UserId == RequestedUserId).FirstOrDefault();
                    if (matchBookings == null)
                    {
                        MatchBooking matchBooking = new MatchBooking();
                        matchBooking.MatchBookingId = new Guid();
                        matchBooking.MatchId = MatchToUpdate.MatchId;
                        matchBooking.UserId = RequestedUserId;
                        matchBooking.CreatedDate = DateTime.Now;

                        _mtckMKRDbContextt.matchBookings.Add(matchBooking);
                        val = await _mtckMKRDbContextt.SaveChangesAsync();
                    }

                    if (val > 0)
                    {
                        flag = true;
                    }
                }
            }
            catch (Exception)
            {

                throw;
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
                var MatchToDelete = await _mtckMKRDbContextt.matches.FindAsync(id);
                if (MatchToDelete == null)
                {
                    flag = false;
                }
                _mtckMKRDbContextt.matches.Remove(MatchToDelete);
                await _mtckMKRDbContextt.SaveChangesAsync();
                val = await _mtckMKRDbContextt.SaveChangesAsync();
                if (val > 0)
                {
                    flag = true;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return flag;
        }

        [HttpGet]
        [Route("SearchPlayers")]
        public async Task<IActionResult> SearchPlayers(Guid gameid,int minRating,int maxrating,int radious,string gameFrame,Guid userId)
        {
            string delimiter = "-"; 
            var results = gameFrame.Split(delimiter);
            try
            {

                var result = await _mtckMKRDbContextt.playerLists.Where(p => (p.GameId == gameid &&  p.UserId != userId)).ToListAsync();

                if(minRating > 0 || maxrating > 0)
                {
                    result = result.Where(p => p.Rating >= minRating).Where(p => p.Rating <= maxrating).ToList();
                }
                 if(radious > 0)
                {
                    result = result.Where(p => p.RadiusLocator  <= radious).ToList();
                }
                 if(gameFrame.Contains("-"))
                {

                    result = result.Where(p => p.MaxFrame >= Convert.ToInt16(results[0])).Where(p => p.MaxFrame <= 
                    Convert.ToInt16(results[1])).ToList();
                }
                else if(gameFrame.Contains("+"))
                {
                    result = result.Where(p => p.MaxFrame >= Convert.ToInt16(gameFrame.Split("+"))).ToList();
                }
                else
                {
                    result = result.Where(p => p.MaxFrame >= Convert.ToInt16(gameFrame)).ToList();
                }
                return Ok(result);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPost]
        [Route("PostAsyncMatchScoreCard")]
        public async Task<bool> PostAsyncMatchScoreCard(MatchScoreCard matchScore)
        {

            bool flag = false;
            int val;
            try
            {
                for (int i = 1; i <= matchScore.FrameNumber; i++)
                {
                    matchScore.MatchScoreCardId = new Guid();
                    matchScore.FrameNumber = i;
                }
                val = await _mtckMKRDbContextt.SaveChangesAsync();
                if (val >= 1)
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

        [HttpGet]
        [Route("SearchPlayersByDateTime")]
        public async Task<IActionResult> SearchPlayersByDateTime(Guid? gameid, int minRating, int maxrating, int radious, string? gameFrame, Guid? userId, DateTime date1)
        {
            try
            {

                var result =  _mtckMKRDbContextt.playerListWithDateTimes.Where(p => p.GameId == gameid && p.UserId != userId).ToList();

                if (minRating > 0 || maxrating > 0)
                {
                    result = result.Where(p => p.Rating >= minRating).Where(p => p.Rating <= maxrating).ToList();
                }
                if (radious > 0)
                {
                    result = result.Where(p => p.RadiusLocator <= radious).ToList();
                }
                if (gameFrame != null)
                {
                    string delimiter = "-";
                    var results = gameFrame.Split(delimiter);
                    if (gameFrame.Contains("-"))
                    {
                        result = result.Where(p => p.MaxFrame >= Convert.ToInt16(results[0])).Where(p => p.MaxFrame <=
                        Convert.ToInt16(results[1])).ToList();
                    }
                    else if (gameFrame.Contains("+"))
                    {
                        result = result.Where(p => p.MaxFrame >= Convert.ToInt16(gameFrame.Split("+"))).ToList();
                    }
                    else
                    {
                        result = result.Where(p => p.MaxFrame >= Convert.ToInt16(gameFrame)).ToList();
                    }
                }
                if (date1 != null) 
                {

                    result = result.Where(p => String.Format("yyyy-MM-dd HHH:mm',p.MatchDate") != date1.ToString()).ToList();
                }
                

                return Ok(result);
            }
            catch (Exception)
            {

                throw;
            }

        }
        [HttpGet]
        [Route("BookedMatches")]
        public async Task<IActionResult> BookedMatches(Guid userID)
        {
            try
            {
                
                 var BookedMatchDetails = await _mtckMKRDbContextt.bookedMatchDetails.Where(p => p.CreatedByUser == userID).ToListAsync();
                 return Ok(BookedMatchDetails);
            }
            catch (Exception)
            {
                throw;
            }

        }

    
        [HttpGet]
        [Route("isMatchCompletedByMatchId")]
        public bool isMatchCompletedByMatchId(Guid MatchId)
        {
            bool isMatchCompleted = false;
            try
            {
                int winnerCount = _mtckMKRDbContextt.matchScoreCards.Where(p => p.MatchId == MatchId).Count();

                var cnt = _mtckMKRDbContextt.matchScoreCards.Where(p => (p.MatchId == MatchId && p.FrameWinner.ToString() == "00000000-0000-0000-0000-000000000000" )).Count();
                if (cnt > 0)
                {
                   isMatchCompleted = false;
                }
                else if (cnt == 0)
                {
                    isMatchCompleted = true;
                }
                return isMatchCompleted;
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpGet]
        [Route("PlaylistForSearchByDate")]
        public  IActionResult PlaylistForSearchByDate(DateTime? date1, int? minRating, int? maxrating, Guid? gameID)

        {
            var date2 = date1.HasValue ? date1.Value.ToString("yyyy-MM-dd HH:mm") : "";
            try
            {

                var result = _mtckMKRDbContextt.Set<PlaylistForSearch>().FromSqlRaw("exec [dbo].[getUsersListByDateTime]  @matchdate='" + date2 +"'").ToList<PlaylistForSearch>();
                
                
                if (minRating > 0 || maxrating > 0)
                {
                    result = result.Where(p => p.Rating >= minRating).Where(p => p.Rating <= maxrating).ToList();
                }
                if (gameID != null )
                {
                  result = result.Where(p => p.GameId == gameID).ToList();
                }
                
                
                return Ok(result);
            }
            catch (Exception)
            {

                throw;
            }

        }


        [HttpGet]
        [Route("PlaylistForSearchByDateWithUserId")]
        public IActionResult PlaylistForSearchByDate(DateTime? date1, int? minRating, int? maxrating, Guid? gameID, Guid? UserId)

        {
            var date2 = date1.HasValue ? date1.Value.ToString("yyyy-MM-dd HH:mm") : "";
            try
            {

                var result = _mtckMKRDbContextt.Set<PlaylistForSearch>().FromSqlRaw("exec [dbo].[getUsersListByDateTime]  @matchdate='" + date2 + "'").ToList<PlaylistForSearch>();
                if (UserId != null)
                {
                    var user = _mtckMKRDbContextt.users.Where(x => x.UserId == UserId).FirstOrDefault();

                    if (user != null)

                        result = result.Where(p => p.UserId != UserId).ToList();
                }

                if (minRating > 0 || maxrating > 0)
                {
                    result = result.Where(p => p.Rating >= minRating).Where(p => p.Rating <= maxrating).ToList();
                }
                if (gameID != null)
                {
                    result = result.Where(p => p.GameId == gameID).ToList();
                }


                return Ok(result);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpGet]
        [Route("PendingMatches")]
        public async Task<IActionResult> PendingMatches(Guid userID)
        {
            try
            {
                var result = _mtckMKRDbContextt.Set<PendingMatchDetail>().FromSqlRaw("exec [dbo].[getPendingMatchesByUserID]  @userId='" + userID + "'").ToList<PendingMatchDetail>();
            
                // var user = _mtckMKRDbContextt.users.Where(p => p.UserId == userID).FirstOrDefault();
                //var pendingMatches = _mtckMKRDbContextt.PendingMatchDetails.Where(p => p.UserID == userID).ToList();
                //pendingMatches = pendingMatches.Where(u => u.Name != user.Name).ToList();

                return Ok(result);
            }
            catch (Exception)
            {

                throw;
            }

        }
        [HttpPost]
        //Added for New Logic to send match request to multiple users 20 JAN 23
        public async Task<bool> PostAsync(MatchCreation match)
        {
            var dateTime = match.MatchDate;
            var dt1 = String.Format("{0:f}", match.MatchDate);
            var loc = _mtckMKRDbContextt.locations.Where(p => p.LocationId == match.LocationId).FirstOrDefault();
            var locName = loc.Location;
            bool flag = false;
            int val;
            try
            {
                     match.MatchId = new Guid();
                    _mtckMKRDbContextt.matchCreations.Add(match);
                     val = await _mtckMKRDbContextt.SaveChangesAsync();
                foreach (var requserId in match.matchRequestUsers)
                {

                    MatchRequestDetail matchRequestDetail = new()
                    {
                        MatchRequestDetailID = new Guid(),
                        MatchId = match.MatchId,
                        RequestedUserId = requserId,
                        isAgreed = false,
                        CreatedDate = DateTime.Now
                    };
                    matchRequestDetail.MatchRequestDetailID = new Guid();
                    _mtckMKRDbContextt.matchRequestDetails.Add(matchRequestDetail);
                   // Added code for Push Notification on 17 Jan

                   NotificationModel notificationModel = new NotificationModel();
                   var userDeviceInfo = _mtckMKRDbContextt.userDeviceInfos.Where(x => x.UserId == requserId).FirstOrDefault();
                    if (userDeviceInfo != null)
                    {
                        notificationModel.DeviceToken = userDeviceInfo.DeviceToken;
                        notificationModel.Title = "MtchMKR Notification";
                        notificationModel.Body = "You have a Match Request from MtchMKR App on  "+ dt1 + "at "    + loc +" ";
                        var result = await _notificationService.SendNotification(notificationModel);

                    }
                }
                val = await _mtckMKRDbContextt.SaveChangesAsync();
                //
                if (val >= 1)
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

    }
}
