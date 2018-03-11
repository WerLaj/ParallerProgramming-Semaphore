using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallerProgrammingSemaphore
{
    public class Factory
    {
        public int curses;
        public int[] storage;
        public int numberOfIngredients;
        public string factoryName;
        public string product;
        public Semaphore curseBinarySemaphore;
        public Semaphore storageAccessSemaphore;

        public Factory()
        {
            curses = 0;
            storage = new[] { 0, 0 };
            numberOfIngredients = 0;
            factoryName = "";
            product = "";
            curseBinarySemaphore = new Semaphore(1, 1);
            //cursesSemaphore = new Semaphore(0, 5);
            storageAccessSemaphore = new Semaphore(1, 1);
        }

        public int getRandomTimeInterval()
        {
            int time = 0;
            Random rnd = new Random();
            time = rnd.Next(1000, 3000);

            return time;
        }
    }

    public class FactoryMercury : Factory
    {
        public FactoryMercury() : base()
        {
            factoryName = "Mercury Factory";
            product = "mercury";
        }

        public void produce()
        {
            int i = 0;
            do
            {
                int time = getRandomTimeInterval();
                Thread.Sleep(time);

                storageAccessSemaphore.WaitOne();
                if (storage[0] == 0)
                {
                    
                    storage[0] = 1;
                    Console.WriteLine(factoryName + "[" + storage[0] + "," + storage[1] + "]");
                    numberOfIngredients = 1;
                    releaseAlchemistSemaphore();
                }
                else if (storage[0] == 1 && storage[1] == 0)
                {
                    
                    storage[1] = 1;
                    Console.WriteLine(factoryName + "[" + storage[0] + "," + storage[1] + "]");
                    numberOfIngredients = 2;
                    releaseAlchemistSemaphore();
                }
                else
                {
                    Console.WriteLine(factoryName + " can't produce because storage is full");
                }
                storageAccessSemaphore.Release();
                i++;
            } while (i < 5);
        }

        public void releaseAlchemistSemaphore()
        {
            if (Program.guildA.numberOfAlchemistsInGuild - 1 >= 0)
            {
                AlchemistA a = Program.guildA.guild[Program.guildA.numberOfAlchemistsInGuild - 1];
                if (a.mercury == false)
                {
                    Console.WriteLine("AlchemistA " + Program.guildA.numberOfAlchemistsInGuild + " semaphore released");
                    a.neededIngredientsSemaphore.Release();
                    a.mercury = true;
                }
            }
            if (Program.guildB.numberOfAlchemistsInGuild - 1 >= 0)
            {
                AlchemistB b = Program.guildB.guild[Program.guildB.numberOfAlchemistsInGuild - 1];
                if (b.mercury == false)
                {
                    Console.WriteLine("AlchemistB " + Program.guildB.numberOfAlchemistsInGuild + " semaphore released");
                    b.neededIngredientsSemaphore.Release();
                    b.mercury = true;
                }
            }

            if (Program.guildD.numberOfAlchemistsInGuild - 1 >= 0)
            {
                AlchemistD d = Program.guildD.guild[Program.guildD.numberOfAlchemistsInGuild - 1];
                if (d.mercury == false)
                {
                    Console.WriteLine("AlchemistD " + Program.guildD.numberOfAlchemistsInGuild + " semaphore released ");
                    d.neededIngredientsSemaphore.Release();
                    d.mercury = true;
                }
            }
            
        }
    }

    public class FactoryLead : Factory
    {
        public FactoryLead() : base()
        {
            factoryName = "Lead Factory";
            product = "lead";
        }

        public void produce()
        {
            int i = 0;
            do
            {
                int time = getRandomTimeInterval();
                Thread.Sleep(time);

                storageAccessSemaphore.WaitOne();
                if (storage[0] == 0)
                {
                    
                    storage[0] = 1;
                    Console.WriteLine(factoryName + "[" + storage[0] + "," + storage[1] + "]");
                    numberOfIngredients = 1;
                    releaseAlchemistSemaphore();

                }
                else if (storage[0] == 1 && storage[1] == 0)
                {
                    
                    storage[1] = 1;
                    Console.WriteLine(factoryName + "[" + storage[0] + "," + storage[1] + "]");
                    numberOfIngredients = 2;
                    releaseAlchemistSemaphore();
                }
                else
                {
                    Console.WriteLine(factoryName + " can't produce because storage is full");
                }
                storageAccessSemaphore.Release();
                i++;
            } while (i < 5);
        }

        public void releaseAlchemistSemaphore()
        {
            if (Program.guildA.numberOfAlchemistsInGuild - 1 >= 0)
            {
                AlchemistA a = Program.guildA.guild[Program.guildA.numberOfAlchemistsInGuild - 1];
                if (a.lead == false)
                {
                    Console.WriteLine("AlchemistA " + Program.guildA.numberOfAlchemistsInGuild + " semaphore released ");
                    a.neededIngredientsSemaphore.Release();
                    a.lead = true;
                }
            }
            if (Program.guildC.numberOfAlchemistsInGuild - 1 >= 0)
            {
                AlchemistC c = Program.guildC.guild[Program.guildC.numberOfAlchemistsInGuild - 1];
                if (c.lead == false)
                {
                    Console.WriteLine("AlchemistC " + Program.guildC.numberOfAlchemistsInGuild + " semaphore released");
                    c.neededIngredientsSemaphore.Release();
                    c.lead = true;
                }
            }
            if (Program.guildD.numberOfAlchemistsInGuild - 1 >= 0)
            {
                AlchemistD d = Program.guildD.guild[Program.guildD.numberOfAlchemistsInGuild - 1];
                if (d.lead == false)
                {
                    Console.WriteLine("AlchemistD " + Program.guildD.numberOfAlchemistsInGuild + " semaphore released ");
                    d.neededIngredientsSemaphore.Release();
                    d.lead = true;
                }
            } 
        }
    }

    public class FactorySulfur : Factory
    {
        public FactorySulfur() : base()
        {
            factoryName = "Sulfur Factory";
            product = "sulfur";
        }

        public void produce()
        {
            int i = 0;
            do
            {
                int time = getRandomTimeInterval();
                Thread.Sleep(time);

                storageAccessSemaphore.WaitOne();
                if (storage[0] == 0)
                {
                    
                    storage[0] = 1;
                    Console.WriteLine(factoryName + "[" + storage[0] + "," + storage[1] + "]");
                    numberOfIngredients = 1;
                    releaseAlchemistSemaphore();

                }
                else if (storage[0] == 1 && storage[1] == 0)
                {
                    
                    storage[1] = 1;
                    Console.WriteLine(factoryName + "[" + storage[0] + "," + storage[1] + "]");
                    numberOfIngredients = 2;
                    releaseAlchemistSemaphore();
                }
                else
                {
                    Console.WriteLine(factoryName + " can't produce because storage is full");
                }
                storageAccessSemaphore.Release();
                i++;
            } while (i < 5);
        }

        public void releaseAlchemistSemaphore()
        {
            if (Program.guildC.numberOfAlchemistsInGuild - 1 >= 0)
            {
                AlchemistC c = Program.guildC.guild[Program.guildC.numberOfAlchemistsInGuild - 1];
                if (c.sulfur == false)
                {
                    Console.WriteLine("AlchemistC " + Program.guildC.numberOfAlchemistsInGuild + " semaphore released ");
                    c.neededIngredientsSemaphore.Release();
                    c.sulfur = true;
                }
            }

            if (Program.guildB.numberOfAlchemistsInGuild - 1 >= 0)
            {
                AlchemistB b = Program.guildB.guild[Program.guildB.numberOfAlchemistsInGuild - 1];
                if (b.sulfur == false)
                {
                    Console.WriteLine("AlchemistB " + Program.guildB.numberOfAlchemistsInGuild + " semaphore released ");
                    b.neededIngredientsSemaphore.Release();
                    b.sulfur = true;
                }
            }

            if (Program.guildD.numberOfAlchemistsInGuild - 1 >= 0)
            {
                AlchemistD d = Program.guildD.guild[Program.guildD.numberOfAlchemistsInGuild - 1];
                if (d.sulfur == false)
                {
                    Console.WriteLine("AlchemistD " + Program.guildD.numberOfAlchemistsInGuild + " semaphore released ");
                    d.neededIngredientsSemaphore.Release();
                    d.sulfur = true;
                }
            } 
        }
    }
}
