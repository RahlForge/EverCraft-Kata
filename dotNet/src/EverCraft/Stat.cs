using System;
using System.Collections.Generic;

namespace EverCraft
{
    public class Stat
    {
        public string Name { get; private set; }

        public int Value
        {
            get { return _value; }
            set {
                if (value < 1 || value > 20)
                    throw new ArgumentOutOfRangeException();
                _value = value;
            }
        }

        public int Modifier { get=>ModiferLookup[Value]; }

        private static Dictionary<int,int> ModiferLookup = new Dictionary<int, int>
            {
                {1, -5}, {6, -2}, {11,  0}, {16, +3},
                {2, -4}, {7, -2}, {12, +1}, {17, +3},
                {3, -4}, {8, -1}, {13, +1}, {18, +4},
                {4, -3}, {9, -1}, {14, +2}, {19, +4},
                {5, -3}, {10, 0}, {15, +2}, {20, +5}
            };
        private int _value = 10;

        public Stat(string statName)
        {
            Name = statName;
        }

        public Stat(int score)
        {
            Value = score;
        }
    }
}
