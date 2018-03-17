using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallerProgrammingSemaphore
{
    public class Sorcerer
    {
        FactoryLead lfac;
        FactoryMercury mfac;
        FactorySulfur sfac;

        public Sorcerer()
        {
            lfac = Program.leadFactory;
            mfac = Program.mercuryFactory;
            sfac = Program.sulfurFactory;
            int time = getRandomTimeInterval();
            Thread.Sleep(time);
        }

        public void removeCurses()
        {
            while(true)
            {
                int time = getRandomTimeInterval();
                Thread.Sleep(time);

                removeCurseFromFactory(lfac);

                removeCurseFromFactory(mfac);

                removeCurseFromFactory(sfac);

            } 
        }

        public int getRandomTimeInterval()
        {
            int time = 0;
            Random rnd = new Random();
            time = rnd.Next(1000, 5000);

            return time;
        }

        public void removeCurseFromFactory(Factory f)
        {
            f.curseBinarySemaphore.WaitOne();

            if (f.curses > 0)
            {
                f.curses--;
                Console.WriteLine("Sorcerer " + Program.getSorcererThreadName(Thread.CurrentThread) + " removes curse from " + f.factoryName);
                if (f.curses == 0)
                {
                    f.cursesSemaphore.Release();
                    Console.WriteLine("Sorcerer " + Program.getSorcererThreadName(Thread.CurrentThread) + " unblocked factory " + f.factoryName);
                }
            }

            f.curseBinarySemaphore.Release();
        }
    }
}
