using AutoMapper;
using ETT.Logic.DTO;
using ETT.Web.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETT.Web.Infrastructure
{
    public static class Mappers
    {
        private static IMapper UserToUserUpdateVMMapper { get; set; }
        private static IMapper UserUpdateVMToUserDTOMapper { get; set; }
        private static IMapper UserToEditUserVMMapper { get; set; }
        private static IMapper EditUserVMToUserDTOMapper { get; set; }

        static Mappers()
        {
            ConfigureUserMapping();
        }

        public static UserUpdateViewModel MapUserToUserUpdateVM(User user)
        {
            return UserToUserUpdateVMMapper.Map<User, UserUpdateViewModel>(user);
        }

        public static UserDTO MapUserUpdateVMToUserDTO(UserUpdateViewModel user)
        {
            return UserUpdateVMToUserDTOMapper.Map<UserUpdateViewModel, UserDTO>(user);
        }

        public static EditUserViewModel MapUserToUserEditVM(User user)
        {
            return UserToEditUserVMMapper.Map<User, EditUserViewModel>(user);
        }

        public static UserDTO MapUserEditVMToUserDTOEditVM(EditUserViewModel user)
        {
            return EditUserVMToUserDTOMapper.Map<EditUserViewModel, UserDTO>(user);
        }

        //public static IEnumerable<UserDTO> MapUserToUserDTO(IEnumerable<User> users)
        //{
        //    return UserToUserDTOMapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(users);
        //}

        private static void ConfigureUserMapping()
        {
            // User -> 
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserUpdateViewModel>()
                    .ForMember(dest => dest.UserId, map => map.MapFrom(src => src.Id))
                   ;
            });
            UserToUserUpdateVMMapper = config.CreateMapper();

            // 
            config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserUpdateViewModel, UserDTO>()
                    .ForMember(dest => dest.UserID, opt => opt.MapFrom(src => src.UserId))
                    .ForMember(dest => dest.PathOfAvatarImage, map => map.Ignore())
                   ;
            });
            UserUpdateVMToUserDTOMapper = config.CreateMapper();

            // 
            config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, EditUserViewModel>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                   ;
            });
            UserToEditUserVMMapper = config.CreateMapper();

            // 
            config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EditUserViewModel, UserDTO>()
                    .ForMember(dest => dest.UserID, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.PathOfAvatarImage, map => map.Ignore())
                   ;
            });
            EditUserVMToUserDTOMapper = config.CreateMapper();
        }
    }
}
