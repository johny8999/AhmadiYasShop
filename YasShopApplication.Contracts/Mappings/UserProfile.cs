using AutoMapper;
using YasShop.Application.Contracts.ApplicationDTO.Users;
using YasShop.Application.Contracts.PresentationDTO.ViewInputs;

namespace YasShop.Application.Contracts.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<viLogin, InpLoginByEmailPassword>();
        }
    }
}
