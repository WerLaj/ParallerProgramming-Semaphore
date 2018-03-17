using System;
using System.Collections;
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

        public void startAlchemists(int n, Thread[] t)
        {
            for (int i = 0; i < n; i++)
            {
                int r = getRandomTimeInterval();
                Thread.Sleep(r);
                t[i].Start();
            }
        }
    }

    public class GuildA : Guilds
    {
        public Queue guild;
        public Semaphore guildSemaphore;

        public GuildA(int n, Thread[] t) : base()
        {
            guild = new Queue();
            guildSemaphore = new Semaphore(0, 1);

            for (int i = 0; i < n; i++)
            {               
                AlchemistA a = new AlchemistA();
                guild.Enqueue(a);
                t[i] = new Thread(a.collectIngredients);
                numberOfAlchemistsInGuild++;
            }
        }
    }

    public class GuildB : Guilds
    {
        public Queue guild;
        public Semaphore guildSemaphore;

        public GuildB(int n, Thread[] t) : base()
        {
            guild = new Queue();
            guildSemaphore = new Semaphore(0, 1);

            for (int i = 0; i < n; i++)
            {
                AlchemistB a = new AlchemistB();
                guild.Enqueue(a);
                t[i] = new Thread(a.collectIngredients);
                numberOfAlchemistsInGuild++;
            }
        }
    }

    public class GuildC : Guilds
    {
        public Queue guild;
        public Semaphore guildSemaphore;

        public GuildC(int n, Thread[] t) : base()
        {
            guild = new Queue();
            guildSemaphore = new Semaphore(0, 1);

            for (int i = 0; i < n; i++)
            {
                AlchemistC a = new AlchemistC();
                guild.Enqueue(a);
                t[i] = new Thread(a.collectIngredients);
                numberOfAlchemistsInGuild++;
            }
        }
    }

    public class GuildD : Guilds
    {
        public Queue guild;
        public Semaphore guildSemaphore;

        public GuildD(int n, Thread[] t) : base()
        {
            guild = new Queue();
            guildSemaphore = new Semaphore(0, 1);

            for (int i = 0; i < n; i++)
            {
                AlchemistD a = new AlchemistD();
                guild.Enqueue(a);
                t[i] = new Thread(a.collectIngredients);
                numberOfAlchemistsInGuild++;
            }
        }
    }
}
