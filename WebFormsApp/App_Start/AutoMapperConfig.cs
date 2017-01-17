using AutoMapper;
using Core.BLL.DTO;
using Core.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using WebFormsApp.ViewModel;

namespace WebFormsApp.App_Start
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<Core.POCO.ApplicationUser, ContactsViewModel>().ForMember("PhoneNumber", opt => opt.MapFrom(a => a.PhoneNumber));

            Mapper.CreateMap<List<Core.POCO.Profile>, List<UserViewModel>>();
            Mapper.CreateMap<CreateUserViewModel, Core.POCO.ApplicationUser>();

            Mapper.CreateMap<ProfileViewModel, Core.POCO.Profile>().ReverseMap();
            Mapper.CreateMap<ContactsViewModel, Core.POCO.Profile>().ReverseMap();
            Mapper.CreateMap<EducationDTO, Core.POCO.Profile>().ReverseMap();
            Mapper.CreateMap<EducationViewModel, Core.POCO.Profile>().ReverseMap();
            Mapper.CreateMap<InterestsViewModel, Core.POCO.Profile>().ReverseMap();
            Mapper.CreateMap<MainViewModel, Core.POCO.Profile>().ReverseMap();
            Mapper.CreateMap<PhoneNumberViewModel, Core.POCO.ApplicationUser>().ReverseMap();
            Mapper.CreateMap<UserViewModel, Core.BLL.DTO.LoginDTO>().ReverseMap();
            Mapper.CreateMap<List<UserViewModel>, List<Core.BLL.DTO.UserDTO>>().ReverseMap();
            Mapper.CreateMap<UserViewModel, Core.BLL.DTO.UserDTO>().ReverseMap();
            Mapper.CreateMap<AlbumDTO, AlbumViewModel>().ReverseMap();
            Mapper.CreateMap<MessageDTO, MessageViewModel>();
            Mapper.CreateMap<FriendsDTO, FriendsViewModel>().ReverseMap();
            Mapper.CreateMap<object[], UserViewModel[]>();

            Mapper.CreateMap<string, ListItem>()
                                    .ForMember("Text", item => item.MapFrom(a => a))
                                    .ForMember("Value", item => item.MapFrom(a => a));

            Mapper.CreateMap<EducationDTO, EducationViewModel>()
                         .ForMember("ProfileID", item => item.MapFrom(a => a.ProfileID))
                         .ForMember("SchoolCountry", item => item.MapFrom(a => a.SchoolCountry))
                         .ForMember("SchoolTown", item => item.MapFrom(a => a.SchoolTown))
                         .ForMember("School", item => item.MapFrom(a => a.School))
                         .ForMember("StartSchoolYear", item => item.MapFrom(a => a.StartSchoolYear))
                         .ForMember("FinishSchoolYear", item => item.MapFrom(a => a.FinishSchoolYear))

                         .ForMember("SchoolCountries", item => item.Ignore())
                         .ForMember("SchoolTowns", item => item.Ignore())
                         .ForMember("StartYears", item => item.Ignore())
                         .ForMember("FinishYears", item => item.Ignore());
        }
    }
}
