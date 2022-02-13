using Terraria;
using Terraria.ModLoader;

namespace SpiritsOfTheForgotten.Buffs
{
    public class LunarPowerBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Celestial Power");
            Description.SetDefault("Celestial energy flows through you.");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.allDamage += 0.1f; 
            
        }
    }
}
