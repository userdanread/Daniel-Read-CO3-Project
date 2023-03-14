using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
namespace Daniel_Read_CO3_Project
{
    public partial class formGAME : Form
    {
        public formGAME()
        {
            InitializeComponent();
        }

        // ITEMS

        private Item GinDay;                        // Emma
        private Item Mechanical_Revolution;         // Ian

        private Item Tech1;            
        private Item RoboBasset_Basil;            
        private Item RoboPug_Ollie;             
        private Item Tech2;            
        private Item Proptosis;            
        private Active_Item Steroids;  



        private Character player;
        int playerHealth;
        int playerSpeed;
        int playerDmg;
        int playerFireRate;
        int playerRange;
        int playerKnockBack;
        int playerShotSpeed;
        int playerBombs;
        int playerKeys;
        int playerCoins;
        long score;

        int roomProg;   // this will be used to create the floor layout
        int playerProg; // this will be used to track the locaiton of the player on the floor.


        bool Uopen = false;
        bool Dopen = false;
        bool Lopen = false;
        bool Ropen = false;

        bool shot = false;
        int prevRoomDirection;

        // will use charSelect from formRun to set to true or false;
        bool Mark;
        bool Bill;

        public int[,] enemySpawns = new int[16, 4];

        

        private static Random getRandom = new Random();

        public static int getRandInt(int max, int min)   // RNG that creates a new random number each time.
        {
            lock (getRandom)
            {
                return getRandom.Next(min, max);
            }
        }


        public List<Item> ItemList = new List<Item>();

        private void formGame_Load(object sender, EventArgs e)
        {
            trophyBox.Visible = false;
            labWinScore.Visible = false;

            //trophyBox.Top = 987;
            //trophyBox.Left = 315;

            trophyBox.Location = new Point(987, 315);

            roomBorder.BringToFront();

            labBlack.SendToBack();
            gameBorder.BringToFront();
            pBoxDoorDown.BringToFront();
            pboxDoorUp.BringToFront();
            pBoxDoorRight.BringToFront();
            pBoxDoorLeft.BringToFront();
            lblPotentialSpawns.BringToFront();
            lblShopRoom.BringToFront();
            labTestingLabels.Visible = false;

            // TEST LABLES 

            foreach (Control X in this.Controls)
            {
                if (X is Label && X.Visible == true)
                {
                    X.BringToFront();
                }
            }

            //// ENEMIES
            //labFireRateTimer.BringToFront();
            //labEnemyClassList.BringToFront();
            //labCurrentEnemies.BringToFront();
            //labClearedRooms.BringToFront();
            //labEnemySpawnsArr.BringToFront();

            //// SHOOTING
            //lblBullets.BringToFront();
            //labCanShoot.BringToFront();
            //labCanShootTimer.BringToFront();
            //labBulletControls.BringToFront();

            //// ITEMS
            //labItemNum.BringToFront();
            //labCollectables.BringToFront();
            //labPickupNum.BringToFront();

            scoreStopWatch = Stopwatch.StartNew();
            duckHuntDog_Stopwatch = Stopwatch.StartNew();
            canShootStopWatch = Stopwatch.StartNew();
            enemiesMove = Stopwatch.StartNew();
            //canShootStopWatch.Stop();


            labEnemySpawnsArr.Text = "Room \t Type1 \t Spawn1 \t Type2 \t Spawn2 \t SpawnMore? \n";
            if (formRun.charSelect == 0)
            {
                Mark = true;
                Bill = false;
            }
            else
            {
                Bill = true;
                Mark = false;
            }
            score = 1000;
 
            playerFireRate = 800;
            playerRange = 10;
            playerKnockBack = 02;
            playerShotSpeed = 06;

            playerBombs = 00;
            playerKeys = 01;     // player will start with 1 key
            playerCoins = 03;
            // MARK
            if (Mark)
            {
                playerHealth = 04;  // stats corrospond to the symbols in the character select screen
                playerSpeed = 08;
                playerDmg = 10;

            }
            else // Bill
            {
                playerHealth = 02;
                playerSpeed = 10;
                playerDmg = 20;

            }


            // ITEM MODIFIERS
            // isVisible, fltSpeed, Xvel, Yvel, isGlued, HealthUp, SpeedUp, DmgUp, FireRateUp,ShotSpeedUp

            GinDay = new Item(true, 0, 0, 0, true, 1, 01, 0, 0, 0);
            ItemList.Add(GinDay);
            Mechanical_Revolution = new Item(true, 0, 0, 0, true, 0, 00, 10, 5, 0);
            ItemList.Add(Mechanical_Revolution);
            RoboPug_Ollie = new Item(true, 0, 0, 0, true, 0, 00, 10, 2, 5);
            ItemList.Add(RoboPug_Ollie);
            RoboBasset_Basil = new Item(true, 0, 0, 0, true, 1, 2, 15, 5, 5);
            ItemList.Add(RoboBasset_Basil);
            Tech1 = new Item(true, 0, 0, 0, true, 0, 00, 5, 10, 0);
            ItemList.Add(Tech1);
            Proptosis = new Item(true, 0, 0, 0, true, 1, 00, 0, 3, 5);
            ItemList.Add(Proptosis);
            Tech2 = new Item(true, 0, 0, 0, true, 0, 3, 5, 5, 5);
            ItemList.Add(Tech2);
            Steroids = new Active_Item(true, 0, 0, 0, true, 0, 01, 1, 0, 0, 3, true);
            ItemList.Add(Steroids);

            
            // CREATING THE PLAYER
            player = new Character(true, 0, 0, 0, false, playerHealth, playerSpeed,
                playerDmg, playerFireRate, playerRange, playerKnockBack, playerShotSpeed, playerBombs, playerKeys, playerCoins);

            if (Mark)
                pBox_Player.Image = Properties.Resources.squareMarkfront;
            if (Bill)
                pBox_Player.Image = Properties.Resources.squareBillfront;

            this.Location = new Point(0, 0);
            pBox_Player.Location = new Point(410, 467);  // Centre of the form
                                                         // pBox_Player.Location = new Point(280, 162);  // Centre of the screen

            pBox_Player.Tag = "player";

            pBox_Player.BringToFront();
            // MAP stuff
            roomProg = 0;  // the starting room
            playerProg = 0;



            label1.Text = "";



            // foreach (int x in roomDirections)
            //      lblPrevRoomAndDirection.Text += x.ToString() + " " ;

            findSpeicalRooms();
            makeMap();


            for (int y = 0; y < map.GetLength(0); y++) // collumn
            {
                for (int x = 0; x < map.GetLength(1); x++) // row
                {
                    if (map[y, x] == 999)
                        label1.Text += "     ";
                    else
                    {
                        if (map[y, x] >= 10)
                            label1.Text += " " + Convert.ToString(map[y, x]) + " ";
                        else
                            label1.Text += " " + Convert.ToString(map[y, x]) + "  ";

                    }
                }
                label1.Text += "\n";
            }

            lblShopRoom.Text = "Shop: " + Convert.ToString(shopRoom);
            lblPotentialSpawns.Text = "Treasure Rooms: ";
            foreach (int x in treasureRooms)
                lblPotentialSpawns.Text += Convert.ToString(x) + " ";

            updateLabels();
            findOpenDoor(playerProg);

            for (int c = clearedRooms.Length - 1; c > 0; c--)
                clearedRooms[c] = 0;

            for (int h = 0; h < player.getCharHealth()-1; h++)
            {
                PictureBox Health = new PictureBox();
                Health.SizeMode = PictureBoxSizeMode.Zoom;
                Health.Image = Properties.Resources.full_heart;
                Health.Height = 30;
                Health.Width = 30;

                Health.Name = "Heart" + Convert.ToString(h);
                Health.Tag = "Heart";


                Health.Location = new Point(70, 25);

                this.Controls.Add(Health);
                foreach (Control X in this.Controls)
                    if (X is PictureBox && X.Tag == "Heart" && X != Health)
                    {
                        if (ControlCollision(Health,X))
                        {
                            Health.Location = new Point(X.Location.X + X.Width + 10, X.Location.Y);
                        }
                    }
 
            }

            foreach (Control X in this.Controls)
            {

                if (X is PictureBox && X.Tag == "Heart")
                    X.BringToFront();
                if (X is PictureBox && X.Tag == "item")
                    X.BringToFront();
            }

            spawnEnemies(enemySpawns, playerProg);

            trophyBox.BringToFront();
        }



        // MAP AND ROOM LAYOUT  

