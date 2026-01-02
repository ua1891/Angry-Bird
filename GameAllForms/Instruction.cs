using System;
using System.Windows.Forms;

namespace LaeeqFramwork.GameAllForms
{
    public partial class Instruction : Form
    {
        public Instruction()
        {
            InitializeComponent();

            // Form behavior
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
        }
        // Close instruction form
        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
            MainForm form = new MainForm();
            form.Show();
        }
    }
}
