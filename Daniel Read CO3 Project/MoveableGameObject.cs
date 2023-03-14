using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daniel_Read_CO3_Project
{
    public class MoveableGameObject
    {
        private float fltSpeed;
        private float fltXVel;
        private float fltYVel;
        private bool isGlued;
        protected bool isVisible;


        public MoveableGameObject(bool IsVisible, float fltSpeed, float fltXVel, float fltYVel, bool isGlued)
        {
            this.isVisible = false;
            this.fltSpeed = fltSpeed;
            this.fltXVel = fltXVel;   // for the object to move, it must have a speed and x,y velocity
            this.fltYVel = fltYVel;   
            this.isGlued = isGlued;   // isGlued with state whether the object can or cant move at a certain moment in time
        }
        public bool getIsVisible()
        {
            return isVisible;
        }
        public void setIsVisible(bool IsVisible)
        {
            isVisible = IsVisible;   // capitals distinguish parameter vs property
        }

        public void makeVisible()
        {
            isVisible = true;
        }
        public void makeInvisible()
        {
            isVisible = false;
        }
        public float GetFltSpeed()
        {
            return fltSpeed;
        }
        public float GetfltXVel()
        {
            return fltXVel;
        }
        public float GetfltYVel()
        {
            return fltYVel;
        }
        public bool GetIsGlued()
        {
            return isGlued;
        }
        public void SetFltSpeed(float speed)
        {
            fltSpeed = speed;
        }
        public void SetFltXVel(float xVel)
        {
            fltXVel = xVel;
        }
        public void SetFltYVel(float yVel)
        {
            fltYVel = yVel;
        }
        public void Glue()
        {
            isGlued = true;
        }
        public void UnGlue()
        {
            isGlued = false;
        }
    }
}
