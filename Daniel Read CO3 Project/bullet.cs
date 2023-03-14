using System;
using System.Collections.Generic;
using System.Text;

namespace Daniel_Read_CO3_Project
{
    class bullet : MoveableGameObject
    {

        private int bulletSpeed;
        private int bulletDirection;
        public bullet(bool isVisible, float fltSpeed, float fltXVel, float fltYVel, bool isGlued, int bulletSpeed, int bulletDirection)
            : base(isVisible,fltSpeed,fltXVel,fltYVel,isGlued)
        {
            this.bulletSpeed = bulletSpeed;
            this.bulletDirection = bulletDirection;
        }


        public void makeVisable()
        {
            isVisible = true;
        }
        public void makeInVisable()
        {
            isVisible = false;
        }
        public void setDirection(int Direction)
        {
            bulletDirection = Direction;
        }

        public void setBulletSpeed(int speed)
        {
            bulletSpeed = speed;
        }
    }
}
