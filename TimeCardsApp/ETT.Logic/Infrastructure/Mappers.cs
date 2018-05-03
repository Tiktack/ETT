using AutoMapper;
using ETT.Logic.DTO;
using ETT.Storage.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETT.Logic.Infrastructure
{
    public static class Mappers
    {
        private static IMapper UserToUserDTOMapper { get; set; }
        private static IMapper UserDTOToUserMapper { get; set; }

        static Mappers()
        {
            ConfigureUserMapping();
        }

        public static UserDTO MapUserToUserDTO(User user)
        {
            return UserToUserDTOMapper.Map<User, UserDTO>(user);
        }

        public static User MapUserDTOToUser(UserDTO user)
        {
            return UserDTOToUserMapper.Map<UserDTO, User>(user);
        }

        public static IEnumerable<UserDTO> MapUserToUserDTO(IEnumerable<User> users)
        {
            return UserToUserDTOMapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(users);
        }

        private static void ConfigureUserMapping()
        {
            // User -> UserDTO
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDTO>()
                    .ForMember(dest => dest.UserID, map => map.MapFrom(src => src.Id))
                    .ForMember(x =>x.PathOfAvatarImage , map => map.MapFrom(src => src.IMGPath));
            });
            UserToUserDTOMapper = config.CreateMapper();

            // UserDTO -> User
            config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserDTO, User>()
                    .ForMember(x => x.Id, map => map.MapFrom(src => src.UserID))
                    .ForMember(x => x.IMGPath, map => map.MapFrom(src => src.PathOfAvatarImage))
                    .ForMember(x => x.Email, map => map.MapFrom(src => src.Email))
                    .ForMember(x => x.UserName, map => map.MapFrom(src => src.Email))
                    .ForMember(x => x.NormalizedEmail, map => map.MapFrom(src => src.Email.ToUpper()))
                    .ForMember(x => x.NormalizedUserName, map => map.MapFrom(src => src.Email.ToUpper()))
                    ;
            });
            UserDTOToUserMapper = config.CreateMapper();
        }
    }
}
