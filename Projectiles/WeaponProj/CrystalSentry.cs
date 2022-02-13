using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using SpiritsOfTheForgotten.Buffs;
using SpiritsOfTheForgotten.Projectiles.WeaponProj;
using SpiritsOfTheForgotten;

namespace SpiritsOfTheForgotten.Projectiles.WeaponProj
{
	public class CrystalSentry : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Northern Light");     //The English name of the projectile
			ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
		}

		

		public override void SetDefaults()
		{
			projectile.width = 26;
			projectile.height = 52;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.alpha = 50;
			projectile.light = 0.5f;
			projectile.tileCollide = false;
			projectile.sentry = true;
			projectile.penetrate = -1;
			Main.projFrames[projectile.type] = 4;
			projectile.timeLeft = Projectile.SentryLifeTime;
			projectile.aiStyle = 123;
		}
		public int timer;
		public override void AI()
		{
			Main.player[projectile.owner].UpdateMaxTurrets();

			timer += 1;
			
			if (timer == 1) Main.PlaySound(SoundID.Item101, projectile.position);


			if (timer % 60 == 0)
			{
				NPC target = Helper.NPCs.FindNearestNPCDirect(projectile.Center, 800);
				
				if (target != null) Projectile.NewProjectile(target.Center + Vector2.UnitY.RotatedByRandom(MathHelper.Pi) * 80, Vector2.Zero, ModContent.ProjectileType<AuroraFlame>(), 20, 5, projectile.owner);
			}

			projectile.frameCounter++;
			if (projectile.frameCounter >= 4)
			{
				projectile.frameCounter = 0;
				projectile.frame = (projectile.frame + 1) % 4;
			}
		}


	}


}