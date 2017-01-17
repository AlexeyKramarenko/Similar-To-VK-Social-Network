using AutoMapper;
using System.Linq;
using Core.BLL.DTO;
using MvcApp.ViewModel;
using System.Collections.Generic;
using POCO = Core.POCO;
using System.Web.Mvc;

namespace MvcApp.App_Start
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<POCO.ApplicationUser, ContactsViewModel>()
                  .ForMember("PhoneNumber", opt => opt.MapFrom(a => a.PhoneNumber));


            Mapper.CreateMap<object[], UserViewModel[]>();
            Mapper.CreateMap<AlbumDTO, AlbumViewModel>();
            Mapper.CreateMap<List<POCO.Profile>, List<UserViewModel>>();
            Mapper.CreateMap<RegistrationViewModel, POCO.ApplicationUser>();

            Mapper.CreateMap<ProfileViewModel, POCO.Profile>().ReverseMap();
            Mapper.CreateMap<ContactsViewModel, POCO.Profile>().ReverseMap();
            Mapper.CreateMap<EducationViewModel, POCO.Profile>().ReverseMap();
            Mapper.CreateMap<EducationDTO, POCO.Profile>().ReverseMap();
            Mapper.CreateMap<InterestsViewModel, POCO.Profile>().ReverseMap();
            Mapper.CreateMap<MainViewModel, POCO.Profile>().ReverseMap();
            Mapper.CreateMap<PhoneNumberViewModel, POCO.ApplicationUser>().ReverseMap();
            Mapper.CreateMap<UserViewModel, Core.BLL.DTO.LoginDTO>().ReverseMap();
            Mapper.CreateMap<List<UserViewModel>, List<Core.BLL.DTO.UserDTO>>().ReverseMap();
            Mapper.CreateMap<UserViewModel, Core.BLL.DTO.UserDTO>().ReverseMap();
            Mapper.CreateMap<FriendsDTO, FriendsViewModel>().ReverseMap();
            Mapper.CreateMap<MessageDTO, MessagesViewModel>();

            Mapper.CreateMap<DialogDTO, DialogViewModel>();
            
            Mapper.CreateMap<EducationDTO, EducationViewModel>()
                                    .ForMember("ProfileID", item => item.MapFrom(a => a.ProfileID))
                                    .ForMember("SchoolCountry", item => item.MapFrom(a => a.SchoolCountry))
                                    .ForMember("SchoolTown", item => item.MapFrom(a => a.SchoolTown))
                                    .ForMember("School", item => item.MapFrom(a => a.School))
                                    .ForMember("StartSchoolYear", item => item.MapFrom(a => a.StartSchoolYear))
                                    .ForMember("FinishSchoolYear", item => item.MapFrom(a => a.FinishSchoolYear));
            
            Mapper.CreateMap<string, SelectListItem>()
                                    .ForMember("Text", item => item.MapFrom(a => a))
                                    .ForMember("Value", item => item.MapFrom(a => a));

            Mapper.CreateMap<int, SelectListItem>()
                                    .ForMember("Text", item => item.MapFrom(a => a))
                                    .ForMember("Value", item => item.MapFrom(a => a));
        }


    }
}
