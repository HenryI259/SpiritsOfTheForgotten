using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using System;
using SpiritsOfTheForgotten.Projectiles.WeaponProj;

namespace SpiritsOfTheForgotten.Projectiles.WeaponProj
{
	public class StardustSentry : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Stardust Portal");     //The English name of the projectile

		}






		public override void SetDefaults()
		{
			projectile.width = 42;
			projectile.height = 48;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.minion = true;
			projectile.timeLeft = 1800;
			projectile.alpha = 255;
			projectile.light = 0.5f;
			projectile.tileCollide = false;
			projectile.sentry = true;
			projectile.penetrate = -1;
			Main.projFrames[projectile.type] = 4;
			

		}
		public int timer;
		public NPC npc;
		public int pull;
		public NPC truenpc;
		public int pullTimer;

		public override void AI()
		{
			
			
			timer += 1;
			pullTimer += 1;
			for (int i = 0; i < Main.maxProjectiles; i++)
			{
				Projectile proj = Main.projectile[i];
				if (proj.type == ModContent.ProjectileType<StardustSentry>() && proj.active && proj.owner == projectile.owner && proj.whoAmI != projectile.whoAmI)
				{
					if (projectile.timeLeft < proj.timeLeft)
						projectile.Kill();  //This projectile is older
					else
						proj.Kill();
				}
			}

			if (timer > 1800)
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
				if (projectile.alpha < 50)
				{
					projectile.alpha = 50;
				}
			}

			if (pullTimer == 60)
			{
				pull += 2;
				pullTimer = 0;
			}

			
			float distance = 600f;
			for (int k = 0; k < 200; k++)
			{
				NPC npc = Main.npc[k];
				if (npc.CanBeChasedBy() && npc.lifeMax < 10000)
				{
					Vector2 newMove = projectile.Center - Main.npc[k].Center;
					float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
					if (distanceTo < distance)
					{
						
						distance = distanceTo;
						AdjustMagnitude(ref newMove, pull);
						npc.velocity += newMove;
					}
				}
			}

           
        
			projectile.frameCounter++;
			if (projectile.frameCounter >= 5)
			{
				projectile.frameCounter = 0;
				projectile.frame = (projectile.frame + 1) % 4;
			}
		}



		private void AdjustMagnitude(ref Vector2 vector, float speed)
		{
			if (speed > 10)
				speed = 10;

			speed /= 70;
			float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
			if (magnitude > speed)
			{
				vector *= speed / magnitude;
			}
		}




	}
}