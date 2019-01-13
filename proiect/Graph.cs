using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proiect
{
    public class Graph
    {
        private int noNodes;  //number of nodes in graph
        public List<int>[] adjacentList; // adjacent list of graph

        public Graph(int V)
        {
            this.noNodes = V;
            adjacentList = new List<int>[noNodes];
            for (int i = 0; i < noNodes; ++i)
            {
                adjacentList[i] = new List<int>();
            }
        }

        public int get_noNodes() { return this.noNodes; }
    }
}
