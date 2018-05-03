using ETT.Logic.DTO;
using ETT.Logic.Interfaces;
using ETT.Logic.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ETT.Tests.Users
{
    //public class UserServiceTest
    //{
    //    [Fact]
    //    public void AddingNewUser()
    //    {
    //        // Arrange
    //        IUserService userService = new UserService();
    //        UserDTO userDTO = new UserDTO
    //        {
    //            FirstName = "Paul",
    //            LastName = "Mitchel",
    //            DateOfBirth = DateTime.Now,
    //            NickName = "Zelda",
    //        };
    //        // Act
    //        userService.CreateUser(userDTO);
    //        // Assert
    //        Assert.NotEmpty(userService.GetUsers());
    //    }

    //    [Fact]
    //    public void CheckUsersInDB()
    //    {
    //        // Arrange
    //        IUserService userService = new UserService();
    //        // Act
    //        IEnumerable<UserDTO> userDTOs = userService.GetUsers();
    //        // Assert
    //        Assert.NotEmpty(userDTOs);
    //    }

    //    [Fact]
    //    public void GettingUser()
    //    {
    //        // Arrange            
    //        IUserService userService = new UserService();
    //        IEnumerable<UserDTO> userDTOs = userService.GetUsers();
    //        int id = (userDTOs as List<UserDTO>)[0].UserID;
    //        // Act
    //        UserDTO user = userService.GetUser(id);
    //        // Assert
    //        Assert.NotNull(user);
    //    }
    //}
}
