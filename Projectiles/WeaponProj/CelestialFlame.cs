using Terraria.ID;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using SpiritsOfTheForgotten.Buffs;

namespace SpiritsOfTheForgotten.Projectiles.WeaponProj
{
    public class CelestialFlame : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Celestial Flame");
		}
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
			int s = Main.rand.Next(3, 6); 

			int dusttype = Main.rand.Next(new int[] { DustID.BlueCrystalShard, DustID.SolarFlare, DustID.PinkFlame, DustID.Vortex });
			if (dusttype != DustID.PinkFlame)
            {
				s /= 2;
            }
			

			int dust = Dust.NewDust(projectile.Center, projectile.width + 2, projectile.height + 2, dusttype, projectile.velocity.X * 0.4f, projectile.velocity.Y * 0.4f, 100, default(Color), s);
			Dust celestialDust = Main.dust[dust];
			celestialDust.noGravity = true;
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(ModContent.BuffType<CelestialFlamesBuff>(), 300);
			
			Main.player[projectile.owner].AddBuff(ModContent.BuffType<LunarPowerBuff>(), 300);
			


		
		int num = Main.rand.Next(3, 5);
			for (int i = 0; i < num; i++)
			{
				int speedX = Main.rand.Next(-5, 6);
				int speedY = Main.rand.Next(-5, 6);
				Projectile.NewProjectile(target.position.X, target.position.Y, speedX, speedY, mod.ProjectileType("CelestialSpark"), damage, 0, projectile.owner, 0, 0);
			}

		}
	}
}
