using ETT.Web.Models.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Localization;
using System.Reflection;

namespace ETT.Web.Util.Validators
{
    public class CustomUserValidator : IUserValidator<User>
    {
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        public CustomUserValidator(int length)
        {
            Length = length;
        }

        public int Length { get; }

        public Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user)
        {
            List<IdentityError> errors = new List<IdentityError>();

            if (user.Email.ToLower().EndsWith("@spam.com"))
            {
                errors.Add(new IdentityError
                {
                    Description = "Данный домен находится в спам-базе. Выберите другой почтовый сервис"
                });
            }
            if (user.UserName.Contains("admin"))
            {
                errors.Add(new IdentityError
                {
                   // Description =//_sharedLocalizer["NoteField"]//"Домен не должен содержать в себе 'admin'"
                });
            }

            if (user.UserName.Length < Length)
            {
                errors.Add(new IdentityError
                {
                    Description = $"Ник пользователя не должен быть короче {Length} символов"
                });
            }

            var userNames = manager.Users.Select(x => x.UserName);
            if (userNames.Contains(user.UserName))
            {
                errors.Add(new IdentityError
                {
                    Description = "Ник пользователя уже содержиться в базе"
                });
            }

            string patternForMail = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
            if (!Regex.IsMatch(user.UserName, patternForMail))
            {
                errors.Add(new IdentityError
                {
                    Description = "Некорректный email адрес"
                });
            }
            return Task.FromResult(errors.Count == 0 ?
            IdentityResult.Success : IdentityResult.Failed(errors.ToArray()));
        }
    }
}
