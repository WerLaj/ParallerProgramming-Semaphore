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
        public int[] collectedIngredients;
        public bool haveAllIngredients;

        public Alchemist()
        {
            neededIngredients = new string[2];
            collectedIngredients = new int[2] { 0, 0 };
            haveAllIngredients = false;
        }

        public void collectIngredient(Factory factory)
        {
            

            
        }
    }

    public class AlchemistA : Alchemist
    {
        public AlchemistA()
        {
            neededIngredients = new string[2] { "lead", "mercury" };
            collectedIngredients = new int[2] { 0, 0 };
            haveAllIngredients = false;
        }
    }

    public class AlchemistB : Alchemist
    {
        public AlchemistB()
        {
            neededIngredients = new string[2] { "mercury", "sulfur" };
            collectedIngredients = new int[2] { 0, 0 };
            haveAllIngredients = false;
        }
    }

    public class AlchemistC : Alchemist
    {
        public AlchemistC()
        {
            neededIngredients = new string[2] { "lead", "sulfur" };
            collectedIngredients = new int[2] { 0, 0 };
            haveAllIngredients = false;
        }
    }

    public class AlchemistD : Alchemist
    {
        public AlchemistD()
        {
            neededIngredients = new string[3] { "mercury", "sulfur", "lead" };
            collectedIngredients = new int[2] { 0, 0 };
            haveAllIngredients = false;
        }
    }
}
