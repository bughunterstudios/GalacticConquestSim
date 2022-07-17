using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticConquestSim
{
    public class Universe
    {
        private Species[] species;
        private Planet[,] planets;
        private Colony[,] colonies;
        private Ship[,] ships;

        public int width, height;
        private int pixel_size;
        Random rand = new Random();

        private (Ship?, Colony?, Point, Point, Point, int)[,] plans;
        private bool[,] hasplan;

        public Universe(int width, int height, int pixel_size, int species_count, int planet_count)
        {
            rand = new Random();
            this.width = width / pixel_size;
            this.height = height / pixel_size;
            this.pixel_size = pixel_size;

            species = new Species[species_count];
            planets = new Planet[this.width, this.height];
            colonies = new Colony[this.width, this.height];
            ships = new Ship[this.width, this.height];

            for (int i = 0; i < species_count; i++)
            {
                species[i] = new Species(this, species_count, i);

                Point c = new Point(rand.Next(this.width), rand.Next(this.height));
                Planet template = new Planet();
                List<Point> possible_colonies = new List<Point>();

                int r = rand.Next(1, 5);
                for (int x = c.X - r; x <= c.X + r; x++)
                {
                    for (int y = c.Y - r; y <= c.Y + r; y++)
                    {
                        if ((x - c.X) * (x - c.X) + (y - c.Y) * (y - c.Y) <= r * r)
                        {
                            planets[CC(x, this.width), CC(y, this.height)] = new Planet(template);
                            possible_colonies.Add(new Point(CC(x, this.width), CC(y, this.height)));
                        }
                    }
                }

                Point chosen_coordinate = possible_colonies[rand.Next(possible_colonies.Count)];
                colonies[chosen_coordinate.X, chosen_coordinate.Y] =
                    new Colony(species[i],
                        planets[chosen_coordinate.X, chosen_coordinate.Y],
                        chosen_coordinate);
            }

            for (int j = 0; j < planet_count; j++)
            {
                Point c = new Point(rand.Next(this.width), rand.Next(this.height));
                Planet template = new Planet();

                int r = rand.Next(1, 5);
                for (int x = c.X - r; x <= c.X + r; x++)
                {
                    for (int y = c.Y - r; y <= c.Y + r; y++)
                    {
                        if ((x - c.X) * (x - c.X) + (y - c.Y) * (y - c.Y) <= r * r)
                        {
                            planets[CC(x, this.width), CC(y, this.height)] = new Planet(template);
                        }
                    }
                }
            }

            this.pixel_size = pixel_size;

            plans = new (Ship?, Colony?, Point, Point, Point, int)[width, height];
            hasplan = new bool[width, height];
        }

        private Color GetCol(int x, int y)
        {
            if (ships[x, y] != null)
            {
                if (ships[x, y].shoot)
                    return Color.White;
                return ships[x, y].species.color;
            }

            if (colonies[x, y] != null)
            {
                if (colonies[x, y].population < 1)
                    return Color.White;
                return colonies[x, y].species.color;
            }

            if (planets[x, y] != null)
                return planets[x, y].color;

            return Color.Black;
        }

        public void DrawBoard(DirectBitmap drawarea, PictureBox picturebox)
        {
            //for (int x = 0; x < width; x++)
            if (height < width)
            {
                Parallel.For(0, width, x =>
                {
                    for (int y = 0; y < height; y++)
                    {
                        for (int x_s = 0; x_s < pixel_size; x_s++)
                        {
                            for (int y_s = 0; y_s < pixel_size; y_s++)
                                drawarea.SetPixel((x * pixel_size) + x_s, (y * pixel_size) + y_s, GetCol(x, y));
                        }
                    }
                });
            }
            else
            {
                Parallel.For(0, height, y =>
                {
                    for (int x = 0; x < width; x++)
                    {
                        for (int x_s = 0; x_s < pixel_size; x_s++)
                        {
                            for (int y_s = 0; y_s < pixel_size; y_s++)
                                drawarea.SetPixel((x * pixel_size) + x_s, (y * pixel_size) + y_s, GetCol(x, y));
                        }
                    }
                });
            }
            picturebox.Image = drawarea.Bitmap;
        }

        public Bitmap SaveBoard()
        {
            Bitmap newimage = new Bitmap(width, height);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    newimage.SetPixel(x, y, GetCol(x, y));
                }
            }
            return newimage;
        }

        private (Planet, int[,], int[,]) CountNeighbors(int x, int y)
        {
            int[,] colony_neighbors = new int[3, 3];
            int[,] ship_neighbors = new int[3, 3];

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Colony colony_spot = colonies[CC(x + i - 1, width), CC(y + j - 1, height)];
                    colony_neighbors[i, j] = colony_spot == null ? -1 : colony_spot.species.index;

                    Ship ship_spot = ships[CC(x + i - 1, width), CC(y + j - 1, height)];
                    ship_neighbors[i, j] = ship_spot == null ? -1 : ship_spot.species.index;
                    if (i == 1 && j == 1)
                        ship_neighbors[i, j] = -1;
                }
            }

            return (planets[x, y], colony_neighbors, ship_neighbors);
        }

        private int CC(int c, int max)
        {
            if (c < 0)
                return max - 1;
            if (c >= max)
                return 0;
            return c;
        }

        private Point CC(Point p)
        {
            return new Point(CC(p.X, width), CC(p.Y, height));
        }

        public void Step()
        {
            foreach (Species s in species)
            {
                s.Step();
            }

            if (height < width)
            {
                Parallel.For(0, width, x =>
                {
                    for (int y = 0; y < height; y++)
                    {
                        if (colonies[x, y] != null || ships[x, y] != null)
                        {
                            plans[x, y] = StepInterior(x, y);
                            hasplan[x, y] = true;
                        }
                        if (planets[x, y] != null)
                            planets[x, y].Step();
                    }
                });
            }
            else
            {
                Parallel.For(0, height, y =>
                {
                    for (int x = 0; x < width; x++)
                    {
                        if (colonies[x, y] != null || ships[x, y] != null)
                        {
                            plans[x, y] = StepInterior(x, y);
                            hasplan[x, y] = true;
                        }
                        if (planets[x, y] != null)
                            planets[x, y].Step();
                    }
                });
            }

            //foreach ((Ship? newship, Colony? newcolony, Point attackpoint, Point newshiplocation, Point originallocation) in plans)
            Parallel.For(0, width, x =>
            {
                for (int y = 0; y < height; y++)
                {
                    if (hasplan[x, y])
                    {
                        (Ship? newship, Colony? newcolony, Point attackpoint, Point newshiplocation, Point originallocation, int shots) = plans[x, y];
                        if (newship != null)
                        {
                            ships[(int)newship.x, (int)newship.y] = newship;
                        }
                        else
                        {
                            if (newcolony != null)
                            {
                                colonies[newcolony.location.X, newcolony.location.Y] = newcolony;
                                ships[newcolony.location.X, newcolony.location.Y] = null;
                            }
                            else
                            {
                                if (!attackpoint.IsEmpty)
                                {
                                    if (ships[CC(attackpoint).X, CC(attackpoint).Y] != null)
                                        ships[CC(attackpoint).X, CC(attackpoint).Y] = null;
                                    else
                                    {
                                        if (colonies[CC(attackpoint).X, CC(attackpoint).Y] != null)
                                        {
                                            if (!colonies[CC(attackpoint).X, CC(attackpoint).Y].Attack(shots))
                                                colonies[CC(attackpoint).X, CC(attackpoint).Y] = null;
                                        }
                                    }
                                }

                                if (ships[CC(newshiplocation).X, CC(newshiplocation).Y] == null)
                                {
                                    ships[CC(newshiplocation).X, CC(newshiplocation).Y] = ships[originallocation.X, originallocation.Y];
                                    ships[originallocation.X, originallocation.Y] = null;
                                }
                            }
                        }

                        hasplan[x, y] = false;
                    }
                }
            });
        }

        // Ship1 is a newly built ship by a colony
        // Colony is weather a colony should be made by a ship
        // Point1 is shooting location by a ship
        // Point2 is new ship location
        // Point3 is the original position of the moving ship
        // int is number of shots
        public (Ship?, Colony?, Point, Point, Point, int) StepInterior(int x, int y)
        {
            Ship? newship = null;
            Colony? newcolony = null;
            Point shootlocation = Point.Empty;
            Point newshiplocation = Point.Empty;
            int shots = 0;

            if (colonies[x, y] != null)
            {
                newship = colonies[x, y].Step(ships[x, y] == null);

                if (colonies[x, y].population <= 0)
                    colonies[x, y] = null;
            }

            if (ships[x, y] != null)
            {
                Planet planet;
                int[,] colony_neighbors;
                int[,] ship_neighbors;

                (planet, colony_neighbors, ship_neighbors) = CountNeighbors(x, y);

                (newcolony, shootlocation, newshiplocation, shots) = ships[x, y].Step(planet, colony_neighbors, ship_neighbors, ships);

                if (ships[x, y].age > rand.Next(400, 1000) * 2)
                    ships[x, y] = null;
            }

            return (newship, newcolony, shootlocation, newshiplocation, new Point(x, y), shots);
        }
    }
}
