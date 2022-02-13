using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using SpiritsOfTheForgotten.Buffs;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework.Graphics;
using System.IO;


namespace SpiritsOfTheForgotten.Projectiles.WeaponProj
{
	public class OrbittingStar : ModProjectile
	{
		public override void SetStaticDefaults()
		{

			ProjectileID.Sets.TrailCacheLength[projectile.type] = 30;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;        //The recording mode
		}



		public override void OnHitNPC(Terraria.NPC target, int damage, float knockback, bool crit)
		{
			Terraria.Main.player[projectile.owner].AddBuff(ModContent.BuffType<LunarPowerBuff>(), 300);
			target.AddBuff(ModContent.BuffType<CelestialFlamesBuff>(), 300);

		}


		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			//Redraw the projectile with the color not influenced by light
			Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}


		public override void SetDefaults()
		{
			projectile.width = 32;              
			projectile.height = 32;             
			projectile.friendly = true;         
			projectile.hostile = false;        										
			projectile.timeLeft = 420;          
			projectile.ignoreWater = true;          
			projectile.tileCollide = false;          
			projectile.extraUpdates = 1;
			projectile.penetrate = -1;
			projectile.alpha = 255;
		}

	

		public int timer;
		public Vector2 origin;
		public bool direction;
		public override void AI()
		{
			timer++;
			if (timer < 60)
            {
				projectile.alpha -= 4;
            }

			if (timer > 360)
            {
				projectile.alpha += 4;
            }

			if (timer == 1)
			{
				origin = projectile.Center;
				projectile.Center += Vector2.UnitY.RotatedByRandom(MathHelper.ToRadians(360)) * 300;
				direction = Main.rand.NextBool();
			}
			Vector2 newMove = origin - projectile.Center;
			float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);

			if (distanceTo > 100)
			{

				AdjustMagnitude(ref newMove, 20);
				projectile.velocity = (50 * projectile.velocity + newMove) / 51f;
				AdjustMagnitude(ref projectile.velocity, 20);
			}
			else
			{
				EaseToSpeed(ref projectile.velocity, 24);
			}

			projectile.velocity = projectile.velocity.RotatedBy(MathHelper.ToRadians(direction ? 3 : -3));

		}

		private void AdjustMagnitude(ref Vector2 vector, float speed)
		{
			float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
			if (magnitude > speed)
			{
				vector *= speed / magnitude;
			}
		}

		private void EaseToSpeed(ref Vector2 vector, float speed)
		{
			float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
			if (magnitude > speed)
			{
				vector *= 0.95f;
			}
			else
			{
				vector *= 1.05f;
			}
		}


	}
}