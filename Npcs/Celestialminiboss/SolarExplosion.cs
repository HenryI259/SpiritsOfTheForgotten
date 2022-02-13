using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using SpiritsOfTheForgotten.Buffs;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework.Graphics;


namespace SpiritsOfTheForgotten.Npcs.Celestialminiboss
{
	public class SolarExplosion : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Solar Explosion");     
			
		}




		public override void SetDefaults()
		{
			projectile.width = 100;               
			projectile.height = 100;             	
			projectile.friendly = false;         
			projectile.hostile = true;         	
			projectile.timeLeft = 180;          	
			projectile.ignoreWater = false;         
			projectile.tileCollide = false;
			projectile.alpha = 255;
			projectile.penetrate = -1;
		}

		public int timer;
		public override void AI()
        {

			for (int i = 0; i < 20; i++)
			{
				int dusttype = DustID.SolarFlare;
				var dust = Dust.NewDustDirect(projectile.Center, 1, 1, dusttype, Main.rand.NextFloat(-6, 5) * 1.3f, Main.rand.NextFloat(-6, 5) * 1.3f, 50, default(Color), 1.25f);
				dust.noGravity = true;

			}

		}

		



	}
}