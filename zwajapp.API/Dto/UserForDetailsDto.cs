using System;
using System.Collections.Generic;
using zwajapp.API.Models;

namespace zwajapp.API.Dto
{
  public class UserForDetailsDto
  {
    public int id { get; set; }

    public string username { get; set; }

    public string Gender { get; set; }

    public int Age { get; set; }

    public string KnownAs { get; set; }

    public DateTime Created { get; set; }

    public DateTime LastActive { get; set; }

    public string Introduction { get; set; }

    public string LookingFor { get; set; }

    public string Interests { get; set; }

    public string City { get; set; }

    public string Country { get; set; }

    public string PhotoURL { get; set; }
    public ICollection<PhotoForDetailsDto> Photos { get; set; }
  }
}