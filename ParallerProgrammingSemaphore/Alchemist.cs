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
        public bool alchemistInLab;
        public int ingredientsToCollect;

        public Alchemist()
        {
            neededIngredients = new string[2];
            //collectedIngredients = new int[2] { 0, 0 };
            done = false;
            lfac = Program.leadFactory;
            mfac = Program.mercuryFactory;
            sfac = Program.sulfurFactory;
            alchemistInLab = false;
        }

        public void takeOneIngeredient(int[] array)
        {
            if (array[0] == 1 && array[1] == 1)
            {
                array[1] = 0;

            }
            else if (array[0] == 1 && array[1] == 0)
            {
                array[0] = 0;
            }
        }

        public void goToLab()
        {
            if (done == true)
            {
                alchemistInLab = true;
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
        public bool lead;
        public bool mercury;
        public AlchemistA() : base()
        {
            neededIngredients = new string[2] { "lead", "mercury" };
            //collectedIngredients = new int[2] { 0, 0 };
            neededIngredientsSemaphore = new Semaphore(0, 2);
            ingredientsToCollect = 2;
            lead = false;
            mercury = false;
        }

        public void collectIngredients()
        {
            int i = 0;
            while (alchemistInLab == false)
            {
                neededIngredientsSemaphore.WaitOne();
                lfac.storageAccessSemaphore.WaitOne();
                mfac.storageAccessSemaphore.WaitOne();
                Console.WriteLine("AlchemistA tries to collect ingredients");
                if (lfac.numberOfIngredients > 0 && mfac.numberOfIngredients > 0 && alchemistInLab == false)
                {
                    Console.WriteLine("AlchemistA collected all ingredients");
                    takeOneIngeredient(lfac.storage);
                    takeOneIngeredient(mfac.storage);
                    ingredientsToCollect = 0;
                    lfac.numberOfIngredients--;
                    mfac.numberOfIngredients--;
                    lead = true;
                    mercury = true;
                    alchemistInLab = true;
                    waitoneAlchemistSemaphore();
                    Program.guildA.numberOfAlchemistsInGuild--;
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
            if (Program.guildC.numberOfAlchemistsInGuild - 1 >= 0)
            {
                AlchemistC c = Program.guildC.guild[Program.guildC.numberOfAlchemistsInGuild - 1];
                if (c.lead == true)
                {
                    Console.WriteLine("Alchemists: C semaphore waited ");
                    c.neededIngredientsSemaphore.WaitOne();
                    c.lead = false;
                }
            }
            if (Program.guildB.numberOfAlchemistsInGuild - 1 >= 0)
            {
                AlchemistB b = Program.guildB.guild[Program.guildB.numberOfAlchemistsInGuild - 1];
                if (b.mercury == true)
                {
                    Console.WriteLine("Alchemists: B semaphore waited ");
                    b.neededIngredientsSemaphore.WaitOne();
                    b.mercury = false;
                }
            }
            if (Program.guildD.numberOfAlchemistsInGuild - 1 >= 0)
            {
                AlchemistD d = Program.guildD.guild[Program.guildD.numberOfAlchemistsInGuild - 1];
                if (d.lead == true)
                {
                    Console.WriteLine("Alchemists: D semaphore waited lead");
                    d.neededIngredientsSemaphore.WaitOne();
                    d.lead = false;
                }
                if (d.mercury == true)
                {
                    Console.WriteLine("Alchemists: D semaphore waited mercury");
                    d.neededIngredientsSemaphore.WaitOne();
                    d.mercury = false;
                }
            }
        }
    }

    public class AlchemistB : Alchemist
    {
        public bool mercury;
        public bool sulfur;

        public AlchemistB() : base()
        {
            neededIngredients = new string[2] { "mercury", "sulfur" };
            //collectedIngredients = new int[2] { 0, 0 };
            neededIngredientsSemaphore = new Semaphore(0, 2);
            ingredientsToCollect = 2;
            mercury = false;
            sulfur = false;
        }

        public void collectIngredients()
        {
            int i = 0;
            while (alchemistInLab == false)
            {
                neededIngredientsSemaphore.WaitOne();
                sfac.storageAccessSemaphore.WaitOne();
                mfac.storageAccessSemaphore.WaitOne();
                Console.WriteLine("AlchemistB tries to collect ingredients");
                if (sfac.numberOfIngredients > 0 && mfac.numberOfIngredients > 0 && alchemistInLab == false)
                {
                    Console.WriteLine("AlchemistB collected all ingredients");
                    takeOneIngeredient(sfac.storage);
                    takeOneIngeredient(mfac.storage);
                    waitoneAlchemistSemaphore();
                    ingredientsToCollect = 0;
                    mercury = true;
                    sulfur = true;
                    sfac.numberOfIngredients--;
                    mfac.numberOfIngredients--;
                    Program.guildB.numberOfAlchemistsInGuild--;
                    alchemistInLab = true;
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
            if (Program.guildA.numberOfAlchemistsInGuild - 1 >= 0)
            {
                AlchemistA a = Program.guildA.guild[Program.guildA.numberOfAlchemistsInGuild - 1];
                if (a.mercury == true)
                {
                    Console.WriteLine("Alchemists: A semaphore waited ");
                    a.neededIngredientsSemaphore.WaitOne();
                    a.mercury = false;
                }

            }
            if (Program.guildC.numberOfAlchemistsInGuild - 1 >= 0)
            {
                AlchemistC c = Program.guildC.guild[Program.guildC.numberOfAlchemistsInGuild - 1];
                if (c.sulfur == true)
                {
                    Console.WriteLine("Alchemists: C semaphore waited ");
                    c.neededIngredientsSemaphore.WaitOne();
                    c.sulfur = false;
                }
            }
            if (Program.guildD.numberOfAlchemistsInGuild - 1 >= 0)
            {
                AlchemistD d = Program.guildD.guild[Program.guildD.numberOfAlchemistsInGuild - 1];
                if (d.mercury == true)
                {
                    Console.WriteLine("Alchemists: D semaphore waited mercury");
                    d.neededIngredientsSemaphore.WaitOne();
                    d.mercury = false;
                }
                if (d.sulfur == true)
                {
                    Console.WriteLine("Alchemists: D semaphore waited sulfur");
                    d.neededIngredientsSemaphore.WaitOne();
                    d.sulfur = false;
                }

            }
        }
    }

    public class AlchemistC : Alchemist
    {
        public bool lead;
        public bool sulfur;

        public AlchemistC() : base()
        {
            neededIngredients = new string[2] { "lead", "sulfur" };
            //collectedIngredients = new int[2] { 0, 0 };
            neededIngredientsSemaphore = new Semaphore(0, 2);
            ingredientsToCollect = 2;
            lead = false;
            sulfur = false;
        }

        public void collectIngredients()
        {
            int i = 0;
            while (alchemistInLab == false)
            {
                neededIngredientsSemaphore.WaitOne();
                sfac.storageAccessSemaphore.WaitOne();
                lfac.storageAccessSemaphore.WaitOne();
                Console.WriteLine("AlchemistC tries to collect ingredients");
                if (sfac.numberOfIngredients > 0 && lfac.numberOfIngredients > 0 && alchemistInLab == false)
                {
                    Console.WriteLine("AlchemistC collected all ingredients");
                    takeOneIngeredient(sfac.storage);
                    takeOneIngeredient(lfac.storage);
                    waitoneAlchemistSemaphore();
                    ingredientsToCollect = 0;
                    sulfur = true;
                    lead = true;
                    sfac.numberOfIngredients--;
                    lfac.numberOfIngredients--;
                    Program.guildC.numberOfAlchemistsInGuild--;
                    alchemistInLab = true;
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
            if (Program.guildA.numberOfAlchemistsInGuild - 1 >= 0)
            {
                AlchemistA a = Program.guildA.guild[Program.guildA.numberOfAlchemistsInGuild - 1];
                if (a.lead == true)
                {
                    Console.WriteLine("Alchemists: A semaphore waited ");
                    a.neededIngredientsSemaphore.WaitOne();
                    a.lead = false;
                }
            }
            if (Program.guildB.numberOfAlchemistsInGuild - 1 >= 0)
            {
                AlchemistB b = Program.guildB.guild[Program.guildB.numberOfAlchemistsInGuild - 1];
                if (b.sulfur == true)
                {
                    Console.WriteLine("Alchemists: B semaphore waited ");
                    b.neededIngredientsSemaphore.WaitOne();
                    b.sulfur = false;
                }
            }
            if (Program.guildD.numberOfAlchemistsInGuild - 1 >= 0)
            {
                AlchemistD d = Program.guildD.guild[Program.guildD.numberOfAlchemistsInGuild - 1];
                if (d.lead == true)
                {
                    Console.WriteLine("Alchemists: D semaphore waited  lead");
                    d.neededIngredientsSemaphore.WaitOne();
                    d.lead = false;
                }
                if(d.sulfur == true)
                {
                    Console.WriteLine("Alchemists: D semaphore waited sulfur");
                    d.neededIngredientsSemaphore.WaitOne();
                    d.sulfur = false;
                }
            }
        }
    }

    public class AlchemistD : Alchemist
    {
        public bool mercury;
        public bool sulfur;
        public bool lead;

        public AlchemistD() : base()
        {
            neededIngredients = new string[3] { "mercury", "sulfur", "lead" };
            //collectedIngredients = new int[2] { 0, 0 };
            neededIngredientsSemaphore = new Semaphore(0, 3);
            ingredientsToCollect = 3;
            mercury = false;
            sulfur = false;
            lead = false;
        }

        public void collectIngredients()
        {
            int i = 0;
            while (alchemistInLab == false)
            {
                neededIngredientsSemaphore.WaitOne();
                sfac.storageAccessSemaphore.WaitOne();
                lfac.storageAccessSemaphore.WaitOne();
                mfac.storageAccessSemaphore.WaitOne();
                Console.WriteLine("AlchemistD tries to collect ingredients");
                if (sfac.numberOfIngredients > 0 && lfac.numberOfIngredients > 0 && mfac.numberOfIngredients > 0 && alchemistInLab == false)
                {
                    Console.WriteLine("AlchemistD collected all ingredients");
                    takeOneIngeredient(sfac.storage);
                    takeOneIngeredient(lfac.storage);
                    takeOneIngeredient(mfac.storage);
                    waitoneAlchemistSemaphore();
                    ingredientsToCollect = 0;
                    lead = true;
                    mercury = true;
                    sulfur = true;
                    sfac.numberOfIngredients--;
                    lfac.numberOfIngredients--;
                    mfac.numberOfIngredients--;
                    Program.guildD.numberOfAlchemistsInGuild--;
                    alchemistInLab = true;
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
            if (Program.guildA.numberOfAlchemistsInGuild - 1 >= 0)
            {
                AlchemistA a = Program.guildA.guild[Program.guildA.numberOfAlchemistsInGuild - 1];
                if(a.lead == true)
                {
                    Console.WriteLine("Alchemists: A semaphore waited lead ");
                    a.neededIngredientsSemaphore.WaitOne();
                    a.lead = false;
                }
                if(a.mercury == true)
                {
                    Console.WriteLine("Alchemists: A semaphore waited mercury");
                    a.neededIngredientsSemaphore.WaitOne();
                    a.mercury = false;
                }                  
            }
            if (Program.guildB.numberOfAlchemistsInGuild - 1 >= 0)
            {
                AlchemistB b = Program.guildB.guild[Program.guildB.numberOfAlchemistsInGuild - 1];
                if(b.mercury == true)
                {
                    Console.WriteLine("Alchemists: B semaphore waited mercury");
                    b.neededIngredientsSemaphore.WaitOne();
                    b.mercury = false;
                }
                if(b.sulfur == true)
                {
                    Console.WriteLine("Alchemists: B semaphore waited sulfur");
                    b.neededIngredientsSemaphore.WaitOne();
                    b.sulfur = false;
                }
            }
            if (Program.guildC.numberOfAlchemistsInGuild - 1 >= 0)
            {
                AlchemistC c = Program.guildC.guild[Program.guildC.numberOfAlchemistsInGuild - 1];
                if(c.lead == true)
                {
                    Console.WriteLine("Alchemists: C semaphore waited lead");
                    c.neededIngredientsSemaphore.WaitOne();
                    c.lead = false;
                }
                if(c.sulfur == true)
                {
                    Console.WriteLine("Alchemists: C semaphore waited sulfur");
                    c.neededIngredientsSemaphore.WaitOne();
                    c.sulfur = false;
                }                  
            }
        }
    }
    
}
