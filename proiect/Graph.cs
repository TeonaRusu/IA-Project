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

        public void BFS(int s)
        {
            //initializare pentru vectorul de vecini
            bool[] visited = new bool[noNodes];
            for (int i = 0; i < noNodes; i++)
            {
                visited[i] = false;
            }

            //lista pentru BFS
            Queue<int> queue = new Queue<int>();

            //nodul de la care plec il pun vizitat
            visited[s] = true;
            queue.Enqueue(s);

            //List<int>.Enumerator j;

            while (queue.Count > 0)
            {
                s = queue.Peek();
                Console.WriteLine(s + " ");
                queue.Dequeue();

                for (int j = 0; j < adjacentList[s].Count; ++j)
                {
                    if(!visited[adjacentList[s][j]])
                    {
                        visited[adjacentList[s][j]] = true;
                        Console.WriteLine(adjacentList[s][j] + " ");
                        queue.Enqueue(adjacentList[s][j]);
                    }
                }
            }
        }

    }
}
