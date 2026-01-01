using LaeeqFramwork.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaeeqFramwork.GameAllForms
{
    public partial class ChooseLevel : Form
    {
        GameRepo repo=new GameRepo();
       public  int Level {  get; set; }
        public int HScore {  get; set; }
        public ChooseLevel()
        {
            InitializeComponent();
            (HScore,Level) = repo.ReadData();        
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form1 Level1 = new Form1(1,HScore);
            this.Close();
            Level1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 Level2 = new Form1(2,HScore);
            this.Close();
            Level2.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 Level3 = new Form1(3,HScore);
            this.Close();
            Level3.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 Level4 = new Form1(4,HScore);
            this.Close();
            Level4.Show();
        }

        private void Resume_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 Continue=new Form1(Level,HScore);
            Continue.Show();
        }
    }
}
