using QuikGraph;
using QuikGraph.Algorithms;
using QuikGraph.Algorithms.MaximumFlow;

namespace AOC_2023.Day25
{
    public class Day25PuzzleManager : PuzzleManager
    {
        public AdjacencyGraph<string, Edge<string>> Graph { get; set; }
        public Day25PuzzleManager()
        {
            var inputHelper = new Day25InputHelper(INPUT_FILE_NAME);
            Graph = inputHelper.Parse();
        }
        public override Task SolveBothParts()
        {
            SolvePartOne();
            Console.WriteLine();
            SolvePartTwo();
            return Task.CompletedTask;
        }

        public override Task SolvePartOne()
        {
            Console.WriteLine($"The solution to part one is '{Solve()}'.");
            return Task.CompletedTask;
        }

        private int Solve()
        {
            // Using https://github.com/KeRNeLith/QuikGraph/wiki/Maximum-Flow and the
            // 'Max-flow min-cut theorem' https://en.wikipedia.org/wiki/Max-flow_min-cut_theorem

            var source = Graph.Vertices.First();
            var sourceGraphCount = 1;
            var sinkGraphCount = 0;
            foreach (var sink in Graph.Vertices)
            {
                if (source == sink)
                {
                    continue;
                }
                // A function which maps an edge to its capacity
                Func<Edge<string>, double> capacityFunc = edge => 1;

                // A function which takes a vertex and returns the edge connecting to its predecessor in the flow network
                TryFunc<string, Edge<string>> flowPredecessors;

                // A function used to create new edges during the execution of the algorithm. These edges are removed before the computation returns
                EdgeFactory<string, Edge<string>> edgeFactory = (source, target) => new Edge<string>(source, target);

                // Reversed edge augmentor
                var reversedEdgeAugmentorAlgorithm = new ReversedEdgeAugmentorAlgorithm<string, Edge<string>>(Graph, edgeFactory);
                reversedEdgeAugmentorAlgorithm.AddReversedEdges();

                // Computing the maximum flow using Edmonds Karp.
                var flow = Graph.MaximumFlow(
                    capacityFunc,
                    source, sink,
                    out flowPredecessors,
                    edgeFactory,
                    reversedEdgeAugmentorAlgorithm);

                reversedEdgeAugmentorAlgorithm.RemoveReversedEdges();

                if (flow == 3)
                {
                    sinkGraphCount++;
                }
                else
                {
                    sourceGraphCount++;
                }
            }
            return sourceGraphCount * sinkGraphCount;
        }

        public override Task SolvePartTwo()
        {
            Console.WriteLine("No part two today. Merry Christmas!");
            return Task.CompletedTask;
        }
    }
}