using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using SpiritsOfTheForgotten.Buffs;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework.Graphics;
using System.IO;


namespace SpiritsOfTheForgotten.Npcs.Celestialminiboss
{
	public class LargeCelestialStar : ModProjectile
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
			projectile.timeLeft = 600;          
			projectile.ignoreWater = true;          
			projectile.tileCollide = false;          
			projectile.extraUpdates = 1;
			projectile.penetrate = -1;
		}

	

		public int timer;
		public int rotations = 0;
		public bool left = false;
		public override void AI()
		{
			
			
			timer++;
			projectile.velocity *= 1.01f;

			if (timer == 1)
            {
				projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;
			}

			if (projectile.ai[0] == 0)
			{
				if (timer == 2)
				{
					projectile.velocity = projectile.velocity.RotatedBy(MathHelper.ToRadians(10));
				}

				if (timer == 4 && !left)
				{
					projectile.velocity = projectile.velocity.RotatedBy(MathHelper.ToRadians(-1f));
					rotations++;
					if (rotations == 60)
					{
						left = true;
						rotations = 0;
					}
					timer = 3;
				}

				if (timer == 4 && left)
				{
					projectile.velocity = projectile.velocity.RotatedBy(MathHelper.ToRadians(1f));
					rotations++;
					if (rotations == 60)
					{
						left = false;
						rotations = 0;
					}
					timer = 3;
				}
			}
			else if (projectile.ai[0] == 1)
			{
				projectile.velocity = projectile.velocity.RotatedBy(MathHelper.ToRadians(-0.75f));
			}
			else if (projectile.ai[0] == 2)
            {
				projectile.velocity = projectile.velocity.RotatedBy(MathHelper.ToRadians(0.75f));
			}
			else if (projectile.ai[0] == 3)
            {
				projectile.velocity = projectile.velocity.RotatedBy(MathHelper.ToRadians(-0.75f));
				projectile.hostile = false;
				projectile.alpha = 255;
				int a = Dust.NewDust(projectile.Center, 1, 1, DustID.PinkFlame, 0f, 0f, 50, Color.White, 1f);
				Main.dust[a].velocity *= 0;
				Main.dust[a].noGravity = true;
				projectile.extraUpdates = 20;
				projectile.timeLeft = 100;
			}
			else if (projectile.ai[0] == 4)
			{
				projectile.velocity = projectile.velocity.RotatedBy(MathHelper.ToRadians(0.75f));
				projectile.hostile = false;
				projectile.alpha = 255;
				int a = Dust.NewDust(projectile.Center, 1, 1, DustID.PinkFlame, 0f, 0f, 50, Color.White, 1.25f);
				Main.dust[a].velocity *= 0;
				Main.dust[a].noGravity = true;
				projectile.extraUpdates = 20;
				projectile.timeLeft = 100;
			}

		}



	}
}