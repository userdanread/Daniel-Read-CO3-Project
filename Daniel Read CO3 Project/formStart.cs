using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Daniel_Read_CO3_Project
{
    public partial class formStart : Form
    {
        public formStart()
        {
            InitializeComponent();
        }

        bool UP = false;
        bool DOWN = false;
        bool SELECT = false;
        bool LEFT = false;
        bool RIGHT = false;
        private void formStart_keyDown(object sender, KeyEventArgs e)  
        {
            if ((e.KeyCode == Keys.W) || (e.KeyCode == Keys.Up))         // when the user presses the W or Up arrow key, the value UP becomes true and can be used later to determine the direction
                UP = true;                                               // the cursor will go
            if ((e.KeyCode == Keys.S) || (e.KeyCode == Keys.Down))
                DOWN = true;
            if (e.KeyCode == Keys.Space)
                SELECT = true;
            if ((e.KeyCode == Keys.A) || (e.KeyCode == Keys.Left))
                LEFT = true;
            if ((e.KeyCode == Keys.D) || (e.KeyCode == Keys.Right))
                RIGHT = true;
        }

        private void formStart_keyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.W) || (e.KeyCode == Keys.Up))
                UP = false;
            if ((e.KeyCode == Keys.S) || (e.KeyCode == Keys.Down))
                DOWN = false;
            if (e.KeyCode == Keys.Space)
                SELECT = false;
            if ((e.KeyCode == Keys.A) || (e.KeyCode == Keys.Left))
                LEFT = false;
            if ((e.KeyCode == Keys.D) || (e.KeyCode == Keys.Right))
                RIGHT = false;
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            while (DOWN)
            {
                if (curStartRun.Visible)                // if the cursor is on 'Start Run', when the player presses
                {                                       // down, the cursor will move to 'Options'
                    curStartRun.Visible = false;
                    curOptions.Visible = true;
                    break;                              // you must break or the code will jump down to quit game, because it sees that curOptions is visible
                }                                       // so does iterates that loop

                if (curOptions.Visible)                 // if the cursor is on 'Options', when the player presses
                {                                       // down, the cursor will move to 'Quit Game'
                    curOptions.Visible = false;
                    curStartRun.Visible = true;
                    break;
                }
                if(curQuitGame.Visible)
                {
                    curQuitGame.Visible = false;
                    curStartRun.Visible = true;
                    break;
                }
            }
            while (UP)
            {
                if (curQuitGame.Visible)                // if the cursor is on 'Quit Game', when the player presses
                {                                       // up, the cursor will move to 'Options
                    curQuitGame.Visible = false;
                    curStartRun.Visible = true;
                    break;
                }
                if (curOptions.Visible)                 // if the cursor is on 'Quit Game', when the player presses
                {                                       // up, the cursor will move to '
                    curOptions.Visible = false;
                    curStartRun.Visible = true;
                    break;
                }
                if(curStartRun.Visible)
                {
                    curStartRun.Visible = false;
                    curQuitGame.Visible = true;
                    break;
                }
            }
            while (RIGHT)
            {
                if (curOptions.Visible)
                {
                    curOptions.Visible = false;
                    curQuitGame.Visible = true;
                    break;
                }
                if (curQuitGame.Visible)
                {
                    curQuitGame.Visible = false;
                    curOptions.Visible = true;
                    break;
                }
                if (curStartRun.Visible)
                {
                    curStartRun.Visible = false;
                    curQuitGame.Visible = true;
                    break;
                }
            }
            while (LEFT)
            {
                if (curOptions.Visible)
                {
                    curOptions.Visible = false;
                    curQuitGame.Visible = true;
                    break;
                }
                if (curQuitGame.Visible)
                {
                    curQuitGame.Visible = false;
                    curOptions.Visible = true;
                    break;
                }
                if (curStartRun.Visible)
                {
                    curStartRun.Visible = false;
                    curOptions.Visible = true;
                    break;
                }
            }
            if (SELECT)                      // When the user presses the Spacebar to select an option
            {
                if (curStartRun.Visible)
                {
                    formRun formRun = new formRun();       // Creates a variable to represent the Run form within this form
                    this.Hide();                       // Hides the Start Menu  
                    timer1.Enabled = false;
                    formRun.Show();                         // Open the Run form 
       
                }
                if (curOptions.Visible)
                {
                    this.Hide();
                    formOptions formOptions = new formOptions();
                    timer1.Enabled = false;
                    formOptions.Show();
                }
                if (curQuitGame.Visible)
                    Application.Exit();                  // Selecting the Quit Game option will close the form, and thus the program.
            }
        }

    }
}