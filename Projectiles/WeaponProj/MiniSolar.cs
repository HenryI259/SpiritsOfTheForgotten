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
	public class MiniSolar : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("MiniSolar");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;        //The recording mode
		}

		public bool dead = false;

		public override void OnHitNPC(Terraria.NPC target, int damage, float knockback, bool crit)
		{
			Terraria.Main.player[projectile.owner].AddBuff(ModContent.BuffType<SolarStrength>(), 600);
			
			target.AddBuff(BuffID.Daybreak, 300, true);
			target.AddBuff(BuffID.OnFire, 300, true);
		
			dead = true;

		}


		public override void SetDefaults()
		{
			projectile.width = 50;
			projectile.height = 50;            
			projectile.friendly = true;        
			projectile.hostile = false;        
			projectile.melee = true;       
			projectile.timeLeft = 400;              
			projectile.light = 0.5f;            
			projectile.ignoreWater = true;          
			projectile.tileCollide = true;
			projectile.penetrate = -1;
			



		}
		public int timer;
		public override void AI()
		{
			
			
			projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;
			
			if (dead)
            {
				timer += 1;
				if (timer == 1)
                {
					Main.PlaySound(SoundID.Item100, projectile.Center);
				}
				projectile.velocity = Vector2.Zero;
				projectile.alpha = 255;
				
				int dustamount = 5;
				for (int i = 0; i < dustamount; i++)
                {
					int s = Main.rand.Next(1, 3);
					Vector2 dustVelocity = new Vector2(10, 0).RotatedByRandom(360);
					int dust = Dust.NewDust(projectile.position + new Vector2(17.5f, 17.5f), 1, 1, DustID.SolarFlare, dustVelocity.X, dustVelocity.Y * 0.4f, 100, default(Color), s);
					Main.dust[dust].noGravity = true;
				}

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

		
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
			dead = true;
			
			return false;
        }










    }
}
