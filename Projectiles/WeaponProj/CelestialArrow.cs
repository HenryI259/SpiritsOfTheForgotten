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
	public class CelestialArrow : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("CelestialArrow");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 15;    //The length of old position to be recorded
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

			projectile.width = 14;               
			projectile.height = 36;              
			            
			projectile.friendly = true;         
			projectile.hostile = false;         
			projectile.ranged = true;          
												
			projectile.timeLeft = 600;          
			projectile.light = 0.5f;            
			projectile.ignoreWater = false;          
			projectile.tileCollide = true;         
			projectile.extraUpdates = 1;            //Set to above 0 if you want the projectile to update multiple time in a frame
			
			


		}
		


		

        public override void Kill(int timeLeft)
		{
			projectile.position.Y += -700;
			int numberProjectiles = 3;
			for (int i = 0; i < numberProjectiles; i++)
			{
				
				int a = Projectile.NewProjectile(projectile.Center.X, projectile.position.Y, Main.rand.Next(-50, 50), 200, ProjectileID.PhantasmalBolt, (int)(projectile.damage * .5f), 5, projectile.owner);
				Main.projectile[a].aiStyle = 1;
				Main.projectile[a].tileCollide = true;
				Main.projectile[a].hostile = false;
				Main.projectile[a].friendly = true;
			}
			
			

		}

        public override void AI()
        {
			projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;
			
		}


    }
}
	
		
