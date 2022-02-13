using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using SpiritsOfTheForgotten.Buffs;

namespace SpiritsOfTheForgotten.Projectiles.WeaponProj
{
    public class MoonlordTenticle : ModProjectile
    {

		public override void SetDefaults()
		{
			projectile.width = 60;
			projectile.height = 196;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.magic = true;
			projectile.timeLeft = 45;
			projectile.alpha = 0;
			projectile.light = 0.5f;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
			Main.projFrames[projectile.type] = 15;
			projectile.penetrate = -1;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = -1;
			
		}

		public int timer;
		Vector2 tentacleDirection;
        public override void AI()
        {
			timer++;

			projectile.frameCounter++;
			if (projectile.frameCounter >= 3)
			{
				projectile.frameCounter = 0;
				projectile.frame = (projectile.frame + 1) % 15;
			}

			if (timer == 1)
			{
				tentacleDirection = projectile.velocity;
				projectile.rotation = projectile.velocity.ToRotation() + 1.57f;
				Main.PlaySound(SoundID.Item117, projectile.Center);
			}

			projectile.velocity *= 0;

			if (timer > 15 && timer < 33)
            {
				projectile.damage = 200;
            }
			else
            {
				projectile.damage = 0;
            }
			
			if (timer == 45)
            {
				Main.PlaySound(SoundID.Item100, projectile.Center);
			}


		}

		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			// Incorporate the original hitbox into collision logic.

			if (projHitbox.Intersects(targetHitbox))
				return true;
			// Otherwise, perform an AABB line collision check to check the whole line.
			float _ = 0f;
			return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), projectile.Center, projectile.Center + tentacleDirection * 2, 28f * projectile.scale, ref _);
		}
		

	}
}