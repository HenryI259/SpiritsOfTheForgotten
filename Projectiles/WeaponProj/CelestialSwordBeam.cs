using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using SpiritsOfTheForgotten.Buffs;
using Microsoft.Xna.Framework.Graphics;

namespace SpiritsOfTheForgotten.Projectiles.WeaponProj
{
	public class CelestialSwordBeam : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.Homing[projectile.type] = true;
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 7;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;        //The recording mode
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			//Redraw the projectile with the color not influenced by light
			Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, Main.projectileTexture[projectile.type].Frame(1, Main.projFrames[projectile.type], 0, projectile.frame), color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}

		public override void OnHitNPC(Terraria.NPC target, int damage, float knockback, bool crit)
		{
			Terraria.Main.player[projectile.owner].AddBuff(ModContent.BuffType<LunarPowerBuff>(), 300);
			target.AddBuff(ModContent.BuffType<CelestialFlamesBuff>(), 300);
		}


		public override void SetDefaults()
		{

			projectile.width = 30;
			projectile.height = 30;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.melee = true;
			projectile.timeLeft = 600;
			projectile.alpha = 255;
			projectile.light = 0.5f;
			projectile.ignoreWater = true;
			projectile.tileCollide = true;
			Main.projFrames[projectile.type] = 4;
		}
		public int timer;
		public override void AI()
		{
			timer += 1;

			int dusttype = Main.rand.Next(new int[] { DustID.SolarFlare, DustID.Vortex, DustID.PinkFlame, DustID.BlueCrystalShard });
			int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, dusttype, projectile.velocity.X, projectile.velocity.Y, 100, default(Color), 1f);
			Dust dust1 = Main.dust[dust];
			dust1.noGravity = true;

			projectile.frameCounter++;
			if (projectile.frameCounter >= 10)
			{
				projectile.frameCounter = 0;
				projectile.frame = (projectile.frame + 1) % 4;
			}

			if (timer > 500)
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
				if (projectile.alpha < 100)
				{
					projectile.alpha = 100;
				}
			}

			int moveSpeed = timer + 15;

			Vector2 move = Main.MouseWorld - projectile.Center;
			if ((float)Math.Sqrt(move.X * move.X + move.Y * move.Y) < 60)
            {
				projectile.Kill();
            }

			AdjustMagnitude(ref move, moveSpeed);
			projectile.velocity = move;


			projectile.rotation = projectile.velocity.ToRotation() + 1.57f;

		}

		private void AdjustMagnitude(ref Vector2 vector, float speed)
		{
			float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
			if (magnitude != speed)
			{
				vector *= speed / magnitude;
			}
		}



        public override void Kill(int timeLeft)
        {
			Vector2 velo = projectile.velocity *= 25 / (float)Math.Sqrt(projectile.velocity.X * projectile.velocity.X + projectile.velocity.Y * projectile.velocity.Y);
			int type1 = Main.rand.Next(new int[] { ModContent.ProjectileType<SolarSwordBeam>(), ModContent.ProjectileType<SolarSwordBeam2>() });
			int type2 = Main.rand.Next(new int[] { ModContent.ProjectileType<VortexSwordBeam>(), ModContent.ProjectileType<VortexSwordBeam2>() });
			int type3 = Main.rand.Next(new int[] { ModContent.ProjectileType<NebulaSwordBeam>(), ModContent.ProjectileType<NebulaSwordBeam2>() });
			int type4 = Main.rand.Next(new int[] { ModContent.ProjectileType<StardustSwordBeam>(), ModContent.ProjectileType<StardustSwordBeam2>() });


			Projectile.NewProjectile(projectile.Center, velo.RotatedBy(0.3f), type1, (int)(projectile.damage * .5f), projectile.knockBack, projectile.owner);
			Projectile.NewProjectile(projectile.Center, velo.RotatedBy(0.1f), type2, (int)(projectile.damage * .5f), projectile.knockBack, projectile.owner);
			Projectile.NewProjectile(projectile.Center, velo.RotatedBy(-0.1f), type3, (int)(projectile.damage * .5f), projectile.knockBack, projectile.owner);
			Projectile.NewProjectile(projectile.Center, velo.RotatedBy(-0.3), type4, (int)(projectile.damage * .5f), projectile.knockBack, projectile.owner);


		}


	}

}