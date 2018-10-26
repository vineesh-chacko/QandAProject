using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QandADomainModels;
using QandAProjectViewModels;
using QandAProjectRepository;
using AutoMapper;
using AutoMapper.Configuration;

namespace QandAServiceLayer
{
    public class UserService : IUserService
    {
        public void DeleteUser(int uid)
        {
            throw new NotImplementedException();
        }

        public List<UserViewModel> GetUsers()
        {
            throw new NotImplementedException();
        }

        public UserViewModel GetUsersByEmail(string Email)
        {
            throw new NotImplementedException();
        }

        public UserViewModel GetUsersByEmailAndPassword(string Email, string Password)
        {
            throw new NotImplementedException();
        }

        public UserViewModel GetUsersByUserID(int UserID)
        {
            throw new NotImplementedException();
        }

        public int InsertUser(RegisterViewModel uvm)
        {
            throw new NotImplementedException();
        }

        public void UpdateUserDetails(EditUserDetailsViewModel uvm)
        {
            throw new NotImplementedException();
        }

        public void UpdateUserPassword(EditUserPasswordViewModel uvm)
        {
            throw new NotImplementedException();
        }
    }

    public interface IUserService
    {
        int InsertUser(RegisterViewModel uvm);
        void UpdateUserDetails(EditUserDetailsViewModel uvm);
        void UpdateUserPassword(EditUserPasswordViewModel uvm);
        void DeleteUser(int uid);
        List<UserViewModel> GetUsers();
        UserViewModel GetUsersByEmailAndPassword(string Email, string Password);
        UserViewModel GetUsersByEmail(string Email);
        UserViewModel GetUsersByUserID(int UserID);
    }
}
