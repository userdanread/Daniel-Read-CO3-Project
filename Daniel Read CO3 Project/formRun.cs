using System;
using System.Windows.Forms;

namespace Daniel_Read_CO3_Project
{
    public partial class formRun : Form
    {
        public formRun()
        {
            InitializeComponent();
        }

        // This will be used to tell the next form which character the user has selected to player,
        // 0 for Mark, 1 for Bill
        public static int charSelect;  
        // static means that this instance is not requires for there to be the variable charSelect
        // meaning that it can be accessed in other forms.

        // Menu navigation
        bool UP = false;
        bool DOWN = false;
        bool LEFT = false;
        bool RIGHT = false;
        bool SELECT = false;
        private void keyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.W) || (e.KeyCode == Keys.Up))         // when the user presses the W or Up arrow key, the value UP becomes true and can be used later to determine the direction
                UP = true;                                               // the cursor will go
            if ((e.KeyCode == Keys.S) || (e.KeyCode == Keys.Down))
                DOWN = true;
            if ((e.KeyCode == Keys.A) || (e.KeyCode == Keys.Left))
                LEFT = true;
            if ((e.KeyCode == Keys.D) || (e.KeyCode == Keys.Right))
                RIGHT = true;
            if (e.KeyCode == Keys.Space)
                SELECT = true;
        }

        private void keyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.W) || (e.KeyCode == Keys.Up))
                UP = false;
            if ((e.KeyCode == Keys.S) || (e.KeyCode == Keys.Down))
                DOWN = false;
            if ((e.KeyCode == Keys.A) || (e.KeyCode == Keys.Left))
                LEFT = false;
            if ((e.KeyCode == Keys.D) || (e.KeyCode == Keys.Right))
                RIGHT = false;
            if (e.KeyCode == Keys.Space)
                SELECT = false;
        }

        private void timerRun_Tick(object sender, EventArgs e)
        {
            while (LEFT)
            {
                if (curBill.Visible)
                {
                    curBill.Visible = false;
                    curMark.Visible = true;
                    break;
                }
                if (curBack.Visible)
                {
                    curBack.Visible = false;
                    curBill.Visible = true;
                    break;
                }
                if (curMark.Visible)
                {
                    curMark.Visible = false;
                    curBack.Visible = true;
                    break;
                }
            }
            while (RIGHT)
            {
                if (curMark.Visible)
                {
                    curMark.Visible = false;
                    curBill.Visible = true;
                    break;
                }
                if (curBill.Visible)
                {
                    curBill.Visible = false;
                    curBack.Visible = true;
                    break;
                }
                if (curBack.Visible)
                {
                    curBack.Visible = false;
                    curMark.Visible = true;
                    break;
                }
            }
            if (SELECT)
            {
                if (curBack.Visible)            // if the player selects 'Back', it will take them to the start screen
                {
                    formStart formStart = new formStart();
                    formStart.Show();
                    timerRun.Enabled = false;
                    this.Close();
                }
                if(curBill.Visible)
                {
                    charSelect = 1;
                    formGAME formGAME = new formGAME();
                    formGAME.Show();
                    timerRun.Enabled = false;
                    this.Close();
                }
                if (curMark.Visible)
                {
                    charSelect = 0;
                    formGAME formGAME = new formGAME();
                    formGAME.Show();
                    timerRun.Enabled = false;
                    this.Close();
                }
            }
        }
    }
}
