using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MtchMKRAPI.Data;
using MtchMKRAPI.Data.Entities;

namespace MtchMKRAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly MtckMKRDbContext _mtckMKRDbContextt;
        private readonly IWebHostEnvironment _environment;
        public ProfileController(MtckMKRDbContext mtckMKRDbContext, IWebHostEnvironment hostEnvironment)
        {
            _mtckMKRDbContextt = mtckMKRDbContext;
            _environment = hostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var profiles = await _mtckMKRDbContextt.profiles.ToListAsync();
                return Ok(profiles);
            }
            catch (Exception)
            {

                throw;
            }

        }
       
        [HttpPost]
        public async Task<bool> PostAsync(Profile profile)
        {
            bool flag = false;
            int val;
            try
            {
                profile.ProfileId = new Guid();
                _mtckMKRDbContextt.profiles.Add(profile);

                val = await _mtckMKRDbContextt.SaveChangesAsync();
                var lastProfileId = profile.ProfileId;
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
        [Route("UpdateProfile")]
        public async Task<bool> UpdateProfile(UserProfileImage userProfileImage)
        {
            bool flag = false;
            int val;
            try
            {
                var profiletoUpdate = _mtckMKRDbContextt.profiles.Where(p => p.UserId == userProfileImage.UserId).FirstOrDefault();
                if (profiletoUpdate != null)
                {
                    profiletoUpdate.RadiusLocator = userProfileImage.RadiusLocator;
                    profiletoUpdate.Preferredgames = userProfileImage.Preferredgames;
                    profiletoUpdate.Rating = userProfileImage.Rating;
                    profiletoUpdate.PlaysAgainst = userProfileImage.PlaysAgainst;
                    profiletoUpdate.RegularAvailability = userProfileImage.RegularAvailability;
                    profiletoUpdate.NotificationMethod = userProfileImage.NotificationMethod;
                    profiletoUpdate.Place = userProfileImage.Place;
                    profiletoUpdate.GameId = (Guid)userProfileImage.GameId;
                    _mtckMKRDbContextt.profiles.Update(profiletoUpdate);

                }
                else
                {
                    profiletoUpdate = new Profile();
                    profiletoUpdate.RadiusLocator = userProfileImage.RadiusLocator;
                    profiletoUpdate.Preferredgames = userProfileImage.Preferredgames;
                    profiletoUpdate.Rating = userProfileImage.Rating;
                    profiletoUpdate.PlaysAgainst = userProfileImage.PlaysAgainst;
                    profiletoUpdate.RegularAvailability = userProfileImage.RegularAvailability;
                    profiletoUpdate.NotificationMethod = userProfileImage.NotificationMethod;
                    profiletoUpdate.Place = userProfileImage.Place;
                    profiletoUpdate.UserId = userProfileImage.UserId;
                    profiletoUpdate.ProfileId = new Guid();
                    profiletoUpdate.CreatedDate = DateTime.Now;
                    profiletoUpdate.GameId = (Guid)userProfileImage.GameId;
                    _mtckMKRDbContextt.profiles.Add(profiletoUpdate);

                }
                var userimgae = _mtckMKRDbContextt.userImage.Where(p => p.UserId == userProfileImage.UserId).FirstOrDefault();
                if (userimgae != null)
                {
                    if (userProfileImage.ImageTitle != null || userProfileImage.ImageData.Length > 0)
                    {
                        userimgae.ImageTitle = userProfileImage.ImageTitle;
                        userimgae.ImageData = userProfileImage.ImageData;
                        userimgae.ImageExtension = userProfileImage.ImageExtension;
                        _mtckMKRDbContextt.userImage.Update(userimgae);
                    }
                    
                }
                else
                {
                    userimgae = new UserImage();
                    if (userProfileImage.ImageTitle != null || userProfileImage.ImageData.Length > 0)
                    {
                        userimgae.ImageTitle = userProfileImage.ImageTitle;
                        userimgae.ImageData = userProfileImage.ImageData;
                        userimgae.ImageExtension = userProfileImage.ImageExtension;
                        userimgae.UserId = userProfileImage.UserId;   
                        userimgae.UserImageId = new Guid();
                        userimgae.CreatedDate = DateTime.Now;
                        _mtckMKRDbContextt.userImage.Add(userimgae);
                    }
                }
                var user = await _mtckMKRDbContextt.users.Where(x => (x.UserId == userProfileImage.UserId)).FirstOrDefaultAsync();
                if (user != null)
                {
                    user.Name = userProfileImage.Name;
                    user.Email = userProfileImage.Email;
                    user.Telephone = userProfileImage.Telephone;
                    user.UserName=  userProfileImage.UserName;
                    _mtckMKRDbContextt.users.Update(user);
                }
                
               
               
                val = await _mtckMKRDbContextt.SaveChangesAsync();
                if (val > 0 )
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
        public async Task<bool> DeleteAsync(Guid id)
        {
            bool flag = false;
            int val;
            try
            {
                var profileToDelete = await _mtckMKRDbContextt.profiles.FindAsync(id);
                if (profileToDelete == null)
                {
                    return flag;
                }
                _mtckMKRDbContextt.profiles.Remove(profileToDelete);
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
        [Route("GetProfileByUserId")]
        public async Task<IActionResult> GetProfileByUserId(Guid userId)
        {
            try
            {

                var profile = await _mtckMKRDbContextt.userProfileImages.Where(x => x.UserId == userId).FirstOrDefaultAsync();

                return Ok(profile);
            }
            catch (Exception)
            {

                throw;
            }

        }


    }

}


