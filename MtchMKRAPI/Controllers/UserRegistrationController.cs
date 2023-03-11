using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MtchMKRAPI.Data;
using MtchMKRAPI.Data.Entities;

namespace MtchMKRAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserRegistrationController : ControllerBase
    {
        private readonly MtckMKRDbContext _mtckMKRDbContextt;

        public UserRegistrationController(MtckMKRDbContext mtckMKRDbContext)
        {
            _mtckMKRDbContextt = mtckMKRDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var Users = await _mtckMKRDbContextt.userRegistration.ToListAsync();

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
                var Users = await _mtckMKRDbContextt.userRegistration.FindAsync(id);
                return Ok(Users);
            }
            catch (Exception)
            {

                throw;
            }

        }
        [HttpPost]
        public async Task<IActionResult> PostAsync(UserRegistration UserRegistration)
        {
            ObjectResults objectResults = new ObjectResults();
            int val;
            try
            {
                if (UserRegistration.Email != null && UserRegistration.UserName != null && UserRegistration.Password != null)
                {

                    UserRegistration.UserRegistrationId = new Guid();
                    _mtckMKRDbContextt.userRegistration.Add(UserRegistration);

                    val = await _mtckMKRDbContextt.SaveChangesAsync();
                    if (val == 1)
                    {
                        objectResults.status = true;
                        objectResults.message = "User Registered Successfully ";
                        
                    }
                }
            }

            catch (Exception ex)
            {
                objectResults.status = false;
                objectResults.message = "User Already Registered";
                
            }
            return Ok(objectResults);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(UserRegistration UserToUpdate)
        {
            ObjectResults obj = new ObjectResults();
            int val;
            try
            {
                _mtckMKRDbContextt.userRegistration.Update(UserToUpdate);
                val = await _mtckMKRDbContextt.SaveChangesAsync();
                if (val == 1)
                {
                    obj.status = true;
                    obj.message = "User Updated Successfully ";

                }
                
            }
            catch (Exception)
            {
                obj.status = false;
                obj.message = "UserName or Email Already Exists";
            }
            return Ok(obj);
        }

        [HttpDelete]
        public async Task<bool> DeleteAsync(Guid id)
        {
            bool flag = false;
            int val;
            try
            {
                var UserToDelete = await _mtckMKRDbContextt.userRegistration.FindAsync(id);
                if (UserToDelete == null)
                {
                    flag = false;
                }
                _mtckMKRDbContextt.userRegistration.Remove(UserToDelete);
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
    }
}