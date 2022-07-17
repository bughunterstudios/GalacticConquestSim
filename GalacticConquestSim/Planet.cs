using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticConquestSim
{
    public class Planet
    {
        public int ship_resource;
        public int ship_resource_total;
        public int population_resource;
        public int population_resource_total;
        public int weapon_resource;
        public int weapon_resource_total;
        public int change_chance;
        public int change_percentage;
        public float speed_change;
        public Color color;

        public Planet()
        {
            Random rand = new Random();
            ship_resource = Math.Max(rand.Next(0, 20), 1);
            ship_resource_total = rand.Next(5000);
            population_resource = Math.Max(rand.Next(0, 5), 1);
            population_resource_total = rand.Next(3000);
            weapon_resource = Math.Max(rand.Next(0, 20), 1);
            weapon_resource_total = rand.Next(5000);
            change_chance = Math.Max(rand.Next(-40, 5), 0);
            change_percentage = rand.Next(3);
            speed_change = MathF.Max((float) rand.NextDouble() - 0.9f, 0f) / 50f;
            
            color = Color.FromArgb(rand.Next(100, 200), rand.Next(100, 200), rand.Next(100, 200));
        }

        public Planet(Planet copy)
        {
            ship_resource = copy.ship_resource;
            ship_resource_total = copy.ship_resource_total;
            population_resource = copy.population_resource;
            population_resource_total = copy.population_resource_total;
            weapon_resource = copy.weapon_resource;
            weapon_resource_total = copy.weapon_resource_total;
            change_chance = copy.change_chance;
            change_percentage = copy.change_percentage;
            speed_change = copy.speed_change;
            color = copy.color;
        }

        public void Step()
        {
            if (population_resource_total <= 0 || ship_resource_total <= 0 || weapon_resource_total <= 0)
                color = Color.DimGray;
        }
    }
}