        private int getRoomY(int room) // here you would input roomProg
        {
            for (int y = 0; y < map.GetLength(0); y++) // for each collumn
            {
                for (int x = 0; x < map.GetLength(1); x++) // row
                {
                    if (map[y, x] == room)
                        return y;
                }
            }
            return 0;
        }
        private int getRoomX(int room)
        {
            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    if (map[y, x] == room)
                        return x;
                }
            }
            return 0;
        }

        // POTENTIAL NEW MAP ALGORITHM


        int[,] map = new int[13, 19];
        public int[] roomDirections = new int[15];

        private void makeMap()   // private or public idk
        {

            for (int y = 0; y < 13; y++)    // for every row
            {
                for (int x = 0; x < 19; x++) // for every collum
                {
                    map[y, x] = 999;
                }
            }


            int startY = getRandInt(11, 1);
            int startX = getRandInt(17, 1);

            int startroomX = startX;
            int startroomY = startY;

            map[startY, startX] = 0;  // starting room
            int currentRoom = 0;
            int numRooms = 1;

            while (numRooms < 16)
            {
                int[] potential_directions = new int[4];
                //  Check for empty spaces around current room
                try
                {
                    if ((startY > 0) && (map[startY - 1, startX] == 999))   // UP
                    {
                        for (int a = 0; a < 5; a++)
                        {
                            if (potential_directions[a] == 0)
                            {
                                potential_directions[a] = 1;
                                break;
                            }
                        }
                    }
                }
                catch { }
                try
                {
                    if ((startY < 11) && (map[startY + 1, startX] == 999))   // DOWN
                    {
                        for (int a = 0; a < 5; a++)
                        {
                            if (potential_directions[a] == 0)
                            {
                                potential_directions[a] = 2;
                                break;
                            }
                        }
                    }
                }
                catch { }
                try
                {
                    if ((startX > 0) && (map[startY, startX - 1] == 999))   // LEFT
                    {
                        for (int a = 0; a < 5; a++)
                        {
                            if (potential_directions[a] == 0)
                            {
                                potential_directions[a] = 3;
                                break;
                            }
                        }
                    }
                }
                catch { }
                try
                {
                    if ((startX < 17) && (map[startY, startX + 1] == 999))   // RIGHT
                    {
                        for (int a = 0; a < 5; a++)
                        {
                            if (potential_directions[a] == 0)
                            {
                                potential_directions[a] = 4;
                                break;
                            }
                        }
                    }
                }
                catch { }

                int SumOfPot = 0;
                for (int x = potential_directions.Length - 1; x >= 0; x--)
                {                                                           // IF THERE ARE NO POTENTIAL DIRECTIONS, BACKTRACK TO PREVIOUS ROOM
                    SumOfPot += potential_directions[x];
                }

                if (SumOfPot == 0)
                {
                    map[startY, startX] = 999;
                    currentRoom -= 1;                       // ERROR - the starting room is flagged as a room without any potential directions
                    startX = getRoomX(currentRoom);
                    startY = getRoomY(currentRoom);
                    numRooms -= 1;
                }
                else
                {
                    int direction = potential_directions[getRandInt(potential_directions.Length - 1, 0)];
                    while (direction == 0)
                        direction = potential_directions[getRandInt(potential_directions.Length - 1, 0)];



                    if (direction == 1)  // up
                        startY -= 1;
                    if (direction == 2) // down
                        startY += 1;
                    if (direction == 3) // left
                        startX -= 1;
                    if (direction == 4) // right
                        startX += 1;

                    for (int x = 0; x < 15; x++)
                        if (roomDirections[x] == 0)
                        {
                            roomDirections[x] = direction;
                            break;
                        }

                    // label newly generated room
                    currentRoom += 1;
                    map[startY, startX] = currentRoom;
                    numRooms += 1;

                    // GENERATING ENEMIES FOR EACH ROOM


                    int enemytype = getRandInt(4, 1);
                    int enemyspawns = getRandInt(5, 1);
                    enemySpawns[currentRoom, 0] = enemytype;
                    enemySpawns[currentRoom, 1] = enemyspawns;

                    int spawnMultitypes = getRandInt(4, 0);     // 1/3 chance to spawn more enemies than usual into the room
                    if (spawnMultitypes == 3)
                    {
                        enemyspawns = getRandInt(3, 1);
                        enemytype = getRandInt(4, 1);
                        enemySpawns[currentRoom, 2] = enemytype;
                        enemySpawns[currentRoom, 3] = enemyspawns;
                    }
                    else
                    {
                        enemySpawns[currentRoom, 2] = 0;
                        enemySpawns[currentRoom, 3] = 0;
                    }


                    // NO ENEMIES SPAWN IN BOSS ROOM, START ROOM, SHOP OR TREASURE ROOMS
                    if ((currentRoom == 15) || (currentRoom == 0) || (currentRoom == shopRoom) || (currentRoom == treasureRooms[0]) || (currentRoom == treasureRooms[1]))
                    {
                        enemySpawns[currentRoom, 1] = 0;
                        enemySpawns[currentRoom, 0] = 0;
                        enemySpawns[currentRoom, 2] = 0;
                        enemySpawns[currentRoom, 3] = 0;
                    }

                    labEnemySpawnsArr.Text += Convert.ToString(currentRoom) + "           " +
                        Convert.ToString(enemySpawns[currentRoom, 0]) + "            " +
                        Convert.ToString(enemySpawns[currentRoom, 1]) + "            " +
                        Convert.ToString(enemySpawns[currentRoom, 2]) + "            " +
                        Convert.ToString(enemySpawns[currentRoom, 3]) + "            ";

                    if (spawnMultitypes == 3)
                        labEnemySpawnsArr.Text += "T" + "\n";
                    else
                        labEnemySpawnsArr.Text += "F" + "\n";
                }
            }
        }



        public int[] treasureRooms = new int[2];                     // to make sure i dont get duplicate special rooms 
        public int shopRoom;
        private void findSpeicalRooms()
        {

            for (int loops = 0; loops < 2; loops++)
            {
                int randomRoom = getRandInt(14, 1);
                for (int x = 0; x < 2; x++)
                {
                    if (treasureRooms[x] == randomRoom)   // makes the treasure room, making sure there are no duplicates
                        randomRoom = getRandInt(14, 1);
                }
                for (int x = 0; x < 2; x++)                 // adds the room num to a list of the treasure rooms
                {
                    if (treasureRooms[x] == 0)
                    {
                        treasureRooms[x] = randomRoom;
                        break;
                    }
                }

                randomRoom = getRandInt(14, 1);
                for (int x = 0; x < 2; x++)
                {
                    if (treasureRooms[x] == randomRoom)
                        randomRoom = getRandInt(14, 1);
                }
                shopRoom = randomRoom;                  // makes a shop
                
            }
        }
        private void findOpenDoor(int room)
        {


            if (room != 0 && room != 15)   // the starting room should only have 1 door open, because you can't go back any further
            {

                prevRoomDirection = roomDirections[playerProg - 1];
                // up
                try
                {
                    if (map[getRoomY(room) - 1, getRoomX(room)] == room + 1)
                    {

                        pboxDoorUp.Image = Properties.Resources.door_open;      // the door to the next room AND previous room need to be open
                        Uopen = true;

                        if (prevRoomDirection == 1)
                        {
                            Dopen = true;
                            Lopen = false;
                            Ropen = false;
                            pBoxDoorDown.Image = Properties.Resources.door_open;
                            pBoxDoorLeft.Image = Properties.Resources.door_closed;
                            pBoxDoorRight.Image = Properties.Resources.door_closed;
                        }
                        if (prevRoomDirection == 4)
                        {
                            Dopen = false;
                            Lopen = true;
                            Ropen = false;
                            pBoxDoorDown.Image = Properties.Resources.door_closed;
                            pBoxDoorLeft.Image = Properties.Resources.door_open;
                            pBoxDoorRight.Image = Properties.Resources.door_closed;
                        }
                        if (prevRoomDirection == 3)
                        {
                            Dopen = false;
                            Lopen = false;
                            Ropen = true;
                            pBoxDoorDown.Image = Properties.Resources.door_closed;
                            pBoxDoorLeft.Image = Properties.Resources.door_closed;
                            pBoxDoorRight.Image = Properties.Resources.door_open;
                        }
                    }
                }
                catch { }
                // down
                try
                {
                    if (map[getRoomY(room) + 1, getRoomX(room)] == room + 1)
                    {
                        pBoxDoorDown.Image = Properties.Resources.door_open;
                        Dopen = true;

                        if (prevRoomDirection == 2)
                        {
                            Uopen = true;
                            Lopen = false;
                            Ropen = false;
                            pboxDoorUp.Image = Properties.Resources.door_open;
                            pBoxDoorLeft.Image = Properties.Resources.door_closed;
                            pBoxDoorRight.Image = Properties.Resources.door_closed;
                        }
                        if (prevRoomDirection == 4)
                        {
                            Uopen = false;
                            Lopen = true;
                            Ropen = false;
                            pboxDoorUp.Image = Properties.Resources.door_closed;
                            pBoxDoorLeft.Image = Properties.Resources.door_open;
                            pBoxDoorRight.Image = Properties.Resources.door_closed;
                        }
                        if (prevRoomDirection == 3)
                        {
                            Uopen = false;
                            Lopen = false;
                            Ropen = true;
                            pboxDoorUp.Image = Properties.Resources.door_closed;
                            pBoxDoorLeft.Image = Properties.Resources.door_closed;
                            pBoxDoorRight.Image = Properties.Resources.door_open;
                        }
                    }
                }
                catch { }
                // right
                try
                {
                    if (map[getRoomY(room), getRoomX(room) + 1] == room + 1)
                    {
                        Ropen = true;
                        pBoxDoorRight.Image = Properties.Resources.door_open;

                        if (prevRoomDirection == 1)
                        {
                            Uopen = false;
                            Dopen = true;
                            Lopen = false;
                            pBoxDoorDown.Image = Properties.Resources.door_open;
                            pBoxDoorLeft.Image = Properties.Resources.door_closed;
                            pboxDoorUp.Image = Properties.Resources.door_closed;
                        }
                        if (prevRoomDirection == 2)
                        {
                            Uopen = true;
                            Dopen = false;
                            Lopen = false;
                            pBoxDoorDown.Image = Properties.Resources.door_closed;
                            pBoxDoorLeft.Image = Properties.Resources.door_closed;
                            pboxDoorUp.Image = Properties.Resources.door_open;
                        }
                        if (prevRoomDirection == 4)
                        {
                            Uopen = false;
                            Dopen = false;
                            Lopen = true;
                            pBoxDoorDown.Image = Properties.Resources.door_closed;
                            pBoxDoorLeft.Image = Properties.Resources.door_open;
                            pboxDoorUp.Image = Properties.Resources.door_closed;
                        }

                    }
                }
                catch { }
                // left
                try
                {
                    if (map[getRoomY(room), getRoomX(room) - 1] == room + 1)
                    {
                        Lopen = true;
                        pBoxDoorLeft.Image = Properties.Resources.door_open;

                        if (prevRoomDirection == 1)
                        {
                            Uopen = false;
                            Dopen = true;
                            Ropen = false;
                            pBoxDoorDown.Image = Properties.Resources.door_open;
                            pBoxDoorRight.Image = Properties.Resources.door_closed;
                            pboxDoorUp.Image = Properties.Resources.door_closed;
                        }
                        if (prevRoomDirection == 2)
                        {
                            Uopen = true;
                            Dopen = false;
                            Ropen = false;
                            pBoxDoorDown.Image = Properties.Resources.door_closed;
                            pBoxDoorRight.Image = Properties.Resources.door_closed;
                            pboxDoorUp.Image = Properties.Resources.door_open;
                        }
                        if (prevRoomDirection == 3)
                        {
                            Uopen = false;
                            Dopen = false;
                            Ropen = true;
                            pBoxDoorDown.Image = Properties.Resources.door_closed;
                            pBoxDoorRight.Image = Properties.Resources.door_open;
                            pboxDoorUp.Image = Properties.Resources.door_closed;
                        }
                    }
                }
                catch { }
            }
            else if (room == 0)
            {
                try
                {
                    if (map[getRoomY(room) - 1, getRoomX(room)] == room + 1)
                    {
                        Uopen = true;
                        Dopen = false;
                        Lopen = false;
                        Ropen = false;

                        pboxDoorUp.Image = Properties.Resources.door_open;      // the door to the next room AND previous room need to be open
                        pBoxDoorDown.Image = Properties.Resources.door_closed;
                        pBoxDoorRight.Image = Properties.Resources.door_closed;
                        pBoxDoorLeft.Image = Properties.Resources.door_closed;
                    }
                }
                catch { }
                try
                {
                    // down
                    if (map[getRoomY(room) + 1, getRoomX(room)] == room + 1)
                    {
                        Uopen = false;
                        Dopen = true;
                        Lopen = false;
                        Ropen = false;

                        pboxDoorUp.Image = Properties.Resources.door_closed;
                        pBoxDoorDown.Image = Properties.Resources.door_open;
                        pBoxDoorRight.Image = Properties.Resources.door_closed;
                        pBoxDoorLeft.Image = Properties.Resources.door_closed;
                    }
                }
                catch { }
                try
                {
                    // right
                    if (map[getRoomY(room), getRoomX(room) + 1] == room + 1)
                    {
                        Uopen = false;
                        Dopen = false;
                        Lopen = false;
                        Ropen = true;

                        pboxDoorUp.Image = Properties.Resources.door_closed;
                        pBoxDoorDown.Image = Properties.Resources.door_closed;
                        pBoxDoorRight.Image = Properties.Resources.door_open;
                        pBoxDoorLeft.Image = Properties.Resources.door_closed;
                    }
                }
                catch { }
                try
                {
                    // left
                    if (map[getRoomY(room), getRoomX(room) - 1] == room + 1)
                    {
                        Uopen = false;
                        Dopen = false;
                        Lopen = true;
                        Ropen = false;

                        pboxDoorUp.Image = Properties.Resources.door_closed;
                        pBoxDoorDown.Image = Properties.Resources.door_closed;
                        pBoxDoorRight.Image = Properties.Resources.door_closed;
                        pBoxDoorLeft.Image = Properties.Resources.door_open;
                    }
                }
                catch { }
            }
            else if (room == 15)
            {
                prevRoomDirection = roomDirections[playerProg - 1];

                if (prevRoomDirection == 1)
                {
                    Uopen = false;
                    Dopen = true;
                    Ropen = false;
                    Lopen = false;
                    pBoxDoorDown.Image = Properties.Resources.door_open;
                    pBoxDoorRight.Image = Properties.Resources.door_closed;
                    pboxDoorUp.Image = Properties.Resources.door_closed;
                    pBoxDoorLeft.Image = Properties.Resources.door_closed;
                }
                if (prevRoomDirection == 2)
                {
                    Uopen = true;
                    Dopen = false;
                    Ropen = false;
                    Lopen = false;
                    pBoxDoorDown.Image = Properties.Resources.door_closed;
                    pBoxDoorRight.Image = Properties.Resources.door_closed;
                    pboxDoorUp.Image = Properties.Resources.door_open;
                    pBoxDoorLeft.Image = Properties.Resources.door_closed;
                }
                if (prevRoomDirection == 4)
                {
                    Uopen = false;
                    Dopen = false;
                    Ropen = false;
                    Lopen = true;
                    pBoxDoorDown.Image = Properties.Resources.door_closed;
                    pBoxDoorRight.Image = Properties.Resources.door_closed;
                    pboxDoorUp.Image = Properties.Resources.door_closed;
                    pBoxDoorLeft.Image = Properties.Resources.door_open;
                }
                if (prevRoomDirection == 3)
                {
                    Uopen = false;
                    Dopen = false;
                    Ropen = true;
                    Lopen = false;
                    pBoxDoorDown.Image = Properties.Resources.door_closed;
                    pBoxDoorRight.Image = Properties.Resources.door_open;
                    pboxDoorUp.Image = Properties.Resources.door_closed;
                    pBoxDoorLeft.Image = Properties.Resources.door_closed;
                }


            }
        }



        bool move_UP = false;
        bool move_DOWN = false;
        bool move_LEFT = false;
        bool move_RIGHT = false;

        bool shoot_UP = false;
        bool shoot_DOWN = false;
        bool shoot_LEFT = false;
        bool shoot_RIGHT = false;




        bool SELECT = false;
        bool PAUSE = false;
        private void formGame_keyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)    // WASD control movement
                move_UP = false;
            if (e.KeyCode == Keys.S)
                move_DOWN = false;
            if (e.KeyCode == Keys.A)
                move_LEFT = false;
            if (e.KeyCode == Keys.D)
                move_RIGHT = false;

            if (e.KeyCode == Keys.Up)  // arrow keys control shooting
                shoot_UP = false;
            if (e.KeyCode == Keys.Down)
                shoot_DOWN = false;
            if (e.KeyCode == Keys.Left)
                shoot_LEFT = false;
            if (e.KeyCode == Keys.Right)
                shoot_RIGHT = false;

            if (e.KeyCode == Keys.Space)
                SELECT = false;
            if (e.KeyCode == Keys.Escape)
                PAUSE = false;
        }

        private void formGame_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
                move_UP = true;
            if (e.KeyCode == Keys.S)
                move_DOWN = true;
            if (e.KeyCode == Keys.A)
                move_LEFT = true;
            if (e.KeyCode == Keys.D)
                move_RIGHT = true;

            if (e.KeyCode == Keys.Up)
            {
                shoot_UP = true;

                if (shot == false)
                {
                    makeBullet(1);
                    shot = true;
                }
                if (shot == true)
                    shot = false;
            }

            if (e.KeyCode == Keys.Down)
            {
                shoot_DOWN = true;

                if (shot == false)
                {
                    makeBullet(2);
                    shot = true;
                }
                if (shot == true)
                    shot = false;
            }

            if (e.KeyCode == Keys.Right)
            {
                shoot_RIGHT = true;

                if (shot == false)
                {
                    makeBullet(3);
                    shot = true;
                }

                if (shot == true)
                    shot = false;
            }

            if (e.KeyCode == Keys.Left)
            {
                shoot_LEFT = true;


                if (shot == false)
                {
                    makeBullet(4);
                    shot = true;
                }
                if (shot == true)
                    shot = false;


            }



            if (e.KeyCode == Keys.Space)
                SELECT = true;
            if (e.KeyCode == Keys.Escape)
                PAUSE = true;
        }

        private bool pcollision(PictureBox a, PictureBox b)
        {
            Rectangle pRect = new Rectangle(a.Location.X, a.Location.Y, a.Width, a.Height);
            Rectangle dRect = new Rectangle(b.Location.X, b.Location.Y, b.Width, b.Height);

            if (pRect.IntersectsWith(dRect))
                return true;
            return false;
        }

        private bool PicureBoxConrolCollision(Control a, PictureBox b)
        {
            Rectangle pRect = new Rectangle(a.Location.X, a.Location.Y, a.Width, a.Height);
            Rectangle dRect = new Rectangle(b.Location.X, b.Location.Y, b.Width, b.Height);

            if (pRect.IntersectsWith(dRect))
                return true;
            return false;
        }
        private bool ControlCollision(Control a, Control b)
        {
            PictureBox controlBoxA = new PictureBox();
            controlBoxA.Location = a.Location;
            controlBoxA.Height = a.Height;
            controlBoxA.Width = a.Width;


            PictureBox controlBoxB = new PictureBox();
            controlBoxB.Location = b.Location;
            controlBoxB.Height = b.Height;
            controlBoxB.Width = b.Width;

            Rectangle pRect = new Rectangle(controlBoxA.Location.X, controlBoxA.Location.Y, controlBoxA.Width, controlBoxA.Height);
            Rectangle dRect = new Rectangle(controlBoxB.Location.X, controlBoxB.Location.Y, controlBoxB.Width, controlBoxB.Height);

            if (pRect.IntersectsWith(dRect))
                return true;
            return false;
        }

        private bool leftCollision(PictureBox box1, PictureBox box2)
        {
            if (box1.Location.X <= box2.Location.X)
                return true;
            return false;
        }
        private bool rightCollision(PictureBox box1, PictureBox box2)
        {
            if (box1.Location.X >= (box2.Location.X + (box2.Width - (box1.Width))))
                return true;
            return false;
        }
        private bool upCollision(PictureBox box1, PictureBox box2)
        {
            if (box1.Location.Y <= box2.Location.Y)
                return true;
            return false;
        }
        private bool downCollision(PictureBox box1, PictureBox box2)
        {
            if (box1.Location.Y >= (box2.Location.Y + (box2.Height - (box1.Height))))
                return true;
            return false;
        }






        private void updateLabels()
        {
            trophyBox.BringToFront();
            // HEALTH
            // Remove any old health boxes
            for (int x = 0; x < player.getCharHealth(); x++)
            { 
                foreach (Control H in this.Controls)
                {
                    if (H is PictureBox && H.Tag == "Heart")
                    {
                        this.Controls.Remove(H);
                        H.Dispose();
                    }
                }
            }
            for (int i = 0; i < player.getCharHealth(); i++)
            {

                PictureBox Health = new PictureBox();
                Health.SizeMode = PictureBoxSizeMode.Zoom;
                Health.Image = Properties.Resources.full_heart;
                Health.Height = 30;
                Health.Width = 30;

                Health.Name = "Heart" + Convert.ToString(i);
                Health.Tag = "Heart";


                Health.Location = new Point(70, 25);

                this.Controls.Add(Health);
                foreach (Control X in this.Controls)
                {
                    if (X is PictureBox && X.Tag == "Heart" && X != Health)
                    {
                        if (ControlCollision(Health, X))
                        {
                            Health.Location = new Point(X.Location.X + X.Width + 10, X.Location.Y);
                        }
                    }
                }
            }
            if (player.getCharHealth() == 1)
            {
                for (int x = 0; x < player.getCharHealth(); x++)
                {
                    foreach (Control H in this.Controls)
                    {
                        if (H is PictureBox && H.Tag == "Heart")
                        {
                            this.Controls.Remove(H);
                            H.Dispose();
                        }
                    }
                }

                PictureBox Health = new PictureBox();
                Health.SizeMode = PictureBoxSizeMode.Zoom;
                Health.Image = Properties.Resources.full_heart;
                Health.Height = 30;
                Health.Width = 30;

                Health.Name = "Heart" + Convert.ToString(0);
                Health.Tag = "Heart";


                Health.Location = new Point(70, 25);

                this.Controls.Add(Health);

            }

            foreach (Control X in this.Controls)
            {

                if (X is PictureBox && X.Tag == "Heart")
                    X.BringToFront();
            }



                // stats
            labBombs.Text = player.getCharBombs().ToString("00");
            labKeys.Text = player.getCharKeys().ToString("00");
            labCoins.Text = player.getCharCoins().ToString("00");
            labScore.Text = score.ToString("00");
            labDmg.Text = player.getCharDmg().ToString("00");
            labSpeed.Text = player.getCharSpeed().ToString("00");
            labFireRate.Text = (player.getCharFireRate() / 100).ToString("00");
            labShotSpeed.Text = player.getCharShotSpeed().ToString("00");
            labRange.Text = playerRange.ToString("00");
            labKnockBack.Text = playerKnockBack.ToString("00");

            // room
            lblRoomNum.Text = "Curent Room: " + playerProg.ToString();

            // clearedRooms
            labClearedRooms.Text = "Cleared Rooms: ";
            for (int c = 0; c < clearedRooms.Length - 1; c++)
                labClearedRooms.Text += Convert.ToString(clearedRooms[c]) + " ";


            for (int x = 0; x < 2; x++)
            {
                if (treasureRooms[x] == playerProg)
                {
                    pTreasureText.Visible = true;
                    pPedastal.Visible = true;
                    labTreasurePrice.Visible = true;
                    pShopText.Visible = false;
                    labShopPrice.Visible = false;
                    pMarketStall.Visible = false;

                    break;
                }
                else if (shopRoom == playerProg)
                {
                    pShopText.Visible = true;
                    pTreasureText.Visible = false;
                    labShopPrice.Visible = true;
                    pMarketStall.Visible = true;
                    labTreasurePrice.Visible = false;
                    pPedastal.Visible = false;
                    break;
                }
                else
                {
                    pShopText.Visible = false;
                    pTreasureText.Visible = false;
                    pMarketStall.Visible = false;
                    labShopPrice.Visible = false;
                    labTreasurePrice.Visible = false; 
                    pPedastal.Visible = false;
                }
            }

           

        }
        private bool ControlleftCollision(Control box1, PictureBox box2)
        {
            if (box1.Location.X <= box2.Location.X)
                return true;
            return false;
        }
        private bool ControlrightCollision(Control box1, PictureBox box2)
        {
            if (box1.Location.X >= (box2.Location.X + (box2.Width - (box1.Width))))
                return true;
            return false;
        }
        private bool ControlupCollision(Control box1, PictureBox box2)
        {
            if (box1.Location.Y <= box2.Location.Y)
                return true;
            return false;
        }
        private bool ControldownCollision(Control box1, PictureBox box2)
        {
            if (box1.Location.Y >= (box2.Location.Y + (box2.Height - (box1.Height))))
                return true;
            return false;
        }



        
        private void spawnEnemies(int[,] enemyList, int room) // enemylist will be the public EnemySpawns 2D array
        {
            int enemyHealth;
            int enemySpeed;
            int enemyFireRate;
            int enemyType;

            int x = enemyList[room, 1];

            // enemyList =              enemyType1  enemySpawns1  enemyType2  enemySpawns2
            //                    room          

            for (int y = 0; y < x; y++)     // for the amount of enemySpawns
            {
                Enemy enemy = null;

                PictureBox enemyBox = new PictureBox();
                enemyBox.Tag = "enemy";
                enemyBox.Width = 20;
                enemyBox.Height = 20;
                enemyBox.Visible = true;
                enemyBox.SizeMode = PictureBoxSizeMode.Zoom;
                
                if (enemyList[room, 0] == 1)
                {
                    enemyBox.Image = Properties.Resources.enemygreen;
                    enemyHealth = 10;
                    enemyFireRate = 750;
                    enemySpeed = 5;
                    enemyType = 1;
                    enemyBox.Width = 30;
                    enemyBox.Height = 30;
                    enemyBox.Name = Convert.ToString(y);
                    enemy = new Enemy(true, 0, 0, 0, false, enemyHealth, enemySpeed, enemyFireRate, y, enemyType);
                }
                if (enemyList[room, 0] == 2)
                {
                    enemyBox.Image = Properties.Resources.enemyblue;
                    enemyHealth = 20;
                    enemyFireRate = 1000; 
                    enemySpeed = 4;
                    enemyType = 2;
                    enemyBox.Width = 35;
                    enemyBox.Height = 35;
                    enemyBox.Name = Convert.ToString(y);
                    enemy = new Enemy(true, 0, 0, 0, false, enemyHealth, enemySpeed, enemyFireRate, y, enemyType);
                }
                if (enemyList[room, 0] == 3)
                {
                    enemyBox.Image = Properties.Resources.enemypurple;
                    enemyHealth = 30;
                    enemyFireRate = 1000;
                    enemySpeed = 3;
                    enemyType = 3;
                    enemyBox.Width = 40;
                    enemyBox.Height = 40;
                    enemyBox.Name = Convert.ToString(y);
                    enemy = new Enemy(true, 0, 0, 0, false, enemyHealth, enemySpeed, enemyFireRate, y, enemyType);
                }
                if (enemyList[room, 0] == 4)
                {
                    enemyBox.Image = Properties.Resources.enemyred;
                    enemyHealth = 40;
                    enemyFireRate = 1500;
                    enemySpeed = 2;
                    enemyType = 4;
                    enemyBox.Width = 45;
                    enemyBox.Height = 45;
                    enemyBox.Name = Convert.ToString(y);
                    enemy = new Enemy(true, 0, 0, 0, false, enemyHealth, enemySpeed, enemyFireRate, y, enemyType);
                }




                enemyBox.Top = getRandInt(400, 220);
                enemyBox.Left = getRandInt(560, 330);


                foreach (Control X in this.Controls)
                {
                    if (X.Tag == "enemy")
                    {
                        if (ControlCollision(X, enemyBox))
                        {
                            //labEnemyCollisions.Text = "EnemyCollisionsDetected? " + " T";
                            //labEnemyCollisions.Text += "\nNew enemy spawn:           " + "X: " + Convert.ToString(enemyBox.Location.X) + " " + "Y: " + Convert.ToString(enemyBox.Location.Y) + "\n";
                            //labEnemyCollisions.Text += "New enemy colliding with: " + "X: " + Convert.ToString(X.Location.X) + " " + "Y: " + Convert.ToString(X.Location.Y) + "\n";

                            enemyBox.Top = getRandInt(400, 220);
                            enemyBox.Left = getRandInt(560, 330);
                        }
                    }
                }
                this.Controls.Add(enemyBox);

                if (enemy != null)
                    enemyClassList.Add(enemy);
            }

            
            for (int z = enemyList[room, 3]; z > 0; z--)
            {
                Enemy enemy = null;
                PictureBox enemyBox = new PictureBox();
                enemyBox.Tag = "enemy";
                enemyBox.Visible = true;
                enemyBox.SizeMode = PictureBoxSizeMode.Zoom;
                enemyBox.Width = 20;
                enemyBox.Height = 30;
                // Multispawns
                if (enemyList[room, 2] == 1)
                {
                    enemyBox.Image = Properties.Resources.enemygreen;
                    enemyHealth = 10;
                    enemyFireRate = 750;
                    enemySpeed = 5;
                    enemyType = 1;
                    enemyBox.Width = 30;
                    enemyBox.Height = 30;
                    enemyBox.Name = Convert.ToString(x + (z - 1));
                    enemy = new Enemy(true, 0, 0, 0, false, enemyHealth, enemySpeed, enemyFireRate, (x + (z - 1)), enemyType);
                }
                if (enemyList[room, 2] == 2)
                {
                    enemyBox.Image = Properties.Resources.enemyblue;
                    enemyHealth = 20;
                    enemyFireRate = 1000;
                    enemySpeed = 4;
                    enemyType = 2;
                    enemyBox.Width = 35;
                    enemyBox.Height = 35;
                    enemyBox.Name = Convert.ToString(x + (z - 1));
                    enemy = new Enemy(true, 0, 0, 0, false, enemyHealth, enemySpeed, enemyFireRate, (x + (z - 1)), enemyType);
                }
                if (enemyList[room, 2] == 3)
                {
                    enemyBox.Image = Properties.Resources.enemypurple;
                    enemyHealth = 30;
                    enemyFireRate = 1000;
                    enemySpeed = 3;
                    enemyType = 3;
                    enemyBox.Width = 40;
                    enemyBox.Height = 40;
                    enemyBox.Name = Convert.ToString(x + (z - 1));
                    enemy = new Enemy(true, 0, 0, 0, false, enemyHealth, enemySpeed, enemyFireRate, (x + (z - 1)), enemyType);
                }
                if (enemyList[room, 2] == 4)
                {
                    enemyBox.Image = Properties.Resources.enemyred;
                    enemyHealth = 40;
                    enemyFireRate = 1500;
                    enemySpeed = 2;
                    enemyType = 4;
                    enemyBox.Width = 45;
                    enemyBox.Height = 45;
                    enemyBox.Name = Convert.ToString(x + (z - 1));
                    enemy = new Enemy(true, 0, 0, 0, false, enemyHealth, enemySpeed, enemyFireRate, (x + (z - 1)), enemyType);
                }


                enemyBox.Top = getRandInt(400, 220);
                enemyBox.Left = getRandInt(560, 330);



                foreach (Control X in this.Controls)
                {
                    if (X.Tag == "enemy")
                    {
                        if (ControlCollision(X, enemyBox))
                        {
                            //labEnemyCollisions.Text = "EnemyCollisionsDetected? " + " T";
                            //labEnemyCollisions.Text += "\nNew enemy spawn:                 " + "X: " + Convert.ToString(enemyBox.Location.X) + " " + "Y: " + Convert.ToString(enemyBox.Location.Y) + "\n";
                            //labEnemyCollisions.Text += "New enemy colliding with:    " + "X: " + Convert.ToString(X.Location.X) + " " + "Y: " + Convert.ToString(X.Location.Y) + "\n";

                            enemyBox.Top = getRandInt(400, 220);
                            enemyBox.Left = getRandInt(560, 330);
                        }
                    }
                }
                this.Controls.Add(enemyBox);

                if (enemy != null)
                    enemyClassList.Add(enemy);

                
            }
        }
        public int totalBullets = 1;
        public List<Control> bulletControls = new List<Control>();
        public List<Enemy> enemyClassList = new List<Enemy>();

        public PictureBox bossBox = new PictureBox();
        Boss boss = null;
        public void spawnBoss()
        {
            int bossHealth;
            int bossFireRate;
            int bossSpeed;
            int randBoss = getRandInt(3, 1);
            
            bossBox.SizeMode = PictureBoxSizeMode.Zoom;

            switch (randBoss)
            {
                case (1):
                    bossBox.Image = Properties.Resources.Boss1;
                    bossBox.Width = 70;
                    bossBox.Height = 70;
                    bossHealth = 250;
                    bossFireRate = 1000;
                    bossSpeed = 8;
                    
                    boss = new Boss(true, 0, 0, 0, false, bossHealth, bossSpeed, bossFireRate, 10, "boss");
                    break;
                case (2):
                    bossBox.Image = Properties.Resources.Boss2;
                    bossBox.Width = 80;
                    bossBox.Height = 80;
                    bossHealth = 300;
                    bossFireRate = 1000;
                    bossSpeed = 7;
                    boss = new Boss(true, 0, 0, 0, false, bossHealth, bossSpeed, bossFireRate, 10, "boss");
                    break;
                case (3):
                    bossBox.Image = Properties.Resources.Boss3;
                    bossBox.Height = 70;
                    bossBox.Width = 70;
                    bossHealth = 280;
                    bossFireRate = 1000;
                    bossSpeed = 8;
                    boss = new Boss(true, 0, 0, 0, false, bossHealth, bossSpeed, bossFireRate, 10, "boss");
                    break;
            }
            bossBox.Tag = "boss";
            this.Controls.Add(bossBox);

            bossBox.Top = 365;
            bossBox.Left = 262;
           
            bossBox.BringToFront();
        }

        Stopwatch canShootStopWatch = new Stopwatch();
        Stopwatch enemiesMove= new Stopwatch();
        private void makeBullet(int direction)
        {

            if (canShoot)
            {
                canShootStopWatch.Restart();
                PictureBox bullet = new PictureBox();


                totalBullets += 1;

                bullet.BackColor = Color.Black;
                bullet.Height = 10;
                bullet.Width = 10;
                bullet.Image = Properties.Resources.playerbullet;
                //bullet.Left = pBox_Player.Left + pBox_Player.Width;
                //bullet.Top = pBox_Player.Top + pBox_Player.Height / 2;
                bullet.Visible = true;
                bullet.SizeMode = PictureBoxSizeMode.Zoom;
                if (direction == 1) // shooting up
                {
                    bullet.Left = pBox_Player.Left + (pBox_Player.Width / 2); // to make the bullet line up with the player's eyes
                    bullet.Top = pBox_Player.Top;
                    bullet.Name = "1";
                }
                if (direction == 2) // shooting down
                {
                    bullet.Left = pBox_Player.Left + (pBox_Player.Width / 2);
                    bullet.Top = pBox_Player.Top + pBox_Player.Height;
                    bullet.Name = "2";
                }
                if (direction == 3) // shooting right
                {
                    bullet.Left = pBox_Player.Left + pBox_Player.Width;
                    bullet.Top = pBox_Player.Top + (pBox_Player.Height / 2);
                    bullet.Name = "3";
                }
                if (direction == 4) // shooting left
                {
                    bullet.Left = pBox_Player.Left;
                    bullet.Top = pBox_Player.Top + (pBox_Player.Height / 2);
                    bullet.Name = "4";
                }


                bullet.Tag = "bullet";
                this.Controls.Add(bullet);

                bulletControls.Add(bullet);
            }
        }

        public int[] rockList = new int[14];
        public int[] clearedRooms = new int[16];

        public bool isRoomCleared(int room)
        {
            try
            {
                if (clearedRooms[room] == 0)
                    return false;
            }
            catch { }
            return true;

        }

        public bool tooManyBullets = false;
        public int itemNum = 0;

        private void spawnItem(int X, int Y, int H)
        {
            PictureBox itemBox = new PictureBox();
            int randomItem = getRandInt(ItemList.Count - 1, 0);

            itemNum = randomItem;

            itemBox.Name = "ITEM";
            itemBox.BringToFront();
            itemBox.Height = H;
            itemBox.Width = H;
            itemBox.SizeMode = PictureBoxSizeMode.Zoom;
            itemBox.Location = new Point(X, Y);
            itemBox.Visible = true;

            itemBox.Image = Properties.Resources.item_GinDay;

            switch (randomItem)
            {
                case 0:
                    itemBox.Image = Properties.Resources.item_GinDay;
                    break;
                case 1:
                    itemBox.Image = Properties.Resources.radar_sensor;
                    break;
                case 2:
                    itemBox.Image = Properties.Resources.itemrobothead;
                    break;
                case 3:
                    itemBox.Image = Properties.Resources.spanner;
                    break;
                case 4:
                    itemBox.Image = Properties.Resources.item_sick_gaming_pc;
                    break;
                case 5:
                    itemBox.Image = Properties.Resources.item_gear;
                    break;
                case 6:
                    itemBox.Image = Properties.Resources.itemcowpickerarmrobotthingy;
                    break;
                case 7:
                    itemBox.Image = Properties.Resources.itemwrench;
                    break;
            }
            itemBox.Tag = "item";
            this.Controls.Add(itemBox);
            itemBox.BringToFront();
        }

        private void roomCleared()
        {

            if (labelupdatorNum == 0)
            {
                updateLabels();
                findOpenDoor(playerProg);
            }

            findOpenDoor(playerProg);

            int room1 = treasureRooms[0];
            int room2 = treasureRooms[1];

            if ((clearedRooms[playerProg] == 0) && (labelupdatorNum == 0) && (playerProg != 0) && (playerProg != shopRoom) && (playerProg != room1) && playerProg != room2)      // drop a collectable
            {

                int x = getRandInt(700, 100);
                int y = getRandInt(500, 100);

                int[] collectArr = new int[] { 1, 2, 3, 4, 5, 6 };        // 1 = bomb         2 = key           3 = coin             4+ = nothing

                int rand = getRandInt(6, 1);

                labPickupNum.Text = "Pickup: " + Convert.ToString(rand);
                PictureBox collectable = new PictureBox();
                collectable.Height = 40;
                collectable.Width = 40;
                collectable.Location = new Point(x, y);
                collectable.SizeMode = PictureBoxSizeMode.Zoom;
                collectable.Visible = true;


                switch (rand)
                {
                    case (4):
                        this.Controls.Remove(collectable);
                        collectable.Dispose();
                        break;
                    case (5):
                        this.Controls.Remove(collectable);
                        collectable.Dispose();
                        break;
                    case (6):
                        this.Controls.Remove(collectable);
                        collectable.Dispose();
                        break;
                    case (1):
                        collectable.Image = Properties.Resources.Bomb_icon;
                        collectable.Tag = "bomb";
                        break;
                    case (2):
                        collectable.Image = Properties.Resources.Key_icon;
                        collectable.Tag = "key";
                        break;
                    case (3):
                        collectable.Image = Properties.Resources.Coin_icon;
                        collectable.Tag = "coin";
                        break;
                }

                try
                {
                    this.Controls.Add(collectable);
                    collectable.BringToFront();
                }
                catch { }
                    
            }

            labelupdatorNum++;
            clearedRooms[playerProg] = 1;
            if (itemNums > 0)
                clearedRooms[playerProg] = 0;

            if (labOUCH.Visible == true)
                labOUCH.Visible = false;
        }
    
        private void newRoom()
        {





            labelupdatorNum = 0;
            Dopen = false;
            Uopen = false;
            Lopen = false;
            Ropen = false;
            pboxDoorUp.Image = Properties.Resources.door_closed;
            pBoxDoorDown.Image = Properties.Resources.door_closed;
            pBoxDoorLeft.Image = Properties.Resources.door_closed;
            pBoxDoorRight.Image = Properties.Resources.door_closed;

            if (playerProg == 0)
            {
                gameBorder.Image = Properties.Resources.start_tutorial_FULL;
            }
            else
            {
                gameBorder.Image = Properties.Resources.black;
            }

            //itemNums = 0;

            labOUCH.Visible = false;
            canPlayerLoseHealth = true;

            for (int x = 0; x < 2; x++)
            {
                foreach (Control I in this.Controls)
                {
                    if (I is PictureBox && (I.Tag == "item" || I.Tag == "coin" || I.Tag == "bomb" || I.Tag == "key"))
                    {
                        this.Controls.Remove(I);
                        I.Dispose();
                    }
                    
                }
            }

            foreach (int x in treasureRooms)
            {
                if ((x == playerProg) && (!isRoomCleared(playerProg)))
                {
                    labTreasurePrice.BringToFront();
                    pPedastal.BringToFront();
                    pTreasureText.BringToFront();

                    spawnItem(425, 262, 80);
                }
                    


            }

            if ((playerProg == shopRoom) && (!isRoomCleared(playerProg)))
            {
                pShopText.BringToFront();
                labShopPrice.BringToFront();
                pMarketStall.BringToFront();

                spawnItem(381, 343, 40);
                spawnItem(478, 343, 40);
            }

            if(playerProg == 15)
            {
                spawnBoss();
                labBossHealth.Visible = true;
                labBossHealth.BringToFront();
            }
            else
            {
                labBossHealth.Visible = false;
            }
            
            canShootStopWatch.Restart();

            for (int x = 0; x < 3; x++) // Code below only deletes half of the controls with the tag?? So this for loop get the rest
            {
                foreach (Control X in this.Controls)
                {
                    if (X is PictureBox && X.Tag == "enemy")
                    {
                        this.Controls.Remove(X);
                        X.Dispose();
                    }
                    else if (X.Tag is PictureBox && X.Tag == "bullet")
                    {
                        this.Controls.Remove(X);
                        bulletControls.Remove(X);
                        X.Dispose();
                    }
                }
            }
            for (int x = 0; x < enemyClassList.Count; x++)
            {
                enemyClassList.RemoveAt(x);
            }


            

            //if (!isRoomCleared(playerProg))
            //    clearedRooms[playerProg - 1] = 1;

            if (!isRoomCleared(playerProg))
            {
                spawnEnemies(enemySpawns, playerProg);
            }
            updateLabels();

            enemiesMove.Restart();

 
            foreach(Control X in this.Controls)
            {
                if (X is PictureBox && X.Tag == "item")
                    X.BringToFront();
            }

            pBox_Player.BringToFront();
        }

        Size size = new Size(200, 200);
        public int enemyNums = 0;
        public int bulletNums = 0;
        public int collectableNums = 0;
        public int itemNums = 0;
        public bool canShoot = true;
        public bool checkforBulletBorderCollision = false;
        public int labelupdatorNum = 0;

        public bool canPlayerLoseHealth = true;
        Stopwatch duckHuntDog_Stopwatch = new Stopwatch();
        Stopwatch scoreStopWatch = new Stopwatch();
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            if (playerProg == 0 && (labStatNames.Visible == false))
            {
                labStatNames.Visible = true;
            }
            else if (playerProg != 0)
            {
                labStatNames.Visible = false;
            }


            if (labBossHealth.Visible == true)
            {
                if (boss.getEnemyHealth() <= 0)
                    labBossHealth.Text = "Boss Health: 0!";
                else
                    labBossHealth.Text = "Boss Health: " + Convert.ToString(boss.getEnemyHealth());
            }

            labPlayerHealth.Text = "playerHealth: " + Convert.ToString(player.getCharHealth());
            labBulletControls.Text = "bulletControls:";
            foreach (Control X in bulletControls)
            {

                switch (X.Name)
                {
                    case ("1"):
                        labBulletControls.Text += " UP";
                        break;
                    case ("2"):
                        labBulletControls.Text += " DOWN";
                        break;
                    case ("3"):
                        labBulletControls.Text += " RIGHT";
                        break;
                    case ("4"):
                        labBulletControls.Text += " LEFT";
                        break;
                }
            }

            labEnemyClassList.Text = "enemyClassList ";
            foreach (var x in enemyClassList)
            {
                labEnemyClassList.Text += Convert.ToString(x.getEnemyNum()) + " ";
            }

            foreach (Control X in this.Controls)
            {

                if (X is PictureBox)
                {
                    if (X.Tag == "bullet")
                        bulletNums++;
                    if (X.Tag == "enemy" || X.Tag == "boss")
                        enemyNums++;
                    if (X.Tag == "item")
                        itemNums++;
                    if (X.Tag == "key" || X.Tag == "bomb" || X.Tag == "coin")
                        collectableNums++;

                }
            }

            if (enemyNums == 0 && playerProg != 15)
                roomCleared();

            lblBullets.Text = "bullets: " + Convert.ToString(bulletNums);
            labCurrentEnemies.Text = "enemies: " + Convert.ToString(enemyNums);
            labItemNum.Text = "itemNum: " + Convert.ToString(itemNums);
            labCollectables.Text = "CollectablesNum: " + Convert.ToString(collectableNums);


            if (bulletNums > 20)
                tooManyBullets = true;


            if (tooManyBullets)
            {
                foreach (Control X in this.Controls)
                {

                    if (X is PictureBox && X.Tag == "bullet")
                    {
                        Controls.Remove(X);
                        X.Dispose();
                        bulletNums -= 1;
                        break;
                    }
                }
                tooManyBullets = false;

            }

            itemNums = 0;
            bulletNums = 0;
            enemyNums = 0;
            collectableNums = 0;

            foreach (Control X in this.Controls)
            {
                if (X is PictureBox && X.Tag == "player")
                {
                    foreach (Control Y in this.Controls)
                    {
                        if (ControlCollision(Y, X))
                        {

                            if (Y is PictureBox)
                            {

                                if (Y.Tag == "item")
                                {
                                    if (playerProg == shopRoom)
                                    {
                                        if (player.getCharCoins() >= 2)
                                        {
                                            giveStats();
                                            player.subCharCoins(2);
                                            updateLabels();
                                            Controls.Remove(Y);
                                            Y.Dispose();
                                            itemNums--;
                                        }
                                    }
                                    foreach (int x in treasureRooms)
                                    {
                                        if (playerProg == x)
                                        {
                                            if (player.getCharKeys() >= 1)
                                            {
                                                giveStats();
                                                player.subCharKeys(1);
                                                updateLabels();
                                                Controls.Remove(Y);
                                                Y.Dispose();
                                                itemNums--;
                                            }
                                        }
                                    }
                                }

                                if (Y.Tag == "key")
                                {
                                    player.addCharKeys(1);
                                    Y.Dispose();
                                    Controls.Remove(Y);
                                    updateLabels();
                                }
                                if (Y.Tag == "coin")
                                {
                                    player.addCharCoins(1);
                                    Y.Dispose();
                                    Controls.Remove(Y);
                                    updateLabels();
                                }
                                if (Y.Tag == "bomb")
                                {
                                    player.addCharBombs(1);
                                    Y.Dispose();
                                    Controls.Remove(Y);
                                    updateLabels();
                                }
                            }
                        }

                        if (Y is PictureBox && Y.Tag == "enemy")
                        {
                            if (ControlCollision(Y, X))
                            {

                                if (canPlayerLoseHealth)
                                {
                                    player.subCharHealth(1);
                                    canPlayerLoseHealth = false;
                                    labOUCH.Visible = true;
                                    labOUCH.BringToFront();
                                    this.Controls.Remove(Y);
                                    Y.Dispose();
                                    updateLabels();
                                }
                            }
                        }
                        if (Y is PictureBox && Y.Tag == "boss")
                        {
                            if (ControlCollision(Y, X))
                            {
                                player.subCharHealth(100);  // will kill the player
                                updateLabels();
                            }
                        }
                        if (Y is PictureBox && Y.Tag == "trophy" && Y.Visible == true)
                        {
                            if (ControlCollision(Y, X))
                            {
                                foreach (Control Z in this.Controls)
                                {
                                    if (Z.Tag == "player" || Z.Tag == "bullet" || Z.Tag == "enemy" || Z.Tag == "item" || Z.Tag == "coin" || Z.Tag == "key" || Z.Tag == "bomb" || Z.Tag == "boss" || Z.Tag == "trophy")
                                    {
                                        Controls.Remove(Z);
                                        Z.Dispose();
                                    }
                                }
                                winScreen.Visible = true;
                                labWinScore.Text = Convert.ToString(score - (scoreStopWatch.ElapsedMilliseconds / 1000));
                                labWinScore.Visible = true;
                                
                                winScreen.BringToFront();
                                labWinScore.BringToFront();
                                break;
                            }
                        }

                    }
                }
            }




            foreach (Control X in this.Controls)
            {

                // if X is a picture box object AND it has a tag of bullet
                // then we will follow the instructions within



                if (X is PictureBox && X.Tag == "bullet")
                {
                    X.BringToFront();
                    if (X.Name == "1")
                        X.Top -= player.getCharShotSpeed();
                    if (X.Name == "2")
                        X.Top += player.getCharShotSpeed();
                    if (X.Name == "3")
                        X.Left += player.getCharShotSpeed();
                    if (X.Name == "4")
                        X.Left -= player.getCharShotSpeed();


                    if (ControlleftCollision(X, gameBorder) || ControlrightCollision(X, gameBorder) || ControlupCollision(X, gameBorder) || ControldownCollision(X, gameBorder))
                    {
                        this.Controls.Remove(X);
                        bulletControls.Remove(X);
                        X.Dispose();
                    }


                    foreach (Control Y in this.Controls)
                    {
                        if (Y is PictureBox && Y.Tag == "enemy")
                        {
                            if (ControlCollision(X, Y))
                            {
                                this.Controls.Remove(X);
                                bulletControls.Remove(X);
                                X.Dispose();


                                // Remvoving health from enemy on hit
                                for (int x = 0; x < enemyClassList.Count; x++)
                                {
                                    if (Y.Name == Convert.ToString(enemyClassList[x].getEnemyNum()))
                                    {
                                        enemyClassList[x].takeEnemyHealth(player.getCharDmg());
                                    }


                                    if (enemyClassList[x].getEnemyHealth() <= 0)
                                    {
                                        enemyClassList.RemoveAt(x);
                                        this.Controls.Remove(Y);
                                        Y.Dispose();
                                    }
                                }
                            }
                        }

                        if (Y is PictureBox && Y.Tag == "boss") // shooting at boss
                        {
                            if (ControlCollision(X, Y))
                            {
                                this.Controls.Remove(X);
                                bulletControls.Remove(X);
                                X.Dispose();

                                boss.takeEnemyHealth(player.getCharDmg());
                            }

                            if (boss.getEnemyHealth() <= 0)
                            {
                                this.Controls.Remove(Y);
                                Y.Dispose();
                                WIN = true;
                                trophyBox.Visible = true;
                                trophyBox.Location = new Point(386, 395);
                                trophyBox.BringToFront();
                                pBox_Player.BringToFront();
                            }

                        }
                    }
                }
            }




            foreach (Control y in this.Controls)
            {
                if (y is PictureBox && y.Tag == "enemy")
                {
                    for (int x = 0; x < enemyClassList.Count; x++)
                    {
                        y.BringToFront();


                        if (y.Top < pBox_Player.Top + 10 && (y.Location.Y + enemyClassList[x].getEnemySpeed() - 1 < pBox_Player.Top + 10))         // EnemyClass Speed can be greater than 1, meaning that adding to it Y.top might make it go
                        {                                                                                                                          // OVER y.Top, into the next if statement to move the enemy down. This causes an infinite
                            y.Location = new Point(y.Location.X, y.Location.Y + enemyClassList[x].getEnemySpeed() - 1);                            // loop wheere the box cycles between moving up and down in the same spot
                            break;
                        }
                        if (y.Top > pBox_Player.Top + 10 && (y.Location.Y - enemyClassList[x].getEnemySpeed() - 1 > pBox_Player.Top + 10))
                        {
                            y.Location = new Point(y.Location.X, y.Location.Y - enemyClassList[x].getEnemySpeed() - 1);
                            break;
                        }
                        if (y.Left < pBox_Player.Left + 8 && (y.Location.X + enemyClassList[x].getEnemySpeed() < pBox_Player.Left + 8))
                        {
                            y.Location = new Point(y.Location.X + enemyClassList[x].getEnemySpeed(), y.Location.Y);
                            break;
                        }
                        if (y.Left >= pBox_Player.Left + 8 && (y.Location.X - enemyClassList[x].getEnemySpeed() > pBox_Player.Left + 8))
                        {
                            y.Location = new Point(y.Location.X - enemyClassList[x].getEnemySpeed(), y.Location.Y);
                            break;
                        }


                        // Check for collisions with other enemies
                        foreach (Control otherEnemy in this.Controls)
                        {
                            if (otherEnemy is PictureBox && otherEnemy.Tag == "enemy" && otherEnemy != y)
                            {
                                if (ControlCollision(y, otherEnemy))
                                {
                                    if (y.Left >= pBox_Player.Left + 8 && (y.Location.X - enemyClassList[x].getEnemySpeed() > pBox_Player.Left + 8))
                                    {
                                        y.Location = new Point(y.Location.X + enemyClassList[x].getEnemySpeed(), y.Location.Y);
                                        break;
                                    }

                                }
                            }

                        }
                    }
                }
                if (y is PictureBox && y.Tag == "boss")
                {
                    y.BringToFront();


                    if (y.Top < pBox_Player.Top + 10 && (y.Location.Y + boss.getEnemySpeed() < pBox_Player.Top + 10))         // EnemyClass Speed can be greater than 1, meaning that adding to it Y.top might make it go
                    {                                                                                                                          // OVER y.Top, into the next if statement to move the enemy down. This causes an infinite
                        y.Location = new Point(y.Location.X, y.Location.Y + boss.getEnemySpeed());                            // loop wheere the box cycles between moving up and down in the same spot
                        break;
                    }
                    if (y.Top > pBox_Player.Top + 10 && (y.Location.Y - boss.getEnemySpeed() > pBox_Player.Top + 10))
                    {
                        y.Location = new Point(y.Location.X, y.Location.Y - boss.getEnemySpeed());
                        break;
                    }
                    if (y.Left < pBox_Player.Left + 8 && (y.Location.X + boss.getEnemySpeed() < pBox_Player.Left + 8))
                    {
                        y.Location = new Point(y.Location.X + boss.getEnemySpeed(), y.Location.Y);
                        break;
                    }
                    if (y.Left >= pBox_Player.Left + 8 && (y.Location.X - boss.getEnemySpeed() > pBox_Player.Left + 8))
                    {
                        y.Location = new Point(y.Location.X - boss.getEnemySpeed(), y.Location.Y);
                        break;
                    }
                }

            }









            labCanShootTimer.Text = "canShootTimer: " + canShootStopWatch.ElapsedMilliseconds.ToString();
            if (canShootStopWatch.ElapsedMilliseconds < player.getCharFireRate())
            {
                canShoot = false;
                labCanShoot.Text = "canShoot?" + " F";
            }
            else
            {
                canShoot = true;
                labCanShoot.Text = "canShoot?" + " T";
            }

         




            if (move_LEFT)   // player movement
            {
                while (!leftCollision(pBox_Player, gameBorder))
                {
                    pBox_Player.Location = new Point(pBox_Player.Location.X - player.getCharSpeed(), pBox_Player.Location.Y);
                    break;
                }
            }
            if (move_RIGHT)
            {
                while (!rightCollision(pBox_Player, gameBorder))
                {
                    pBox_Player.Location = new Point(pBox_Player.Location.X + player.getCharSpeed(), pBox_Player.Location.Y);
                    break;
                }
            }
            if (move_UP)
            {
                while (!upCollision(pBox_Player, gameBorder))
                {
                    pBox_Player.Location = new Point(pBox_Player.Location.X, pBox_Player.Location.Y - player.getCharSpeed());
                    break;
                }
            }
            if (move_DOWN)
            {
                while (!downCollision(pBox_Player, gameBorder))
                {
                    pBox_Player.Location = new Point(pBox_Player.Location.X, pBox_Player.Location.Y + player.getCharSpeed());
                    break;
                }
            }

            if (shoot_UP)
            {
                if (Mark)
                    pBox_Player.Image = Properties.Resources.squareMarkBack;  // we want the character to be facing the direction they are shooting
                else
                    pBox_Player.Image = Properties.Resources.squareBillback;

            }
            if (shoot_DOWN)
            {
                if (Mark)
                    pBox_Player.Image = Properties.Resources.squareMarkfront;
                else
                    pBox_Player.Image = Properties.Resources.squareBillfront;

            }

            if (shoot_RIGHT)
            {
                if (Mark)
                    pBox_Player.Image = Properties.Resources.squareMarkRight;
                else
                    pBox_Player.Image = Properties.Resources.squareBillright;

            }
            if (shoot_LEFT)
            {
                if (Mark)
                    pBox_Player.Image = Properties.Resources.squareMarkLeft;
                else
                    pBox_Player.Image = Properties.Resources.squareBillleft;

            }


            // DOOR COLLISION

            if (pcollision(pBox_Player, pboxDoorUp))   // UP
            {
                if (Uopen)
                {
                    pBox_Player.Location = new Point(410, 485);
                    try
                    {
                        prevRoomDirection = playerProg;
                        if (map[getRoomY(playerProg) - 1, getRoomX(playerProg)] == playerProg + 1)  // (in this example) if player goes up, it means they are going further INTO the map
                            playerProg++;
                        else
                            playerProg--;                                                               // else, they must be going backwards
                    }
                    catch { }

                    newRoom();

                }
            }
            if (pcollision(pBox_Player, pBoxDoorDown))   // DOWN
            {
                if (Dopen)
                {
                    pBox_Player.Location = new Point(410, 90);
                    try
                    {
                        if (map[getRoomY(playerProg) + 1, getRoomX(playerProg)] == playerProg + 1)
                            playerProg++;
                        else
                            playerProg--;
                    }
                    catch { }


                    newRoom();
                }
            }
            if (pcollision(pBox_Player, pBoxDoorRight))    // RIGHT
            {
                if (Ropen)
                {
                    pBox_Player.Location = new Point(88, 284);
                    try
                    {
                        if (map[getRoomY(playerProg), getRoomX(playerProg) + 1] == playerProg + 1)
                            playerProg++;
                        else
                            playerProg--;
                    }
                    catch { }

                    newRoom();

                }
            }
            if (pcollision(pBox_Player, pBoxDoorLeft))    // LEFT
            {
                if (Lopen)
                {
                    pBox_Player.Location = new Point(744, 284);
                    try
                    {
                        if (map[getRoomY(playerProg), getRoomX(playerProg) - 1] == playerProg + 1)
                            playerProg++;
                        else
                            playerProg--;
                    }
                    catch { }

                    newRoom();
                }
            }



            // PLAYER DEATH

            duckHuntTimer.Text = "DuckHuntTimer: " + Convert.ToString(duckHuntDog_Stopwatch.ElapsedMilliseconds);
            if (player.getCharHealth() <= 0)
            {
                foreach (Control X in this.Controls)
                {
                    if (X.Tag == "player" || X.Tag == "bullet" || X.Tag == "enemy" || X.Tag == "item" || X.Tag == "coin" || X.Tag == "key" || X.Tag == "bomb" || X.Tag == "boss")
                    {
                        Controls.Remove(X);
                        X.Dispose();
                    }
                }
                gameOverScreen.BringToFront();
                gameOverScreen.Visible = true;


                //duckHuntTimer.BringToFront();
                //duckHuntTimer.Visible = true;
                if (duckHuntDog_Stopwatch.ElapsedMilliseconds >= 100)
                {
                    duckHuntDog.BringToFront();
                    duckHuntDog.Visible = true;
                }
                if (duckHuntDog_Stopwatch.ElapsedMilliseconds >= 200)
                {
                    duckHuntDog.Visible = false;
                    duckHuntDog_Stopwatch.Restart();
                }


                labClicktheX.BringToFront();
                labClicktheX.Visible = true;

                labAltF4.BringToFront();
                labAltF4.Visible = true;

            }

            ;
            labScore.Text = Convert.ToString(score - (scoreStopWatch.ElapsedMilliseconds / 1000));

            //labScore.Text = scoreStopWatch.ElapsedMilliseconds.ToString();
            


            if (playerProg == 15 && boss.getEnemyHealth() <= 0)
            {
                

                foreach (Control X in this.Controls)
                {
                    if (X.Tag == "boss")
                    {
                        Controls.Remove(X);
                        X.Dispose();
                    }
                }

                
            }
        }
            

        public bool WIN = false;
        
  
        private void formGAME_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }



        public void giveStats()
        {
            switch (itemNum)
            {
                case (0):
                    player.addCharHealth(GinDay.getHealthUp());
                    player.addCharSpeed(GinDay.getSpeedUp());
                    player.addCharDmg(GinDay.getDmgUp());
                    player.subCharFireRate(GinDay.getFireRateUp());
                    player.addCharShotSpeed(GinDay.getShotSpeedUp());
                    break;
                case (1):
                    player.addCharHealth(Mechanical_Revolution.getHealthUp());
                    player.addCharSpeed(Mechanical_Revolution.getSpeedUp());
                    player.addCharDmg(Mechanical_Revolution.getDmgUp());
                    player.subCharFireRate(Mechanical_Revolution.getFireRateUp());
                    player.addCharShotSpeed(Mechanical_Revolution.getShotSpeedUp());
                    break;
                case (2):
                    player.addCharHealth(RoboPug_Ollie.getHealthUp());
                    player.addCharSpeed(RoboPug_Ollie.getSpeedUp());
                    player.addCharDmg(RoboPug_Ollie.getDmgUp());
                    player.subCharFireRate(RoboPug_Ollie.getFireRateUp());
                    player.addCharShotSpeed(RoboPug_Ollie.getShotSpeedUp());
                    break;
                case (3):
                    player.addCharHealth(RoboBasset_Basil.getHealthUp());
                    player.addCharSpeed(RoboBasset_Basil.getSpeedUp());
                    player.addCharDmg(RoboBasset_Basil.getDmgUp());
                    player.subCharFireRate(RoboBasset_Basil.getFireRateUp());
                    player.addCharShotSpeed(RoboBasset_Basil.getShotSpeedUp());
                    break;
                case (4):
                    player.addCharHealth(Tech1.getHealthUp());
                    player.addCharSpeed(Tech1.getSpeedUp());
                    player.addCharDmg(Tech1.getDmgUp());
                    player.subCharFireRate(Tech1.getFireRateUp());
                    player.addCharShotSpeed(Tech1.getShotSpeedUp());
                    break;
                case (5):
                    player.addCharHealth(Proptosis.getHealthUp());
                    player.addCharSpeed(Proptosis.getSpeedUp());
                    player.addCharDmg(Proptosis.getDmgUp());
                    player.subCharFireRate(Proptosis.getFireRateUp());
                    player.addCharShotSpeed(Proptosis.getShotSpeedUp());
                    break;
                case (6):
                    player.addCharHealth(Tech2.getHealthUp());
                    player.addCharSpeed(Tech2.getSpeedUp());
                    player.addCharDmg(Tech2.getDmgUp());
                    player.subCharFireRate(Tech2.getFireRateUp());
                    player.addCharShotSpeed(Tech2.getShotSpeedUp());
                    break;
                case (7):// ACTIVE ITEM   - Doesnt add stats immedietely, but shows on the game screen (Top left in TBOIR)
                    break;
            }
        }


    }
}