using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using SpiritsOfTheForgotten.Projectiles.WeaponProj;

namespace SpiritsOfTheForgotten.Projectiles.WeaponProj
{
	public class VortexSentry : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Vortex Energy Field");     //The English name of the projectile
			
		}

		public override void OnHitNPC(Terraria.NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.Electrified, 300); 
		}

		public override void SetDefaults()
		{
			projectile.width = 96;               
			projectile.height = 96;             
			projectile.friendly = true;        
			projectile.hostile = false;         
			projectile.ranged = true;           										
			projectile.timeLeft = 600;          
			projectile.alpha = 255;             
			projectile.light = 0.5f;            
			projectile.tileCollide = false;                    
			projectile.timeLeft = 1200;
			projectile.sentry = true;
			projectile.penetrate = -1;
			Main.projFrames[projectile.type] = 4;
			

		}
		public int timer;
		public override void AI()
		{
			timer += 1;
			if (timer > 1200)
			{
				// Fade out
				projectile.alpha += 25;
				if (projectile.alpha > 255)
				{
					projectile.alpha = 255;
				}
			}
			else
			{
				// Fade in
				projectile.alpha -= 25;
				if (projectile.alpha < 25)
				{
					projectile.alpha = 25;
				}
			}

			for (int i = 0; i < Main.maxProjectiles; i++)
			{
				Projectile proj = Main.projectile[i];
				if (proj.type == ModContent.ProjectileType<VortexSentry>() && proj.active && proj.owner == projectile.owner && proj.whoAmI != projectile.whoAmI)
                {
					if (projectile.timeLeft < proj.timeLeft)
						projectile.Kill();  //This projectile is older
					else
						proj.Kill();
				}
			}

			for (int i = 0; i < Main.maxProjectiles; i++)
			{
				if (Main.projectile[i].active && projectile.Hitbox.Intersects(Main.projectile[i].Hitbox) && Main.projectile[i].ranged && Main.projectile[i].friendly)
			    {
					Main.projectile[i].GetGlobalProjectile<SotfGlobalProjectile>().vortexBoost = true;

				}
			}

			projectile.frameCounter++;
			if (projectile.frameCounter >= 4)
			{
				projectile.frameCounter = 0;
				projectile.frame = (projectile.frame + 1) % 3;
			}
		}
	

		

		

	}
}