using System.Text.RegularExpressions;

namespace AOC_2023.Day20
{
    public class Day20InputHelper : InputHelper<Dictionary<string, Module>>
    {
        public Day20InputHelper(string fileName) : base(fileName)
        {
        }

        public override Dictionary<string, Module> Parse()
        {
            var modules = new Dictionary<string, Module>();
            using (var sr = new StreamReader(InputPath))
            {
                string ln;
                while ((ln = sr.ReadLine()!) != null)
                {
                    var nameRegex = new Regex(@"\w+");
                    var matches = nameRegex.Matches(ln);
                    var name = matches.First().Value;
                    var outputNames = matches.Skip(1).Select(x => x.Value).ToList();
                    Module module;
                    if (ln[0] == '%')
                    {
                        module = new FlipFlopModule(matches.First().Value, outputNames);
                    }
                    else if (ln[0] == '&')
                    {
                        module = new ConjunctionModule(matches.First().Value, outputNames);
                    }
                    else
                    {
                        module = new BroadcastModule(matches.First().Value, outputNames);
                    }
                    modules.Add(name, module);
                }

                sr.DiscardBufferedData();
                sr.BaseStream.Seek(0, SeekOrigin.Begin);

                while ((ln = sr.ReadLine()!) != null)
                {
                    var nameRegex = new Regex(@"\w+");
                    var matches = nameRegex.Matches(ln);
                    var name = matches.First().Value;
                    var outputNames = matches.Skip(1).Select(x => x.Value).ToList();
                    foreach (var outputName in outputNames)
                    {
                        if (modules.ContainsKey(outputName))
                        {
                            modules[outputName].AddInput(name);
                        }
                    }
                }
            }
            return modules;
        }
    }
}