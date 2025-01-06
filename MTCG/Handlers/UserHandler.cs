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

        public void RetrieveUser(user user)
        {
            repository.GetAll().ToList().ForEach(p => Console.WriteLine(p));
        }
        public void UpdateUser(user user)
        {
            repository.Update(user);
        }
        public user GetByUsername(string name)
        {
           return repository.GetByUsername(name);
        }
        public void RemoveUser(user user)
        {
            repository.Delete(user);
        }
        public UserRepository repository { get; set; }
    }
}
