using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using SpiritsOfTheForgotten.Buffs;

namespace SpiritsOfTheForgotten.Projectiles.WeaponProj
{
	public class CelestialStar : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Celestial Star");     //The English name of the projectile
			ProjectileID.Sets.Homing[projectile.type] = true;
		}



		public override void OnHitNPC(Terraria.NPC target, int damage, float knockback, bool crit)
		{
			Terraria.Main.player[projectile.owner].AddBuff(ModContent.BuffType<LunarPowerBuff>(), 300);
			target.AddBuff(ModContent.BuffType<CelestialFlamesBuff>(), 300);
		}


		public override void SetDefaults()
		{

			projectile.width = 42;              
			projectile.height = 16;             
			             
			projectile.friendly = true;         
			projectile.hostile = false;         
			projectile.melee = true;           
											   
			projectile.timeLeft = 600;          
			projectile.alpha = 255;             
			projectile.light = 0.5f;            
			projectile.ignoreWater = true;          
			projectile.tileCollide = false;         
			projectile.extraUpdates = 1;            
			Main.projFrames[projectile.type] = 8;

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

			projectile.frameCounter++;
			if (projectile.frameCounter >= 16)
			{
				projectile.frameCounter = 0;
				projectile.frame = (projectile.frame + 1) % 8;
			}
			
			if (projectile.localAI[0] == 0f)
			{
				AdjustMagnitude(ref projectile.velocity);
				projectile.localAI[0] = 1f;
			}
			Vector2 move = Vector2.Zero;
			float distance = 400f;
			bool target = false;
			for (int k = 0; k < 200; k++)
			{
				NPC npc = Main.npc[k];
				if (npc.CanBeChasedBy())
				{
					Vector2 newMove = Main.npc[k].Center - projectile.Center;
					float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
					if (distanceTo < distance)
					{
						move = newMove;
						distance = distanceTo;
						target = true;
					}
				}
			}
			if (target)
			{
				AdjustMagnitude(ref move);
				projectile.velocity = (10 * projectile.velocity + move) / 11f;
				AdjustMagnitude(ref projectile.velocity);
			}
			
			projectile.rotation = projectile.velocity.ToRotation();

		}
		
		private void AdjustMagnitude(ref Vector2 vector)
		{
			float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
			if (magnitude > 9f)
			{
				vector *= 9f / magnitude;
			}
		}
		




	}

}