using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using SpiritsOfTheForgotten.Buffs;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework.Graphics;

namespace SpiritsOfTheForgotten.Projectiles.WeaponProj
{
	public class CelestialLaser : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Celestial Laser");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 45;    //The length of old position to be recorded
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
				spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, Main.projectileTexture[projectile.type].Frame(1, Main.projFrames[projectile.type], 0, projectile.frame), color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}
		

		public override void SetDefaults()
		{

			projectile.width = 6;
			projectile.height = 10;

			projectile.friendly = true;
			projectile.hostile = false;
			projectile.magic = true;

			projectile.timeLeft = 3000;
			projectile.light = 0.5f;
			projectile.ignoreWater = false;
			projectile.tileCollide = true;
			projectile.extraUpdates = 5;            //Set to above 0 if you want the projectile to update multiple time in a frame

			Main.projFrames[projectile.type] = 4;


		}



		

		public override void AI()
		{
			projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;
			

			projectile.frameCounter++;
			if (projectile.frameCounter >= 60)
			{
				projectile.frameCounter = 0;
				projectile.frame = (projectile.frame + 1) % 60;
			}
		}
	}


	
}