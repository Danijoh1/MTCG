using MTCG.Battlelogic;
using MTCG.Models;

namespace MTCG.Tests
{
    public class Fighttests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Compare_Cards_Damage_ReturnWinner()
        {

            //Arrange
            Fightlogic battle = new Fightlogic();
            card card1 = new monstercard("1","Test1",5);
            
            card card2 = new monstercard("2","Test2",10);
            //Act
            card winner = battle.Fight(card1, card2);

            //Assert
            Assert.That(Equals(winner, card2));
            
        }
        [Test]
        public void Compare_Monster_with_Spellcard()
        {
            //Arrange
            Fightlogic battle = new Fightlogic();
            card card1 = new monstercard("1", "Test1", 5);

            card card2 = new spellcard("2", "Test2", 10);
            //Act
            card winner = battle.Fight(card1, card2);

            //Assert
            Assert.That(Equals(winner, card2));
        }
        [Test]
        public void Compare_Fire_with_Water_Card()
        {
            //Arrange
            Fightlogic battle = new Fightlogic();
            card card1 = new monstercard("1", "FireTest1", 5);

            card card2 = new spellcard("2", "WaterTest2", 10);
            //Act
            card winner = battle.Fight(card1, card2);

            //Assert
            Assert.That(Equals(winner, card2));
        }
        [Test]
        public void Compare_Water_with_Fire_Card()
        {
            //Arrange
            Fightlogic battle = new Fightlogic();
            card card1 = new monstercard("1", "WaterTest1", 5);

            card card2 = new spellcard("2", "FireTest2", 10);
            //Act
            card winner = battle.Fight(card1, card2);

            //Assert
            Assert.That(Equals(winner, card1));
        }
        [Test]
        public void Compare_Water_with_Normal_Card()
        {
            //Arrange
            Fightlogic battle = new Fightlogic();
            card card1 = new monstercard("1", "WaterTest1", 5);

            card card2 = new spellcard("2", "Test2", 10);
            //Act
            card winner = battle.Fight(card1, card2);

            //Assert
            Assert.That(Equals(winner, card2));
        }
        [Test]
        public void Compare_Normal_with_Fire_Card()
        {
            //Arrange
            Fightlogic battle = new Fightlogic();
            card card1 = new monstercard("1", "Test1", 5);

            card card2 = new spellcard("2", "FireTest2", 10);
            //Act
            card winner = battle.Fight(card1, card2);

            //Assert
            Assert.That(Equals(winner, card2));
        }
        [Test]
        public void Compare_Cards_with_Equal_Damage()
        {
            //Arrange
            Fightlogic battle = new Fightlogic();
            card card1 = new monstercard("1", "Test1", 5);

            card card2 = new spellcard("2", "Test2", 5);
            //Act
            card winner = battle.Fight(card1, card2);

            //Assert
            Assert.That(Equals(winner, null));
        }
        [Test]
        public void Compare_Monstercards_with_effective_Elements()
        {
            //Arrange
            Fightlogic battle = new Fightlogic();
            card card1 = new monstercard("1", "WaterTest1", 5);

            card card2 = new monstercard("2", "FireTest2", 10);
            //Act
            card winner = battle.Fight(card1, card2);

            //Assert
            Assert.That(Equals(winner, card2));
        }
        [Test]
        public void Check_Goblins_with_Dragons()
        {
            //Arrange
            Fightlogic battle = new Fightlogic();
            card card1 = new monstercard("1", "WaterGoblin", 10);

            card card2 = new monstercard("2", "Dragon", 5);
            //Act
            card winner = battle.Fight(card1, card2);

            //Assert
            Assert.That(Equals(winner, card2));
        }
        [Test]
        public void Check_FireElves_with_Dragons()
        {
            //Arrange
            Fightlogic battle = new Fightlogic();
            card card1 = new monstercard("1", "FireElve", 5);

            card card2 = new monstercard("2", "Dragon", 10);
            //Act
            card winner = battle.Fight(card1, card2);

            //Assert
            Assert.That(Equals(winner, card1));
        }
        [Test]
        public void Check_Wizzards_with_Orks()
        {
            //Arrange
            Fightlogic battle = new Fightlogic();
            card card1 = new monstercard("1", "Wizzard", 5);

            card card2 = new monstercard("2", "Ork", 10);
            //Act
            card winner = battle.Fight(card1, card2);

            //Assert
            Assert.That(Equals(winner, card1));
        }
        [Test]
        public void Check_Kraken_with_Spells()
        {
            //Arrange
            Fightlogic battle = new Fightlogic();
            card card1 = new monstercard("1", "Kraken", 5);

            card card2 = new spellcard("2", "Spell", 10);
            //Act
            card winner = battle.Fight(card1, card2);

            //Assert
            Assert.That(Equals(winner, card1));
        }
        [Test]
        public void Check_Knights_with_Waterspells()
        {
            //Arrange
            Fightlogic battle = new Fightlogic();
            card card1 = new monstercard("1", "Knight", 5);

            card card2 = new spellcard("2", "Waterspell", 10);
            //Act
            card winner = battle.Fight(card1, card2);

            //Assert
            Assert.That(Equals(winner, card2));
        }
    }
}