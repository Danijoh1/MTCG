using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCG.Handlers;
using MTCG.Models;
using MTCG.Repositories;
using MTCG.Models;
using System.Security.Cryptography.X509Certificates;
using Npgsql;
using System.Data;

namespace MTCG.Handlers
{
    public class DatabaseHandlers
    {
        public DatabaseHandlers() 
        {
            PackageRepository PackageRepository = new PackageRepository("Host=localhost;Username=user;Password=password;Database=mtgcdb");
            PackageHandler PackageHandler = new PackageHandler(PackageRepository);
            CardRepository CardRepository = new CardRepository("Host=localhost;Username=user;Password=password;Database=mtgcdb");
            CardHandler CardHandler = new CardHandler(CardRepository);
            UserRepository UserRepository = new UserRepository("Host=localhost;Username=user;Password=password;Database=mtgcdb");
            UserHandler userHandler = new UserHandler(UserRepository);
        }
        public CardHandler CardHandler { get; set; }
        public UserHandler UserHandler { get; set; }
        public PackageHandler PackageHandler { get; set; }
        public PackageRepository PackageRepository { get; set; }
        public CardRepository CardRepository {  get; set; }
        public UserRepository UserRepository { get; set; }
    }
}