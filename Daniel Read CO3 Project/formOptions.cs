using System;
using System.Windows.Forms;

namespace Daniel_Read_CO3_Project
{
    public partial class formOptions : Form
    {
        public formOptions()
        {
            InitializeComponent();
        }

        bool UP = false;
        bool DOWN = false;
        bool SELECT = false;

        private void formOptions_keyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.W) || (e.KeyCode == Keys.Up))         // when the user presses the W or Up arrow key, the value UP becomes true and can be used later to determine the direction
                UP = true;                                               // the cursor will go
            if ((e.KeyCode == Keys.S) || (e.KeyCode == Keys.Down))
                DOWN = true;
            if (e.KeyCode == Keys.Space)
                SELECT = true;
        }
        private void formOptions_keyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.W) || (e.KeyCode == Keys.Up))
                UP = false;
            if ((e.KeyCode == Keys.S) || (e.KeyCode == Keys.Down))
                DOWN = false;
            if (e.KeyCode == Keys.Space)
                SELECT = false;
        }
        private void formOptionsTimer_Tick(object sender, EventArgs e)
        {
            while (DOWN)
            {
                if (curBack.Visible)
                {
                    curBack.Visible = false;
                    curControls.Visible = true;
                    break;
                }
                if (curControls.Visible)
                {
                    curControls.Visible = false;
                    curVideo.Visible = true;
                    break;
                }
                if (curVideo.Visible)
                {
                    curVideo.Visible = false;
                    curBack.Visible = true;
                    break;
                }
            }
            while (UP)
            {
                if (curBack.Visible)
                {
                    curBack.Visible = false;
                    curVideo.Visible = true;
                    break;
                }
                if (curVideo.Visible)
                {
                    curVideo.Visible = false;
                    curControls.Visible = true;
                    break;
                }
                if (curControls.Visible)
                {
                    curControls.Visible = false;
                    curBack.Visible = true;
                    break;
                }
            }
            if (SELECT)
            {
                if (curBack.Visible)
                {
                    this.Hide();
                    formStart formStart = new formStart();
                    formStart.Show();
                }
            }
        }
    }
}
