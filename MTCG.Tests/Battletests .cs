using MTCG.Battlelogic;
using MTCG.Models;

namespace MTCG.Tests
{
    public class Battletests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Player2_wins_battle()
        {
            //Arrange
            Battlerules testbattle = new Battlerules();
            user Player1 = new user();
            user Player2 = new user();
            Player1.username = "Test1";
            Player2.username = "Test2";
            card card1Player1 = new monstercard("1", "Test1", 5);
            card card2Player1 = new monstercard("1", "Test1", 5);
            card card3Player1 = new monstercard("1", "Test1", 5);
            card card4Player1 = new monstercard("1", "Test1", 5);
            card card1Player2 = new monstercard("2", "Test2", 10);
            card card2Player2 = new monstercard("2", "Test2", 10);
            card card3Player2 = new monstercard("2", "Test2", 10);
            card card4Player2 = new monstercard("2", "Test2", 10);
            List<card> cardList1 = new List<card>();
            List<card> cardList2 = new List<card>();
            cardList1.Add(card1Player1);
            cardList1.Add(card2Player1);
            cardList1.Add(card3Player1);
            cardList1.Add(card4Player1);
            cardList2.Add(card1Player2);
            cardList2.Add(card2Player2);
            cardList2.Add(card3Player2);
            cardList2.Add(card4Player2);
            Player1.deck = cardList1;
            Player2.deck = cardList2;
            //Act
            string ?winner = testbattle.Battle(Player1, Player2);
            //Assert
            Assert.That(Equals(winner, Player2.username));
            
        }
        [Test]
        public void Player1_wins_battle()
        {

            //Arrange
            Battlerules testbattle = new Battlerules();
            user Player1 = new user();
            user Player2 = new user();
            Player1.username = "Test1";
            Player2.username = "Test2";
            card card1Player1 = new monstercard("1", "Test1", 10);
            card card2Player1 = new monstercard("1", "Test1", 10);
            card card3Player1 = new monstercard("1", "Test1", 10);
            card card4Player1 = new monstercard("1", "Test1", 10);
            card card1Player2 = new monstercard("2", "Test2", 5);
            card card2Player2 = new monstercard("2", "Test2", 5);
            card card3Player2 = new monstercard("2", "Test2", 5);
            card card4Player2 = new monstercard("2", "Test2", 5);
            List<card> cardList1 = new List<card>();
            List<card> cardList2 = new List<card>();
            cardList1.Add(card1Player1);
            cardList1.Add(card2Player1);
            cardList1.Add(card3Player1);
            cardList1.Add(card4Player1);
            cardList2.Add(card1Player2);
            cardList2.Add(card2Player2);
            cardList2.Add(card3Player2);
            cardList2.Add(card4Player2);
            Player1.deck = cardList1;
            Player2.deck = cardList2;
            //Act
            string ?winner = testbattle.Battle(Player1, Player2);
            //Assert
            Assert.That(Equals(winner, Player1.username));

        }
        [Test]
        public void Draw()
        {
            //Arrange
            Battlerules testbattle = new Battlerules();
            user Player1 = new user();
            user Player2 = new user();
            Player1.username = "Test1";
            Player2.username = "Test2";
            card card1Player1 = new monstercard("1", "Test1", 5);
            card card2Player1 = new monstercard("1", "Test1", 5);
            card card3Player1 = new monstercard("1", "Test1", 5);
            card card4Player1 = new monstercard("1", "Test1", 5);
            card card1Player2 = new monstercard("2", "Test2", 5);
            card card2Player2 = new monstercard("2", "Test2", 5);
            card card3Player2 = new monstercard("2", "Test2", 5);
            card card4Player2 = new monstercard("2", "Test2", 5);
            List<card> cardList1 = new List<card>();
            List<card> cardList2 = new List<card>();
            cardList1.Add(card1Player1);
            cardList1.Add(card2Player1);
            cardList1.Add(card3Player1);
            cardList1.Add(card4Player1);
            cardList2.Add(card1Player2);
            cardList2.Add(card2Player2);
            cardList2.Add(card3Player2);
            cardList2.Add(card4Player2);
            Player1.deck = cardList1;
            Player2.deck = cardList2;
            //Act
            string ?winner = testbattle.Battle(Player1, Player2);
            //Assert
            Assert.That(Equals(winner, null));
        }
        [Test]
        public void Player2_gains_ELO()
        {
            //Arrange
            Battlerules testbattle = new Battlerules();
            user Player1 = new user();
            user Player2 = new user();
            Player1.username = "Test1";
            Player2.username = "Test2";
            card card1Player1 = new monstercard("1", "Test1", 5);
            card card2Player1 = new monstercard("1", "Test1", 5);
            card card3Player1 = new monstercard("1", "Test1", 5);
            card card4Player1 = new monstercard("1", "Test1", 5);
            card card1Player2 = new monstercard("2", "Test2", 10);
            card card2Player2 = new monstercard("2", "Test2", 10);
            card card3Player2 = new monstercard("2", "Test2", 10);
            card card4Player2 = new monstercard("2", "Test2", 10);
            List<card> cardList1 = new List<card>();
            List<card> cardList2 = new List<card>();
            cardList1.Add(card1Player1);
            cardList1.Add(card2Player1);
            cardList1.Add(card3Player1);
            cardList1.Add(card4Player1);
            cardList2.Add(card1Player2);
            cardList2.Add(card2Player2);
            cardList2.Add(card3Player2);
            cardList2.Add(card4Player2);
            Player1.deck = cardList1;
            Player2.deck = cardList2;
            //Act
            string ?winner = testbattle.Battle(Player1, Player2);
            testbattle.BattleWon(Player1, Player2,winner);
            //Assert
            Assert.That(Equals(Player2.ELO, 103));

        }
        [Test]
        public void Player1_gains_ELO()
        {
            //Arrange
            Battlerules testbattle = new Battlerules();
            user Player1 = new user();
            user Player2 = new user();
            Player1.username = "Test1";
            Player2.username = "Test2";
            card card1Player1 = new monstercard("1", "Test1", 10);
            card card2Player1 = new monstercard("1", "Test1", 10);
            card card3Player1 = new monstercard("1", "Test1", 10);
            card card4Player1 = new monstercard("1", "Test1", 10);
            card card1Player2 = new monstercard("2", "Test2", 5);
            card card2Player2 = new monstercard("2", "Test2", 5);
            card card3Player2 = new monstercard("2", "Test2", 5);
            card card4Player2 = new monstercard("2", "Test2", 5);
            List<card> cardList1 = new List<card>();
            List<card> cardList2 = new List<card>();
            cardList1.Add(card1Player1);
            cardList1.Add(card2Player1);
            cardList1.Add(card3Player1);
            cardList1.Add(card4Player1);
            cardList2.Add(card1Player2);
            cardList2.Add(card2Player2);
            cardList2.Add(card3Player2);
            cardList2.Add(card4Player2);
            Player1.deck = cardList1;
            Player2.deck = cardList2;
            //Act
            string winner = testbattle.Battle(Player1, Player2);
            testbattle.BattleWon(Player1, Player2, winner);
            //Assert
            Assert.That(Equals(Player1.ELO, 103));

        }
        [Test]
        public void Nobody_gains_ELO()
        {
            //Arrange
            Battlerules testbattle = new Battlerules();
            user Player1 = new user();
            user Player2 = new user();
            Player1.username = "Test1";
            Player2.username = "Test2";
            card card1Player1 = new monstercard("1", "Test1", 5);
            card card2Player1 = new monstercard("1", "Test1", 5);
            card card3Player1 = new monstercard("1", "Test1", 5);
            card card4Player1 = new monstercard("1", "Test1", 5);
            card card1Player2 = new monstercard("2", "Test2", 5);
            card card2Player2 = new monstercard("2", "Test2", 5);
            card card3Player2 = new monstercard("2", "Test2", 5);
            card card4Player2 = new monstercard("2", "Test2", 5);
            List<card> cardList1 = new List<card>();
            List<card> cardList2 = new List<card>();
            cardList1.Add(card1Player1);
            cardList1.Add(card2Player1);
            cardList1.Add(card3Player1);
            cardList1.Add(card4Player1);
            cardList2.Add(card1Player2);
            cardList2.Add(card2Player2);
            cardList2.Add(card3Player2);
            cardList2.Add(card4Player2);
            Player1.deck = cardList1;
            Player2.deck = cardList2;
            //Act
            string ?winner = testbattle.Battle(Player1, Player2);
            //Assert
            testbattle.BattleWon(Player1, Player2, winner);
            //Assert
            Assert.That(Equals(Player1.ELO, 100)&&Equals(Player2.ELO,100));
        }
    }
}