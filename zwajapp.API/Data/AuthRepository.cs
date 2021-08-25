using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using zwajapp.API.Models;

namespace zwajapp.API.Data
{
  public class AuthRepository : IAuthRepository
  {
    private readonly DataContext _db;
    public AuthRepository(DataContext db)
    {
      _db = db;
    }
    public async Task<User> Login(string username, string password)
    {
      var user = await _db.Users.FirstOrDefaultAsync(x => x.username == username);
      if (user == null)
      {
        return null;
      }
      if (!VerifyPasswordHash(password, user.passwordHash, user.passwordSalt))
      {
        return null;
      }
      return user;
    }

    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
      using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
      {
        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        for (int i = 0; i < computedHash.Length; i++)
        {
          if (computedHash[i] != passwordHash[i])
          {
            return false;
          }
        }
        return true;
      };
    }

    public async Task<User> Register(User user, string password)
    {
      byte[] passwordSalt, passwordHash;
      CreatePasswordHash(password, out passwordSalt, out passwordHash);
      user.passwordSalt = passwordSalt;
      user.passwordHash = passwordHash;
      await _db.Users.AddAsync(user);
      _db.SaveChanges();
      return user;
    }

    private void CreatePasswordHash(string password, out byte[] passwordSalt, out byte[] passwordHash)
    {
      using (var hmac = new System.Security.Cryptography.HMACSHA512())
      {
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
      };
    }

    public async Task<bool> UserExists(string username)
    {
      if (await _db.Users.AnyAsync(x => x.username == username))
      {
        return true;
      }
      return false;
    }
  }
}