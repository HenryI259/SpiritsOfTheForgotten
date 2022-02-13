using Microsoft.Xna.Framework;
using SpiritsOfTheForgotten.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace SpiritsOfTheForgotten.Npcs
{
    public class SotfGlobalNPC : GlobalNPC
    {
		public override bool InstancePerEntity => true;
		public bool CelestialFlames;
		public float npcEndurance;

		public override void ResetEffects(NPC npc)
		{
			CelestialFlames = false;
			npcEndurance = 0;
		}
		public override void UpdateLifeRegen(NPC npc, ref int damage)
		{
			
			if (CelestialFlames)
			{
				if (npc.lifeRegen > 0)
				{
					npc.lifeRegen = 0;
				}
				npc.lifeRegen -= 100;
				if (damage < 10)
				{
					damage = 10;
				}
				npc.defDefense -= 20;
			}
		}

        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
			damage = (int)(Math.Max(1 - npcEndurance, 0.01) * damage);
			
        }

        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
			damage = (int)(Math.Max(1 - npcEndurance, 0.01) * damage);
		}
        public override void DrawEffects(NPC npc, ref Color drawColor)
		{
			if (CelestialFlames)
			{
				if (Main.rand.Next(4) < 3)
				{
					int dustAmount = 1;
					for (int i = 0; i < dustAmount; i++)
					{
						int s = Main.rand.Next(1, 3);
						int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, DustID.Vortex, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), s);
						Main.dust[dust].noGravity = true;
						if (Main.rand.NextBool(4))
						{
							Main.dust[dust].noGravity = false;
							Main.dust[dust].scale *= 0.5f;
						}
					}

					
				}
				Vector3 light = Color.Cyan.ToVector3();
				Lighting.AddLight(npc.position, light.X, light.Y, light.Z);
			}
		}

	}
}