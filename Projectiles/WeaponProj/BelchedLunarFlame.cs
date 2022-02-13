using Terraria.ID;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using SpiritsOfTheForgotten.Buffs;

namespace SpiritsOfTheForgotten.Projectiles.WeaponProj
{
    public class BelchedLunarFlame : ModProjectile
    {
		
		public override void SetDefaults()
		{
			projectile.width = 5;
			projectile.height = 5;
			projectile.friendly = true;
			projectile.ranged = true;
			projectile.damage = 100;
			projectile.knockBack = 5;
			projectile.rotation = projectile.velocity.ToRotation();
			projectile.tileCollide = true;
			projectile.timeLeft = 40;
			projectile.penetrate = 10;
			projectile.alpha = 255;
		}
		public override void AI()
		{
			projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
			int dustamount = 4;
			for (int i = 0; i < dustamount; i++)
			{
				int s = Main.rand.Next(1, 3);
				Dust.NewDust(projectile.position - new Vector2(2f, 2f), projectile.width + 4, projectile.height + 4, DustID.Vortex, projectile.velocity.X * 0.4f, projectile.velocity.Y * 0.4f, 100, default(Color), s);

			}
			
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(ModContent.BuffType<CelestialFlamesBuff>(), 300);
			
			
			


		
		

		}
	}
}
