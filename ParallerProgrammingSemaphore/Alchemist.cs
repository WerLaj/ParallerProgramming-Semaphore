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
        public FactoryLead lfac;
        public FactoryMercury mfac;
        public FactorySulfur sfac;
        public bool alchemistInLab;

        public Alchemist()
        {
            neededIngredients = new string[2];
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
        }

        public void collectIngredients()
        {
            Program.guildA.guildSemaphore.WaitOne();
            lfac.storageAccessSemaphore.WaitOne();
            mfac.storageAccessSemaphore.WaitOne();
                
            Console.WriteLine("AlchemistA " + Program.guildA.numberOfAlchemistsInGuild + "  collected all ingredients");
            takeOneIngeredient(lfac.storage);
            takeOneIngeredient(mfac.storage);
            lfac.storageSemaphore.Release();
            mfac.storageSemaphore.Release();
            alchemistInLab = true;
            Program.guildA.guild.Dequeue();
            Program.guildA.numberOfAlchemistsInGuild--;
            Console.WriteLine("GuildA " + Program.guildA.numberOfAlchemistsInGuild + "  dequeued");       

            mfac.storageAccessSemaphore.Release();
            lfac.storageAccessSemaphore.Release();
        }
    }

    public class AlchemistB : Alchemist
    { 

        public AlchemistB() : base()
        {
            neededIngredients = new string[2] { "mercury", "sulfur" };
        }

        public void collectIngredients()
        {
            Program.guildB.guildSemaphore.WaitOne();
            sfac.storageAccessSemaphore.WaitOne();
            mfac.storageAccessSemaphore.WaitOne();

            Console.WriteLine("AlchemistB " + Program.guildB.numberOfAlchemistsInGuild + "  collected all ingredients");
            takeOneIngeredient(sfac.storage);
            takeOneIngeredient(mfac.storage);
            sfac.storageSemaphore.Release();
            mfac.storageSemaphore.Release();
            alchemistInLab = true;
            Program.guildB.numberOfAlchemistsInGuild--;
            Program.guildB.guild.Dequeue();
            Console.WriteLine("GuildB " + Program.guildB.numberOfAlchemistsInGuild + "  dequeued");

            mfac.storageAccessSemaphore.Release();
            sfac.storageAccessSemaphore.Release();       
        }
    }

    public class AlchemistC : Alchemist
    {
        public AlchemistC() : base()
        {
            neededIngredients = new string[2] { "lead", "sulfur" };
        }

        public void collectIngredients()
        {
            Program.guildC.guildSemaphore.WaitOne();
            sfac.storageAccessSemaphore.WaitOne();
            lfac.storageAccessSemaphore.WaitOne();
                
            Console.WriteLine("AlchemistC " + Program.guildC.numberOfAlchemistsInGuild + "  collected all ingredients");
            takeOneIngeredient(sfac.storage);
            takeOneIngeredient(lfac.storage);
            lfac.storageSemaphore.Release();
            sfac.storageSemaphore.Release();               
            alchemistInLab = true;
            Program.guildC.guild.Dequeue();
            Program.guildC.numberOfAlchemistsInGuild--;
            Console.WriteLine("GuildC " + Program.guildC.numberOfAlchemistsInGuild + "  dequeued");
               
            lfac.storageAccessSemaphore.Release();
            sfac.storageAccessSemaphore.Release();          
        }
    }

    public class AlchemistD : Alchemist
    {
        public AlchemistD() : base()
        {
            neededIngredients = new string[3] { "mercury", "sulfur", "lead" };
        }

        public void collectIngredients()
        {     
            Program.guildD.guildSemaphore.WaitOne();
            sfac.storageAccessSemaphore.WaitOne();
            lfac.storageAccessSemaphore.WaitOne();
            mfac.storageAccessSemaphore.WaitOne();
            
            Console.WriteLine("AlchemistD " + Program.guildD.numberOfAlchemistsInGuild + "  collected all ingredients");
            takeOneIngeredient(sfac.storage);
            takeOneIngeredient(lfac.storage);
            takeOneIngeredient(mfac.storage);
            sfac.storageSemaphore.Release();
            mfac.storageSemaphore.Release();
            lfac.storageSemaphore.Release();           
            alchemistInLab = true;
            Program.guildD.numberOfAlchemistsInGuild--;
            Program.guildD.guild.Dequeue();
            Console.WriteLine("GuildD " + Program.guildD.numberOfAlchemistsInGuild + "  dequeued");


            mfac.storageAccessSemaphore.Release();
            lfac.storageAccessSemaphore.Release();
            sfac.storageAccessSemaphore.Release();
        }
    }
}
