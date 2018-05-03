using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ETT.Logic.Interfaces
{
    public interface IFileService
    {
        Task<bool> SetAvatarImageAsync(int? userId, string rootPath, IFormFile file);
        string GetAvatarPath(int? userId);
        void RemoveAvatarImage(int? userId, string rootPath);
    }
}
