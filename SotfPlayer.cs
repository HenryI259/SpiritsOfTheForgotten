using Terraria;
using SpiritsOfTheForgotten;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using static Terraria.ModLoader.ModContent;
using SpiritsOfTheForgotten.Projectiles;
using SpiritsOfTheForgotten.Projectiles.WeaponProj;
using SpiritsOfTheForgotten.Items.Weapons;

namespace SpiritsOfTheForgotten
{
	public class SotfPlayer : ModPlayer
	{
		public bool SolarStrength;
		public bool CelestialFlames;
		public bool Contract;
		public bool usinglauncher;
		public bool CelestialSet;
		public float AmmoConsumeReduction;
		public int CelestialClass;
		public int SolarTimer;
		public override void ResetEffects()
		{
			CelestialFlames = false;
			Contract = false;
			SolarStrength = false;
			usinglauncher = false;
			CelestialSet = false;
			AmmoConsumeReduction = 1f;
		}

        public override void PreUpdate()
        {
			if (CelestialSet)
			{
				if (player.HeldItem.melee) CelestialClass = 1;
				else if (player.HeldItem.ranged) CelestialClass = 2;
				else if (player.HeldItem.magic) CelestialClass = 3;
				else if (player.HeldItem.summon) CelestialClass = 4;
				else
					CelestialClass = 0;
			}
		}

        public override void UpdateBadLifeRegen()
		{
			if (CelestialFlames)
			{
				
				
				player.lifeRegen -= 10;
				player.statDefense -= 10;
			}

		}

		public override bool ConsumeAmmo(Item weapon, Item ammo)
		{
			return Main.rand.Next(1, 101) < (AmmoConsumeReduction - 1f) * 100;
		}

		public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
			if (player.GetModPlayer<SotfPlayer>().SolarStrength == true && item.melee)
			{
				Projectile.NewProjectile(target.Center.X, target.Center.Y, 0, 0, ProjectileID.SolarWhipSwordExplosion, (int)(item.damage * .5f), item.knockBack * 2f, player.whoAmI);


			}
		}

        public override bool Shoot(Item item, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			if (Contract && Main.rand.Next(0, 11) == 0)
			{
				Vector2 velo;
				velo.X = speedX;
				velo.Y = speedY;

				AdjustMagnitude(ref velo, 12);
				Projectile.NewProjectile(position.X, position.Y, velo.X, velo.Y, ModContent.ProjectileType<SnatcherPotion>(), damage, knockBack, player.whoAmI);
				Main.PlaySound(SoundID.Item106);
			}
			return base.Shoot(item, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack); ;
        }
        public override void DrawEffects(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
		{
			if (CelestialFlames)
			{
				if (Main.rand.Next(4) < 3)
				{
					int dustAmount = 1;
					for (int i = 0; i < dustAmount; i++)
					{
						int s = Main.rand.Next(1, 3);
						int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, DustID.Vortex, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, default(Color), s);
						Main.dust[dust].noGravity = true;
						Main.playerDrawDust.Add(dust);

						if (Main.rand.NextBool(4))
						{
							Main.dust[dust].noGravity = false;
							Main.dust[dust].scale *= 0.5f;
						}
					}

					
				}
				Vector3 light = Color.Cyan.ToVector3();
				Lighting.AddLight(player.position, light.X, light.Y, light.Z);
			}


		}

		public bool channeled;
		public int channel;
		
		public override void PostUpdate()
		{
			Vector2 velocity1 = new Vector2(Main.MouseWorld.X - player.Center.X, Main.MouseWorld.Y - player.Center.Y);
			AdjustMagnitude(ref velocity1, 20);

			if (player.HeldItem.type == ModContent.ItemType<CelestialRocketLauncher>())
			{
				if (player.channel)
				{
					channel++;
					if (channel == 60)
					{
						channeled = true;
						Main.PlaySound(SoundID.Item68, player.Center);
						short dust = DustID.Vortex;
						DustRing(ref dust);
					}

				}
				else if (!player.channel)
				{
					if (channeled)
					{ 
						Projectile.NewProjectile(player.Center.X, player.Center.Y, velocity1.X, velocity1.Y, ModContent.ProjectileType<LunarRocket>(), 2000, 20, player.whoAmI);
						Main.PlaySound(SoundID.Item11, player.Center);
						channeled = false;
					}
					channel = 0;
					
				}
				
			}

			SolarTimer--;
			if (SolarTimer > 0)
            {
				
				for (int i = 0; i < 120; i++)
                {
					Vector2 position = Vector2.UnitY.RotatedBy(MathHelper.ToRadians(i * 3)) * 30;
					Vector2 velo = position.RotatedBy(MathHelper.ToRadians(90));
					var a = Dust.NewDustDirect(player.Center + position, 1, 1, DustID.SolarFlare, velo.X * 0.35f, velo.Y * 0.35f, 50, default(Color), 0.75f);
					a.noGravity = true;
                }
				for (int i = 0; i < Main.maxProjectiles; i++)
                {
					Projectile projectile = Main.projectile[i];
					if (projectile.Hitbox.Intersects(player.Hitbox) && projectile.hostile == true)
					{
						NPC closest = Helper.NPCs.FindNearestNPCDirect(player.Center, 9999);
						Vector2 velocity;
						if (closest == null)
						{
							velocity = -projectile.velocity;
						}
						else
						{
							velocity = closest.Center - projectile.Center;
						}
						if (velocity != Vector2.Zero) velocity.Normalize();
						velocity *= projectile.velocity.Length();
						projectile.velocity = velocity;
						projectile.friendly = true;
						projectile.hostile = false;
						projectile.damage *= 5;
                    }
                }
            }
		}

		private void DustRing(ref short dust)
        {
			Vector2 velo = new Vector2(10, 0);
			for (int i = 0; i < 360; i++)
            {
				Dust.NewDust(player.Center, 1, 1, dust, velo.X, velo.Y, 100, default(Color), 0.5f);
				velo = velo.RotatedBy(MathHelper.ToRadians(1));
            }
        }


		private void AdjustMagnitude(ref Vector2 vector, float speed)
		{
			float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
			if (magnitude >= speed)
			{
				vector *= speed / magnitude;
			}
		}


	}
}