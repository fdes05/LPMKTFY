using MKTFY.Models.Entities;
using MKTFY.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.App.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<UserVM> GetByEmail(string email);

        Task<User> GetByUserName(string userName);

        Task<bool> VerifyEmail(string email);
        
    }
}
