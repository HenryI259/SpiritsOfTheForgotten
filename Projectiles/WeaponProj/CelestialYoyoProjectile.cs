using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using SpiritsOfTheForgotten.Buffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;



namespace SpiritsOfTheForgotten.Projectiles.WeaponProj
{
	public class CelestialYoyoProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{		
			ProjectileID.Sets.YoyosLifeTimeMultiplier[projectile.type] = -1f;
			ProjectileID.Sets.YoyosMaximumRange[projectile.type] = 450f;
			ProjectileID.Sets.YoyosTopSpeed[projectile.type] = 22f;
		}


       
        public override void SetDefaults()
		{
			projectile.extraUpdates = 0;
			projectile.width = 26;
			projectile.height = 26;
			projectile.aiStyle = 99;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.melee = true;
			projectile.scale = 1f;
		}
		// notes for aiStyle 99: 
		// localAI[0] is used for timing up to YoyosLifeTimeMultiplier
		// localAI[1] can be used freely by specific types
		// ai[0] and ai[1] usually point towards the x and y world coordinate hover point
		// ai[0] is -1f once YoyosLifeTimeMultiplier is reached, when the player is stoned/frozen, when the yoyo is too far away, or the player is no longer clicking the shoot button.
		// ai[0] being negative makes the yoyo move back towards the player
		// Any AI method can be used for dust, spawning projectiles, etc specific to your yoyo.

		public int timer;
		
        public override void PostAI()
		{
			if (Main.rand.NextBool())
			{
				int dusttype = Main.rand.Next(new int[] { DustID.SolarFlare, DustID.Vortex, DustID.PinkFlame, DustID.BlueCrystalShard });
				Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, dusttype);
				dust.noGravity = true;
				dust.scale = 1.6f;
			}

			float distanceFromTarget = 600f;
			Vector2 targetCenter = projectile.position;
			bool foundTarget = false;

			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC npc = Main.npc[i];
				if (npc.CanBeChasedBy())
				{
					float between = Vector2.Distance(npc.Center, projectile.Center);
					bool closest = Vector2.Distance(projectile.Center, targetCenter) > between;
					bool inRange = between < distanceFromTarget;
					if ((closest && inRange)|| !foundTarget)
					{
						distanceFromTarget = between;
						targetCenter = npc.Center;
						foundTarget = true;
					}
				}
			}
			distanceFromTarget /= 10;
			if (distanceFromTarget < 15 )
            {
				distanceFromTarget = 15;
            }

			timer++;
			if (timer >= (int)distanceFromTarget)
            {
				for (int i = 0; i < 8; i++)
                {
					Vector2 direction = new Vector2(0, -1.5f);
					direction = direction.RotatedBy(MathHelper.ToRadians(45f * i));
					Projectile.NewProjectile(projectile.Center, direction, ModContent.ProjectileType<CelestialYoyoStar>(), projectile.damage, projectile.knockBack, projectile.owner);
                }
				timer = 0;
            }

		}
	}
}