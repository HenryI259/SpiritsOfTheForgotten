using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using SpiritsOfTheForgotten.Buffs;

namespace SpiritsOfTheForgotten.Projectiles.WeaponProj
{
	public class CelestialPlatform : ModProjectile
	{

		public override void SetDefaults()
		{
			projectile.width = 82;
			projectile.height = 82;
			projectile.timeLeft = 42;
			projectile.alpha = 0;
			projectile.light = 0.5f;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
			Main.projFrames[projectile.type] = 14;
			projectile.penetrate = -1;

		}

		public int timer;
		public override void AI()
		{
			timer++;

			projectile.frameCounter++;
			if (projectile.frameCounter >= 3)
			{
				projectile.frameCounter = 0;
				projectile.frame = (projectile.frame + 1) % 14;
			}


		}

		


	}
}