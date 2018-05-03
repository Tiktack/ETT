using AutoMapper;
using ETT.Storage.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETT.Storage.Infrastucture
{
    public static class Mappers
    {
        private static IMapper ChangedUserToUserForUpdateMapper { get; set; }

        static Mappers()
        {
            ConfigureUserMapping();
        }

        public static void MapUserToUserForUpdate(User userSrc, User userDest)
        {
            //return ChangedUserToUserForUpdateMapper.Map<User, User>(user);
            ChangedUserToUserForUpdateMapper.Map(userSrc, userDest);
        }

        //public static IEnumerable<UserDTO> MapUserToUserDTO(IEnumerable<User> users)
        //{
        //    return UserToUserDTOMapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(users);
        //}

        private static void ConfigureUserMapping()
        {
            // User -> UserForUpdate
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, User>()
                    .ForMember(dest => dest.IMGPath, opt => opt.Condition(p => p.IMGPath != null))
                    .ForMember(dest => dest.NickName, opt => opt.MapFrom(p => p.NickName))
                    .ForMember(dest => dest.FirstName, opt => opt.MapFrom(p => p.FirstName))
                    .ForMember(dest => dest.LastName, opt => opt.MapFrom(p => p.LastName))
                    .ForMember(dest => dest.Company, opt => opt.MapFrom(p => p.Company))
                    .ForMember(dest => dest.Position, opt => opt.MapFrom(p => p.Position))
                    .ForMember(dest => dest.About, opt => opt.MapFrom(p => p.About))
                    .ForMember(dest => dest.Email, opt => opt.MapFrom(p => p.Email))
                    .ForMember(dest => dest.UserName, opt => opt.MapFrom(p => p.UserName))
                    .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(p => p.DateOfBirth))
                    .ForMember(dest => dest.NormalizedUserName, opt => opt.MapFrom(p => p.NormalizedUserName))
                    .ForMember(dest => dest.NormalizedEmail, opt => opt.MapFrom(p => p.NormalizedEmail))
                    .IgnoreAllUnmapped()
                    ;
            });
            ChangedUserToUserForUpdateMapper = config.CreateMapper();
        }
    }
}
