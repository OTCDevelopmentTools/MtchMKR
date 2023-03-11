using Microsoft.AspNetCore.Mvc;
using MtchMKRAPI.Data.Entities;
using MtchMKRAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace MtchMKRAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserPaymentInfoController : ControllerBase
    {
        private readonly MtckMKRDbContext _mtckMKRDbContextt;
        public UserPaymentInfoController(MtckMKRDbContext mtckMKRDbContext)
        {
            _mtckMKRDbContextt = mtckMKRDbContext;
        }

        [HttpPost]
        public async Task<bool> PostAsync(UsersPaymentInfo usersPaymentInfo)
        {
            bool flag = false;
            int val;
            try
            {
                if (usersPaymentInfo.UserId != null && usersPaymentInfo.TransactionId != null && usersPaymentInfo.MatchCount != null)
                {

                    usersPaymentInfo.UsersPaymentInfoId = new Guid();
                    usersPaymentInfo.CreatedDate = DateTime.Now;
                    _mtckMKRDbContextt.usersPaymentInfos.Add(usersPaymentInfo);

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

        [HttpGet]
        [Route("GetUserPaymentInfoDetails")]
        public async Task<IActionResult> GetUserPaymentInfoDetails(Guid UserId)
        {
            try  
            {
                var userPaymentInfoDetails = await _mtckMKRDbContextt.usersPaymentInfos.ToListAsync();
                return Ok(userPaymentInfoDetails);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
