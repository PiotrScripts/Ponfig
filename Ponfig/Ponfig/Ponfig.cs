using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ponfig
{
    public class Ponfig
    {
        public List<Option> Options = new List<Option>();

        public Ponfig() { }
        public Ponfig(string parseInput)
        {
            Options = Parse(parseInput);
        }

        /// <summary>
        /// Parses inputted string and overwrites the Ponfig.
        /// </summary>
        /// <param name="parseInput"></param>
        public void Load(string parseInput)
        {
            Options = Parse(parseInput);
        }

        /// <summary>
        /// Parses inputted string to options and adds them to the Ponfig.
        /// </summary>
        /// <param name="parseInput"></param>
        public void Add(string parseInput)
        {
            Options.AddRange(Parse(parseInput));
        }

        /// <summary>
        /// Creates a new option and adds it to the Ponfig.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void Add(string name, string value)
        {
            Options.Add(new Option(name, value));
        }

        /// <summary>
        /// Adds an option to the Ponfig.
        /// </summary>
        /// <param name="option"></param>
        public void Add(Option option)
        {
            Options.Add(option);
        }

        /// <summary>
        /// Parses inputted string to options, if option doesn't exist it's added to the Ponfig.
        /// </summary>
        /// <param name="parseInput"></param>
        public void Default(string parseInput)
        {
            foreach (Option option in Parse(parseInput))
            {
                if (!Exists(option.Name))
                    Options.Add(option);
            }
        }

        /// <summary>
        /// Creates and adds an option to the Ponfig if it doesn't exist.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void Default(string name, string value)
        {
            if (!Exists(name))
                Options.Add(new Option(name, value));
        }

        /// <summary>
        /// Adds an option to the Ponfig if it doesn't already exist.
        /// </summary>
        /// <param name="option"></param>
        public void Default(Option option)
        {
            if (!Exists(option.Name))
                Options.Add(option);
        }

        /// <summary>
        /// Writes the Ponfig to the specified path.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="smartSave"></param>
        public void Save(string path, bool smartSave)
        {
            if (!File.Exists(path) || !smartSave)
            {
                string content = string.Empty;
                foreach (Option option in Options)
                {
                    content += $"{option.Name}={option.Value}\n";
                }
                File.WriteAllText(path, content);
            }
            else
            {
                List<string> content = File.ReadAllLines(path).ToList();
                foreach (Option option in Options)
                {
                    int lineIndex = content.FindIndex(x => x.StartsWith($"{option.Name}="));
                    if (lineIndex >= 0)
                        content[lineIndex] = $"{option.Name}={option.Value}";
                    else
                        content.Add($"{option.Name}={option.Value}");
                }
                File.WriteAllLines(path, content);
            }
        }

        /// <summary>
        /// Gets an option from the Ponfig.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Option Get(string name)
        {
            return Options.Find(x => x.Name == name);
        }

        /// <summary>
        /// Sets the value of an option in the Ponfig.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void Set(string name, string value)
        {
            Options.Find(x => x.Name == name).Value = value;
        }

        /// <summary>
        /// Checks if an option exists in the Ponfig.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool Exists(string name)
        {
            return Options.Exists(x => x.Name == name);
        }

        /// <summary>
        /// Parses the inputted string into options.
        /// </summary>
        /// <param name="parseInput"></param>
        /// <returns></returns>
        public List<Option> Parse(string parseInput)
        {
            List<Option> options = new List<Option>();

            foreach(string line in parseInput.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None))
            {
                if(!line.Contains("=") || line.StartsWith("#"))
                    continue;

                Option option = new Option();
                option.Name = line.Remove(line.IndexOf('='));
                option.Value = line.Substring(line.IndexOf("=") + 1);

                options.Add(option);
            }

            return options;
        }
    }

    public class Option
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public Option() { }
        public Option(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}
