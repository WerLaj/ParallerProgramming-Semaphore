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
        public string factoryName;
        public Semaphore curseBinarySemaphore;

        public Factory()
        {
            curses = 0;
            storage = new[] { 0, 0 };
            factoryName = "";
            curseBinarySemaphore = new Semaphore(1, 1);
            //cursesSemaphore = new Semaphore(0, 5);
        }

        public void produce()
        {
            Random rnd = new Random();
            
        }
    }

    public class FactoryMercury : Factory
    {
        public FactoryMercury() : base()
        {
            factoryName = "Mercury Factory";
        }
    }

    public class FactoryLead : Factory
    {
        public FactoryLead() : base()
        {
            factoryName = "Lead Factory";
        }
    }

    public class FactorySulfur : Factory
    {
        public FactorySulfur() : base()
        {
            factoryName = "Sulfur Factory";
        }
    }
}
