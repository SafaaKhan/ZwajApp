
using Microsoft.EntityFrameworkCore;
using zwajapp.API.Models;

namespace zwajapp.API.Data
{
  public class DataContext : DbContext
  {
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<WeatherForecast> WeatherForecasts { get; set; }
    public DbSet<User> Users { get; set; }
  }
}