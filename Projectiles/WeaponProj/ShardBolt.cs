using Terraria.ID;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace SpiritsOfTheForgotten.Projectiles.WeaponProj
{
	public class ShardBolt : ModProjectile
	{
		
		public override void SetDefaults()
		{
			projectile.width = 5;
			projectile.height = 5;
			projectile.friendly = true;
			projectile.magic = true;
			projectile.damage = 10;
			projectile.knockBack = 2;
			projectile.rotation = projectile.velocity.ToRotation();
			projectile.tileCollide = true;
			projectile.timeLeft = 300;
			projectile.penetrate = 2;
			projectile.ignoreWater = true;
			projectile.alpha = 255;
		}
		public override void AI()
		{
			projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
			for (int i = 0; i < 4; i++)
			{
				int s = Main.rand.Next(1, 3);
				int a = Dust.NewDust(projectile.position, 4, 4, 61, Main.rand.NextFloat(-5, 6), Main.rand.NextFloat(-5, 6), 10, default(Color), s * 1.5f);
				int b = Dust.NewDust(projectile.position, 4, 4, 62, Main.rand.NextFloat(-5, 6), Main.rand.NextFloat(-5, 6), 10, default(Color), s * 1.5f);
				Main.dust[a].noGravity = true;
				Main.dust[b].noGravity = true;
			}
		}
		
			
	}
}
