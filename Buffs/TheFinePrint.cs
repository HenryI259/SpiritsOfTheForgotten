using Terraria;
using Terraria.ModLoader;

namespace SpiritsOfTheForgotten.Buffs
{
	public class TheFinePrint : ModBuff
	{


		public override void SetDefaults()
		{
			DisplayName.SetDefault("The Fine Print");
			Description.SetDefault("You really didn't read the fine print did you kiddo, well it says you take 25% more damage as well");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			
		}
		public override void Update(Player player, ref int buffIndex)
		{
			player.endurance -= 0.25f;
		}

		
	}



}