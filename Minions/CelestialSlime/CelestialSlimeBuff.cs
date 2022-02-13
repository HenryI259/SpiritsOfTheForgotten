using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;

namespace SpiritsOfTheForgotten.Minions.CelestialSlime
{
	

	public class CelestialSlimeBuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Celestial Slime");
			Description.SetDefault("A slime from the heavens seems to be helping you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			if (player.ownedProjectileCounts[ProjectileType<CelestialSlimeMinion>()] > 0)
			{
				player.buffTime[buffIndex] = 18000;
			}
			else
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}