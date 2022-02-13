using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using SpiritsOfTheForgotten.Buffs;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework.Graphics;
using SpiritsOfTheForgotten.Projectiles.WeaponProj;

namespace SpiritsOfTheForgotten.Projectiles.WeaponProj
{
	public class MiniVortex : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("MiniVortex");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;        //The recording mode
		}



		


		public override void SetDefaults()
		{

			projectile.width = 50;
			projectile.height = 50;            
			projectile.friendly = true;         
			projectile.hostile = false;         
			projectile.ranged = true;          
			projectile.timeLeft = 600;          
			projectile.alpha = 255;            
			projectile.light = 0.5f;            
			projectile.ignoreWater = true;          
			projectile.tileCollide = true;          
			



		}
		public int timer;
		public override void AI()
		{
			timer += 1;
			if (timer > 500)
			{
				// Fade out
				projectile.alpha += 25;
				if (projectile.alpha > 255)
				{
					projectile.alpha = 255;
				}
			}
			else
			{
				// Fade in
				projectile.alpha -= 25;
				if (projectile.alpha < 100)
				{
					projectile.alpha = 100;
				}
			}
			projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;
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

		public override void Kill(int timeLeft)
		{

			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, 0, ModContent.ProjectileType<VortexSentry>(), 40, 0, projectile.owner);
			Main.PlaySound(SoundID.Item94, projectile.Center);
		}


	}
}
