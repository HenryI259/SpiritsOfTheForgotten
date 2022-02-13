using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using SpiritsOfTheForgotten.Buffs;
using System.Security.Cryptography.X509Certificates;

namespace SpiritsOfTheForgotten.Projectiles.WeaponProj
{
	public class LunarRocket : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			Main.projFrames[projectile.type] = 4;
			ProjectileID.Sets.Homing[projectile.type] = true;
		}

		public bool dead;

		public override void OnHitNPC(Terraria.NPC target, int damage, float knockback, bool crit)
		{
			Terraria.Main.player[projectile.owner].AddBuff(ModContent.BuffType<LunarPowerBuff>(), 300);
			target.AddBuff(ModContent.BuffType<CelestialFlamesBuff>(), 300);

			projectile.velocity *= 0f;
			projectile.alpha = 255;
			projectile.timeLeft = 3;
			projectile.ranged = true;
			projectile.position.X += projectile.width / 2;
			projectile.position.Y += projectile.height / 2;
			projectile.width = 300;
			projectile.height = 300;
			projectile.position.X -= projectile.width / 2;
			projectile.position.Y -= projectile.height / 2;
			projectile.knockBack = 20f;
			dead = true;
		}





		public override void SetDefaults()
		{

			projectile.width = 30;
			projectile.height = 30;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.timeLeft = 600;
			projectile.ignoreWater = false;
			projectile.tileCollide = true;
			//projectile.aiStyle = 16;
			projectile.penetrate = -1;
		}


		public int dusttype; // = Main.rand.Next(new int[] { DustID.BlueCrystalShard, DustID.SolarFlare, DustID.PinkFlame, DustID.Vortex });

		public override void AI()
		{
			dusttype = Main.rand.Next(new int[] { DustID.BlueCrystalShard, DustID.SolarFlare, DustID.PinkFlame, DustID.Vortex });

			Vector2 move = Vector2.Zero;
			float distance = 400f;
			bool target = false;
			for (int k = 0; k < 200; k++)
			{
				NPC npc = Main.npc[k];
				if (npc.CanBeChasedBy())
				{
					Vector2 newMove = Main.npc[k].Center - projectile.Center;
					float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
					if (distanceTo < distance)
					{
						move = newMove;
						distance = distanceTo;
						target = true;
					}
				}
			}
			if (target && !dead)
			{
				AdjustMagnitude(ref move, 20);
				projectile.velocity = (8 * projectile.velocity + move) / 9f;
				AdjustMagnitude(ref projectile.velocity, 20);
			}

			projectile.frameCounter++;
			if (projectile.frameCounter >= 8)
			{
				projectile.frameCounter = 0;
				projectile.frame = (projectile.frame + 1) % 4;
			}

			projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;

			if (Math.Abs(projectile.velocity.X) >= 8f || Math.Abs(projectile.velocity.Y) >= 8f)
			{
				for (int num364 = 0; num364 < 2; num364++)
				{
					float num365 = 0f;
					float num366 = 0f;
					if (num364 == 1)
					{
						num365 = projectile.velocity.X * 0.5f;
						num366 = projectile.velocity.Y * 0.5f;
					}
					int num367 = Dust.NewDust(new Vector2(projectile.position.X + 3f + num365, projectile.position.Y + 3f + num366) - projectile.velocity * 0.5f, projectile.width - 8, projectile.height - 8, dusttype, 0f, 0f, 100);
					Dust dust77 = Main.dust[num367];
					Dust dust189 = dust77;
					dust189.scale *= 2f + (float)Main.rand.Next(10) * 0.1f;
					dust77 = Main.dust[num367];
					dust189 = dust77;
					dust189.velocity *= 0.2f;
					Main.dust[num367].noGravity = true;
					num367 = Dust.NewDust(new Vector2(projectile.position.X + 3f + num365, projectile.position.Y + 3f + num366) - projectile.velocity * 0.5f, projectile.width - 8, projectile.height - 8, 31, 0f, 0f, 100, default(Color), 0.5f);
					Main.dust[num367].fadeIn = 1f + (float)Main.rand.Next(5) * 0.1f;
					dust77 = Main.dust[num367];
					dust189 = dust77;
					dust189.velocity *= 0.05f;
				}
			}
			if (Math.Abs(projectile.velocity.X) < 15f && Math.Abs(projectile.velocity.Y) < 15f)
			{
				projectile.velocity *= 1.1f;
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{



			projectile.velocity *= 0f;
			projectile.alpha = 255;
			projectile.timeLeft = 3;

			projectile.position.X += projectile.width / 2;
			projectile.position.Y += projectile.height / 2;
			projectile.width = 300;
			projectile.height = 300;
			projectile.position.X -= projectile.width / 2;
			projectile.position.Y -= projectile.height / 2;
			projectile.knockBack = 20f;
			dead = true;
			return false;
		}

		public override void Kill(int timeLeft)
		{
			projectile.position.X += projectile.width / 2;
			projectile.position.Y += projectile.height / 2;
			projectile.width = 22;
			projectile.height = 22;
			projectile.position.X -= projectile.width / 2;
			projectile.position.Y -= projectile.height / 2;
			for (int num695 = 0; num695 < 30; num695++)
			{
				int num696 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 1.5f);
				Dust dust31 = Main.dust[num696];
				Dust dust226 = dust31;
				dust226.velocity *= 1.4f;
			}
			for (int num697 = 0; num697 < 20; num697++)
			{
				int num698 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, dusttype, 0f, 0f, 100, default(Color), 3.5f);
				Main.dust[num698].noGravity = true;
				Dust dust32 = Main.dust[num698];
				Dust dust226 = dust32;
				dust226.velocity *= 7f;
				num698 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, dusttype, 0f, 0f, 100, default(Color), 1.5f);
				dust32 = Main.dust[num698];
				dust226 = dust32;
				dust226.velocity *= 3f;
			}
			for (int num699 = 0; num699 < 2; num699++)
			{
				float num700 = 0.4f;
				if (num699 == 1)
				{
					num700 = 0.8f;
				}
				int num702 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default(Vector2), Main.rand.Next(61, 64));
				Gore gore4 = Main.gore[num702];
				Gore gore40 = gore4;
				gore40.velocity *= num700;
				Main.gore[num702].velocity.X += 1f;
				Main.gore[num702].velocity.Y += 1f;
				num702 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default(Vector2), Main.rand.Next(61, 64));
				gore4 = Main.gore[num702];
				gore40 = gore4;
				gore40.velocity *= num700;
				Main.gore[num702].velocity.X -= 1f;
				Main.gore[num702].velocity.Y += 1f;
				num702 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default(Vector2), Main.rand.Next(61, 64));
				gore4 = Main.gore[num702];
				gore40 = gore4;
				gore40.velocity *= num700;
				Main.gore[num702].velocity.X += 1f;
				Main.gore[num702].velocity.Y -= 1f;
				num702 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default(Vector2), Main.rand.Next(61, 64));
				gore4 = Main.gore[num702];
				gore40 = gore4;
				gore40.velocity *= num700;
				Main.gore[num702].velocity.X -= 1f;
				Main.gore[num702].velocity.Y -= 1f;
			}
			Main.PlaySound(SoundID.Item14, projectile.Center);
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