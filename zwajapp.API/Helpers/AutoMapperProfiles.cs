using System.Linq;
using AutoMapper;
using zwajapp.API.Dto;
using zwajapp.API.Models;

namespace zwajapp.API.Helpers
{
  public class AutoMapperProfiles : Profile
  {
    public AutoMapperProfiles()
    {
      CreateMap<User, UserForListDto>().ForMember(dest => dest.PhotoURL, opt =>
      {
        opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).URL);
      }).ForMember(dest => dest.Age, opt =>
      {
        opt.MapFrom(src => src.DateOfBirth.CalculateAge());
      });



      CreateMap<User, UserForDetailsDto>()
      .ForMember(dest => dest.PhotoURL, opt =>
      {
        opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).URL);
      }).ForMember(dest => dest.Age, opt =>
      {
        opt.MapFrom(src => src.DateOfBirth.CalculateAge());
      });


      CreateMap<Photo, PhotoForDetailsDto>();
    }
  }
}