using chit_chat_api.DB_Data;
using chit_chat_api.Models;
using chit_chat_api.Models.Model_DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace chit_chat_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagement : ControllerBase
    {
        private readonly _dbContext _dbContext;
        public UserManagement(_dbContext context)
        {
            _dbContext = context;
        }
        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser([FromBody] UserDto userdto)
        {
            if (userdto == null)
            {
                return BadRequest("Please fill the required fields.");
            }
            var user = _dbContext.Users.Where(x => x.user_email == userdto.user_email).FirstOrDefault();
            if (user != null)
            {
                return BadRequest("User already exist");
            }
            var newuser = new User
            {
                user_name = userdto.user_name,
                user_email = userdto.user_email,
                user_password = userdto.user_password,
                created_at = DateTime.Now,
            };
            _dbContext.Users.Add(newuser);
            await _dbContext.SaveChangesAsync();
            return Ok(new
            {
                message = "User rigister successfully.",
                user = new
                {
                    newuser.user_id,
                    newuser.user_name,
                    newuser.user_email,
                    newuser.created_at
                }
            });
        }
        [HttpPost("UploadProfile/{user_id}")]
        public async Task<IActionResult> UploadProfile(int user_id, [FromBody] Upload_User_Proile_Dto profile_dto)
        {
            if (profile_dto == null)
            {
                return NotFound();
            }
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.user_id == user_id);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var image = await _dbContext.ProfileImages.FirstOrDefaultAsync(x => x.user_id == user_id);
            if (image != null)
            {
                image.image_url = profile_dto.Profile_image;
                image.created_at = DateTime.Now;
            }
            else
            {
                var uploadprofile = new User_Profile_Image
                {
                    image_url = profile_dto.Profile_image,
                    created_at = DateTime.Now,
                    user_id = user_id,
                };
                _dbContext.Add(uploadprofile);
            }

            await _dbContext.SaveChangesAsync();
            return Ok(new
            {
                message = "User profile uploaded successfully.",
                user = new
                {
                    user_id = user_id,
                }
            });
        }

        [HttpPost("LoginUser")]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserDto logindto)

        {
            if (logindto == null || string.IsNullOrWhiteSpace(logindto.user_email) || string.IsNullOrWhiteSpace(logindto.password))
            {
                return BadRequest("Email and password are required.");
            }
            var user = await _dbContext.Users.FirstOrDefaultAsync(n => n.user_email == logindto.user_email);
            if (user == null)
            {
                return Unauthorized("User not Registered.");
            }
            if (user.user_password != logindto.password)
            {
                return NotFound("Incorrect password");
            }
            return Ok(
                new
                {
                    message = "User Logged in successfully.",
                    user = user.user_name,
                });
        }
        [HttpGet("Users")]
        public async Task<IActionResult> Users()
        {
            var all_users = await _dbContext.Users
           .Include(u => u.User_Profile_Image)
           .Select(u => new
           {
               u.user_id,
               u.user_name,
               u.user_email,
               u.created_at,
               profile_image = u.User_Profile_Image != null
                   ? new
                   {
                       u.User_Profile_Image.user_id,
                       u.User_Profile_Image.image_url,
                       u.User_Profile_Image.created_at
                   }
                   : null
           })
           .ToListAsync();
            if (all_users == null)
            {
                return NotFound("User not found.");
            }
            return Ok(all_users);
        }

    }
}
