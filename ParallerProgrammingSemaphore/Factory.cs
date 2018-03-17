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
        public Semaphore curseBinarySemaphore;
        public Semaphore cursesSemaphore;
        public Semaphore storageAccessSemaphore;
        public Semaphore storageSemaphore;
        

        public Factory()
        {
            curses = 0;
            storage = new[] { 0, 0 };
            numberOfIngredients = 0;
            factoryName = "";
            curseBinarySemaphore = new Semaphore(1, 1);
            cursesSemaphore = new Semaphore(1, 1);
            storageAccessSemaphore = new Semaphore(1, 1);
            storageSemaphore = new Semaphore(2, 2);
        }

        public int getRandomTimeInterval()
        {
            int time = 0;
            Random rnd = new Random();
            time = rnd.Next(1000, 5000);

            return time;
        }

    }

    public class FactoryMercury : Factory
    {
        public FactoryMercury() : base()
        {
            factoryName = "Mercury Factory";
        }

        public void produce()
        {
            while (true)
            {
                storageSemaphore.WaitOne();

                int time = getRandomTimeInterval();
                Thread.Sleep(time);

                cursesSemaphore.WaitOne();
                storageAccessSemaphore.WaitOne();
                Program.alchemistChoice.WaitOne();

                if (storage[0] == 0)
                {

                    storage[0] = 1;
                    Console.WriteLine(factoryName + "[" + storage[0] + "," + storage[1] + "]");
                    Program.mercuryFactory.numberOfIngredients++;
                    releaseAlchemistSemaphore();
                }
                else if (storage[0] == 1 && storage[1] == 0)
                {

                    storage[1] = 1;
                    Console.WriteLine(factoryName + "[" + storage[0] + "," + storage[1] + "]");
                    Program.mercuryFactory.numberOfIngredients++;
                    releaseAlchemistSemaphore();
                }

                Program.alchemistChoice.Release();
                storageAccessSemaphore.Release();
                cursesSemaphore.Release();
            }
        }

        public void releaseAlchemistSemaphore()
        {
            if (Program.guildD.numberOfAlchemistsInGuild > 0 && Program.mercuryFactory.numberOfIngredients > 0 && Program.leadFactory.numberOfIngredients > 0 && Program.sulfurFactory.numberOfIngredients > 0)
            {                
                Console.WriteLine("AlchemistD " + Program.guildD.numberOfAlchemistsInGuild + " semaphore released ");
                Program.guildD.guildSemaphore.Release();
                Program.sulfurFactory.numberOfIngredients--;
                Program.leadFactory.numberOfIngredients--;
                Program.mercuryFactory.numberOfIngredients--;
                return;
            }
            else
            {
                Random rnd = new Random();
                int n = rnd.Next(1, 2);

                if (n % 2 == 0)
                {
                    if (Program.guildA.numberOfAlchemistsInGuild > 0 && Program.mercuryFactory.numberOfIngredients > 0 && Program.leadFactory.numberOfIngredients > 0)
                    {
                        Console.WriteLine("AlchemistA " + Program.guildA.numberOfAlchemistsInGuild + " semaphore released");
                        Program.guildA.guildSemaphore.Release();
                        Program.leadFactory.numberOfIngredients--;
                        Program.mercuryFactory.numberOfIngredients--;
                        return;
                    }
                    else
                    {
                        if (Program.guildB.numberOfAlchemistsInGuild - 1 >= 0 && Program.mercuryFactory.numberOfIngredients > 0 && Program.sulfurFactory.numberOfIngredients > 0)
                        {
                            Console.WriteLine("AlchemistB " + Program.guildB.numberOfAlchemistsInGuild + " semaphore released");
                            Program.guildB.guildSemaphore.Release();
                            Program.sulfurFactory.numberOfIngredients--;
                            Program.mercuryFactory.numberOfIngredients--;
                            return;
                        }
                    }
                }
                else
                {
                    if (Program.guildB.numberOfAlchemistsInGuild - 1 >= 0 && Program.mercuryFactory.numberOfIngredients > 0 && Program.sulfurFactory.numberOfIngredients > 0)
                    {
                        Console.WriteLine("AlchemistB " + Program.guildB.numberOfAlchemistsInGuild + " semaphore released");
                        Program.guildB.guildSemaphore.Release();
                        Program.sulfurFactory.numberOfIngredients--;
                        Program.mercuryFactory.numberOfIngredients--;
                        return;
                    }
                    else
                    {
                        if (Program.guildA.numberOfAlchemistsInGuild > 0 && Program.mercuryFactory.numberOfIngredients > 0 && Program.leadFactory.numberOfIngredients > 0)
                        {
                            Console.WriteLine("AlchemistA " + Program.guildA.numberOfAlchemistsInGuild + " semaphore released");
                            Program.guildA.guildSemaphore.Release();
                            Program.leadFactory.numberOfIngredients--;
                            Program.mercuryFactory.numberOfIngredients--;
                            return;
                        }
                    }
                }
            }
        }
    }

    public class FactoryLead : Factory
    {
        public FactoryLead() : base()
        {
            factoryName = "Lead Factory";
        }

        public void produce()
        {
            while (true)
            {
                storageSemaphore.WaitOne();

                int time = getRandomTimeInterval();
                Thread.Sleep(time);

                cursesSemaphore.WaitOne();
                storageAccessSemaphore.WaitOne();
                Program.alchemistChoice.WaitOne();

                if (storage[0] == 0)
                {
                    storage[0] = 1;
                    Console.WriteLine(factoryName + "[" + storage[0] + "," + storage[1] + "]");
                    Program.leadFactory.numberOfIngredients++;
                    releaseAlchemistSemaphore();

                }
                else if (storage[0] == 1 && storage[1] == 0)
                {

                    storage[1] = 1;
                    Console.WriteLine(factoryName + "[" + storage[0] + "," + storage[1] + "]");
                    Program.leadFactory.numberOfIngredients++;
                    releaseAlchemistSemaphore();
                }

                Program.alchemistChoice.Release();
                storageAccessSemaphore.Release();
                cursesSemaphore.Release();
            }
        }

        public void releaseAlchemistSemaphore()
        {
            if (Program.guildD.numberOfAlchemistsInGuild > 0 && Program.mercuryFactory.numberOfIngredients > 0 && Program.leadFactory.numberOfIngredients > 0 && Program.sulfurFactory.numberOfIngredients > 0)
            {
                Console.WriteLine("AlchemistD " + Program.guildD.numberOfAlchemistsInGuild + " semaphore released ");
                Program.guildD.guildSemaphore.Release();
                Program.sulfurFactory.numberOfIngredients--;
                Program.leadFactory.numberOfIngredients--;
                Program.mercuryFactory.numberOfIngredients--;
                return;
            }
            else
            {
                Random rnd = new Random();
                int n = rnd.Next(1, 2);

                if (n % 2 == 0)
                {
                    if (Program.guildA.numberOfAlchemistsInGuild > 0 && Program.mercuryFactory.numberOfIngredients > 0 && Program.leadFactory.numberOfIngredients > 0)
                    {
                        Console.WriteLine("AlchemistA " + Program.guildA.numberOfAlchemistsInGuild + " semaphore released");
                        Program.guildA.guildSemaphore.Release();
                        Program.leadFactory.numberOfIngredients--;
                        Program.mercuryFactory.numberOfIngredients--;
                        return;
                    }
                    else
                    {
                        if (Program.guildC.numberOfAlchemistsInGuild > 0 && Program.leadFactory.numberOfIngredients > 0 && Program.sulfurFactory.numberOfIngredients > 0)
                        {
                            Console.WriteLine("AlchemistC " + Program.guildB.numberOfAlchemistsInGuild + " semaphore released");
                            Program.guildC.guildSemaphore.Release();
                            Program.sulfurFactory.numberOfIngredients--;
                            Program.leadFactory.numberOfIngredients--;
                            return;
                        }
                    }
                }
                else
                {
                    if (Program.guildC.numberOfAlchemistsInGuild > 0 && Program.leadFactory.numberOfIngredients > 0 && Program.sulfurFactory.numberOfIngredients > 0)
                    {
                        Console.WriteLine("AlchemistC " + Program.guildB.numberOfAlchemistsInGuild + " semaphore released");
                        Program.guildC.guildSemaphore.Release();
                        Program.sulfurFactory.numberOfIngredients--;
                        Program.leadFactory.numberOfIngredients--;
                        return;
                    }
                    else
                    {
                        if (Program.guildA.numberOfAlchemistsInGuild > 0 && Program.mercuryFactory.numberOfIngredients > 0 && Program.leadFactory.numberOfIngredients > 0)
                        {
                            Console.WriteLine("AlchemistA " + Program.guildA.numberOfAlchemistsInGuild + " semaphore released");
                            Program.guildA.guildSemaphore.Release();
                            Program.leadFactory.numberOfIngredients--;
                            Program.mercuryFactory.numberOfIngredients--;
                            return;
                        }
                    }
                }
            }
        }
    }

    public class FactorySulfur : Factory
    {
        public FactorySulfur() : base()
        {
            factoryName = "Sulfur Factory";
        }

        public void produce()
        {
            while (true)
            {
                storageSemaphore.WaitOne();

                int time = getRandomTimeInterval();
                Thread.Sleep(time); 

                cursesSemaphore.WaitOne();

                storageAccessSemaphore.WaitOne();
                Program.alchemistChoice.WaitOne();
                if (storage[0] == 0)
                {

                    storage[0] = 1;
                    Console.WriteLine(factoryName + "[" + storage[0] + "," + storage[1] + "]");
                    Program.sulfurFactory.numberOfIngredients++;
                    releaseAlchemistSemaphore();

                }
                else if (storage[0] == 1 && storage[1] == 0)
                {

                    storage[1] = 1;
                    Console.WriteLine(factoryName + "[" + storage[0] + "," + storage[1] + "]");
                    Program.sulfurFactory.numberOfIngredients++;
                    releaseAlchemistSemaphore();
                }
                Program.alchemistChoice.Release();
                storageAccessSemaphore.Release();

                cursesSemaphore.Release();
            }
        }

        public void releaseAlchemistSemaphore()
        {
            if (Program.guildD.numberOfAlchemistsInGuild > 0 && Program.mercuryFactory.numberOfIngredients > 0 && Program.leadFactory.numberOfIngredients > 0 && Program.sulfurFactory.numberOfIngredients > 0)
            {
                Console.WriteLine("AlchemistD " + Program.guildD.numberOfAlchemistsInGuild + " semaphore released ");
                Program.guildD.guildSemaphore.Release();
                Program.sulfurFactory.numberOfIngredients--;
                Program.leadFactory.numberOfIngredients--;
                Program.mercuryFactory.numberOfIngredients--;
                return;
            }
            else
            {
                Random rnd = new Random();
                int n = rnd.Next(1, 2);

                if (n % 2 == 0)
                {
                    if (Program.guildC.numberOfAlchemistsInGuild > 0 && Program.leadFactory.numberOfIngredients > 0 && Program.sulfurFactory.numberOfIngredients > 0)
                    {
                        Console.WriteLine("AlchemistC " + Program.guildA.numberOfAlchemistsInGuild + " semaphore released");
                        Program.guildC.guildSemaphore.Release();
                        Program.sulfurFactory.numberOfIngredients--;
                        Program.leadFactory.numberOfIngredients--;
                        return;
                    }
                    else
                    {
                        if (Program.guildB.numberOfAlchemistsInGuild - 1 >= 0 && Program.mercuryFactory.numberOfIngredients > 0 && Program.sulfurFactory.numberOfIngredients > 0)
                        {
                            Console.WriteLine("AlchemistB " + Program.guildB.numberOfAlchemistsInGuild + " semaphore released");
                            Program.guildB.guildSemaphore.Release();
                            Program.sulfurFactory.numberOfIngredients--;
                            Program.mercuryFactory.numberOfIngredients--;
                            return;
                        }
                    }
                }
                else
                {
                    if (Program.guildB.numberOfAlchemistsInGuild - 1 >= 0 && Program.mercuryFactory.numberOfIngredients > 0 && Program.sulfurFactory.numberOfIngredients > 0)
                    {
                        Console.WriteLine("AlchemistB " + Program.guildB.numberOfAlchemistsInGuild + " semaphore released");
                        Program.guildB.guildSemaphore.Release();
                        Program.sulfurFactory.numberOfIngredients--;
                        Program.mercuryFactory.numberOfIngredients--;
                        return;
                    }
                    else
                    {
                        if (Program.guildC.numberOfAlchemistsInGuild > 0 && Program.leadFactory.numberOfIngredients > 0 && Program.sulfurFactory.numberOfIngredients > 0)
                        {
                            Console.WriteLine("AlchemistC " + Program.guildA.numberOfAlchemistsInGuild + " semaphore released");
                            Program.guildC.guildSemaphore.Release();
                            Program.sulfurFactory.numberOfIngredients--;
                            Program.leadFactory.numberOfIngredients--;
                            return;
                        }
                    }
                }
            }
        }
    }
}
