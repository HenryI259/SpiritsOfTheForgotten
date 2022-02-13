using Terraria.ID;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;


namespace SpiritsOfTheForgotten.Projectiles.WeaponProj
{
	public class CrystalWave : ModProjectile
	{

		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 8;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[projectile.type] = 1;        //The recording mode
		}

		public override void SetDefaults()
		{
			projectile.width = 30;
			projectile.height = 46;
			projectile.friendly = true;
			projectile.magic = true;
			projectile.damage = 10;
			projectile.knockBack = 2;
			projectile.tileCollide = false;
			projectile.timeLeft = 300;
			projectile.penetrate = -1;
			projectile.ignoreWater = true;
		}
		public override void AI()
		{

			projectile.rotation = projectile.velocity.ToRotation();

			for (int i = 0; i < 1; i++)
			{
				int s = Main.rand.Next(1, 3);
				int a = Dust.NewDust(projectile.position, projectile.width, projectile.height, 61, Main.rand.NextFloat(-5, 6), Main.rand.NextFloat(-5, 6), 10, default(Color), s);
				//int b = Dust.NewDust(projectile.position, projectile.width, projectile.height, 62, Main.rand.NextFloat(-5, 6), Main.rand.NextFloat(-5, 6), 10, default(Color), s * 1.5f);
				Main.dust[a].noGravity = true;
				//Main.dust[b].noGravity = true;
			}
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


	}
}
