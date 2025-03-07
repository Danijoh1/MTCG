using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.Models
{
    public class card
    {
        public enum CardCategory
        {
            Monster,
            Spell
        }
        public card() { }
        public card(string id, string name, float damage) { 
            this.id = id;
            this.name = name;
            this.damage = damage;
            this.element = DetermineElement();
            this.type = DetermineType();


        }
        public string DetermineElement()
        {
            string DeterminedElement;
            if (name.Contains("Fire") || (name.Contains("Dragon") && !name.Contains("Water") && !name.Contains("Regular")))
            {
                DeterminedElement = "fire";
            }
            else if (name.Contains("Water") || (name.Contains("Kraken") && !name.Contains("Fire") && !name.Contains("Regular")))
            {
                DeterminedElement = "water";
            }
            else
            {
                DeterminedElement = "normal";
            }
            return DeterminedElement;
        }
        public string DetermineType()
        {
            string Determinedtype;
            if (name.Contains("Dragon"))
            {
                Determinedtype = "Dragon";
            }
            else if(name.Contains("Goblin"))
            {
                Determinedtype = "Goblin";
            }
            else if (name.Contains("Wizzard"))
            {
                Determinedtype = "Wizzard";
            }
            else if (name.Contains("Ork"))
            {
                Determinedtype = "Ork";
            }
            else if (name.Contains("Kraken"))
            {
                Determinedtype = "Kraken";
            }
            else if (name.Contains("Elve"))
            {
                Determinedtype = "Elve";
            }
            else if (name.Contains("Knight"))
            {
                Determinedtype = "Knight";
            }
            else
            {
                Determinedtype = "normal";
            }
            return Determinedtype;
        }
        public string id { get; set; }
        public string name { get; set; }
        public float damage { get; set; }
        public readonly string element;
        public readonly string type;
        public CardCategory Category { get; set; }
    }
}
