using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallerProgrammingSemaphore
{
    public class Alchemist
    {
        public string[] neededIngredients;
        //public int[] collectedIngredients;
        public bool done;
        public Semaphore neededIngredientsSemaphore;
        public FactoryLead lfac;
        public FactoryMercury mfac;
        public FactorySulfur sfac;

        public Alchemist()
        {
            neededIngredients = new string[2];
            //collectedIngredients = new int[2] { 0, 0 };
            done = false;
            lfac = Program.leadFactory;
            mfac = Program.mercuryFactory;
            sfac = Program.sulfurFactory;
        }

        public void takeOneIngeredient(int[] array)
        {
            if(array[0] == 1 && array[1] == 1 )
            {
                array[1] = 0;
                Console.WriteLine("Second ingredient collected");

            }
            else if (array[0] == 1 && array[1] == 0)
            {
                array[0] = 0;
                Console.WriteLine("First ingredient collected");
            }
        }

        public void goToLab()
        {
            if (done == true)
            {
                Thread.Sleep(50000);
                Console.WriteLine("Alchemist went to lab");
            }
        }

        public int getRandomTimeInterval()
        {
            int time = 0;
            Random rnd = new Random();
            time = rnd.Next(1000, 5000);

            return time;
        }
    }

    public class AlchemistA : Alchemist
    {
        public AlchemistA() : base()
        {
            neededIngredients = new string[2] { "lead", "mercury" };
            //collectedIngredients = new int[2] { 0, 0 };
            neededIngredientsSemaphore = new Semaphore(0,2); 
        }

        public void collectIngredients()
        {
            int i = 0;
            while (i < 10)
            {
                neededIngredientsSemaphore.WaitOne();
                lfac.storageAccessSemaphore.WaitOne();
                mfac.storageAccessSemaphore.WaitOne();
                Console.WriteLine("AlchemistA tries to collect ingredients");
                if (lfac.numberOfIngredients > 0 && mfac.numberOfIngredients > 0)
                {
                    Console.WriteLine("AlchemistA collected all ingredients");
                    takeOneIngeredient(lfac.storage);
                    takeOneIngeredient(mfac.storage);
                    waitoneAlchemistSemaphore();
                    lfac.numberOfIngredients--;
                    mfac.numberOfIngredients--;
                    Program.guildA.numberOfAlchemistsInGuild--;
                    done = true;
                }
                mfac.storageAccessSemaphore.Release();
                lfac.storageAccessSemaphore.Release();
                neededIngredientsSemaphore.Release();

                goToLab();
                i++;
                Thread.Sleep(1000);
            }
        }

        public void waitoneAlchemistSemaphore()
        {
            AlchemistC c = Program.guildC.guild[Program.guildC.numberOfAlchemistsInGuild-1];
            AlchemistB b = Program.guildB.guild[Program.guildB.numberOfAlchemistsInGuild-1];
            AlchemistD d = Program.guildD.guild[Program.guildD.numberOfAlchemistsInGuild-1];

            c.neededIngredientsSemaphore.WaitOne();
            b.neededIngredientsSemaphore.WaitOne();
            d.neededIngredientsSemaphore.WaitOne();
            d.neededIngredientsSemaphore.WaitOne();
        }
    }

    public class AlchemistB : Alchemist
    {
        public AlchemistB() : base()
        {
            neededIngredients = new string[2] { "mercury", "sulfur" };
            //collectedIngredients = new int[2] { 0, 0 };
            neededIngredientsSemaphore = new Semaphore(0, 2);
        }

        public void collectIngredients()
        {
            int i = 0;
            while (i < 10)
            {
                neededIngredientsSemaphore.WaitOne();
                sfac.storageAccessSemaphore.WaitOne();
                mfac.storageAccessSemaphore.WaitOne();
                Console.WriteLine("AlchemistB tries to collect ingredients");
                if (sfac.numberOfIngredients > 0 && mfac.numberOfIngredients > 0)
                {
                    Console.WriteLine("AlchemistB collected all ingredients");
                    takeOneIngeredient(sfac.storage);
                    takeOneIngeredient(mfac.storage);
                    waitoneAlchemistSemaphore();
                    sfac.numberOfIngredients--;
                    mfac.numberOfIngredients--;
                    Program.guildB.numberOfAlchemistsInGuild--;
                    done = true;
                }
                mfac.storageAccessSemaphore.Release();
                sfac.storageAccessSemaphore.Release();
                neededIngredientsSemaphore.Release();

                goToLab();
                i++;
                Thread.Sleep(1000);
            }
        }

        public void waitoneAlchemistSemaphore()
        {
            AlchemistA a = Program.guildA.guild[Program.guildA.numberOfAlchemistsInGuild - 1];
            AlchemistC c = Program.guildC.guild[Program.guildC.numberOfAlchemistsInGuild - 1];
            AlchemistD d = Program.guildD.guild[Program.guildD.numberOfAlchemistsInGuild - 1];

            c.neededIngredientsSemaphore.WaitOne();
            c.neededIngredientsSemaphore.WaitOne();
            d.neededIngredientsSemaphore.WaitOne();
            d.neededIngredientsSemaphore.WaitOne();
        }
    }

    public class AlchemistC : Alchemist
    {
        public AlchemistC() : base()
        {
            neededIngredients = new string[2] { "lead", "sulfur" };
            //collectedIngredients = new int[2] { 0, 0 };
            neededIngredientsSemaphore = new Semaphore(0, 2);
        }

        public void collectIngredients()
        {
            int i = 0;
            while (i < 10)
            {
                neededIngredientsSemaphore.WaitOne();
                sfac.storageAccessSemaphore.WaitOne();
                lfac.storageAccessSemaphore.WaitOne();
                Console.WriteLine("AlchemistC tries to collect ingredients");
                if (sfac.numberOfIngredients > 0 && lfac.numberOfIngredients > 0)
                {
                    Console.WriteLine("AlchemistC collected all ingredients");
                    takeOneIngeredient(sfac.storage);
                    takeOneIngeredient(lfac.storage);
                    waitoneAlchemistSemaphore();
                    sfac.numberOfIngredients--;
                    lfac.numberOfIngredients--;
                    Program.guildC.numberOfAlchemistsInGuild--;
                    done = true;
                }
                lfac.storageAccessSemaphore.Release();
                sfac.storageAccessSemaphore.Release();
                neededIngredientsSemaphore.Release();

                goToLab();
                i++;
                Thread.Sleep(1000);
            }
        }

        public void waitoneAlchemistSemaphore()
        {
            AlchemistA a = Program.guildA.guild[Program.guildA.numberOfAlchemistsInGuild - 1];
            AlchemistB b = Program.guildB.guild[Program.guildB.numberOfAlchemistsInGuild - 1];
            AlchemistD d = Program.guildD.guild[Program.guildD.numberOfAlchemistsInGuild - 1];

            a.neededIngredientsSemaphore.WaitOne();
            b.neededIngredientsSemaphore.WaitOne();
            d.neededIngredientsSemaphore.WaitOne();
            d.neededIngredientsSemaphore.WaitOne();
        }
    }

    public class AlchemistD : Alchemist
    {
        public AlchemistD() : base()
        {
            neededIngredients = new string[3] { "mercury", "sulfur", "lead" };
            //collectedIngredients = new int[2] { 0, 0 };
            neededIngredientsSemaphore = new Semaphore(0, 3);
        }

        public void collectIngredients()
        {
            int i = 0;
            while (i < 10)
            {
                neededIngredientsSemaphore.WaitOne();
                sfac.storageAccessSemaphore.WaitOne();
                lfac.storageAccessSemaphore.WaitOne();
                mfac.storageAccessSemaphore.WaitOne();
                Console.WriteLine("AlchemistD tries to collect ingredients");
                if (sfac.numberOfIngredients > 0 && lfac.numberOfIngredients > 0 && mfac.numberOfIngredients > 0)
                {
                    Console.WriteLine("AlchemistD collected all ingredients");
                    takeOneIngeredient(sfac.storage);
                    takeOneIngeredient(lfac.storage);
                    takeOneIngeredient(mfac.storage);
                    sfac.numberOfIngredients--;
                    lfac.numberOfIngredients--;
                    mfac.numberOfIngredients--;
                    Program.guildD.numberOfAlchemistsInGuild--;
                    done = true;
                }
                mfac.storageAccessSemaphore.Release();
                lfac.storageAccessSemaphore.Release();
                sfac.storageAccessSemaphore.Release();
                neededIngredientsSemaphore.Release();

                goToLab();
                i++;
                Thread.Sleep(1000);
            }
        }

        public void waitoneAlchemistSemaphore()
        {
            AlchemistA a = Program.guildA.guild[Program.guildA.numberOfAlchemistsInGuild - 1];
            AlchemistB b = Program.guildB.guild[Program.guildB.numberOfAlchemistsInGuild - 1];
            AlchemistC c = Program.guildC.guild[Program.guildC.numberOfAlchemistsInGuild - 1];

            a.neededIngredientsSemaphore.WaitOne();
            a.neededIngredientsSemaphore.WaitOne();
            b.neededIngredientsSemaphore.WaitOne();
            b.neededIngredientsSemaphore.WaitOne();
            c.neededIngredientsSemaphore.WaitOne();
            c.neededIngredientsSemaphore.WaitOne();
        }
    }
}
