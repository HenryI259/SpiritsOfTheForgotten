using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using SpiritsOfTheForgotten.Buffs;
using System;


namespace SpiritsOfTheForgotten.Projectiles.WeaponProj
{
	public class CelestialSlash : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.width = 72;
			projectile.height = 109;
			projectile.aiStyle = 75;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.tileCollide = false;
			projectile.hide = true;
			projectile.ownerHitCheck = true; 
			projectile.melee = true;
			Main.projFrames[projectile.type] = 24;
			projectile.usesIDStaticNPCImmunity = true;
			projectile.idStaticNPCHitCooldown = 21;
			projectile.scale = 1.25f;
		}

		

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			Terraria.Main.player[projectile.owner].AddBuff(ModContent.BuffType<LunarPowerBuff>(), 300);
			target.AddBuff(ModContent.BuffType<CelestialFlamesBuff>(), 300);

			int numberProjectiles = Main.rand.Next(3, 4);
			for (int i = 0; i < numberProjectiles; i++)
			{

				Vector2 position = projectile.Center + new Vector2(Main.rand.NextFloat(-100, 100), Main.rand.NextFloat(-100, 100) - 500);
				Vector2 velocity = Main.MouseWorld - position;
				AdjustMagnitude(ref velocity, 12);
				int a = Projectile.NewProjectile(position, velocity, ProjectileID.PhantasmalBolt, projectile.damage, projectile.knockBack, projectile.owner, 0, 1);
				Main.projectile[a].tileCollide = false;
				Main.projectile[a].hostile = false;
				Main.projectile[a].friendly = true;
				Main.projectile[a].melee = true;
			}
		}


		public int spriteDirection = 1;
		public int soundDelay = 0;
		public float num = 0f;
		public bool netUpdate = false;
		public int timer;
		public override void AI()
        {
			
			projectile.position.Y += 10;
            

			


			if (spriteDirection == -1)
			{
				num = (float)Math.PI;
			}

			

			soundDelay--;
			if (soundDelay <= 0)
			{
				Main.PlaySound(SoundID.Item117, projectile.Center);
				soundDelay = 21;
			}
			if (Main.myPlayer == projectile.owner)
			{
				if (Main.player[projectile.owner].channel && !Main.player[projectile.owner].noItems && !Main.player[projectile.owner].CCed)
				{
					float num27 = 1f;
					if (Main.player[projectile.owner].inventory[Main.player[projectile.owner].selectedItem].shoot == projectile.type)
					{
						num27 = Main.player[projectile.owner].inventory[Main.player[projectile.owner].selectedItem].shootSpeed * projectile.scale;
					}
					Vector2 vector16 = Main.MouseWorld - Main.player[projectile.owner].RotatedRelativePoint(Main.player[projectile.owner].MountedCenter, true); ;
					vector16.Normalize();
					if (vector16.HasNaNs())
					{
						vector16 = Vector2.UnitX * Main.player[projectile.owner].direction;
					}
					vector16 *= num27;
					if (vector16.X != projectile.velocity.X || vector16.Y != projectile.velocity.Y)
					{
						netUpdate = true;
					}
					projectile.velocity = vector16;
				}
				else
				{
					projectile.Kill();
				}
			}
			Vector2 vector17 = projectile.Center + projectile.velocity * 3f;
			Lighting.AddLight(vector17, 0.8f, 0.8f, 0.8f);
			if (Main.rand.Next(2) == 0)
			{
				int dusttype = Main.rand.Next(new int[] { DustID.BlueCrystalShard, DustID.SolarFlare, DustID.PinkFlame, DustID.Vortex });
				int num28 = Dust.NewDust(vector17 - projectile.Size / 2f, projectile.width, projectile.height, dusttype, projectile.velocity.X / 5, projectile.velocity.Y / 5, 100, default(Color), 1f);
				Main.dust[num28].noGravity = true;
				Main.dust[num28].position -= projectile.velocity;
			}
		

			projectile.frameCounter++;	
			if (projectile.frameCounter >= 3)
			{
				projectile.frameCounter = 0;
				projectile.frame = (projectile.frame + 1) % 24;
			}
		}


		private void AdjustMagnitude(ref Vector2 vector, float speed)
		{
			float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
			if (magnitude != speed)
			{
				vector *= speed / magnitude;
			}
		}

	}
}