using MTCG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.Battlelogic
{
    public class Fightlogic
    {
        public card Fight(card card1, card card2)
        {
            float card1ActualDamage = card1.damage;
            float card2ActualDamage = card2.damage;
            if (IsSpecial(card1, card2))
            {
                card1ActualDamage = SpecialDamageCheck(card1, card2);
                //if (card1ActualDamage == card1.damage)
                //{
                //    card1ActualDamage = SpecialDamageCheck(card2, card1);
                //}
                card2ActualDamage = SpecialDamageCheck(card2, card1);
                //if (card2ActualDamage == card2.damage)
                //{
                //    card2ActualDamage = SpecialDamageCheck(card1, card2);
                //}
            }
            else
            {
                if (!IsPureMonsterFight(card1, card2))
                {
                    card1ActualDamage = ElementalDamageCheck(card1, card2);
                    card2ActualDamage = ElementalDamageCheck(card2, card1);
                }
            }
            if (card1ActualDamage > card2ActualDamage)
            {
                return card1;
            }
            else if (card1ActualDamage < card2ActualDamage)
            {
                return card2;
            }
            else
            {
                return null;
            }
        }
        public bool IsPureMonsterFight(card card1, card card2)
        {
            if (card1.Category == card.CardCategory.Monster && card2.Category == card.CardCategory.Monster)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public float ElementalDamageCheck(card card1, card card2)
        {
            float inflictedDamage = card1.damage;
            if (card1.element != card2.element)
            {
                if (card1.element == "fire")
                {
                    if (card2.element == "normal")
                    {
                        inflictedDamage *= 2;
                    }
                    else if (card2.element == "water")
                    {
                        inflictedDamage /= 2;
                    }
                }
                else if (card1.element == "normal")
                {
                    if (card2.element == "water")
                    {
                        inflictedDamage *= 2;
                    }
                    else if (card2.element == "fire")
                    {
                        inflictedDamage /= 2;
                    }
                }
                else if (card1.element == "water")
                {
                    if (card2.element == "fire")
                    {
                        inflictedDamage *= 2;
                    }
                    else if (card2.element == "normal")
                    {
                        inflictedDamage /= 2;
                    }
                }
            }
            return inflictedDamage;
        }
        public bool IsSpecial(card card1, card card2)
        {
            if(card1.type != "normal")
            {
                return true;
            }
            if(card2.type != "normal")
            {
                return true;
            }
            return false;
        }
        public float SpecialDamageCheck(card card1, card card2)
        {
            float inflictedDamage = 0;
            if (card1.type == "Goblin" && card2.type == "Dragon")
            {
                inflictedDamage = 0;
            }
            else if (card1.type == "Ork" && card2.type == "Wizzard")
            {
                inflictedDamage = 0;
            }
            else if (card1.type == "Dragon" && (card2.type == "Elve" && card2.element == "fire"))
            {
                inflictedDamage = 0;
            }
            else if (card1.Category == card.CardCategory.Spell && card2.type == "Kraken")
            {
                inflictedDamage = 0;
            }
            else if (card1.type == "Knight" && card2.Category == card.CardCategory.Spell && card2.element == "water")
            {
                inflictedDamage = -1;
            }  
            else
            {
                inflictedDamage = card1.damage;
            }
            return inflictedDamage;

        }
    }
}
