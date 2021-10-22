using System.Collections.Generic;
using System.Threading.Tasks;
using zwajapp.API.Models;

namespace zwajapp.API.Data
{
  public interface IZwajRepository
  {
    //add
    void Add<T>(T entity) where T : class;
    //delete
    void Delete<T>(T entity) where T : class;
    //update
    Task<bool> SaveAll();
    //get a list of users 
    Task<IEnumerable<User>> GetUsers();
    //get an user
    Task<User> GetUser(int id);
  }
}