using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticConquestSim
{
    public class Species
    {
        public int violence_chance;
        public int min_weapons;
        public int max_weapons;
        public int expand_chance;
        public float speed;
        public bool[] enemies;
        public Color color;
        public int index;

        public Universe universe;
        private Random rand = new Random();

        public Species(Universe universe, int total_species, int index)
        {
            this.universe = universe;
            rand = new Random();
            violence_chance = rand.Next(1, 100);
            max_weapons = rand.Next(1, 10);
            min_weapons = rand.Next(0, max_weapons);
            expand_chance = rand.Next(10, 100);
            speed = (float) rand.NextDouble() * 2f;
            enemies = new bool[total_species];

            color = Color.FromArgb(rand.Next(50, 150), rand.Next(50, 150), rand.Next(50, 150));

            this.index = index;
        }

        public void Change(int chance, int amount)
        {
            if (rand.Next(100) < chance)
                violence_chance = Math.Clamp(violence_chance + ((rand.Next(100) < 50 ? -1 : 1) * amount), 1, 100);
            if (rand.Next(100) < chance)
                expand_chance = Math.Clamp(expand_chance + ((rand.Next(100) < 50 ? -1 : 1) * amount), 10, 100);
        }

        public void Step()
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i] = rand.Next(100) < violence_chance;
            }
            enemies[index] = false;
        }
    }
}
