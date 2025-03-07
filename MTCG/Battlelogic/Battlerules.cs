using MTCG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.Battlelogic
{
    public class Battlerules
    {
        public string Battle(user Player1, user Player2) {
            Fightlogic battlelogic = new Fightlogic();
            List<card> Player1Battledeck = Player1.deck;
            int CurrentRound = 1;
            int MaxRound = 100; 
            List<card> Player2Battledeck = Player2.deck;
            while (CurrentRound != MaxRound)
            {
                Random Randomnumber = new Random();
                if (Player1Battledeck.Count > 0 && Player2Battledeck.Count > 0)
                {
                    int RandomIndex1 = Randomnumber.Next(0, Player1Battledeck.Count);
                    card card1;
                    if (RandomIndex1 == 0)
                    {
                        card1 = Player1Battledeck[0];
                    }
                    else
                    {
                        card1 = Player1Battledeck[index: RandomIndex1 -1];
                    }
                    int RandomIndex2 = Randomnumber.Next(0, Player2Battledeck.Count);
                    card card2;
                    if (RandomIndex2 == 0)
                    {
                        card2 = Player2Battledeck[0];
                    }
                    else
                    {
                        card2 = Player2Battledeck[index: RandomIndex2 - 1];
                    }
                    card winner = battlelogic.Fight(card1, card2);
                    if (winner != null)
                    {
                            if (card1 == winner)
                            {
                                Player2Battledeck.Remove(card2);
                                Player1Battledeck.Add(card2);
                            }
                            else
                            {
                                Player2Battledeck.Add(card1);
                                Player1Battledeck.Remove(card1);
                            }
                    }
                    CurrentRound += 1;
                }
                else if (Player1Battledeck.Count == 0)
                {
                    return Player2.username;
                }
                else
                {
                    return Player1.username;
                }
            }
            return null;
            //draw
        }
        public void BattleWon(user Player1, user Player2,string name)
        {
            if(name != null)
            {
                if(name == Player1.username)
                {
                    //player1 wins
                    Player1.ELO += 3;
                    Player2.ELO -= 5;
                }
                else
                {
                    //player2 wins
                    Player2.ELO += 3;
                    Player1.ELO -= 5;
                }
            }
        }
    }
}
