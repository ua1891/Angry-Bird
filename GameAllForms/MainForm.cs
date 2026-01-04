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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Common.PlaySound("Assets/Audio/MainOp2.mp3");
            // Attach hover effect to labels
            AttachHover(label1);
            AttachHover(label2);
            AttachHover(label3); // if you have more labels

            // Make labels feel clickable
            label2.Cursor = Cursors.Hand;
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            ForeColor = Color.Red;
        }

        // ===== Hover Effects =====
        private void AttachHover(Label lbl)
        {
            lbl.MouseEnter += Label_MouseEnter;
            lbl.MouseLeave += Label_MouseLeave;
        }

        private void Label_MouseEnter(object sender, EventArgs e)
        {
            Label lbl = sender as Label;
            if (lbl != null)
                lbl.ForeColor = Color.Red;
        }

        private void Label_MouseLeave(object sender, EventArgs e)
        {
            Label lbl = sender as Label;
            if (lbl != null)
                lbl.ForeColor = Color.White; // or original color
        }

        private void label2_Click(object sender, EventArgs e)
        {
            using (Instruction instructionForm = new Instruction())
            {
                this.Hide();
                instructionForm.ShowDialog() ; //modal
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            ChooseLevel level = new ChooseLevel();
            level.Show();
        }
    }
}
