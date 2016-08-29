using AutoMapper;
using Core.BLL.DTO;
using WebFormsApp.ViewModel;
using System.Collections.Generic;
using POCO = Core.POCO;

namespace WebFormsApp.App_Start
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<POCO.ApplicationUser, ContactsViewModel>().ForMember("PhoneNumber", opt => opt.MapFrom(a => a.PhoneNumber));


            Mapper.CreateMap<object[], UserViewModel[]>();
            Mapper.CreateMap<List<POCO.Profile>, List<UserViewModel>>();
            Mapper.CreateMap<RegistrationViewModel, POCO.ApplicationUser>();

            Mapper.CreateMap<ProfileViewModel, POCO.Profile>().ReverseMap();
            Mapper.CreateMap<ContactsViewModel, POCO.Profile>().ReverseMap();
            //Mapper.CreateMap<EducationViewModel, POCO.Profile>().ReverseMap();
            Mapper.CreateMap<InterestsViewModel, POCO.Profile>().ReverseMap();
            Mapper.CreateMap<MainViewModel, POCO.Profile>().ReverseMap();
            Mapper.CreateMap<PhoneNumberViewModel, POCO.ApplicationUser>().ReverseMap();
            Mapper.CreateMap<UserViewModel,  Core.BLL.DTO.LoginDTO>().ReverseMap();
            Mapper.CreateMap<List<UserViewModel>, List< Core.BLL.DTO.UserDTO>>().ReverseMap();
            Mapper.CreateMap<UserViewModel,  Core.BLL.DTO.UserDTO>().ReverseMap();

            Mapper.CreateMap<MessageDTO, MessagesViewModel>();


        }
    }
}
