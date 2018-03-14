using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallerProgrammingSemaphore
{
    //--------------------------------
    //one semaphore for each guild instead of one semaphore for each alchemist
    //--------------------------------
    public class Alchemist
    {
        public string[] neededIngredients;
        //public int[] collectedIngredients;
        public bool done;
        //public Semaphore neededIngredientsSemaphore;
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
        public AlchemistA() : base()
        {
            neededIngredients = new string[2] { "lead", "mercury" };
            //collectedIngredients = new int[2] { 0, 0 };
            //neededIngredientsSemaphore = new Semaphore(0, 1);
            ingredientsToCollect = 2;
        }

        public void collectIngredients()
        {
            Program.guildA.guildSemaphore.WaitOne();
            lfac.storageAccessSemaphore.WaitOne();
            mfac.storageAccessSemaphore.WaitOne();
            Console.WriteLine("AlchemistA " + Program.guildA.numberOfAlchemistsInGuild + "  tries to collect ingredients");
            if (lfac.storage[0]==1 && mfac.storage[0] == 1 && alchemistInLab == false)
            {
                Console.WriteLine("AlchemistA " + Program.guildA.numberOfAlchemistsInGuild + "  collected all ingredients");
                takeOneIngeredient(lfac.storage);
                takeOneIngeredient(mfac.storage);
                ingredientsToCollect = 0;
                alchemistInLab = true;
                //waitoneAlchemistSemaphore();
                Program.guildA.guild.Dequeue();
                Program.guildA.numberOfAlchemistsInGuild--;
                Console.WriteLine("GuildA " + Program.guildA.numberOfAlchemistsInGuild + "  dequeued");
                lfac.storageSemaphore.Release();
                mfac.storageSemaphore.Release();
            }
            else
            {
                Console.WriteLine("==================");
            }
            mfac.storageAccessSemaphore.Release();
            lfac.storageAccessSemaphore.Release();
            //neededIngredientsSemaphore.Release();
           
        }

        //public void waitoneAlchemistSemaphore()
        //{
        //    if (Program.guildC.numberOfAlchemistsInGuild - 1 >= 0)
        //    {
        //        AlchemistC c = Program.guildC.guild[Program.guildC.numberOfAlchemistsInGuild - 1];
        //        if (c.lead == true)
        //        {
        //            //Console.WriteLine("Alchemists: C semaphore waited ");
        //            c.neededIngredientsSemaphore.WaitOne();
        //            c.lead = false;
        //        }
        //    }
        //    if (Program.guildB.numberOfAlchemistsInGuild - 1 >= 0)
        //    {
        //        AlchemistB b = Program.guildB.guild[Program.guildB.numberOfAlchemistsInGuild - 1];
        //        if (b.mercury == true)
        //        {
        //            //Console.WriteLine("Alchemists: B semaphore waited ");
        //            b.neededIngredientsSemaphore.WaitOne();
        //            b.mercury = false;
        //        }
        //    }
        //    if (Program.guildD.numberOfAlchemistsInGuild - 1 >= 0)
        //    {
        //        AlchemistD d = Program.guildD.guild[Program.guildD.numberOfAlchemistsInGuild - 1];
        //        if (d.lead == true)
        //        {
        //            //Console.WriteLine("Alchemists: D semaphore waited lead");
        //            d.neededIngredientsSemaphore.WaitOne();
        //            d.lead = false;
        //        }
        //        if (d.mercury == true)
        //        {
        //            //Console.WriteLine("Alchemists: D semaphore waited mercury");
        //            d.neededIngredientsSemaphore.WaitOne();
        //            d.mercury = false;
        //        }
        //    }
        //}
    }

    public class AlchemistB : Alchemist
    { 

        public AlchemistB() : base()
        {
            neededIngredients = new string[2] { "mercury", "sulfur" };
            //collectedIngredients = new int[2] { 0, 0 };
            //neededIngredientsSemaphore = new Semaphore(0, 1);
            ingredientsToCollect = 2;
        }

        public void collectIngredients()
        {
            Program.guildB.guildSemaphore.WaitOne();
            //neededIngredientsSemaphore.WaitOne();
            sfac.storageAccessSemaphore.WaitOne();
            mfac.storageAccessSemaphore.WaitOne();
            Console.WriteLine("AlchemistB " + Program.guildB.numberOfAlchemistsInGuild + "  tries to collect ingredients");
            if (sfac.storage[0] == 1 && mfac.storage[0] == 1 && alchemistInLab == false)
            {
                Console.WriteLine("AlchemistB " + Program.guildB.numberOfAlchemistsInGuild + "  collected all ingredients");
                takeOneIngeredient(sfac.storage);
                takeOneIngeredient(mfac.storage);
                //waitoneAlchemistSemaphore();
                ingredientsToCollect = 0;
                sfac.numberOfIngredients--;
                mfac.numberOfIngredients--;
                Program.guildB.numberOfAlchemistsInGuild--;
                alchemistInLab = true;
                Program.guildB.guild.Dequeue();
                Console.WriteLine("GuildB " + Program.guildB.numberOfAlchemistsInGuild + "  dequeued");
                sfac.storageSemaphore.Release();
                mfac.storageSemaphore.Release();
            }
            else
            {
                Console.WriteLine("==================");
            }
            mfac.storageAccessSemaphore.Release();
            sfac.storageAccessSemaphore.Release();
            //neededIngredientsSemaphore.Release();
            
        }

        //public void waitoneAlchemistSemaphore()
        //{
        //    if (Program.guildA.numberOfAlchemistsInGuild - 1 >= 0)
        //    {
        //        AlchemistA a = Program.guildA.guild[Program.guildA.numberOfAlchemistsInGuild - 1];
        //        if (a.mercury == true)
        //        {
        //            //Console.WriteLine("Alchemists: A semaphore waited ");
        //            a.neededIngredientsSemaphore.WaitOne();
        //            a.mercury = false;
        //        }

        //    }
        //    if (Program.guildC.numberOfAlchemistsInGuild - 1 >= 0)
        //    {
        //        AlchemistC c = Program.guildC.guild[Program.guildC.numberOfAlchemistsInGuild - 1];
        //        if (c.sulfur == true)
        //        {
        //            //Console.WriteLine("Alchemists: C semaphore waited ");
        //            c.neededIngredientsSemaphore.WaitOne();
        //            c.sulfur = false;
        //        }
        //    }
        //    if (Program.guildD.numberOfAlchemistsInGuild - 1 >= 0)
        //    {
        //        AlchemistD d = Program.guildD.guild[Program.guildD.numberOfAlchemistsInGuild - 1];
        //        if (d.mercury == true)
        //        {
        //            //Console.WriteLine("Alchemists: D semaphore waited mercury");
        //            d.neededIngredientsSemaphore.WaitOne();
        //            d.mercury = false;
        //        }
        //        if (d.sulfur == true)
        //        {
        //            //Console.WriteLine("Alchemists: D semaphore waited sulfur");
        //            d.neededIngredientsSemaphore.WaitOne();
        //            d.sulfur = false;
        //        }

        //    }
        //}
    }

    public class AlchemistC : Alchemist
    {
        public AlchemistC() : base()
        {
            neededIngredients = new string[2] { "lead", "sulfur" };
            //collectedIngredients = new int[2] { 0, 0 };
            //neededIngredientsSemaphore = new Semaphore(0, 1);
            ingredientsToCollect = 2;
        }

        public void collectIngredients()
    {
            Program.guildC.guildSemaphore.WaitOne();
            //neededIngredientsSemaphore.WaitOne();
            sfac.storageAccessSemaphore.WaitOne();
            lfac.storageAccessSemaphore.WaitOne();
            Console.WriteLine("AlchemistC " + Program.guildC.numberOfAlchemistsInGuild + " tries to collect ingredients");
            if (lfac.storage[0] == 1 && sfac.storage[0] == 1 && alchemistInLab == false)
            {
                Console.WriteLine("AlchemistC " + Program.guildC.numberOfAlchemistsInGuild + "  collected all ingredients");
                takeOneIngeredient(sfac.storage);
                takeOneIngeredient(lfac.storage);
                //waitoneAlchemistSemaphore();
                ingredientsToCollect = 0;
                sfac.numberOfIngredients--;
                lfac.numberOfIngredients--;
                Program.guildC.numberOfAlchemistsInGuild--;
                alchemistInLab = true;
                Program.guildC.guild.Dequeue();
                Console.WriteLine("GuildC " + Program.guildC.numberOfAlchemistsInGuild + "  dequeued");
                lfac.storageSemaphore.Release();
                sfac.storageSemaphore.Release();
            }
            else
            {
                Console.WriteLine("==================");
            }
            lfac.storageAccessSemaphore.Release();
            sfac.storageAccessSemaphore.Release();
            //neededIngredientsSemaphore.Release();
        }

        //public void waitoneAlchemistSemaphore()
        //{
        //    if (Program.guildA.numberOfAlchemistsInGuild - 1 >= 0)
        //    {
        //        AlchemistA a = Program.guildA.guild[Program.guildA.numberOfAlchemistsInGuild - 1];
        //        if (a.lead == true)
        //        {
        //            //Console.WriteLine("Alchemists: A semaphore waited ");
        //            a.neededIngredientsSemaphore.WaitOne();
        //            a.lead = false;
        //        }
        //    }
        //    if (Program.guildB.numberOfAlchemistsInGuild - 1 >= 0)
        //    {
        //        AlchemistB b = Program.guildB.guild[Program.guildB.numberOfAlchemistsInGuild - 1];
        //        if (b.sulfur == true)
        //        {
        //            //Console.WriteLine("Alchemists: B semaphore waited ");
        //            b.neededIngredientsSemaphore.WaitOne();
        //            b.sulfur = false;
        //        }
        //    }
        //    if (Program.guildD.numberOfAlchemistsInGuild - 1 >= 0)
        //    {
        //        AlchemistD d = Program.guildD.guild[Program.guildD.numberOfAlchemistsInGuild - 1];
        //        if (d.lead == true)
        //        {
        //            //Console.WriteLine("Alchemists: D semaphore waited  lead");
        //            d.neededIngredientsSemaphore.WaitOne();
        //            d.lead = false;
        //        }
        //        if(d.sulfur == true)
        //        {
        //            //Console.WriteLine("Alchemists: D semaphore waited sulfur");
        //            d.neededIngredientsSemaphore.WaitOne();
        //            d.sulfur = false;
        //        }
        //    }
        //}
    }

    public class AlchemistD : Alchemist
    {
        public AlchemistD() : base()
        {
            neededIngredients = new string[3] { "mercury", "sulfur", "lead" };
            //collectedIngredients = new int[2] { 0, 0 };
            //neededIngredientsSemaphore = new Semaphore(0, 1);
            ingredientsToCollect = 3;
        }

        public void collectIngredients()
        {
            Program.guildD.guildSemaphore.WaitOne();
            //neededIngredientsSemaphore.WaitOne();
            sfac.storageAccessSemaphore.WaitOne();
            lfac.storageAccessSemaphore.WaitOne();
            mfac.storageAccessSemaphore.WaitOne();
            Console.WriteLine("AlchemistD " + Program.guildD.numberOfAlchemistsInGuild + "  tries to collect ingredients");
            if (lfac.storage[0] == 1 && mfac.storage[0] == 1 && sfac.storage[0] == 1 &&  alchemistInLab == false)
            {
                Console.WriteLine("AlchemistD " + Program.guildD.numberOfAlchemistsInGuild + "  collected all ingredients");
                takeOneIngeredient(sfac.storage);
                takeOneIngeredient(lfac.storage);
                takeOneIngeredient(mfac.storage);
                //waitoneAlchemistSemaphore();
                ingredientsToCollect = 0;
                sfac.numberOfIngredients--;
                lfac.numberOfIngredients--;
                mfac.numberOfIngredients--;
                Program.guildD.numberOfAlchemistsInGuild--;
                alchemistInLab = true;
                Program.guildD.guild.Dequeue();
                Console.WriteLine("GuildD " + Program.guildD.numberOfAlchemistsInGuild + "  dequeued");
                sfac.storageSemaphore.Release();
                mfac.storageSemaphore.Release();
                lfac.storageSemaphore.Release();
            }
            else
            {
                Console.WriteLine("==================");
            }
            mfac.storageAccessSemaphore.Release();
            lfac.storageAccessSemaphore.Release();
            sfac.storageAccessSemaphore.Release();
            //neededIngredientsSemaphore.Release();
        }

        //public void waitoneAlchemistSemaphore()
        //{
        //    if (Program.guildA.numberOfAlchemistsInGuild - 1 >= 0)
        //    {
        //        AlchemistA a = Program.guildA.guild[Program.guildA.numberOfAlchemistsInGuild - 1];
        //        if(a.lead == true)
        //        {
        //            //Console.WriteLine("Alchemists: A semaphore waited lead ");
        //            a.neededIngredientsSemaphore.WaitOne();
        //            a.lead = false;
        //        }
        //        if(a.mercury == true)
        //        {
        //            //Console.WriteLine("Alchemists: A semaphore waited mercury");
        //            a.neededIngredientsSemaphore.WaitOne();
        //            a.mercury = false;
        //        }                  
        //    }
        //    if (Program.guildB.numberOfAlchemistsInGuild - 1 >= 0)
        //    {
        //        AlchemistB b = Program.guildB.guild[Program.guildB.numberOfAlchemistsInGuild - 1];
        //        if(b.mercury == true)
        //        {
        //            //Console.WriteLine("Alchemists: B semaphore waited mercury");
        //            b.neededIngredientsSemaphore.WaitOne();
        //            b.mercury = false;
        //        }
        //        if(b.sulfur == true)
        //        {
        //            //Console.WriteLine("Alchemists: B semaphore waited sulfur");
        //            b.neededIngredientsSemaphore.WaitOne();
        //            b.sulfur = false;
        //        }
        //    }
        //    if (Program.guildC.numberOfAlchemistsInGuild - 1 >= 0)
        //    {
        //        AlchemistC c = Program.guildC.guild[Program.guildC.numberOfAlchemistsInGuild - 1];
        //        if(c.lead == true)
        //        {
        //            //Console.WriteLine("Alchemists: C semaphore waited lead");
        //            c.neededIngredientsSemaphore.WaitOne();
        //            c.lead = false;
        //        }
        //        if(c.sulfur == true)
        //        {
        //            //Console.WriteLine("Alchemists: C semaphore waited sulfur");
        //            c.neededIngredientsSemaphore.WaitOne();
        //            c.sulfur = false;
        //        }                  
        //    }
        //}
    }
    
}
