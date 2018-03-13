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
        string threadName;
        FactoryLead lfac;
        FactoryMercury mfac;
        FactorySulfur sfac;

        public Sorcerer()
        {
            threadName = Thread.CurrentThread.Name;
            lfac = Program.leadFactory;
            mfac = Program.mercuryFactory;
            sfac = Program.sulfurFactory;
        }

        public void removeCurses()
        {
            int i = 0;

            do
            {
                int time = getRandomTimeInterval();
                //Console.WriteLine("---Sorcerer " + Program.getSorcererThreadName(Thread.CurrentThread) + " sleeps for " + time + " sec");
                Thread.Sleep(time);

                removeCurseFromFactory(lfac);

                removeCurseFromFactory(mfac);

                removeCurseFromFactory(sfac);

                i++;
            } while (i < 15);
        }

        public int getRandomTimeInterval()
        {
            int time = 0;
            Random rnd = new Random();
            time = rnd.Next(1000, 10000);

            return time;
        }

        public void removeCurseFromFactory(Factory f)
        {
            f.curseBinarySemaphore.WaitOne();

            if (f.curses > 0)
            {
                f.curses--;
                Console.WriteLine("---Sorcerer " + Program.getSorcererThreadName(Thread.CurrentThread) + " removes curse from " + f.factoryName);
                if (f.curses == 0)
                {
                    f.cursesSemaphore.Release();
                    f.n++;
                    Console.WriteLine("---Sorcerer " + Program.getSorcererThreadName(Thread.CurrentThread) + " unblocked factory " + f.factoryName);
                }
            }

            f.curseBinarySemaphore.Release();
        }
    }
}
