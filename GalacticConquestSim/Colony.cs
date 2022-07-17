using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticConquestSim
{
    public class Colony
    {
        public Species species;
        public Planet planet;
        public int ships;
        public int population;
        public int weapons;
        public Point location;

        private Random rand;

        public Colony(Species species, Planet planet, Point location)
        {
            this.species = species;
            ships = 0;
            population = 1;
            weapons = 0;
            this.planet = planet;
            this.location = location;
            rand = new Random();

            species.speed += planet.speed_change;
            species.speed = Math.Min(species.speed, 2);
        }

        // Returns whether a new ship is built
        public Ship? Step(bool noship)
        {
            population--;

            if (planet.ship_resource_total >= 0 && planet.population_resource_total >= 0 && planet.weapon_resource_total >= 0)
            {
                ships += planet.ship_resource;
                planet.ship_resource_total -= planet.ship_resource;

                population += planet.population_resource;
                planet.population_resource_total -= planet.population_resource;

                weapons += planet.weapon_resource;
                planet.weapon_resource_total -= planet.weapon_resource;
            }

            species.Change(planet.change_chance, planet.change_percentage);

            if (rand.Next(100) < species.expand_chance && noship && ships > 0 && weapons >= species.min_weapons && population > 1)
            {
                int weapons_for_ship = rand.Next(species.min_weapons, Math.Clamp(weapons, 0, species.max_weapons));
                weapons -= weapons_for_ship;
                ships--;
                population--;
                return new Ship(species, weapons_for_ship, location.X, location.Y);
            }

            return null;
        }

        // Return true if still alive
        public bool Attack(int shots)
        {
            population -= shots;
            return population > 0;
        }
    }
}
