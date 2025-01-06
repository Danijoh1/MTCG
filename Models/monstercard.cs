using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.Models
{
    public class monstercard :card
    {
        public monstercard(string id, string name, float damage) :base(id, name, damage)
        {
            Category = CardCategory.Monster;
        }
    }
}
