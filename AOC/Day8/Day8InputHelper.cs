namespace AOC_2023.Day8
{
    public class Day8InputHelper : InputHelper<(string, List<Node>)>
    {
        public Day8InputHelper(string fileName) : base(fileName)
        {
        }

        public override (string, List<Node>) Parse()
        {
            var directions = string.Empty;
            var nodes = new List<Node>();
            using (var sr = new StreamReader(InputPath))
            {
                directions = sr.ReadLine()!;

                var ln = sr.ReadLine()!;
                while ((ln = sr.ReadLine()!) != null)
                {
                    nodes.Add(new Node(ln.Substring(0, 3), ln.Substring(7, 3), ln.Substring(12, 3)));
                }

                foreach (var node in nodes)
                {
                    node.Left = nodes.FirstOrDefault(x => x.Name == node.LeftName);
                    node.Right = nodes.FirstOrDefault(x => x.Name == node.RightName);
                }
            }
            return (directions, nodes);
        }
    }
}