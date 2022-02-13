using Terraria;
using Terraria.ModLoader;
using SpiritsOfTheForgotten.Npcs;
using static Terraria.ModLoader.ModContent;


namespace SpiritsOfTheForgotten.Buffs
{
	public class MysteriousCurse : ModBuff
	{


		public override void SetDefaults()
		{
			DisplayName.SetDefault("Mysterious Curse");
			Description.SetDefault("You now take 25% more damage");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			
		}
		
		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<SotfGlobalNPC>().npcEndurance -= 0.25f;
		}
	}



}