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
        IUserRepository userRepository;
        public UserService()
        {
            userRepository = new UserRepository();

        }

        public void DeleteUser(int uid)
        {
            userRepository.DeleteUser(uid);
        }

        public List<UserViewModel> GetUsers()
        {
            List<User> userList = userRepository.GetUsers();
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<User, UserViewModel>(); cfg.IgnoreUnmapped(); });
            var mapper = config.CreateMapper();
            List<UserViewModel> userViewModels = mapper.Map<List<User>,List<UserViewModel>>(userList);
            if (userViewModels.Count != 0)
                return userViewModels;
            return null;
        }

        public UserViewModel GetUsersByEmail(string Email)
        {
            User u = userRepository.GetUserByEmails(Email).FirstOrDefault();
            UserViewModel uvm = null;
            if (u != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<User, UserViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                uvm = mapper.Map<User, UserViewModel>(u);
            }
            return uvm;
        }

        public UserViewModel GetUsersByEmailAndPassword(string email, string password)
        {
            var user = userRepository.GetUserByEmailsAndPassword(email, SHA256HashGenerator.GenerateHash(password));
            if(user != null){
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<User, UserViewModel>(); cfg.IgnoreUnmapped(); });
                var mapper = config.CreateMapper();
                var userViewModels = mapper.Map<User, UserViewModel>(user);
                return userViewModels;
            }
            return null;
        }

        public UserViewModel GetUsersByUserID(int UserID)
        {
            throw new NotImplementedException();
        }

        public int InsertUser(RegisterViewModel uvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<RegisterViewModel, User>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            var user = mapper.Map<RegisterViewModel, User>(uvm);
            user.PasswordHash = SHA256HashGenerator.GenerateHash(uvm.Password);
            userRepository.InsertUser(user);
            return userRepository.GetLatestUserId();
        }

        public void UpdateUserDetails(EditUserDetailsViewModel uvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EditUserDetailsViewModel, User>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            User user = mapper.Map<EditUserDetailsViewModel, User>(uvm);
            userRepository.UpdateUserDetails(user);


        }

        public void UpdateUserPassword(EditUserPasswordViewModel uvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EditUserPasswordViewModel, User>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            var user = mapper.Map<EditUserPasswordViewModel, User>(uvm);
            var _user = userRepository.GetUserByUserId(user.UserId);
            _user.PasswordHash = SHA256HashGenerator.GenerateHash(uvm.Password);
            userRepository.UpdateUserPassword(_user);


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
