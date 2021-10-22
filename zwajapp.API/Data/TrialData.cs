using System.Collections.Generic;
using Newtonsoft.Json;
using zwajapp.API.Models;

namespace zwajapp.API.Data
{
  public class TrialData
  {
    private readonly DataContext _db;
    public TrialData(DataContext db)
    {
      _db = db;
    }

    public void TrialUsers()
    {
      var usersData = System.IO.File.ReadAllText("Data/UserTrialData.json");
      var users = JsonConvert.DeserializeObject<List<User>>(usersData);
      foreach (var user in users)
      {
        byte[] passwordHash, passwordSalt;
        CreatePasswordHash("password", out passwordSalt, out passwordHash);
        user.passwordHash = passwordHash;
        user.passwordSalt = passwordSalt;
        user.username = user.username.ToLower();
        _db.Add(user);
      }
      _db.SaveChanges();
    }

    private void CreatePasswordHash(string password, out byte[] passwordSalt, out byte[] passwordHash)
    {
      using (var hmac = new System.Security.Cryptography.HMACSHA512())
      {
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
      };
    }
  }
}