using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using SpiritsOfTheForgotten.Buffs;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework.Graphics;


namespace SpiritsOfTheForgotten.Projectiles.WeaponProj
{
	public class NebulaCloud : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Nebula Cloud");     
			
		}




		public override void SetDefaults()
		{
			projectile.width = 28;               
			projectile.height = 30;             	
			projectile.friendly = true;         
			projectile.hostile = false;         	
			projectile.timeLeft = 180;          	
			projectile.ignoreWater = false;         
			projectile.tileCollide = true;
			projectile.alpha = 75;
			Main.projFrames[projectile.type] = 3;
			projectile.penetrate = -1;
		}

		public int timer;
		public override void AI()
        
        {
			timer += 1;
			float rotationsPerSecond = Main.rand.NextFloat(1, 10) / 10f;
			bool rotateClockwise = true;
			
			projectile.rotation += (rotateClockwise ? 1 : -1) * MathHelper.ToRadians(rotationsPerSecond * 6f);

			if (timer > 120)
            {
					projectile.alpha += 3;

			}

			projectile.velocity = (15 * projectile.velocity) / 16f;

			projectile.frameCounter++;
			if (projectile.frameCounter >= 5)
			{
				projectile.frameCounter = 0;
				projectile.frame = (projectile.frame + 1) % 3;
			}
		}

		



	}
}