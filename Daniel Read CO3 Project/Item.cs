using System;
using System.Collections.Generic;
using System.Text;

namespace Daniel_Read_CO3_Project
{
    public class Item : MoveableGameObject
    {
        private int HealthUp;
        private int SpeedUp;
        private int DmgUp;
        private int FireRateUp;
        private int ShotSpeedUp;

        public Item(bool isVisible, float fltSpeed, float fltXVel, float fltYVel, bool isGlued, int HealthUp,
            int SpeedUp, int DmgUp, int FireRateUp, int ShotSpeedUp)
            : base(isVisible, fltSpeed, fltXVel, fltYVel, isGlued)
        {
            this.HealthUp = HealthUp;
            this.SpeedUp = SpeedUp;
            this.DmgUp = DmgUp;
            this.FireRateUp = FireRateUp;
            this.ShotSpeedUp = ShotSpeedUp;
        }
        public int getHealthUp()
        {
            return HealthUp;
        }
        public int getSpeedUp()
        {
            return SpeedUp;
        }
        public int getDmgUp()
        {
            return DmgUp;   
        }
        public int getFireRateUp()
        {
            return FireRateUp;
        }
        public int getShotSpeedUp()
        {
            return ShotSpeedUp;
        }
    }
}
