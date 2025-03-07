using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.Models
{
    public class user
    {
        public user() {
            coins = 20;
            ELO = 100;
            battlesFought = 0;
        }
        public int id { get; set; }
        public string username {  get; set; }
        public string password { get; set; }
        public int coins { get; set; }
        public int ELO { get; set; }
        public List<card> deck { get; set; }
        public List<card> stack {  get; set; }
        public int battlesFought { get; set; }
        public string image { get; set; }
        public string bio {  get; set; }
    }
}
