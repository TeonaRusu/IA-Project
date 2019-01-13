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

        private void button_start_Click(object sender, EventArgs e)
        {
            _graph = new Graph(15);
            _noNodes = _graph.get_noNodes();
            _adjacentList = _graph.adjacentList;

            ReadAdjacentMatrix();
            /* doar ptr a verifica daca se citeste bine matricea de adiacenta si se creeaza lista; stergem dupa*/
            textBoxResults.ResetText();
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
        }
    }
}

