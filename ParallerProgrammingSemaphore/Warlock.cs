using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallerProgrammingSemaphore
{
    public class Warlock
    {
        FactoryLead lfac;
        FactoryMercury mfac;
        FactorySulfur sfac;

        public Warlock()
        {
            lfac = Program.leadFactory;
            mfac = Program.mercuryFactory;
            sfac = Program.sulfurFactory;
            int time = getRandomTimeInterval();
            Thread.Sleep(time);
        }

        public void curse()
        {
            while(true)
            {
                Factory fac;
                fac = getRandomFactory();

                int time = getRandomTimeInterval();
                Thread.Sleep(time);

                fac.curseBinarySemaphore.WaitOne();

                if (fac.curses == 0)
                {
                    fac.cursesSemaphore.WaitOne();
                }
                fac.curses++;
                Console.WriteLine("---" + fac.factoryName + " cursed by Warlock" + Program.getWarlockThreadName(Thread.CurrentThread));

                fac.curseBinarySemaphore.Release();
            }
        }

        public Factory getRandomFactory()
        {
            Factory fac = new Factory();

            Random rnd = new Random();
            int factoryType = rnd.Next(1, 4);

            if (factoryType == 1)
            {
                fac = lfac;
            }
            else if (factoryType == 2)
            {
                fac = mfac;
            }
            else
            {
                fac = sfac;
            }

            return fac;
        }

        public int getRandomTimeInterval()
        {
            int time = 0;
            Random rnd = new Random();
            time = rnd.Next(5000, 15000);

            return time;
        }
    }
}
