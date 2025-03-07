using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCG.Repositories;
using MTCG.Models;
using System.Security.Cryptography.X509Certificates;

namespace MTCG.Handlers
{
    public class UserHandler
    {
        public UserHandler(UserRepository repository)
        {
            this.repository = repository;
        }
        public void AddUser(user user)
        {
            repository.Add(user);
        }
        public void UpdateUserInfo(user user,string name, string bio, string image)
        {
            repository.UpdateUserInfo(user, name,bio,image);
        }
        public user GetByUsername(string name)
        {
           return repository.GetByUsername(name);
        }
        public void ChangeCoins(user user)
        {
            repository.UpdateCoins(user);
        }
        public void RemoveUser(user user)
        {
            repository.Delete(user);
        }
        public UserRepository repository { get; set; }
    }
}
