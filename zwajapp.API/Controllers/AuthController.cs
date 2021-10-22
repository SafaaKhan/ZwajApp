using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using zwajapp.API.Data;
using zwajapp.API.Dto;
using zwajapp.API.Models;

namespace zwajapp.API.Controllers
{

  [AllowAnonymous]
  [Route("api/[controller]")]
  [ApiController]
  public class AuthController : ControllerBase
  {
    private readonly IAuthRepository _authRepo;


    private readonly IConfiguration _config;

    public AuthController(IAuthRepository authRepo, IConfiguration config)
    {
      _config = config;
      _authRepo = authRepo;

    }

    [HttpPost("Register")]//register
    public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
    {
      //validation
      userForRegisterDto.Username = userForRegisterDto.Username.ToLower();
      if (await _authRepo.UserExists(userForRegisterDto.Username))
      {
        return BadRequest("The user already exists");
      }
      var userToBeCreate = new User
      {
        username = userForRegisterDto.Username
      };

      var createdUser = await _authRepo.Register(userToBeCreate, userForRegisterDto.Password);
      return StatusCode(201);

    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
    {

      var userFromRepo = await _authRepo.Login(userForLoginDto.Username.ToLower(), userForLoginDto.Password);
      if (userFromRepo == null)
      {
        return Unauthorized();
      }
      //claims
      var claims = new[]{
       new Claim(ClaimTypes.NameIdentifier,userFromRepo.id.ToString()),
       new Claim(ClaimTypes.Name,userForLoginDto.Username)
      };
      //key
      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
      //credintial
      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.Now.AddDays(1),
        SigningCredentials = creds
      };
      var tokenHandler = new JwtSecurityTokenHandler();
      var token = tokenHandler.CreateToken(tokenDescriptor);
      return Ok(new
      {
        token = tokenHandler.WriteToken(token)
      });

    }

  }
}