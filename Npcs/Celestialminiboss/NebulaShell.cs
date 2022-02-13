using System.Threading;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using SpiritsOfTheForgotten.Items;
using MonoMod.Cil;
using Steamworks;
using SpiritsOfTheForgotten.Npcs.Celestialminiboss;
using Microsoft.Xna.Framework.Graphics;
using SpiritsOfTheForgotten.Projectiles.WeaponProj;

namespace SpiritsOfTheForgotten.Npcs.Celestialminiboss
{
	public class NebulaShell : ModNPC
	{
		public override void SetDefaults()
		{
			npc.aiStyle = -1;
			npc.lifeMax = 7500;
			npc.damage = 0;
			npc.defense = 30;
			npc.knockBackResist = 0f;
			npc.width = 58;
			npc.height = 58;
			npc.value = Item.buyPrice(0, 0, 0, 0);
			npc.npcSlots = 2f;
			npc.lavaImmune = true;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.HitSound = SoundID.NPCHit34;
			npc.DeathSound = SoundID.NPCDeath56;
			npc.buffImmune[24] = true;
		}

		public float spinTimer;
		public int owner;
        public override void AI()
        {
			spinTimer++;
			if (spinTimer == 1)
			{
				for (int i = 0; i < 250; i++)
				{
					int dusttype = DustID.PinkFlame;
					var dust = Dust.NewDustDirect(npc.Center, npc.width, npc.height, dusttype, Main.rand.NextFloat(-6, 5) * 2f, Main.rand.NextFloat(-6, 5) * 2f, 50, default(Color), 2f);
					dust.noGravity = true;
				}
			}
			Player playerTarget = Main.player[npc.target];
			npc.Center = playerTarget.Center - Vector2.UnitY.RotatedBy(MathHelper.ToRadians((spinTimer * 0.5f) + 180)) * 400;
			npc.rotation = (playerTarget.Center - npc.Center).ToRotation() + MathHelper.ToRadians(225);
			if (!Main.player[npc.target].HeldItem.ranged && !Main.player[npc.target].HeldItem.melee && !Main.player[npc.target].HeldItem.summon)
			{
				if (spinTimer % 30 == 0)
				{

					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						owner = Main.myPlayer;
					}
					int numberProjectiles = Main.rand.Next(2, 4);
					for (int i = 0; i < numberProjectiles; i++)
					{
						var a = Projectile.NewProjectileDirect(npc.Center, new Vector2(Main.rand.NextFloat(-5, 6), Main.rand.NextFloat(-5, 6)), ModContent.ProjectileType<NebulaCloud>(), 15, 2, owner);
						a.friendly = false;
						a.hostile = true;
					}
				}
			}
		}

		public override void NPCLoot()
		{
			Item.NewItem(npc.getRect(), ItemID.FragmentNebula, Main.rand.Next(4, 6));
			Item.NewItem(npc.getRect(), ItemID.FragmentNebula, Main.rand.Next(4, 6));
			Item.NewItem(npc.getRect(), ItemID.FragmentNebula, Main.rand.Next(4, 6));
		}

		private void AdjustMagnitude(ref Vector2 vector, float speed)
		{
			float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
			if (magnitude > speed)
			{
				vector *= speed / magnitude;
			}
		}

	}
}