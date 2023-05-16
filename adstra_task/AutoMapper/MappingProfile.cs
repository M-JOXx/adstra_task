using adstra_task.Models;
using adstra_task.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace adstra_task.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, RegisterViewModel>()
                .ForMember(dest => dest.Email,source => source.MapFrom(s => s.UserName)).ReverseMap();

            CreateMap<ApplicationUser, EditUserViewModel>();//Get

            CreateMap<EditUserViewModel, ApplicationUser>();//Post

            CreateMap<ApplicationUser, UsersViewModel >()
                .ForMember(dest => dest.FullName, source => source.MapFrom(s => s.FirstName + " " + s.LastName))
                .ForMember(dest => dest.Email, source => source.MapFrom(s => s.Email))
                .ForMember(dest => dest.PhoneNumber, source => source.MapFrom(s => s.PhoneNumber)).ReverseMap();
            CreateMap<ApplicationUser, ProfileViewModel>()
                .ForMember(dest => dest.FullName, source => source.MapFrom(s => s.FirstName + " " + s.LastName));

        }
    }
}
