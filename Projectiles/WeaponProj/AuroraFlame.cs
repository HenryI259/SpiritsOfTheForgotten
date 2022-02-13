using Microsoft.Xna.Framework;
using SpiritsOfTheForgotten.Projectiles.WeaponProj;
using SpiritsOfTheForgotten.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using System;
using SpiritsOfTheForgotten.Buffs;

namespace SpiritsOfTheForgotten.Projectiles.WeaponProj
{
    public class AuroraFlame : ModProjectile
    {
		public override void SetDefaults()
		{
			projectile.width = 36;
			projectile.height = 52;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.alpha = 0;
			projectile.light = 0.5f;
			projectile.tileCollide = false;
			projectile.minion = true;
			projectile.penetrate = -1;
			Main.projFrames[projectile.type] = 4;
			projectile.timeLeft = 300;
			projectile.penetrate = -1;
		}

		public bool hit = false;
        public override void AI()
        {
            if (projectile.timeLeft == 9)
            {
				projectile.velocity *= 0f;
				projectile.alpha = 255;
				projectile.position.X += projectile.width / 2;
				projectile.position.Y += projectile.height / 2;
				projectile.width = 128;
				projectile.height = 128;
				projectile.position.X -= projectile.width / 2;
				projectile.position.Y -= projectile.height / 2;
				projectile.knockBack = 5f;
				for (int i = 0; i < 40;  i++)
                {
					int num698 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 61, 0f, 0f, 100, default(Color), 2.25f);
					Main.dust[num698].noGravity = true;
					Dust dust32 = Main.dust[num698];
					Dust dust226 = dust32;
					dust226.velocity *= 7f;
					num698 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 61, 0f, 0f, 100, default(Color), 0.75f);
					dust32 = Main.dust[num698];
					dust226 = dust32;
					dust226.velocity *= 3f;
				}
				hit = true;
			}
			projectile.rotation = projectile.velocity.X * 0.05f;

			NPC target = Helper.NPCs.FindNearestNPCDirect(projectile.Center, 200);

			Vector2 move = (target != null ? target.Center - projectile.Center : Vector2.Zero);

			if (target != null)
			{
				projectile.velocity = move;
				AdjustMagnitude(ref projectile.velocity, 5);
			}
			else
            {
				projectile.velocity *= 0.95f;
            }

			projectile.frameCounter++;
			if (projectile.frameCounter >= 10)
			{
				projectile.frameCounter = 0;
				projectile.frame = (projectile.frame + 1) % 4;
			}
		}

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			if (!hit) projectile.timeLeft = 10;
        }


		private void AdjustMagnitude(ref Vector2 vector, float speed)
		{
			float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
			if (magnitude > speed)
			{
				vector *= speed / magnitude;
			}
		}
	}
}