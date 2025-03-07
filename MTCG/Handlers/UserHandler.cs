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
        public void UpdateUserInfo(user user)
        {
            repository.UpdateUserInfo(user);
        }
        public List<user> GetScore()
        {
            return repository.GetScore();
        }
        public user GetByUsername(string name)
        {
           return repository.GetByUsername(name);
        }
        public user GetUserInfoByUsername(string name)
        {
            return repository.GetUserInfoByUsername(name);
        }
        public user GetStatsByUsername(string name)
        {
            return repository.GetStatsByUsername(name);
        }
        public void ChangeCoins(user user)
        {
            repository.UpdateCoins(user);
        }
        public void UpdateELO(user user)
        {
            repository.UpdateELO(user);
        }
        public void RemoveUser(user user)
        {
            repository.Delete(user);
        }
        public UserRepository repository { get; set; }
    }
}
