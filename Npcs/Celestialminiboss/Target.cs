using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using SpiritsOfTheForgotten.Buffs;
using SpiritsOfTheForgotten.Projectiles.WeaponProj;

namespace SpiritsOfTheForgotten.Npcs.Celestialminiboss
{
	public class Target : ModProjectile
	{

		public override void SetDefaults()
		{
			projectile.width = 64;
			projectile.height = 64;
			projectile.timeLeft = 60;
			projectile.alpha = 0;
			projectile.light = 0.5f;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
			projectile.penetrate = -1;
			projectile.hostile = true;

		}

        public override void Kill(int timeLeft)
        {
			var a = Projectile.NewProjectileDirect(projectile.Center + new Vector2(0, -300), new Vector2(0, 10), ModContent.ProjectileType<SolarShard>(), 25, 3, projectile.owner);
			a.hostile = true;
			a.friendly = false;
		}




    }
}