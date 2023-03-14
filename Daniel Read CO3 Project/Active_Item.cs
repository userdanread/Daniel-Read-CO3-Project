using System;
using System.Collections.Generic;
using System.Text;

namespace Daniel_Read_CO3_Project
{
    class Active_Item : Item
    {
        private int charge;
        private bool canUse;
        
        public Active_Item(bool isVisible, float fltSpeed, float fltXVel, float fltYVel, bool isGlued, int HealthUp,
            int SpeedUp, int DmgUp, int FireRateUp, int ShotSpeedUp, int Charge, bool CanUse)
            : base(isVisible, fltSpeed, fltXVel, fltYVel, isGlued, HealthUp, SpeedUp, DmgUp, FireRateUp, ShotSpeedUp)
        {
            this.charge = Charge;
            this.canUse = CanUse;
        }
    }
}
