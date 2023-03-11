using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MtchMKRAPI.Data;
using MtchMKRAPI.Data.Entities;
using MtchMKRAPI.Services;

namespace MtchMKRAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly MtckMKRDbContext _mtckMKRDbContextt;
        private readonly INotificationService _notificationService;
        public UserController(MtckMKRDbContext mtckMKRDbContext)
        {
            _mtckMKRDbContextt = mtckMKRDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var Users = await _mtckMKRDbContextt.users.ToListAsync();
                
                return Ok(Users);
            }
            catch (Exception)
            {

                throw;
            }

        }
        [HttpGet]
        [Route("get-User-by-id")]
        public async Task<IActionResult> GetUserByIdAsync(Guid id)
        {
            try
            {
                var Users = await _mtckMKRDbContextt.users.FindAsync(id);
                return Ok(Users);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpGet]
        [Route("Login")]
        public async Task<IActionResult> Login(string userName, string password)
        {

            try
            {
                var user = await _mtckMKRDbContextt.users.Where(x => (x.UserName == userName && x.Password == password)).FirstOrDefaultAsync();
                
                return Ok(user);
            }
            catch (Exception)
            {

                throw;
            }

        }
        [HttpPost]
        [Route("SaveUserDeviceInfo")]
        public async Task<IActionResult> SaveUserDeviceInfo(UserDeviceInfo userDeviceInfo)
        {
                        
            bool flag = false;
            int val=0;
            try
            {
                // Aded on 17 jan for Sending Push Notification to save Token in Database
                if (userDeviceInfo != null)
                {
                    var userDevInfos = await _mtckMKRDbContextt.userDeviceInfos.Where(x => x.UserId == userDeviceInfo.UserId).FirstOrDefaultAsync();
                    if (userDevInfos == null)
                    {
                        
                        userDeviceInfo.UsersDeviceInfoId = new Guid();
                        var userDevInfo = _mtckMKRDbContextt.userDeviceInfos.Add(userDeviceInfo);

                    }
                    else
                    {
                        userDevInfos.DeviceId = userDeviceInfo.DeviceId;
                        userDevInfos.DeviceToken = userDeviceInfo.DeviceToken;
                        userDevInfos.DeviceType = userDeviceInfo.DeviceType;
                        _mtckMKRDbContextt.userDeviceInfos.Update(userDevInfos);
                    }
                     val = await _mtckMKRDbContextt.SaveChangesAsync();
                }
                if (val == 1)
                {
                    flag = true;
                }

            }

            catch (Exception ex)
            {
                
                flag = false;
            }
            return Ok(flag);
        }
        [HttpGet]
        [Route("isUserPresent")]
        public async Task<bool> isUserPresent(string email)
        {
            bool userPresent = false;
          
            try
            {
                if (email != null)
                {
                    var Users = await _mtckMKRDbContextt.users.Where(x => x.Email == email).FirstOrDefaultAsync();
                    if (Users != null)
                    {
                        userPresent = true;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return userPresent;
        }
        [HttpPost]
        public async Task<IActionResult> PostAsync(User user)
        {
            ObjectResults objectResults = new ObjectResults();
            
            int val;
            try
            {
                if (user.Email != null && user.UserName != null && user.Password != null)
                {

                    user.UserId = new Guid();
                    _mtckMKRDbContextt.users.Add(user);

                    val = await _mtckMKRDbContextt.SaveChangesAsync();
                    if (val == 1)
                    {
                        objectResults.message = "User Registered Suceessfully";
                    }
                }
            }

            catch (Exception ex)
            {
                objectResults.message = "User Already Exists with either UserName or Email Id ";
            }
            return Ok(objectResults);
        }

        [HttpPut]
        public async Task<bool> PutAsync(User UserToUpdate)
        {
            bool flag = false;
            int val;
            try
            {
                _mtckMKRDbContextt.users.Update(UserToUpdate);
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

        [HttpDelete]
        public async Task<bool> DeleteAsync(Guid id)
        {
            bool flag = false;
            int val;
            try
            {
                var UserToDelete = await _mtckMKRDbContextt.users.FindAsync(id);
                if (UserToDelete == null)
                {
                    flag = false;
                }
                _mtckMKRDbContextt.users.Remove(UserToDelete);
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

        [HttpGet]
        [Route("ForgotPassword")]
        public async Task<bool> ForgotPassword(string userName)
        {
            bool flag = false;

            try
            {
                var user = await _mtckMKRDbContextt.users.Where(x => (x.UserName == userName)).FirstOrDefaultAsync();
                if (user != null)
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return flag;

        }
        [HttpPut]
        [Route("ResetPassword")]
        public async Task<bool> ResetPassword(string userName, string newPassword)
        {
            bool flag = false;

            try
            {
                var user = await _mtckMKRDbContextt.users.Where(x => (x.UserName == userName)).FirstOrDefaultAsync();
                if (user != null)
                    user.Password = newPassword;
                user.ConfirmPassword = newPassword;
                _mtckMKRDbContextt.users.Update(user);

                await _mtckMKRDbContextt.SaveChangesAsync();

                flag = true;
            }
            catch (Exception)
            {

                throw;
            }
            return flag;

        }

        [HttpPost]
        [Route("UploadImage")]
        public async Task<bool> UploadImage(UserImage userImage)
        {
            var flag = false;
            try
            {

                if (userImage.ImageTitle == null || userImage.ImageData.Length == 0)
                {
                    return flag; ;
                }
                userImage.UserImageId = new Guid();
                _mtckMKRDbContextt.userImage.Add(userImage);

                var created = await _mtckMKRDbContextt.SaveChangesAsync();
                if (created > 0)
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


        [HttpPut]
        [Route("UpdateImage")]
        public async Task<bool> UpdateImage(UserImage userImage)
        {
            var flag = false;
            try
            {
                if (userImage.ImageTitle == null || userImage.ImageData.Length == 0)
                {
                    return flag; ;
                }
                var objfiles = _mtckMKRDbContextt.userImage.Where(p => p.UserId == userImage.UserId).FirstOrDefault();
                if (objfiles != null)
                {
                    _mtckMKRDbContextt.userImage.Update(objfiles);

                    var created = await _mtckMKRDbContextt.SaveChangesAsync();
                    if (created > 0)
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
        [Route("DeleteImage")]
        public async Task<bool> DeleteImage(Guid userId)
        {
            var flag = false;
            try
            {
                var profileToDelete = _mtckMKRDbContextt.userImage.Where(p => p.UserId == userId).FirstOrDefault();

                if (profileToDelete != null)
                {
                    _mtckMKRDbContextt.userImage.Remove(profileToDelete);

                    var deleted = await _mtckMKRDbContextt.SaveChangesAsync();
                    if (deleted > 0)
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

        [HttpGet]
        [Route("ShowCase")]
        public async Task<IActionResult> ShowCase(Guid userId)
        {
            try
            {

                var v = await _mtckMKRDbContextt.userProfileImages.Where(p => p.UserId == userId).FirstOrDefaultAsync();
                return Ok(v);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}

