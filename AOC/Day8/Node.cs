namespace AOC_2023.Day8
{
    public class Node
    {
        public string Name { get; private set; }
        public string LeftName { get; private set; }
        public string RightName { get; private set; }
        public Node? Left { get; set; }
        public Node? Right { get; set; }

        public Node(string name, string leftName, string rightName)
        {
            Name = name;
            LeftName = leftName;
            RightName = rightName;
        }
    }
}
