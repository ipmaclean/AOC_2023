using System.Text.RegularExpressions;
using QuikGraph;

namespace AOC_2023.Day25
{
    public class Day25InputHelper : InputHelper<AdjacencyGraph<string, Edge<string>>>
    {
        public Day25InputHelper(string fileName) : base(fileName)
        {
        }

        public override AdjacencyGraph<string, Edge<string>> Parse()
        {

            var edges = new List<Edge<string>>();

            using (var sr = new StreamReader(InputPath))
            {
                var nameRegex = new Regex(@"\w+");
                string ln;
                while ((ln = sr.ReadLine()!) != null)
                {
                    var nodeNames = nameRegex.Matches(ln).Select(x => x.Value).ToList();
                    for (var i = 1; i < nodeNames.Count; i++)
                    {
                        edges.Add(new Edge<string>(nodeNames[0], nodeNames[i]));
                    }
                }
            }
            return edges.ToAdjacencyGraph<string, Edge<string>>();
        }
    }
}