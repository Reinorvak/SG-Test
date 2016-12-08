using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SG_Interactive_Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OpenFile.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(OpenFile.FileName);

                // Sort out the years people lived and died
                SortedDictionary<int, int> People = new SortedDictionary<int, int>();
     
                do
                {
                    // Split the incoming data to the Born, Died
                    string[] content = sr.ReadLine().Split(',');

                    int Born =0, Died=0; // Make sure they're actual numbers
                    if (Int32.TryParse(content[0], out Born) && Int32.TryParse(content[1], out Died))
                    {
                        // Add a key for the born
                        if (People.ContainsKey(Born))
                        {
                            People[Born]++;
                        }
                        else
                            People.Add(Born, 1);

                        // Add a key for the dead
                        if (People.ContainsKey(Died))
                        {
                            People[Died]--;
                        }
                        else
                            People.Add(Died, -1);
                    }
                    else
                    {
                        MessageBox.Show("Incorrect format in data set!");
                        break;
                    }
                }
                while (sr.EndOfStream == false);
                sr.Close();

                int nMostLiving = 0, nLiving = 0;
                // Go through the list to find out when the most people were alive
                foreach(var entry in People)
                {
                    // Both alive and dead are stored, so add to the number
                    nLiving += entry.Value;
                    if (nLiving > nMostLiving)
                    {
                        // Store it
                        nMostLiving = nLiving;
                        // Write out the best yet
                        BestYear.Text = entry.Key.ToString();
                    }
                    // Add the data point
                    Stats.Series["Living"].Points.AddXY(entry.Key, nLiving);
                }
            }
        }
    }
}
