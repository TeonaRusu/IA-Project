using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proiect
{
    public partial class main_form : Form
    {
        Graph _graph;
        int _noNodes;
        List<int>[] _adjacentList;
        int _start;
        int _goal;
        

        public main_form()
        {
            InitializeComponent();
        }

        private void ReadAdjacentMatrix()
        {
            for (int i = 0; i < _noNodes; ++i)
            {
                for (int j = 0; j < _noNodes; ++j)
                {
                    string name = string.Format("elem_{0}_{1}", i , j );
                    TextBox tb = this.Controls.Find(name, true)[0] as TextBox;
                    int value = Convert.ToInt32(tb.Text);
                    if (value == 1)
                    {
                        _adjacentList[i].Add(j);
                    }
                }
            }
        }

        private void ReadStartGoal()
        {
            _start = Convert.ToInt32(textBox_start.Text);
            _goal = Convert.ToInt32(textBox_goal.Text);
        }

        private void button_start_Click(object sender, EventArgs e)
        {
            _graph = new Graph(15);
            _noNodes = _graph.get_noNodes();
            _adjacentList = _graph.adjacentList;

            /* citeste matricea de adiacenta si creeaza lista de adiacenta */
            ReadAdjacentMatrix();

            /* citeste start si goal */
            ReadStartGoal();

            /* doar ptr a verifica daca se citeste bine matricea de adiacenta si se creeaza lista; stergem dupa*/
            textBoxResults.ResetText();
            /*textBoxResults.Text = "Start: " + _start.ToString();
            textBoxResults.AppendText("\r\nGoal: " + _goal.ToString() + "\r\n");
            */
            for (int i = 0; i < _adjacentList.Length; ++i)
            {
                textBoxResults.AppendText((i).ToString() + " -> ");
                for (int j = 0; j < _adjacentList[i].Count; ++j)
                {
                    string v = _adjacentList[i][j].ToString();
                    textBoxResults.AppendText(v + " ");
                }
                textBoxResults.AppendText("\r\n");
            }
            /* ------- */

            if ( BiDirSearch(_start, _goal) == -1 )
                textBoxResults.AppendText("\r\n Nu exista cale!"); ;
            

        }

        private void BFS(Queue<int> queue, bool[] visited, int[] parent)
        {
            int current = queue.Peek();

            queue.Dequeue();

            for (int j = 0; j < _adjacentList[current].Count; ++j)
            {
                if (!visited[_adjacentList[current][j]])
                {
                    parent[_adjacentList[current][j]] = current;
                    visited[_adjacentList[current][j]] = true;
                    textBoxResults.AppendText(_adjacentList[current][j] + " ");
                    queue.Enqueue(_adjacentList[current][j]);
                }
            }
        }

        private int IsIntersecting(bool[] s_visited, bool[] t_visited)
        {
            for (int i = 0; i < _noNodes; i++)
            {
                if (s_visited[i] && t_visited[i])
                {
                    textBoxResults.AppendText("\n\ri --> " + i.ToString());
                    return i;
                }
            }
            return -1;
        }

        private void printPath(int[] s_parent, int[] t_parent, int s, int t, int intersectNode)
        {
            Queue<int> path = new Queue<int>();
            Queue<int> pathReversed;
            path.Enqueue(intersectNode);
            int i = intersectNode;
        
            while (i != s)
            {
                path.Enqueue(s_parent[i]);
                i = s_parent[i];
            }

            pathReversed = new Queue<int>(path.Reverse());

            i = intersectNode;

            while (i != t)
            {
                pathReversed.Enqueue(t_parent[i]);
                i = t_parent[i];
            }

            textBoxResults.AppendText("\r\n Path: ");
            while (pathReversed.Count > 0)
            {
                textBoxResults.AppendText(pathReversed.Dequeue().ToString() + " ");
            }

        }

        private int BiDirSearch(int s, int t)
        {
            bool[] s_visited = new bool[_noNodes];
            bool[] t_visited = new bool[_noNodes];

            int[] s_parent = new int[_noNodes];
            int[] t_parent = new int[_noNodes];

            Queue<int> s_queue = new Queue<int>();
            Queue<int> t_queue = new Queue<int>();

            int intersectNode = -1;

            for (int i = 0; i < _noNodes; i++)
            {
                s_visited[i] = false;
                t_visited[i] = false;
            }

            s_queue.Enqueue(s);
            s_visited[s] = true;
            s_parent[s] = -1;

            t_queue.Enqueue(t);
            t_visited[t] = true;
            t_parent[t] = -1;

            while (s_queue.Count > 0 && t_queue.Count > 0)
            {
                BFS(s_queue, s_visited, s_parent);
                BFS(t_queue, t_visited, t_parent);

                intersectNode = IsIntersecting(s_visited, t_visited);
                if (intersectNode != -1)
                {
                    printPath(s_parent, t_parent, s, t, intersectNode);
                    return 1;
                }
            }
            
            return -1;
        }
    }
}

