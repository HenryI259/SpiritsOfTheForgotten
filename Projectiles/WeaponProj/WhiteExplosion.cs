using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using SpiritsOfTheForgotten.Buffs;

namespace SpiritsOfTheForgotten.Projectiles.WeaponProj
{
    public class WhiteExplosion : ModProjectile
    {

		public override void SetDefaults()
		{
			projectile.width = 32;
			projectile.height = 32;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.ranged = true;
			projectile.timeLeft = 21;
			projectile.alpha = 0;
			projectile.light = 0.5f;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
			Main.projFrames[projectile.type] = 7;
			projectile.penetrate = -1;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = -1;
			projectile.scale = 2;
		}

		
        public override void AI()
        {
			

			projectile.frameCounter++;
			if (projectile.frameCounter >= 3)
			{
				projectile.frameCounter = 0;
				projectile.frame = (projectile.frame + 1) % 7;
			}

			


		}

		
		

	}
}