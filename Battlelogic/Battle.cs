using MTCG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.Battlelogic
{
    public class Battle
    {
        public Battle(user Player1, user Player2) {
            Fightlogic battlelogic = new Fightlogic();
            CurrentRound = 1;
            MaxRound = 100;
            for (int i = 0; i != Player1.deck.playerdeck.Length;i++)
            {
                Player1Battledeck.Add(Player1.deck.playerdeck[i]);
            }
            for (int j = 0; j != Player2.deck.playerdeck.Length; j++)
            {
                Player2Battledeck.Add(Player2.deck.playerdeck[j]);
            }
            while(CurrentRound != MaxRound)
            {
                Random Randomnumber = new Random();
                if (Player1Battledeck.Count != 0 || Player2Battledeck.Count != 0)
                {
                    int RandomIndex1 = Randomnumber.Next(0, Player1Battledeck.Count);
                    card card1 = Player1Battledeck[RandomIndex1];
                    int RandomIndex2 = Randomnumber.Next(0, Player2Battledeck.Count);
                    card card2 = Player2Battledeck[RandomIndex2];
                    card winner = battlelogic.Fight(card1, card2);
                    if (winner != null)
                    {
                        BattleWon(card1, card2, winner);
                    }
                    CurrentRound += 1;
                }
                else if (Player1Battledeck.Count == 0)
                {
                    //player2 wins
                    Player2.ELO += 3;
                    Player1.ELO -= 5;
                }
                else
                {
                    //player1 wins
                    Player1.ELO += 3;
                    Player2.ELO -= 5;
                }
            }
            //draw
        }
        public void BattleWon(card card1, card card2, card winner)
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
        public user Player1 { get; set; }
        public user Player2 { get; set; }
        public List<card> Player1Battledeck {  get; set; }
        public List<card> Player2Battledeck { get; set; }
        public int CurrentRound { get; set; }
        public int MaxRound { get; set; }
    }
}
