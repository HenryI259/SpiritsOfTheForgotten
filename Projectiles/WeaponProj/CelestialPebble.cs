using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using SpiritsOfTheForgotten.Buffs;
using System.Security.Cryptography.X509Certificates;

namespace SpiritsOfTheForgotten.Projectiles.WeaponProj
{
	public class CelestialPebble : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;        //The recording mode
		}



		public override void OnHitNPC(Terraria.NPC target, int damage, float knockback, bool crit)
		{
			Terraria.Main.player[projectile.owner].AddBuff(ModContent.BuffType<LunarPowerBuff>(), 300);
			target.AddBuff(ModContent.BuffType<CelestialFlamesBuff>(), 300);

		}





		public override void SetDefaults()
		{

			projectile.width = 14;               //The width of projectile hitbox
			projectile.height = 14;              //The height of projectile hitbox

			projectile.friendly = true;         //Can the projectile deal damage to enemies?
			projectile.hostile = false;         //Can the projectile deal damage to the player?
												//projectile.rotation = projectile.velocity.ToRotation();

			projectile.timeLeft = 600;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)


			projectile.ignoreWater = false;          //Does the projectile's speed be influenced by water?
			projectile.tileCollide = true;          //Can the projectile collide with tiles?
			projectile.extraUpdates = 1;            //Set to above 0 if you want the projectile to update multiple time in a frame



		}

		public override void AI()
		{
			projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;

			for (int i = 0; i < 1; i++)
            {
				int a = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.PlatinumCoin);
				Main.dust[a].noGravity = true;
            }
		}
		


	}
}