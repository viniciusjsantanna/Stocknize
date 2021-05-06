using AutoMapper;
using Stocknize.Domain.Entities;
using Stocknize.Domain.Models.User;

namespace Stocknize.Domain.Automapper
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<UserInputModel, User>()
                .ForPath(e => e.Credentials.Login, opt => opt.MapFrom(e => e.Login))
                .ForPath(e => e.Credentials.Password, opt => opt.MapFrom(e => e.Password));

            CreateMap<User, UserLoggedOutputModel>()
                .ForMember(e => e.Login, opt => opt.MapFrom(e => e.Credentials.Login))
                .ForMember(e => e.Name, opt => opt.MapFrom(e => e.Name))
                .ForAllOtherMembers(e => e.Ignore());

            CreateMap<User, UserOutputModel>()
                .ForMember(e => e.CompanyName, opt => opt.MapFrom(e => e.Company.Name));
        }
    }
}
