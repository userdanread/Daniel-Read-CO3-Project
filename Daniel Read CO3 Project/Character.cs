using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daniel_Read_CO3_Project
{
    internal class Character : MoveableGameObject
    {

        // This will represent the player that the user will control throughout the game
        private int charHealth;
        private int charSpeed;
        private int charDmg;
        private int charFireRate;
        private int charRange;
        private int charKnockBack;
        private int charShotSpeed;

        private int charBombs;
        private int charKeys;
        private int charCoins;

        
        public Character(bool isVisible, float fltSpeed, float fltXVel, float fltYVel,
            bool isGlued, int charHealth, int charSpeed, int charDmg, int charFireRate, int charRange, int charKnockBack,
            int charShotSpeed, int charBombs, int charKeys, int charCoins)
            : base(isVisible, fltSpeed, fltXVel, fltYVel, isGlued)
        {
            this.charHealth = charHealth;
            this.charSpeed = charSpeed;
            this.charDmg = charDmg;
            this.charFireRate = charFireRate;     
            this.charRange = charRange;
            this.charKnockBack = charKnockBack;
            this.charShotSpeed = charShotSpeed;
            this.charBombs = charBombs;          // the player's collectibles 
            this.charKeys = charKeys;
            this.charCoins = charCoins;
        }
        public int getCharHealth()
        {
            return charHealth;
        }
        public int getCharSpeed()
        {
            return charSpeed;
        }
        public int getCharDmg()
        {
            return charDmg;
        }
        public int getCharFireRate()
        {
            return charFireRate;
        }
        public int getCharRange()
        {
            return charRange;
        }
        public int getCharShotSpeed()
        {
            return charShotSpeed;
        }
        public int getCharKnockBack()
        {
            return charKnockBack;
        }
        public int getCharBombs()
        {
            return charBombs;
        }
        public int getCharKeys()
        {
            return charKeys;
        }
        public int getCharCoins()
        {
            return charCoins;
        }

        // Sets these stats at the START of the game.
        public void setCharHealth(int health)
        {
            charHealth = health;
        }
        public void setCharDmg(int dmg)
        {
            charDmg = dmg;
        }
        public void setCharFireRate(int FR)
        {
            charFireRate = FR;
        }
        public void setCharRange(int range)
        {
            charRange = range;
        }
        public void setCharShotSpeed(int SP)
        {
            charShotSpeed = SP;
        }
        public void setCharKnockBack(int KB)
        {
            charKnockBack = KB;
        }
        public void setCharBombs(int bombs)
        {
            charBombs = bombs;
        }
        public void setCharKeys(int keys)
        {
            charKeys = keys;
        }
        public void setCharCoins(int coins)
        {
            charCoins = coins;
        }
        public void setCharSpeed(int speed)
        {
            charSpeed = speed;
        }

        // ADDS to the stats MID game (when picking up/using items)
        public void addCharHealth(int health)
        {
            charHealth += health;
        }
        public void addCharSpeed(int speed)
        {
            charSpeed += speed;
        }
        public void addCharDmg(int dmg)
        {
            charDmg += dmg;
        }
        public void addCharFireRate(int FR)
        {
            charFireRate -= FR;
        }
        public void addCharRange(int range)
        {
            charRange += range;
        }
        public void addCharShotSpeed(int SP)
        {
            charShotSpeed += SP;
        }
        public void addCharKnockBack(int KB)
        {
            charKnockBack += KB;
        }
        public void addCharBombs(int bombs)
        {
            charBombs += bombs;
        }
        public void addCharKeys(int keys)
        {
            charKeys += keys;
        }
        public void addCharCoins(int coins)
        {
            charCoins += coins;
        }

        // SUBTRACTS from these stats (some items will decrease stats whilst also raising others)
        public void subCharHealth(int health)
        {
            charHealth -= health;
        }
        public void subCharDmg(int dmg)
        {
            charDmg -= dmg;
        }
        public void subCharFireRate(int FR)
        {
            charFireRate -= FR * 50;
        }
        public void subCharRange(int range)
        {
            charRange -= range;
        }
        public void subCharShotSpeed(int SP)
        {
            charShotSpeed -= SP;
        }
        public void subCharKnockBack(int KB)
        {
            charKnockBack -= KB ;
        }
        public void subCharBombs(int bombs)
        {
            charBombs -= bombs;
        }
        public void subCharKeys(int keys)
        {
            charKeys -= keys;
        }
        public void subCharCoins(int coins)
        {
            charCoins -= coins;
        }
    }
}
