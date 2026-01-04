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
    GameRepo repo = new GameRepo();
    public int Level { get; set; }
    public int HScore { get; set; }
        public ChooseLevel()
        {
            InitializeComponent();
            (HScore, Level) = repo.ReadData();
            Common.PlaySound("Assets/Audio/ChooseLevel.mp3");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 Level1 = new Form1(1, HScore);
            Level1.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 Level2 = new Form1(2, HScore);
            Level2.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 Level3 = new Form1(3, HScore);
            Level3.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 Level4 = new Form1(4, HScore);
            this.Hide();
            Level4.Show();
        }

        private void Resume_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 Continue = new Form1(Level, HScore);
            Continue.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MainForm main = new MainForm();
            main.Show();
            this.Close();
        }
    }
}
