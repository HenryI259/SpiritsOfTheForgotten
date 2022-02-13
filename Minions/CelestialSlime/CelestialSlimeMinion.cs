using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using Microsoft.Xna.Framework;
using SpiritsOfTheForgotten.Items;
using SpiritsOfTheForgotten.Minions.CelestialSlime;
using System;
using SpiritsOfTheForgotten.Projectiles.WeaponProj;

namespace SpiritsOfTheForgotten.Minions.CelestialSlime
{
	
	public class CelestialSlimeMinion : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Celestial Slime");
			Main.projFrames[projectile.type] = 6;
			// targeting feature
			ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;

			// Minion stuff
			Main.projPet[projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
			ProjectileID.Sets.Homing[projectile.type] = true;
		}

		public sealed override void SetDefaults()
		{
			projectile.width = 104;
			projectile.height = 104;
			projectile.tileCollide = true;
			projectile.friendly = true;
			projectile.minion = true;
			projectile.minionSlots = 1f;
			projectile.penetrate = -1;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 10;
		}

       
        public override bool? CanCutTiles()
		{
			return false;
		}

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
			return false;
        }

        public override bool MinionContactDamage()
		{
			return true;
		}

		public int timer;
		public bool foundTarget;
		Vector2 targetCenter;
		public int jumpTimer;
		public int jump;
		public int maxFrames;
		public int frameSpeed;
		public override void AI()
		{
			Player player = Main.player[projectile.owner];
			timer++;

			#region Active check
			// This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
			if (player.dead || !player.active)
			{
				player.ClearBuff(ModContent.BuffType<CelestialSlimeBuff>());
			}
			if (player.HasBuff(ModContent.BuffType<CelestialSlimeBuff>()))
			{
				projectile.timeLeft = 2;
			}
			#endregion

			#region General behavior
			Vector2 idlePosition = player.Center;
			projectile.velocity.Y += 0.5f; // Make it affected by gravity
			idlePosition.Y -= 48f; // Go up 48 coordinates (three tiles from the center of the player)

			if (timer >= jumpTimer)
            {
				projectile.velocity.Y -= jump;
				Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y + projectile.height / 2f, 0f, 0f, ModContent.ProjectileType<CelestialPlatform>(), 0, 0, projectile.owner);
				timer = 0;
            }

			projectile.spriteDirection = projectile.direction;

			// If your minion doesn't aimlessly move around when it's idle, you need to "put" it into the line of other summoned minions
			// The index is projectile.minionPos
			float minionPositionOffsetX = (10 + projectile.minionPos * 40) * -player.direction;
			idlePosition.X += minionPositionOffsetX; // Go behind the player

			// All of this code below this line is adapted from Spazmamini code (ID 388, aiStyle 66)

			// Teleport to player if distance is too big
			Vector2 vectorToIdlePosition = idlePosition - projectile.Center;
			float distanceToIdlePosition = vectorToIdlePosition.Length();
			if (Main.myPlayer == player.whoAmI && distanceToIdlePosition > 2000f)
			{
				// Whenever you deal with non-regular events that change the behavior or position drastically, make sure to only run the code on the owner of the projectile,
				// and then set netUpdate to true
				projectile.position = idlePosition;
				for (int i = 0; i < 20; i++)
                {
					int dusttype = Main.rand.Next(new int[] { DustID.SolarFlare, DustID.Vortex, DustID.PinkFlame, DustID.BlueCrystalShard });
					Dust.NewDustDirect(projectile.Center, 1, 1, dusttype, Main.rand.NextFloat(-5, 6) * 0.5f, Main.rand.NextFloat(-5, 6) * 0.5f, 50, default(Color), 1f);
				}
				projectile.velocity *= 0.1f;
				projectile.netUpdate = true;
			}

			// If your minion is flying, you want to do this independently of any conditions
			float overlapVelocity = 0.04f;
			for (int i = 0; i < Main.maxProjectiles; i++)
			{
				// Fix overlap with other minions
				Projectile other = Main.projectile[i];
				if (i != projectile.whoAmI && other.active && other.owner == projectile.owner && Math.Abs(projectile.position.X - other.position.X) + Math.Abs(projectile.position.Y - other.position.Y) < projectile.width)
				{
					if (projectile.position.X < other.position.X) projectile.velocity.X -= overlapVelocity;
					else projectile.velocity.X += overlapVelocity;

					if (projectile.position.Y < other.position.Y) projectile.velocity.Y -= overlapVelocity;
					else projectile.velocity.Y += overlapVelocity;
				}
			}
			#endregion

			#region Find target
			// Starting search distance
			float distanceFromTarget = 700f;
			Vector2 targetCenter = projectile.position;
			bool foundTarget = false;

			// This code is required if your minion weapon has the targeting feature
			if (player.HasMinionAttackTargetNPC)
			{
				NPC npc = Main.npc[player.MinionAttackTargetNPC];
				float between = Vector2.Distance(npc.Center, projectile.Center);
				// Reasonable distance away so it doesn't target across multiple screens
				if (between < 2000f)
				{
					distanceFromTarget = between;
					targetCenter = npc.Center;
					foundTarget = true;
				}
			}
			if (!foundTarget)
			{
				// This code is required either way, used for finding a target
				for (int i = 0; i < Main.maxNPCs; i++)
				{
					NPC npc = Main.npc[i];
					if (npc.CanBeChasedBy())
					{
						float between = Vector2.Distance(npc.Center, projectile.Center);
						bool closest = Vector2.Distance(projectile.Center, targetCenter) > between;
						bool inRange = between < distanceFromTarget;
						bool lineOfSight = Collision.CanHitLine(projectile.position, projectile.width, projectile.height, npc.position, npc.width, npc.height);
						// Additional check for this specific minion behavior, otherwise it will stop attacking once it dashed through an enemy while flying though tiles afterwards
						// The number depends on various parameters seen in the movement code below. Test different ones out until it works alright
						bool closeThroughWall = between < 100f;
						if (((closest && inRange) || !foundTarget) && (lineOfSight || closeThroughWall))
						{
							distanceFromTarget = between;
							targetCenter = npc.Center;
							foundTarget = true;
						}
					}
				}
			}



			// friendly needs to be set to true so the minion can deal contact damage
			// friendly needs to be set to false so it doesn't damage things like target dummies while idling
			// Both things depend on if it has a target or not, so it's just one assignment here
			// You don't need this assignment if your minion is shooting things instead of dealing contact damage
			projectile.friendly = foundTarget;
			#endregion

			#region Movement

			// movement for attacking
			float speed = 20f;
			float inertia = 15f;

			if (foundTarget)
			{
				if (distanceFromTarget > 40f)
				{
					
					Vector2 direction = targetCenter - projectile.Center;
					direction.Normalize();
					direction *= speed;
					projectile.velocity = (projectile.velocity * (inertia - 1) + direction) / inertia;
				}
				jump = 10;
				jumpTimer = 20;
				maxFrames = 6;
				frameSpeed = 5;
			}
			else
			{
				
				if (distanceToIdlePosition > 600f)
				{
					// Speed when far away
					speed = 16f;
					inertia = 45f;
				}
				else
				{
					// Speed when close
					speed = 6f;
					inertia = 70f;
				}
				if (distanceToIdlePosition > 20f)
				{
					
					vectorToIdlePosition.Normalize();
					vectorToIdlePosition *= speed;
					projectile.velocity = (projectile.velocity * (inertia - 1) + vectorToIdlePosition) / inertia;
				}
				else if (projectile.velocity == Vector2.Zero)
				{
					// If its standing still make it move
					projectile.velocity.X = -0.15f;
					projectile.velocity.Y = -0.05f;
				}
				jumpTimer = 40;
				jump = 20;
				maxFrames = 2;
				frameSpeed = 10;
			}
			#endregion

			#region Animation and visuals
			

			projectile.frameCounter++;
			if (projectile.frameCounter >= frameSpeed)
			{
				projectile.frameCounter = 0;
				projectile.frame++;
				if (projectile.frame >= maxFrames)
				{
					projectile.frame = 0;
				}
			}

			Lighting.AddLight(projectile.Center, Color.White.ToVector3() * 0.78f);
			#endregion
		}

		// Makes it fall through platforms
		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			if (foundTarget)
			{
				Vector2 toTarget = targetCenter - projectile.Center;
				if (toTarget.Y > 0 && Math.Abs(toTarget.X) < 300)
				{
					fallThrough = true;
				}
				else
				{
					fallThrough = false;
				}
			}
			else
			{
				fallThrough = false;
			}
			return base.TileCollideStyle(ref width, ref height, ref fallThrough);
		}
	}
}