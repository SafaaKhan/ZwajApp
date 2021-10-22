using System;

namespace zwajapp.API.Dto
{
  public class PhotoForDetailsDto
  {
    public int Id { get; set; }
    public string Description { get; set; }
    public string URL { get; set; }
    public DateTime DateAdded { get; set; }
    public bool IsMain { get; set; }
  }
}