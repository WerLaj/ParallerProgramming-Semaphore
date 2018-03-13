using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallerProgrammingSemaphore
{
    public class Guilds
    {
        public int numberOfAlchemistsInGuild;

        public Guilds()
        {
            numberOfAlchemistsInGuild = 0;
        }

        public int getRandomTimeInterval()
        {
            int time = 0;
            Random rnd = new Random();
            time = rnd.Next(1000, 3000);

            return time;
        }
    }

    public class GuildA : Guilds
    {
        public AlchemistA[] guild;

        public GuildA(int n, Thread[] t)
        {
            guild = new AlchemistA[n];
            numberOfAlchemistsInGuild = 0;

            for (int i = 0; i < n; i++)
            {
                int r = getRandomTimeInterval();
                //Thread.Sleep(r);
                AlchemistA a = new AlchemistA();
                guild[i] = a;
                t[i] = new Thread(a.collectIngredients);
                t[i].Start();
                numberOfAlchemistsInGuild++;
            }
        }
    }

    public class GuildB : Guilds
    {
        public AlchemistB[] guild;

        public GuildB(int n, Thread[] t)
        {
            guild = new AlchemistB[n];
            numberOfAlchemistsInGuild = 0;

            for (int i = 0; i < n; i++)
            {
                int r = getRandomTimeInterval();
                //Thread.Sleep(r);
                AlchemistB a = new AlchemistB();
                guild[i] = a;
                t[i] = new Thread(a.collectIngredients);
                t[i].Start();
                numberOfAlchemistsInGuild++;
            }
        }
    }

    public class GuildC : Guilds
    {
        public AlchemistC[] guild;

        public GuildC(int n, Thread[] t)
        {
            guild = new AlchemistC[n];
            numberOfAlchemistsInGuild = 0;

            for (int i = 0; i < n; i++)
            {
                int r = getRandomTimeInterval();
                //Thread.Sleep(r);
                AlchemistC a = new AlchemistC();
                guild[i] = a;
                t[i] = new Thread(a.collectIngredients);
                t[i].Start();
                numberOfAlchemistsInGuild++;
            }
        }
    }

    public class GuildD : Guilds
    {
        public AlchemistD[] guild;

        public GuildD(int n, Thread[] t)
        {
            guild = new AlchemistD[n];
            numberOfAlchemistsInGuild = 0;

            for (int i = 0; i < n; i++)
            {
                int r = getRandomTimeInterval();
                //Thread.Sleep(r);
                AlchemistD a = new AlchemistD();
                guild[i] = a;
                t[i] = new Thread(a.collectIngredients);
                t[i].Start();
                numberOfAlchemistsInGuild++;
            }
        }
    }
}
