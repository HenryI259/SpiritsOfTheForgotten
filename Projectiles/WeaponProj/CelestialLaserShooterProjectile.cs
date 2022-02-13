using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using SpiritsOfTheForgotten.Buffs;
using System;


namespace SpiritsOfTheForgotten.Projectiles.WeaponProj
{
	public class CelestialLaserShooterProjectile : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.width = 30;
			projectile.height = 92;
			projectile.aiStyle = 75;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.tileCollide = false;
			projectile.hide = true;
			projectile.ownerHitCheck = true; 
			projectile.magic = true;
			Main.projFrames[projectile.type] = 3;
		}

		

	

		public int spriteDirection = 1;
		public float soundDelay = 0;
		public float num = 0f;
		public bool netUpdate = false;
		public int timer;
		public float delay = 30;
		public bool rotate;
		public override void AI()
        {
			
			projectile.position.Y -= 6;
            
			
			


			if (spriteDirection == -1)
			{
				num = (float)Math.PI;
			}

			
			
			soundDelay--;
			if (soundDelay <= 0)
			{
				Main.PlaySound(SoundID.Item115, projectile.Center);
				Vector2 direction = Main.MouseWorld - projectile.Center;
				direction.Normalize();
				Vector2 position = projectile.Center + direction * 50;
				Vector2 newDirection = direction.RotatedByRandom(MathHelper.ToRadians(delay / 2)) * 15f;
				Projectile.NewProjectile(position, newDirection, ModContent.ProjectileType<CelestialBeam>(), 300, 5, projectile.owner);
				
				if (delay > 5)
					delay -= 0.5f;
				soundDelay = delay;
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
			
		

			projectile.frameCounter++;	
			if (projectile.frameCounter >= 5)
			{
				projectile.frameCounter = 0;
				projectile.frame = (projectile.frame + 1) % 3;
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