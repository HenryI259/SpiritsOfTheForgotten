using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using SpiritsOfTheForgotten.Buffs;
using System.Security.Cryptography.X509Certificates;

namespace SpiritsOfTheForgotten.Projectiles.WeaponProj
{
	public class CelestialMoon : ModProjectile
	{
		



		public override void OnHitNPC(Terraria.NPC target, int damage, float knockback, bool crit)
		{
			Terraria.Main.player[projectile.owner].AddBuff(ModContent.BuffType<LunarPowerBuff>(), 300);
			target.AddBuff(ModContent.BuffType<CelestialFlamesBuff>(), 300);

		}





		public override void SetDefaults()
		{

			projectile.width = 48;
			projectile.height = 48;            
			projectile.friendly = true;        
			projectile.hostile = false;        
			projectile.timeLeft = 600;        
			projectile.ignoreWater = false;          
			projectile.tileCollide = true;  
			projectile.extraUpdates = 2;
		}

		public override void AI()
		{

			projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;

			/*
			if (projectile.position.Y > Main.player[projectile.owner].MouseWorld.Y - 300f)
			{
				projectile.tileCollide = true;
			}
			if ((double)projectile.position.Y < Main.worldSurface * 16.0)
			{
				projectile.tileCollide = true;
			}
			*/

			//projectile.scale = projectile.ai[1];
			projectile.rotation += projectile.velocity.X * 2f;
			Vector2 position = projectile.Center + Vector2.Normalize(projectile.velocity) * 10f;
			Dust obj8 = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Vortex, 0, 0, 100, default(Color), 0.5f)];
			obj8.position = position;
			obj8.velocity = projectile.velocity.RotatedBy(1.5707963705062866) * 0.33f + projectile.velocity / 4f;
			obj8.position += projectile.velocity.RotatedBy(1.5707963705062866);
			obj8.fadeIn = 0.5f;
			obj8.noGravity = true;
			Dust obj9 = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Vortex, 0, 0, 100, default(Color), 0.5f)];
			obj9.position = position;
			obj9.velocity = projectile.velocity.RotatedBy(-1.5707963705062866) * 0.33f + projectile.velocity / 4f;
			obj9.position += projectile.velocity.RotatedBy(-1.5707963705062866);
			obj9.fadeIn = 0.5f;
			obj9.noGravity = true;
			for (int num100 = 0; num100 < 1; num100++)
			{
				int num101 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.Vortex, 0, 0, 100, default(Color), 0.5f);
				Main.dust[num101].velocity *= 0.5f;
				Main.dust[num101].scale *= 1.3f;
				Main.dust[num101].fadeIn = 1f;
				Main.dust[num101].noGravity = true;
			}

			/*
			Vector2 position1 = new Vector2(projectile.velocity.Y * -1, projectile.velocity.X);
			Vector2 position2 = new Vector2(projectile.velocity.Y, projectile.velocity.X * -1);
			AdjustMagnitude(ref position1, 9);
			AdjustMagnitude(ref position2, 15);

			int dust1 = Dust.NewDust(position1 + projectile.Center, 1, 1, DustID.Vortex, projectile.velocity.Y * -0.4f, projectile.velocity.X * 0.4f, 100, default(Color), 0.6f);
			Main.dust[dust1].noGravity = true;
			int dust2 = Dust.NewDust(position2 + projectile.Center, 1, 1, DustID.Vortex, projectile.velocity.Y * 0.4f, projectile.velocity.X * -0.4f, 100, default(Color), 0.6f);
			Main.dust[dust2].noGravity = true;
			*/
		}



        public override void Kill(int timeLeft)
        {
			Main.PlaySound(SoundID.Item89, projectile.position);
			projectile.position.X += projectile.width / 2;
			projectile.position.Y += projectile.height / 2;
			projectile.width = (int)(128f * projectile.scale);
			projectile.height = (int)(128f * projectile.scale);
			projectile.position.X -= projectile.width / 2;
			projectile.position.Y -= projectile.height / 2;
			for (int num264 = 0; num264 < 8; num264++)
			{
				Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.Vortex, 0f, 0f, 100, default(Color), 1.5f);
			}
			for (int num265 = 0; num265 < 32; num265++)
			{
				int num266 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.Vortex, 0f, 0f, 100, default(Color), 2.5f);
				Main.dust[num266].noGravity = true;
				Dust dust196 = Main.dust[num266];
				Dust dust226 = dust196;
				dust226.velocity *= 3f;
				num266 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.Vortex, 0f, 0f, 100, default(Color), 1.5f);
				dust196 = Main.dust[num266];
				dust226 = dust196;
				dust226.velocity *= 2f;
				Main.dust[num266].noGravity = true;
			}
			for (int num267 = 0; num267 < 2; num267++)
			{
				int num269 = Gore.NewGore(projectile.position + new Vector2((float)(projectile.width * Main.rand.Next(100)) / 100f, (float)(projectile.height * Main.rand.Next(100)) / 100f) - Vector2.One * 10f, default(Vector2), Main.rand.Next(61, 64));
				Gore gore38 = Main.gore[num269];
				Gore gore40 = gore38;
				gore40.velocity *= 0.3f;
				Main.gore[num269].velocity.X += (float)Main.rand.Next(-10, 11) * 0.05f;
				Main.gore[num269].velocity.Y += (float)Main.rand.Next(-10, 11) * 0.05f;
			}
			/*
			if (owner == Main.myPlayer)
			{
				localAI[1] = -1f;
				maxPenetrate = 0;
				Damage();
			}
			*/
			for (int num270 = 0; num270 < 5; num270++)
			{
				int num271 = Utils.SelectRandom<int>(Main.rand, 6, 259, 158);
				int num272 = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Vortex, 2.5f * (float)projectile.direction, -2.5f);
				Main.dust[num272].alpha = 200;
				Dust dust195 = Main.dust[num272];
				Dust dust226 = dust195;
				dust226.velocity *= 2.4f;
				dust195 = Main.dust[num272];
				dust226 = dust195;
				dust226.scale += Main.rand.NextFloat();
			}
		}


        private void AdjustMagnitude(ref Vector2 vector, float speed)
		{
			float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
			if (magnitude != speed)
			{
				vector *= speed / magnitude;
			}
		}


	}
}