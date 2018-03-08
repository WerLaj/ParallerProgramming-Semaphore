using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallerProgrammingSemaphore
{
    public class Guilds
    {
        public Alchemist[] guild;
        public int numberOfAlchemistsInGuild;

        public Guilds()
        {
            guild = new Alchemist[3];
            numberOfAlchemistsInGuild = 0;
        }
    }

    public class GuildA : Guilds
    {
        public AlchemistA[] guild;

        public GuildA()
        {
            guild = new AlchemistA[3];
            numberOfAlchemistsInGuild = 0;
        }
    }

    public class GuildB : Guilds
    {
        public AlchemistB[] guild;

        public GuildB()
        {
            guild = new AlchemistB[3];
            numberOfAlchemistsInGuild = 0;
        }
    }

    public class GuildC : Guilds
    {
        public AlchemistC[] guild;

        public GuildC()
        {
            guild = new AlchemistC[3];
            numberOfAlchemistsInGuild = 0;
        }
    }

    public class GuildD : Guilds
    {
        public AlchemistD[] guild;

        public GuildD()
        {
            guild = new AlchemistD[3];
            numberOfAlchemistsInGuild = 0;
        }
    }
}
