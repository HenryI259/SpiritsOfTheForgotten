using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;

namespace SpiritsOfTheForgotten.Minions.AuroraStick
{
	

	public class AuroraStickBuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Aurora Stick");
			Description.SetDefault("A small stick is floating about you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			if (player.ownedProjectileCounts[ProjectileType<AuroraStickMinion>()] > 0)
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