using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace ParallerProgrammingSemaphore
{
    

public class Program
    {
        public static Thread[] warlocks;
        public static Thread[] sorcerers;
        public static FactoryLead leadFactory = new FactoryLead();
        public static FactoryMercury mercuryFactory = new FactoryMercury();
        public static FactorySulfur sulfurFactory = new FactorySulfur();

        static void Main(string[] args)
        {
            warlocks = new Thread[3];
            sorcerers = new Thread[3];

            for (int i = 0; i < 3; i++)
            {
                Warlock w = new Warlock();
                warlocks[i] = new Thread(w.curse);
                warlocks[i].Name = "warlock_" + i;
                warlocks[i].Start();
            }

            for (int i = 0; i < 3; i++)
            {
                Sorcerer s = new Sorcerer();
                sorcerers[i] = new Thread(s.removeCurses);
                sorcerers[i].Name = "sorcerer_" + i;
                sorcerers[i].Start();
            }

            Console.ReadKey();
        }

        public static int getWarlockThreadName(Thread t)
        {
            int n = 0;

            n = Array.IndexOf(warlocks, t); 

            return n;
        }

        public static int getSorcererThreadName(Thread t)
        {
            int n = 0;

            n = Array.IndexOf(sorcerers, t);

            return n;
        }

    }
}
