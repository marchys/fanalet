﻿public class EnemyRedSlugStats : EnemyStats
{
    public EnemyRedSlugStats()
    {
        EntityName = "RedSlug";
        EntityDescription = "It's a red slug";
        Level = 1;
        Attack = 3;
        Life = 8;
        MaxLife = 8;
        BaseSpeed = BaseSpeed * 0.7f;
        AgroSpeed = BaseSpeed * 1.2f;
        CurrentSpeed = BaseSpeed;
        Mass = 40;	
    }
}
