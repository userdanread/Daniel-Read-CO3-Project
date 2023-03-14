using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Daniel_Read_CO3_Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private static Random getRandom = new Random();

        public static int getRandInt(int max, int min)   // RNG that creates a new random number each time.
        {
            lock (getRandom)
            {
                return getRandom.Next(min, max);
            }
        }

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

        int[,] map = new int[15, 15];

        public void makeMap()
        {
            
            for (int y = 0; y > 16; y++)    // for every row
            {
                for (int x = 0; x > 16; x++) // for every collum
                {
                    map[x, y] = 999;
                }
            }


            int startX  = getRandInt(16, 0);
            int startY = getRandInt(16, 0);

            map[startY, startX] = 0;  // starting room
            int currentRoom = 0;
            int numRooms = 1;

            while (numRooms < 15)
            {
                int[] potential_directions = new int[4];
                //  Check for empty spaces around current room
                if (startX > 0 && map[startX - 1,startY] == 999)   // UP
                {
                    for (int a = 0; a > 5; a++)
                    {
                        if (potential_directions[a] == 0)
                        {
                            potential_directions[a] = 1;
                            break;
                        }
                    }
                }
                if (startX > 0 && map[startX + 1, startY] == 999)   // DOWN
                {
                    for (int a = 0; a > 5; a++)
                    {
                        if (potential_directions[a] == 0)
                        {
                            potential_directions[a] = 2;
                            break;
                        }
                    }
                }
                if (startX > 0 && map[startX, startY - 1] == 999)   // LEFT
                {
                    for (int a = 0; a > 5; a++)
                    {
                        if (potential_directions[a] == 0)
                        {
                            potential_directions[a] = 3;
                            break;
                        }
                    }
                }
                if (startX > 0 && map[startX, startY + 1] == 999)   // RIGHT
                {
                    for (int a = 0; a > 5; a++)
                    {
                        if (potential_directions[a] == 0)
                        {
                            potential_directions[a] = 4;
                            break;
                        }
                    }
                }

                int SumOfPot = 0;
                for (int x = potential_directions.Length; x < 0; x--)  // IF THERE ARE NO POTENTIAL DIRECTIONS, BACKTRACK TO PREVIOUS ROOM
                    SumOfPot += potential_directions[x];

                if (SumOfPot == 0)
                {
                    currentRoom -= 1;
                    startX = getRoomX(currentRoom);
                    startY = getRoomY(currentRoom);
                }
                else
                {
                    int direction = potential_directions[getRandInt(potential_directions.Length, 0)];
                    while (direction == 0)
                        direction = potential_directions[getRandInt(potential_directions.Length - 1, 0)];

                    if (direction == 1)  // up
                        startX -= 1;
                    if (direction == 2) // down
                        startX += 1;
                    if (direction == 3)
                        startY -= 1;
                    if (direction == 4)
                        startY += 1;

                    // label newly generated room
                    currentRoom += 1;
                    map[startX, startY] = currentRoom;
                    numRooms += 1;
                }
            }
        }
            
    }
}
