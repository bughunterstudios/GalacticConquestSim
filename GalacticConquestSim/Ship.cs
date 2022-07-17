using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticConquestSim
{
    public class Ship
    {
        public Species species;
        public int weapons;
        public float x;
        public float y;
        public float direction;
        public bool shoot;
        public int age;

        private float speed;
        private Random rand;

        public Ship(Species species, int weapons, int x, int y)
        {
            this.species = species;
            this.weapons = weapons;
            this.x = x;
            this.y = y;
            rand = new Random();
            direction = (float) rand.NextDouble() * 2f * MathF.PI;
            age = 0;
            speed = species.speed;
        }

        public Point Coordinates()
        {
            return new Point((int) x, (int) y);
        }

        private Point Move(Ship[,] ships)
        {
            float new_x = x + MathF.Cos(direction) * speed;
            if (new_x < 0)
                new_x += species.universe.width;
            if (new_x >= species.universe.width)
                new_x -= species.universe.width;
            float new_y = y + MathF.Sin(direction) * speed;
            if (new_y < 0)
                new_y += species.universe.height;
            if (new_y >= species.universe.height)
                new_y -= species.universe.height;

            if (ships[(int)new_x, (int)new_y] != null && ships[(int)new_x, (int)new_y] != this)
            {
                direction = (float)rand.NextDouble() * 2f * MathF.PI;
                return Coordinates();
            }

            x = new_x;
            y = new_y;

            return Coordinates();
        }

        // Colony shows weather the ship is destroyed and a colony created
        // Point shows an attack direction between (-1, -1) and (1, 1).
        // Second point shows the coordinates of the ship.
        // int is shots.
        public (Colony?, Point, Point, int) Step(Planet planet, int[,] colony_neighbors, int[,] ship_neighbors, Ship[,] ships)
        {
            shoot = false;
            age++;

            if (planet != null && colony_neighbors[1, 1] == -1 && rand.Next(100) < species.expand_chance)
            {
                if (planet.population_resource_total > 0)
                    return (new Colony(species, planet, Coordinates()), Point.Empty, Coordinates(), 0);
                else
                {
                    if (planet.weapon_resource_total > 0)
                    {
                        weapons++;
                        planet.weapon_resource_total--;
                    }
                }
            }

            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    if (colony_neighbors[x, y] != -1 && species.enemies[colony_neighbors[x, y]])
                    {
                        if (rand.Next(100) < species.violence_chance && weapons > 0)
                        {
                            shoot = true;
                            int shots = rand.Next(1, weapons);
                            weapons -= shots;
                            return (null, new Point((int) this.x + x - 1, (int) this.y + y - 1), Coordinates(), shots);
                        }
                    }

                    if (ship_neighbors[x, y] != -1 && species.enemies[ship_neighbors[x, y]])
                    {
                        if (rand.Next(100) < species.violence_chance && weapons > 0)
                        {
                            shoot = true;
                            weapons--;
                            return (null, new Point((int)this.x + x - 1, (int) this.y + y - 1), Coordinates(), 1);
                        }
                    }
                }
            }

            return (null, Point.Empty, Move(ships), 0);
        }
    }
}
