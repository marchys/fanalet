﻿public class BasePoStats : BaseCaracterStats 
{
	public BasePoStats()
    {
        EntityName = "Po";
        EntityDescription = "Te mola po";
	    Protagonist = true;
        Attack = 2;
        OiLife = 100;
        MaxOiLife = 100;
        BaseSpeed = 5.5f;
        AttackCadence = 0.6f;
	    RedHearts = 0;
	    BlueHearts = 0;
        YellowHearts = 0;
    }
    
}
