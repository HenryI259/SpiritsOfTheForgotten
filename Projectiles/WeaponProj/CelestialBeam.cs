using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using SpiritsOfTheForgotten.Buffs;
using System.Security.Cryptography.X509Certificates;

namespace SpiritsOfTheForgotten.Projectiles.WeaponProj
{
	public class CelestialBeam : ModProjectile
	{
		



		public override void OnHitNPC(Terraria.NPC target, int damage, float knockback, bool crit)
		{
			Terraria.Main.player[projectile.owner].AddBuff(ModContent.BuffType<LunarPowerBuff>(), 300);
			target.AddBuff(ModContent.BuffType<CelestialFlamesBuff>(), 300);

			projectile.velocity *= 0f;
			projectile.alpha = 255;
			projectile.timeLeft = 3;

			projectile.position.X += projectile.width / 2;
			projectile.position.Y += projectile.height / 2;
			projectile.width = 60;
			projectile.height = 60;
			projectile.position.X -= projectile.width / 2;
			projectile.position.Y -= projectile.height / 2;
			projectile.knockBack = 5f;
		}





		public override void SetDefaults()
		{
			projectile.magic = true;
			projectile.width = 14;
			projectile.height = 14;            
			projectile.friendly = true;        
			projectile.hostile = false;        
			projectile.timeLeft = 300;        
			projectile.ignoreWater = true;          
			projectile.tileCollide = true;  
			projectile.extraUpdates = 20;
			projectile.alpha = 255;
			projectile.tileCollide = true;
			projectile.penetrate = -1;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = -1;
		}

		public override void AI()
		{

			projectile.rotation = projectile.velocity.ToRotation();

			/*
			if (projectile.position.Y > Main.player[projectile.owner].MouseWorld.Y - 300f)
			{
				projectile.tileCollide = true;
			}
			if ((double)projectile.position.Y < Main.worldSurface * 16.0)
			{
				projectile.tileCollide = true;
			}
			*/

			//projectile.scale = projectile.ai[1];
			
			
			for (int num100 = 0; num100 < 10; num100++)
			{
				int num101 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.PinkFlame, 0, 0, 100, default(Color), 0.5f);
				Main.dust[num101].velocity *= 0;
				Main.dust[num101].scale *= 2f;
				Main.dust[num101].fadeIn = 1f;
				Main.dust[num101].noGravity = true;
			}

			
		}



        public override void Kill(int timeLeft)
        {
			for (float i = 0; i < 360; i += 2)
            {
				Vector2 direction = Vector2.UnitY.RotatedBy(MathHelper.ToRadians(i)) * 30;
				int num101 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y) + direction, 1, 1, DustID.PinkFlame, 0, 0, 100, default(Color), 1f);
				//Main.dust[num101].velocity *= 0;
				Main.dust[num101].scale *= 2f;
				Main.dust[num101].fadeIn = 1f;
				Main.dust[num101].noGravity = true;

				/*
				Vector2 direction1 = Vector2.UnitY.RotatedBy(MathHelper.ToRadians(i)) * 10;
				int num102 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y) + direction1, 1, 1, DustID.PinkFlame, 0, 0, 100, default(Color), 1f);
				//Main.dust[num101].velocity *= 0;
				Main.dust[num102].scale *= 2f;
				Main.dust[num102].fadeIn = 1f;
				Main.dust[num102].noGravity = true;
				*/
			}
			
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{



			projectile.velocity *= 0f;
			projectile.alpha = 255;
			projectile.timeLeft = 3;

			projectile.position.X += projectile.width / 2;
			projectile.position.Y += projectile.height / 2;
			projectile.width = 60;
			projectile.height = 60;
			projectile.position.X -= projectile.width / 2;
			projectile.position.Y -= projectile.height / 2;
			projectile.knockBack = 5f;
			return false;
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