using System;
using System.Collections.Generic;
using System.Text;

namespace Daniel_Read_CO3_Project
{
    public class Enemy : MoveableGameObject 
    {
        private int enemySpeed;
        private int enemyHealth;
        private int enemyFireRate;
        private int enemyNum;
        private int enemyType;

        public Enemy(bool isVisible, float fltSpeed, float fltXVel, float fltYVel, bool isGlued, int enemyHealth
            , int enemySpeed, int enemyFireRate, int enemyNum, int enemyType)
            : base(isVisible, fltSpeed, fltXVel, fltYVel, isGlued)
        {
            this.enemySpeed = enemySpeed;
            this.enemyHealth = enemyHealth;
            this.enemyFireRate = enemyFireRate;
            this.enemyNum = enemyNum;
            this.enemyType = enemyType;
        }

        public int getEnemyType()
        {
            return enemyType;
        }
        public int getEnemyHealth()
        {
            return enemyHealth;
        }
        public int getEnemyFireRate()
        {
            return enemyFireRate;
        }
        public int getEnemySpeed()
        {
            return enemySpeed;
        }
        public int getEnemyNum()
        {
            return enemyNum;
        }


        public void setEnemyHealth(int h)
        {
            enemyHealth = h;
        }
        public void setEnemyFireRate(int f)
        {
            enemyFireRate = f;
        }
        public void setEnemySpeed(int s)
        {
            enemySpeed = s;
        }
        public void setEnemyNum(int n)
        {
            enemyNum = n;
        }

        public void takeEnemyHealth(int h)
        {
            enemyHealth -= h;
        }


    }

}
