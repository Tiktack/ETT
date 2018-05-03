using ETT.Logic.DTO;
using ETT.Logic.Interfaces;
using ETT.Logic.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;

namespace ETT.Tests.Users
{
    public class UsersGettingTests
    {
        [Fact]
        public void CheckExistsUsersFromSearchUsersByEmail()
        {
            // Arrange
            IUserService userService = new UserService();
            string Email = "mail";
            int limit = 5;
            // Act
            var users = userService.SearchUsersByEmail(Email, limit);
            // Assert
            Assert.NotEmpty(users);
        }

        [Fact]
        public void CheckRandomStringFromSearchUsersByEmail()
        {
            // Arrange
            IUserService userService = new UserService();
            string Email = "dkq09y789d09q2fqrq34fq3f3";
            int limit = 5;
            // Act
            var users = userService.SearchUsersByEmail(Email, limit);
            // Assert
            Assert.Empty(users);
        }

        [Fact]
        public void CheckLimitFromSearchUsersByEmail()
        {
            // Arrange
            IUserService userService = new UserService();
            string Email = "mail";
            int limit1 = 2;
            int limit2 = 3;
            // Act
            var users1 = userService.SearchUsersByEmail(Email, limit1);
            var users2 = userService.SearchUsersByEmail(Email, limit2);
            // Assert
            Assert.Equal(2, users1.Count());
            Assert.Equal(3, users2.Count());
        }
    }
}
