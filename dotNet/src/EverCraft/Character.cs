using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverCraft
{
    public class Character
    {
        /// <summary>
        /// Character's Name
        /// </summary>
        public string Name { get; set; } = "Filbert the Fighter";


        /// <summary>
        /// Character's Alignment
        /// </summary>
        public Alignments Alignment { get; set; }

        /// <summary>
        /// Character's Armor Class
        /// </summary>
        public int ArmorClass
        {
            get { return _armorClass + Dex.Modifier; }
        }

        /// <summary>
        /// Character's Hitpionts
        /// </summary>
        public int HitPoints
        {
            get
            {
                return Math.Max(1, _hitPoints + Con.Modifier);
            }
            set { _hitPoints = value;  } 
        }

        /// <summary>
        /// Determines if the character is still alive according
        /// to his current hit point value
        /// </summary>
        public bool IsAlive
        {
            get
            {
                return (CurrentHitPoints > 0);
            }
        }

        public Dictionary<Stats, Stat> Abilities = new Dictionary<Stats, Stat>
        {
            {Stats.Str, new Stat("Strength")},
            {Stats.Dex, new Stat("Dexterity") },
            {Stats.Con, new Stat("Constitution") },
            {Stats.Int, new Stat("Intelligence") },
            {Stats.Wis, new Stat("Wisdom") },
            {Stats.Cha, new Stat("Charisma") }
        };

        private int _armorClass = 10;
        private int _hitPoints = 5;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Character()
        {
           
        }

        public Stat Str => this.Abilities[Stats.Str];
        public Stat Dex => this.Abilities[Stats.Dex];
        public Stat Con => this.Abilities[Stats.Con];
        public Stat Int => this.Abilities[Stats.Int];
        public Stat Wis => this.Abilities[Stats.Wis];
        public Stat Cha => this.Abilities[Stats.Cha];

        public int CurrentHitPoints => HitPoints - CurrentDamage;

        public int CurrentDamage { get; set; } = 0;

        /// <summary>
        /// Rolls a 20-sided die
        /// </summary>
        /// <returns>The result of the d20 roll</returns>
        public static int GetD20()
        {
            return 4; // Placeholder for DieRoller
        }

        public static AttackResult AttackIsHit(int ac, int dieResult)
        {
            if (dieResult == 20)
                return AttackResult.CriticalHit;
            else if (ac <= dieResult)
                return AttackResult.Hit;
            else
                return AttackResult.Miss;
        }

        /// <summary>
        /// Hit the character
        /// </summary>
        public void Hit(AttackResult result)
        {
            switch (result)
            {
                case AttackResult.Hit:
                    HitPoints -= 1;
                    break;
                case AttackResult.CriticalHit:
                    HitPoints -= 2;
                    break;
                default:
                    //do nothing
                    break;
            }
        }

        public int Attack(int attackRoll)
        {
            return attackRoll + Abilities[Stats.Str].Modifier;
        }

        public int Damage(bool criticalHit = false)
        {
            var damage = 0;
            if (!criticalHit)
                damage = 1 + Abilities[Stats.Str].Modifier;
            else
                damage = 2 + (2 * Str.Modifier);

            if (damage < 1)
                damage = 1;

            return damage;
        }
    }
}
