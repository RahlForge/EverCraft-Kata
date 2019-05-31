using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EverCraft;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestDefaultName()
        {
            var character = new Character();

            Assert.IsNotNull(character.Name);
        }

        [TestMethod]
        public void TestSetName()
        {
            var name = "My Test Name";

            var character = new Character();
            character.Name = name;

            Assert.AreEqual(character.Name, name);
        }

        [TestMethod]
        public void TestGoodAlignment()
        {
            var alignment = Alignments.Good;
            var character = new Character();
            character.Alignment = alignment;
            Assert.AreEqual(character.Alignment, Alignments.Good);
        }

        [TestMethod]
        public void TestEvilAlignment()
        {
            var alignment = Alignments.Evil;
            var character = new Character();
            character.Alignment = alignment;

            Assert.AreEqual(character.Alignment, Alignments.Evil);
        }

        [TestMethod]
        public void TestNeutralAlignment()
        {
            var alignment = Alignments.Neutral;
            var character = new Character();
            character.Alignment = alignment;

            Assert.AreEqual(character.Alignment, Alignments.Neutral);
        }

        [TestMethod]
        public void TestDefaultAlignment()
        {
            var character = new Character();

            Assert.AreEqual(character.Alignment, Alignments.Neutral);
        }

        [TestMethod]
        public void TestDefaultAC()
        {
            var character = new Character();

            Assert.AreEqual(character.ArmorClass, 10);
        }

        [TestMethod]
        public void TestDefaultHP()
        {
            var character = new Character();

            Assert.AreEqual(character.HitPoints, 5);
        }

        [TestMethod]
        public void TestMinHPWithNegativeConModifier()
        {
            var character = new Character();
            character.Con.Value = 1;

            Assert.IsTrue(character.CurrentHitPoints >= 1);
        }

        [TestMethod]
        public void TestAttack()
        {
            var dieResult = Character.GetD20();

            Assert.IsTrue(dieResult > 0 && dieResult <= 20);

        }

        [TestMethod]
        public void TestAttackEqualsACIsHit()
        {
            var opponentAC = 10;
            var attackRoll = 10;

            Assert.AreEqual(Character.AttackIsHit(opponentAC, attackRoll), AttackResult.Hit);
        }

        [TestMethod]
        public void TestAttackGreaterACIsHit()
        {
            var opponentAC = 10;
            var attackRoll = 11;

            Assert.AreEqual(Character.AttackIsHit(opponentAC, attackRoll), AttackResult.Hit);
        }

        [TestMethod]
        public void TestAttackMisses()
        {
            var opponentAC = 10;
            var attackRoll = 9;

            Assert.AreEqual(Character.AttackIsHit(opponentAC, attackRoll), AttackResult.Miss);
        }

        [TestMethod]
        public void TestEffectOnHit()
        {
            var opponent = new Character();
            var startingHP = opponent.HitPoints;
            var result = AttackResult.Hit;

            opponent.Hit(result);

            Assert.AreEqual(opponent.HitPoints, startingHP - 1);
        }

        [TestMethod]
        public void TestAttackIsCriticalHit()
        {
            var opponentAC = 14;
            var attackRoll = 20;

            Assert.AreEqual(Character.AttackIsHit(opponentAC, attackRoll), AttackResult.CriticalHit);
        }

        [TestMethod]
        public void TestEffectOnCriticalHit()
        {

            var opponent = new Character();
            var startingHP = opponent.HitPoints;
            var result = AttackResult.CriticalHit;

            opponent.Hit(result);

            Assert.AreEqual(opponent.HitPoints, startingHP - 2);
        }

        [TestMethod]
        public void TestEffectOnMiss()
        {
            var opponent = new Character();
            var startingHP = opponent.HitPoints;
            var result = AttackResult.Miss;
            opponent.Hit(result);

            Assert.AreEqual(opponent.HitPoints, startingHP);
        }

        [TestMethod]
        public void TestDeath()
        {
            var character = new Character();

            character.CurrentDamage = 5;

            Assert.IsFalse(character.IsAlive);

        }

        [TestMethod]
        public void TestDeathWithPositiveConMod()
        {
            var character = new Character();
            character.Con.Value = 12;
            character.CurrentDamage = 14;
            Assert.IsFalse(character.IsAlive);
        }

        [TestMethod]
        public void TestCharHasAllStats()
        {
            var character = new Character();

            var abilities = Enum.GetNames(typeof(Stats));
            foreach (var ability in abilities)
                Assert.IsTrue(character.Abilities.ContainsKey((Stats)Enum.Parse(typeof(Stats), ability)));
        }


        [TestMethod]
        public void TestStatGreaterThanOrEquals1()
        {
            var stat = new Stat("");
            for (int i = 0; i > -5; i--)
            {
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => stat.Value = i);
            }
        }

        [TestMethod]
        public void TestStatLessThanOrEquals20()
        {
            var stat = new Stat("");
            for (int i = 21; i < 25; i++)
            {
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => stat.Value=i);
            }
        }

        [TestMethod]
        public void TestStatModifiers()
        {
            var modifiers = new Dictionary<int, int>
            {
                {1, -5}, {6, -2}, {11,  0}, {16, +3},
                {2, -4}, {7, -2}, {12, +1}, {17, +3},
                {3, -4}, {8, -1}, {13, +1}, {18, +4},
                {4, -3}, {9, -1}, {14, +2}, {19, +4},
                {5, -3}, {10, 0}, {15, +2}, {20, +5}
            };

            foreach(var kvp in modifiers)
            {
                var stat = new Stat(kvp.Key);
                Assert.AreEqual(stat.Modifier, kvp.Value);
            }

        }

        [TestMethod]
        public void TestStatDefaultIs10()
        {
            var stat = new Stat("");

            Assert.AreEqual(stat.Value, 10);
        }

        [TestMethod]
        public void TestAttackBonus()
        {
            var character = new Character();

            character.Str.Value=12;
            var attackRoll = Character.GetD20();

            Assert.AreEqual(character.Attack(attackRoll), attackRoll + 1);
        }

        [TestMethod]
        public void TestStrengthModifiesDamage()
        {
            var character = new Character();
            character.Str.Value=12;

            Assert.AreEqual(character.Damage(), 2);
        }

        [TestMethod]
        public void TestCritDoublesStrengthDamage()
        {
            var character = new Character();
            character.Str.Value=12;

            Assert.AreEqual(4, character.Damage(true));
        }

        [TestMethod]
        public void TestMinumumDamageIs1()
        {
            var character = new Character();
            character.Str.Value = 1;

            Assert.IsTrue(character.Damage(false) >= 1);

        }

        [TestMethod]
        public void TestDexModifiesAC()
        {
            var character = new Character();
            character.Dex.Value = 14;

            Assert.AreEqual(character.ArmorClass, 12);
        }

        [TestMethod]
        public void TestConModifiesHP()
        {
            var character = new Character();
            character.Con.Value = 14;
            Assert.AreEqual(7, character.HitPoints);

        }
    }
}
