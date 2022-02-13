using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using SpiritsOfTheForgotten.Buffs;

namespace SpiritsOfTheForgotten.Npcs.Celestialminiboss
{
	public class SolarOrb : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			
			ProjectileID.Sets.Homing[projectile.type] = true;
		}




		public override void SetDefaults()
		{

			projectile.width = 42;
			projectile.height = 42;

			projectile.friendly = false;
			projectile.hostile = true;

			projectile.timeLeft = 180;
			projectile.alpha = 20;
			projectile.light = 0.5f;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;

		}
		public int timer;
		public bool foundTarget;
		public Vector2 targetCenter;
		public Player target;
		public override void AI()
		{
			

			var a = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.SolarFlare, 0, 0);
			a.noGravity = true;

			for (int i = 0; i < Main.maxNPCs; i++)
			{
				Player player = Main.player[i];

				float between = Vector2.Distance(player.Center, projectile.Center);
				bool closest = Vector2.Distance(projectile.Center, targetCenter) > between;
				if ((closest) || !foundTarget)
				{
					targetCenter = player.Center;
					foundTarget = true;
					target = player;
				}

			}
			
			if (foundTarget)
			{
				Vector2 move = target.Center - projectile.Center;
				AdjustMagnitude(ref move);
				projectile.velocity = (10 * projectile.velocity + move) / 11f;
				AdjustMagnitude(ref projectile.velocity);
			}


		}

		private void AdjustMagnitude(ref Vector2 vector)
		{
			float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
			if (magnitude > 5f)
			{
				vector *= 5f / magnitude;
			}
		}

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
			projectile.Kill();
        }

        public override void Kill(int timeLeft)
        {
			var a = Projectile.NewProjectileDirect(projectile.Center, Vector2.Zero, ModContent.ProjectileType<SolarExplosion>(), 15, 2, projectile.owner);
			a.ai[0] = 1;
        }

    }

}