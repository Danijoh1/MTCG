using MTCG.Models;
using MTCG.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.Handlers
{
    public class PackageHandler
    {
        public PackageHandler(PackageRepository repository)
        {
            this.repository = repository;
        }
        public packages AddPackage()
        {
            packages package = new packages();
            package = repository.Add();
            return package;
        }
        public packages GetUnsoldPackage()
        {
            packages package = new packages();
            package = repository.GetUnsoldPackage();
            return package;
        }
        public void SellPackage(packages package)
        {
            repository.SellPackage(package);
        }
        public void RemoveUser(packages package)
        {
            repository.Delete(package);
        }
        public PackageRepository repository { get; set; }
    }
}
