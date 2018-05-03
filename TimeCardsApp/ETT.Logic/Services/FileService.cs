using ETT.Logic.DTO;
using ETT.Logic.Interfaces;
using ETT.Logic.Managers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Security.Cryptography;

namespace ETT.Logic.Services
{
    public class FileService : IFileService
    {
        public string GetAvatarPath(int? userId)
        {
            using (AccountManager usersManager = new AccountManager())
            {
                UserDTO userDTO = usersManager.GetUser(userId);

                string path;
                if (userDTO != null)
                {
                    if (userDTO.PathOfAvatarImage != null)
                        path = Path.Combine(@"\img\avatars", userDTO.PathOfAvatarImage);
                    else
                        path = @"\img\avatars\__nope__.png";
                }
                else
                    path = null;

                return path;
            }
        }

        public void RemoveAvatarImage(int? userId, string rootPath)
        {
            if (GetAvatarPath(userId)?.CompareTo(@"\img\avatars\__nope__.png") != 0)
            {
                string oldPath = Path.Combine(rootPath, GetAvatarPath(userId)?.Substring(1) ?? "");
                if (File.Exists(oldPath))
                {
                    File.Delete(oldPath);
                }

                SetImagePathToUserAccount(userId, "__nope__.png");
            }            
        }

        public async Task<bool> SetAvatarImageAsync(int? userId, string rootPath, IFormFile file)
        {
            try
            {
                UserService userService = new UserService();
                UserDTO user = userService.GetUser(userId);
                if (user == null) return false;

                string path = Path.Combine(rootPath, "img", "avatars");
                if (file.Length > 0)
                {
                    //var rand = RandomNumberGenerator.Create();
                    //byte[] bytes = new byte[8];
                    //rand.GetNonZeroBytes(bytes);
                    //BigInteger bigInteger = new BigInteger(bytes);

                    string filename = file.FileName;
                    string[] fNameComponents = filename.Split('.');
                    fNameComponents[0] = fNameComponents[0].Replace(" ", "_");
                    filename = String.Concat(
                        fNameComponents[0],
                        new Random().Next(999999999).ToString(),
                        //bigInteger.ToString(),
                        ".",
                        fNameComponents[1]);

                    path = Path.Combine(path, filename);

                    //
                    RemoveAvatarImage(userId, rootPath);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                        SetImagePathToUserAccount(userId, filename);
                        return true;
                    }
                }

                return false;
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
                //return false;
            }
        }

        private void SetImagePathToUserAccount(int? id, string path)
        {
            using (AccountManager usersManager = new AccountManager())
            {
                var userf = usersManager.GetUser(id);
                userf.PathOfAvatarImage = path;
                usersManager.UpdateUser(userf);
            }
        }
    }
}
