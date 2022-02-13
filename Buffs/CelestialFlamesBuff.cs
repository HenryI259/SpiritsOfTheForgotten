using Terraria;
using Terraria.ModLoader;

namespace SpiritsOfTheForgotten.Buffs
{
    public class CelestialFlamesBuff : ModBuff
    {


		public override void SetDefaults()
		{
			DisplayName.SetDefault("Lunar Flames");
			Description.SetDefault("Lunar flames burn away your essence");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			longerExpertDebuff = true;
		}	
		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<SotfPlayer>().CelestialFlames = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<Npcs.SotfGlobalNPC>().CelestialFlames = true;
		}
	}


	
}
