using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using SpiritsOfTheForgotten.Buffs;
using Microsoft.Xna.Framework.Graphics;

namespace SpiritsOfTheForgotten.Projectiles.WeaponProj
{
	public class NebulaSwordBeam2 : ModProjectile
	{

        public override void SetStaticDefaults()
        {
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 4;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;        //The recording mode
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

			projectile.width = 22;
			projectile.height = 22;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.melee = true;
			projectile.timeLeft = 300;
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


			int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.PinkFlame, projectile.velocity.X, projectile.velocity.Y, 100, default(Color), 2f);
			Dust dust1 = Main.dust[dust];
			dust1.noGravity = true;

			projectile.rotation = projectile.velocity.ToRotation() + 1.57f;

		}

		




	}

}