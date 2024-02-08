using Ponfig;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PonfigTest
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            const string SETTINGS_PATH = "Settings.txt";

            Ponfig.Ponfig ponfig = new Ponfig.Ponfig();
            ponfig.Add("BoolSettingTest", "True");
            ponfig.Add("IntSettingTest", "123");
            ponfig.Add("KeysSettingTest", "Z");
            ponfig.Default(new Option("BoolSettingTest", "False"));
        }
    }
}
