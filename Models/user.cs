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
        }
        public string Username {  get; set; }
        public string Password { get; set; }
        public int coins { get; set; }
    }
}
