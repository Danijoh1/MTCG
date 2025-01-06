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
        public int Id { get; set; }
        public string Username {  get; set; }
        public string Password { get; set; }
        public int coins { get; set; }
        public int ELO { get; set; }
        public deck deck { get; set; }
        public stack stack {  get; set; }
        public int battlesFought { get; set; }
    }
}
