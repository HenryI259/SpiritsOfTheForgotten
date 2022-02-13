using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using SpiritsOfTheForgotten.Buffs;
using SpiritsOfTheForgotten.Projectiles.WeaponProj;

namespace SpiritsOfTheForgotten.Projectiles.WeaponProj
{
	public class CelestialSentry : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Celestial Core");     //The English name of the projectile
			ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
		}

		public override void OnHitNPC(Terraria.NPC target, int damage, float knockback, bool crit)
		{
			Main.player[projectile.owner].AddBuff(ModContent.BuffType<LunarPowerBuff>(), 300);

			target.AddBuff(ModContent.BuffType<CelestialFlamesBuff>(), 300);

			int num = Main.rand.Next(1, 3);
			for (int i = 0; i < num; i++)
			{
				int speedX = Main.rand.Next(-5, 6);
				int speedY = Main.rand.Next(-5, 6);
				Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, speedX, speedY, ModContent.ProjectileType<CelestialSpark>(), 400, 0, projectile.owner, 0, 0);
			}
		}

		public override void SetDefaults()
		{
			projectile.width = 44;
			projectile.height = 44;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.ranged = true;
			projectile.alpha = 50;
			projectile.light = 0.5f;
			projectile.tileCollide = false;
			projectile.sentry = true;
			projectile.penetrate = -1;
			Main.projFrames[projectile.type] = 11;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 10;
			projectile.timeLeft = Projectile.SentryLifeTime;
			projectile.aiStyle = 123;

		}
		public int timer;
		public override void AI()
		{
			timer += 1;
			Main.player[projectile.owner].UpdateMaxTurrets();

			if (timer % 40 == 0)
			{

				
				Projectile.NewProjectile(projectile.Center, Vector2.Zero, ModContent.ProjectileType<OrbittingStar>(), 300, 5, projectile.owner);
					
					


			}

			projectile.frameCounter++;
			if (projectile.frameCounter >= 4)
			{
				projectile.frameCounter = 0;
				projectile.frame = (projectile.frame + 1) % 11;
			}
		}


	}


}