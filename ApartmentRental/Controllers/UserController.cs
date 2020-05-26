using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApartmentRental.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using ApartmentRental.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace ApartmentRental.Controllers
{
    [Authorize(Roles = Role.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApartmentContext _context;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;

        public UserController(ApartmentContext context, 
            IOptions<AppSettings> appSettings,
            IMapper mapper)
        {
            _context = context;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]AuthenticateModel model)
        {
            var user = Authenticate(model.Username, model.Password);

            if (user == null)
                return await Task.FromResult(BadRequest(new { message = "Username or password is incorrect" }));

            return await Task.FromResult(Ok(user));
        }

        private User Authenticate(string username, string password)
        {
            var user = _context.Users.SingleOrDefault(x => x.Username == username && x.Password == password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user.WithoutPassword();
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserViewModel>>> GetUsers()
        {
            return await _mapper.ProjectTo<UserViewModel>(_context.Users).ToListAsync();
        }

        [AllowAnonymous]
        [HttpGet("realtor")]
        public async Task<ActionResult<IEnumerable<UserViewModel>>> GetRealtors()
        {
            return await _mapper
                .ProjectTo<UserViewModel>(
                    _context.Users.Where(x => x.Role == Role.Realtor))
                .ToListAsync();
        }

        [AllowAnonymous]
        [HttpGet("current")]
        public UserViewModel GetCurrentUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.Name);

            return  _mapper.Map<UserViewModel>(_context.Users.Find(Int32.Parse(userId)));
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public ActionResult<UserViewModel> GetUser(int id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            return _mapper.Map<UserViewModel>(user);
        }

        // PUT: api/User/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/User
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<UserViewModel>> PostUser(User user)
        {
            return await AddUser(user);
        }

        // POST: api/User/signup
        [AllowAnonymous]
        [HttpPost("signup")]
        public async Task<ActionResult<UserViewModel>> SignupUser(User user)
        {
            user.Role = Role.Client;
            user.Token = string.Empty;

            return await AddUser(user);
        }

        private async Task<ActionResult<UserViewModel>> AddUser(User user)
        {
            if(_context.Users.FirstOrDefault(u => u.Username == user.Username) != null)
            {
                return BadRequest(new { message = "Username already exists."});
            }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", 
                new { id = user.Id }, 
                _mapper.Map<UserViewModel>(user));
        }


        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserViewModel>> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserViewModel>(user);
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
