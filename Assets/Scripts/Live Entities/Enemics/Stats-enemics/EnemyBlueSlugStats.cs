﻿public class EnemyBlueSlugStats : EnemyStats
{
    public EnemyBlueSlugStats()
    {
        EntityName = "BlueSlug";
        EntityDescription = "It's a blue slug";
        Level = 2;
        Attack = 4;
        Life = 16;
        MaxLife = 16;
        BaseSpeed = BaseSpeed * 0.8f;
        AgroSpeed = BaseSpeed * 1.3f;
        CurrentSpeed = BaseSpeed;
        Mass = 40;
    }
}
