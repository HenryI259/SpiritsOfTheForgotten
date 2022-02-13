using Terraria;
using Terraria.ModLoader;

namespace SpiritsOfTheForgotten.Buffs
{
    public class SolarStrength : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Solar Strength");
            Description.SetDefault("All your melee attacks explode into solar eruptions.");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<SotfPlayer>().SolarStrength = true;
            player.allDamage += 0.15f;
            
        }
    }
}
