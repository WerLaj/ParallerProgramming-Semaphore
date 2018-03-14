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
        public int n;
        public int curses;
        public int[] storage;
        public int numberOfIngredients;
        public string factoryName;
        public string product;
        public Semaphore curseBinarySemaphore;
        public Semaphore cursesSemaphore;
        public Semaphore storageAccessSemaphore;
        public Semaphore storageSemaphore;
        

        public Factory()
        {
            n = 1;
            curses = 0;
            storage = new[] { 0, 0 };
            numberOfIngredients = 0;
            factoryName = "";
            product = "";
            curseBinarySemaphore = new Semaphore(1, 1);
            cursesSemaphore = new Semaphore(1, 1);
            storageAccessSemaphore = new Semaphore(1, 1);
            storageSemaphore = new Semaphore(2, 2);
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
            while (true)
            {
                storageSemaphore.WaitOne();

                int time = getRandomTimeInterval();
                Thread.Sleep(time); //kosztowna operacja w semaforze

                cursesSemaphore.WaitOne();
                storageAccessSemaphore.WaitOne();
                Program.alchemistChoice.WaitOne();

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

                Program.alchemistChoice.Release();
                storageAccessSemaphore.Release();
                cursesSemaphore.Release();
            }
        }

        public void releaseAlchemistSemaphore()
        {
            if (Program.guildD.numberOfAlchemistsInGuild > 0)
            {
                //AlchemistD d = (AlchemistD)Program.guildD.guild.Peek();
                if (Program.mercuryFactory.numberOfIngredients > 0)
                {
                    if(Program.leadFactory.numberOfIngredients > 0 && Program.sulfurFactory.numberOfIngredients > 0)
                    {
                        Console.WriteLine("AlchemistD " + Program.guildD.numberOfAlchemistsInGuild + " semaphore released ");
                        Program.guildD.guildSemaphore.Release();
                        Program.sulfurFactory.numberOfIngredients--;
                        Program.leadFactory.numberOfIngredients--;
                        Program.mercuryFactory.numberOfIngredients--;
                        return;
                    }
                }
            }
            else
            {
                Random rnd = new Random();
                int n = rnd.Next(1, 2);

                if (n % 2 == 0)
                {
                    if (Program.guildA.numberOfAlchemistsInGuild > 0)
                    {
                        //AlchemistA a = (AlchemistA)Program.guildA.guild.Peek();
                        if (Program.mercuryFactory.numberOfIngredients > 0)
                        {
                            if (Program.leadFactory.numberOfIngredients > 0)
                            {
                                Console.WriteLine("AlchemistA " + Program.guildA.numberOfAlchemistsInGuild + " semaphore released");
                                Program.guildA.guildSemaphore.Release();
                                Program.leadFactory.numberOfIngredients--;
                                Program.mercuryFactory.numberOfIngredients--;
                                return;
                            }                           
                        }
                    }
                    else
                    {
                        if (Program.guildB.numberOfAlchemistsInGuild - 1 >= 0)
                        {
                            //AlchemistB b = (AlchemistB)Program.guildB.guild.Peek();
                            if (Program.mercuryFactory.numberOfIngredients > 0)
                            {
                                if (Program.sulfurFactory.numberOfIngredients > 0)
                                {
                                    Console.WriteLine("AlchemistB " + Program.guildB.numberOfAlchemistsInGuild + " semaphore released");
                                    Program.guildB.guildSemaphore.Release();
                                    Program.sulfurFactory.numberOfIngredients--;
                                    Program.mercuryFactory.numberOfIngredients--;
                                    return;
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (Program.guildB.numberOfAlchemistsInGuild - 1 >= 0)
                    {
                        //AlchemistB b = (AlchemistB)Program.guildB.guild.Peek();
                        if (Program.mercuryFactory.numberOfIngredients > 0)
                        {
                            if (Program.sulfurFactory.numberOfIngredients > 0)
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
                        if (Program.guildA.numberOfAlchemistsInGuild > 0)
                        {
                            //AlchemistA a = (AlchemistA)Program.guildA.guild.Peek();
                            if (Program.mercuryFactory.numberOfIngredients > 0)
                            {
                                if (Program.leadFactory.numberOfIngredients > 0)
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
        }

        //public void releaseAlchemistSemaphore()
        //{
        //    if (Program.guildA.numberOfAlchemistsInGuild - 1 >= 0)
        //    {
        //        AlchemistA a = Program.guildA.guild[Program.guildA.numberOfAlchemistsInGuild - 1];
        //        if (a.mercury == false)
        //        {
        //            //Console.WriteLine("AlchemistA " + Program.guildA.numberOfAlchemistsInGuild + " semaphore released");
        //            a.neededIngredientsSemaphore.Release();
        //            a.mercury = true;
        //        }
        //    }
        //    if (Program.guildB.numberOfAlchemistsInGuild - 1 >= 0)
        //    {
        //        AlchemistB b = Program.guildB.guild[Program.guildB.numberOfAlchemistsInGuild - 1];
        //        if (b.mercury == false)
        //        {
        //            //Console.WriteLine("AlchemistB " + Program.guildB.numberOfAlchemistsInGuild + " semaphore released");
        //            b.neededIngredientsSemaphore.Release();
        //            b.mercury = true;
        //        }
        //    }

        //    if (Program.guildD.numberOfAlchemistsInGuild - 1 >= 0)
        //    {
        //        AlchemistD d = Program.guildD.guild[Program.guildD.numberOfAlchemistsInGuild - 1];
        //        if (d.mercury == false)
        //        {
        //            //Console.WriteLine("AlchemistD " + Program.guildD.numberOfAlchemistsInGuild + " semaphore released ");
        //            d.neededIngredientsSemaphore.Release();
        //            d.mercury = true;
        //        }
        //    }

        //}
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
            while (true)
            {
                storageSemaphore.WaitOne();

                int time = getRandomTimeInterval();
                Thread.Sleep(time); //kosztowna operacja w semaforze

                cursesSemaphore.WaitOne();

                storageAccessSemaphore.WaitOne();
                Program.alchemistChoice.WaitOne();
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
                Program.alchemistChoice.Release();
                storageAccessSemaphore.Release();

                cursesSemaphore.Release();
            }
        }

        public void releaseAlchemistSemaphore()
        {
            if (Program.guildD.numberOfAlchemistsInGuild - 1 >= 0)
            {
               // AlchemistD d = (AlchemistD)Program.guildD.guild.Peek();
                if (Program.leadFactory.numberOfIngredients > 0)
                {
                    if (Program.mercuryFactory.numberOfIngredients > 0 && Program.sulfurFactory.numberOfIngredients > 0)
                    {
                        Console.WriteLine("AlchemistD " + Program.guildD.numberOfAlchemistsInGuild + " semaphore released ");
                        Program.guildD.guildSemaphore.Release();
                        Program.sulfurFactory.numberOfIngredients--;
                        Program.leadFactory.numberOfIngredients--;
                        Program.mercuryFactory.numberOfIngredients--;
                        return;
                    }
                }
            }
            else
            {
                Random rnd = new Random();
                int n = rnd.Next(1, 2);

                if (n % 2 == 0)
                {
                    if (Program.guildA.numberOfAlchemistsInGuild - 1 >= 0)
                    {
                        //AlchemistA a = (AlchemistA)Program.guildA.guild.Peek();
                        if (Program.leadFactory.numberOfIngredients > 0)
                        {
                            if (Program.mercuryFactory.numberOfIngredients > 0)
                            {
                                Console.WriteLine("AlchemistA " + Program.guildA.numberOfAlchemistsInGuild + " semaphore released");
                                Program.guildA.guildSemaphore.Release();
                                Program.leadFactory.numberOfIngredients--;
                                Program.mercuryFactory.numberOfIngredients--;
                                return;
                            }                          
                        }
                    }
                    else
                    {
                        if (Program.guildC.numberOfAlchemistsInGuild - 1 >= 0)
                        {
                            //AlchemistC b = (AlchemistC)Program.guildC.guild.Peek();
                            if (Program.leadFactory.numberOfIngredients > 0)
                            {
                                if (Program.sulfurFactory.numberOfIngredients > 0)
                                {
                                    Console.WriteLine("AlchemistC " + Program.guildB.numberOfAlchemistsInGuild + " semaphore released");
                                    Program.guildC.guildSemaphore.Release();
                                    Program.sulfurFactory.numberOfIngredients--;
                                    Program.leadFactory.numberOfIngredients--;
                                    return;
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (Program.guildC.numberOfAlchemistsInGuild - 1 >= 0)
                    {
                        //AlchemistC b = (AlchemistC)Program.guildC.guild.Peek();
                        if (Program.leadFactory.numberOfIngredients > 0)
                        {
                            if (Program.sulfurFactory.numberOfIngredients > 0)
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
                        if (Program.guildA.numberOfAlchemistsInGuild - 1 >= 0)
                        {
                           // AlchemistA a = (AlchemistA)Program.guildA.guild.Peek();
                            if (Program.leadFactory.numberOfIngredients > 0)
                            {
                                if (Program.mercuryFactory.numberOfIngredients > 0)
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
        }

        //public void releaseAlchemistSemaphore()
        //{
        //    if (Program.guildA.numberOfAlchemistsInGuild - 1 >= 0)
        //    {
        //        AlchemistA a = Program.guildA.guild[Program.guildA.numberOfAlchemistsInGuild - 1];
        //        if (a.lead == false)
        //        {
        //            //Console.WriteLine("AlchemistA " + Program.guildA.numberOfAlchemistsInGuild + " semaphore released ");
        //            a.neededIngredientsSemaphore.Release();
        //            a.lead = true;
        //        }
        //    }
        //    if (Program.guildC.numberOfAlchemistsInGuild - 1 >= 0)
        //    {
        //        AlchemistC c = Program.guildC.guild[Program.guildC.numberOfAlchemistsInGuild - 1];
        //        if (c.lead == false)
        //        {
        //            //Console.WriteLine("AlchemistC " + Program.guildC.numberOfAlchemistsInGuild + " semaphore released");
        //            c.neededIngredientsSemaphore.Release();
        //            c.lead = true;
        //        }
        //    }
        //    if (Program.guildD.numberOfAlchemistsInGuild - 1 >= 0)
        //    {
        //        AlchemistD d = Program.guildD.guild[Program.guildD.numberOfAlchemistsInGuild - 1];
        //        if (d.lead == false)
        //        {
        //            //Console.WriteLine("AlchemistD " + Program.guildD.numberOfAlchemistsInGuild + " semaphore released ");
        //            d.neededIngredientsSemaphore.Release();
        //            d.lead = true;
        //        }
        //    } 
        //}
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
            while (true)
            {
                storageSemaphore.WaitOne();

                int time = getRandomTimeInterval();
                Thread.Sleep(time); //kosztowna operacja w semaforze

                cursesSemaphore.WaitOne();

                storageAccessSemaphore.WaitOne();
                Program.alchemistChoice.WaitOne();
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
                Program.alchemistChoice.Release();
                storageAccessSemaphore.Release();

                cursesSemaphore.Release();
            }
        }

        public void releaseAlchemistSemaphore()
        {
            if (Program.guildD.numberOfAlchemistsInGuild - 1 >= 0)
            {
                //AlchemistD d = (AlchemistD)Program.guildD.guild.Peek();
                if (Program.sulfurFactory.numberOfIngredients > 0)
                {
                    if (Program.leadFactory.numberOfIngredients > 0 && Program.mercuryFactory.numberOfIngredients > 0)
                    {
                        Console.WriteLine("AlchemistD " + Program.guildD.numberOfAlchemistsInGuild + " semaphore released ");
                        Program.guildD.guildSemaphore.Release();
                        Program.sulfurFactory.numberOfIngredients--;
                        Program.leadFactory.numberOfIngredients--;
                        Program.mercuryFactory.numberOfIngredients--;
                        return;
                    }
                }
            }
            else
            {
                Random rnd = new Random();
                int n = rnd.Next(1, 2);

                if (n % 2 == 0)
                {
                    if (Program.guildC.numberOfAlchemistsInGuild - 1 >= 0)
                    {
                        //AlchemistC a = (AlchemistC)Program.guildC.guild.Peek();
                        if (Program.sulfurFactory.numberOfIngredients > 0)
                        {
                            if (Program.leadFactory.numberOfIngredients > 0)
                            {
                                Console.WriteLine("AlchemistC " + Program.guildA.numberOfAlchemistsInGuild + " semaphore released");
                                Program.guildC.guildSemaphore.Release();
                                Program.sulfurFactory.numberOfIngredients--;
                                Program.leadFactory.numberOfIngredients--;
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (Program.guildB.numberOfAlchemistsInGuild - 1 >= 0)
                        {
                            //AlchemistB b = (AlchemistB)Program.guildB.guild.Peek();
                            if (Program.sulfurFactory.numberOfIngredients > 0)
                            {
                                if (Program.mercuryFactory.numberOfIngredients > 0)
                                {
                                    Console.WriteLine("AlchemistB " + Program.guildB.numberOfAlchemistsInGuild + " semaphore released");
                                    Program.guildB.guildSemaphore.Release();
                                    Program.sulfurFactory.numberOfIngredients--;
                                    Program.mercuryFactory.numberOfIngredients--;
                                    return;
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (Program.guildB.numberOfAlchemistsInGuild - 1 >= 0)
                    {
                        //AlchemistB b = (AlchemistB)Program.guildB.guild.Peek();
                        if (Program.sulfurFactory.numberOfIngredients > 0)
                        {
                            if (Program.mercuryFactory.numberOfIngredients > 0)
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
                        if (Program.guildC.numberOfAlchemistsInGuild - 1 >= 0)
                        {
                            //AlchemistC a = (AlchemistC)Program.guildC.guild.Peek();
                            if (Program.sulfurFactory.numberOfIngredients > 0)
                            {
                                if (Program.leadFactory.numberOfIngredients > 0)
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

        //public void releaseAlchemistSemaphore()
        //{
        //    if (Program.guildC.numberOfAlchemistsInGuild - 1 >= 0)
        //    {
        //        AlchemistC c = Program.guildC.guild[Program.guildC.numberOfAlchemistsInGuild - 1];
        //        if (c.sulfur == false)
        //        {
        //            //Console.WriteLine("AlchemistC " + Program.guildC.numberOfAlchemistsInGuild + " semaphore released ");
        //            c.neededIngredientsSemaphore.Release();
        //            c.sulfur = true;
        //        }
        //    }

        //    if (Program.guildB.numberOfAlchemistsInGuild - 1 >= 0)
        //    {
        //        AlchemistB b = Program.guildB.guild[Program.guildB.numberOfAlchemistsInGuild - 1];
        //        if (b.sulfur == false)
        //        {
        //            //Console.WriteLine("AlchemistB " + Program.guildB.numberOfAlchemistsInGuild + " semaphore released ");
        //            b.neededIngredientsSemaphore.Release();
        //            b.sulfur = true;
        //        }
        //    }

        //    if (Program.guildD.numberOfAlchemistsInGuild - 1 >= 0)
        //    {
        //        AlchemistD d = Program.guildD.guild[Program.guildD.numberOfAlchemistsInGuild - 1];
        //        if (d.sulfur == false)
        //        {
        //            //Console.WriteLine("AlchemistD " + Program.guildD.numberOfAlchemistsInGuild + " semaphore released ");
        //            d.neededIngredientsSemaphore.Release();
        //            d.sulfur = true;
        //        }
        //    }
        //}
    }
}
