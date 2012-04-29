using System.Collections.Generic;
using Byte.Library.Collection;
using Xunit;

namespace Byte.Library.UnitTests.Collection
{
    public class WeightedGraphTests
    {
        [Fact]
        public void TraverseTest()
        {
            var graph = new WeightedGraph<string, int>();

            var one = new WeightedGraph<string, int>.WeightedNode("one");
            var two = new WeightedGraph<string, int>.WeightedNode("two");
            var three = new WeightedGraph<string, int>.WeightedNode("three");
            var four = new WeightedGraph<string, int>.WeightedNode("four");
            var five = new WeightedGraph<string, int>.WeightedNode("five");
            var six = new WeightedGraph<string, int>.WeightedNode("six");

            graph.Nodes.Add(one);
            graph.Nodes.Add(two);
            graph.Nodes.Add(three);
            graph.Nodes.Add(four);
            graph.Nodes.Add(five);

            graph.Connect(one, two, 5);
            graph.Connect(one, five, 2);
            graph.Connect(two, five, 1);
            graph.Connect(two, four, 8);
            graph.Connect(two, three, 1);
            graph.Connect(four, five, 15);
            graph.Connect(four, three, 13);

            graph.StartNode = one;

            var visitedOrder = new List<string>();

            foreach (var node in graph)
            {
                visitedOrder.Add(node);
            }

            Assert.Equal("one", visitedOrder[0]);
            Assert.Equal("five", visitedOrder[1]);
            Assert.Equal("two", visitedOrder[2]);
            Assert.Equal("three", visitedOrder[3]);
            Assert.Equal("four", visitedOrder[4]);
        }
    }
}
