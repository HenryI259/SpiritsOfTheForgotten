using System.Threading;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using SpiritsOfTheForgotten.Items;
using MonoMod.Cil;
using Steamworks;
using SpiritsOfTheForgotten.Npcs.Celestialminiboss;
using Microsoft.Xna.Framework.Graphics;
using SpiritsOfTheForgotten.Projectiles.WeaponProj;
using SpiritsOfTheForgotten.Items.Vanity;

namespace SpiritsOfTheForgotten.Npcs.Celestialminiboss
{
	[AutoloadBossHead]
	public class CelestialMiniboss : ModNPC
	{
		public override void BossHeadSlot(ref int index) => index = (phase2 ? ModContent.GetModBossHeadSlot("SpiritsOfTheForgotten/Npcs/Celestialminiboss/CelestialSoul_Head_Boss") : ModContent.GetModBossHeadSlot("SpiritsOfTheForgotten/Npcs/Celestialminiboss/CelestialMiniboss_Head_Boss"));

		public override bool Autoload(ref string name)
		{
			mod.AddBossHeadTexture("SpiritsOfTheForgotten/Npcs/Celestialminiboss/CelestialSoul_Head_Boss");
			return base.Autoload(ref name);
		}


		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Celestial Construct");
			Main.npcFrameCount[npc.type] = 16;
		}

		public override void SetDefaults()
		{
			npc.aiStyle = -1;
			npc.lifeMax = 75000;
			npc.damage = 150;
			npc.defense = 40;
			npc.knockBackResist = 0f;
			npc.width = 128;
			npc.height = 128;
			npc.value = Item.buyPrice(0, 15, 0, 0);
			npc.npcSlots = 10f;
			npc.lavaImmune = true;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.HitSound = SoundID.NPCHit34;
			npc.DeathSound = SoundID.NPCDeath56;
			npc.buffImmune[24] = true;
			npc.boss = true;
			music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Celestial");
			bossBag = ItemType<CelestialConstructBag>();
		}

        #region varibles
        public int attack = 1;
		public int classAttack = 1;
		public int timer;
		public Player playerTarget;
		public int meteorTimer = 120;
		public Vector2 newMove;
		public float distanceTo;
		public int owner;
		public int teleportTimer;
		public Vector2 teleportPosition1;
		public int dashTimer;
		public int starTimer;
		public float speed;
		public int deadTimer;
		public bool dead;
		public float spinTimer;
		public float spinning;
		public bool isSpinning;
		public float spin;
		public Vector2 spinPos;
		public Color color;
		public float spinTimer1;
		public float spinning1;
		public bool isSpinning1;
		public float spin1;
		public Vector2 spinPos1;
		public float starTimer1;
		public float shooting;
		public float starRotation;
		public bool turning;
		public int timer2;
		public int AttackTimer;
		public bool phase2;
		public int Phase2Timer;
		public int BeginTimer = 0;
		public bool phase2true;
		public int Npc1;
		public int Npc2;
		public int Npc3;
		public int Npc4;
		public bool pause;
		public bool dead1;
		public bool dead2;
		public bool dead3;
		public bool dead4;
		#endregion

		public override void AI()
        {
			npc.TargetClosest();
			Player playerTarget = Main.player[npc.target];
			Vector2 newMove = playerTarget.Center - npc.Center;
			float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
			if (playerTarget.dead || distanceTo > 10000)
			{
				npc.velocity.X *= 0.9f;
				npc.velocity.Y -= 0.2f;
				deadTimer += 1;
				dead = true;
				if (deadTimer > 150)
					npc.active = false;


			}
			else
			{
				deadTimer = 0;
				dead = false;
			}

			if ((!Main.expertMode && npc.life < 20000) || (Main.expertMode && npc.life < 35000))
            {
				phase2 = true;
            }
			else
            {
				phase2 = false;
            }

			

			if (!dead)
			{
				//timer that switches attacks
				if (!pause)
				{
					timer++;
					if (timer == 600)
					{
						if (attack == 1)
						{
							bool randomAttack = Main.rand.NextBool();
							int randomAttack1 = Main.rand.Next(1, 4);
							if (!phase2true)
							{
								if (randomAttack)
								{

									attack = 2;

								}
								else
								{

									attack = 3;

								}
							}
							else
                            {
								if (randomAttack1 == 1)
                                {
									attack = 2;
                                }
								else if (randomAttack1 == 2)
                                {
									attack = 3;
                                }
								else if (randomAttack1 == 3)
                                {
									attack = 4;
                                }
                            }
						}
						else
						{
							attack = 1;
						}

						npc.TargetClosest();
						if (Main.player[npc.target].HeldItem.melee)
						{
							classAttack = 1;
						}
						else if (Main.player[npc.target].HeldItem.ranged)
						{
							classAttack = 2;
						}
						else if (Main.player[npc.target].HeldItem.magic)
						{
							classAttack = 3;
						}
						else if (Main.player[npc.target].HeldItem.summon)
						{
							classAttack = 4;
						}
						else
						{
							classAttack = Main.rand.Next(1, 5);
						}
						//reset values
						isSpinning = false;
						teleportTimer = 0;
						meteorTimer = 118;
						dashTimer = 0;
						starTimer = 0;
						spinTimer = 0;
						isSpinning1 = false;
						starTimer1 = 0;
						npc.alpha = 0;
						npc.damage = 150;
						npc.dontTakeDamage = false;
						AttackTimer = 0;

						timer = 0;
					}
				}

				//normal attacks
				if (!phase2true && !pause)
				{


					if (attack == 1)
					{

						teleportTimer++;
						if (teleportTimer == 1 || (float)Math.Sqrt(((playerTarget.Center.X - teleportPosition1.X) * (playerTarget.Center.X - teleportPosition1.X)) + ((playerTarget.Center.Y - teleportPosition1.Y) * (playerTarget.Center.Y - teleportPosition1.Y))) > 1500f)
						{
							teleportPosition1 = playerTarget.Center + Vector2.One.RotatedByRandom(360) * 250;
						}
						if (teleportTimer > 120)
						{

							npc.position = teleportPosition1;
							Main.PlaySound(SoundID.Item101, npc.Center);
							for (int i = 0; i < 500; i++)
							{
								int dusttype = Main.rand.Next(new int[] { DustID.SolarFlare, DustID.Vortex, DustID.PinkFlame, DustID.BlueCrystalShard });
								var dust = Dust.NewDustDirect(teleportPosition1, npc.width, npc.height, dusttype, Main.rand.NextFloat(-6, 5) * 2f, Main.rand.NextFloat(-6, 5) * 2f, 50, default(Color), 1.25f);
								dust.noGravity = true;
							}
							teleportTimer = 0;
						}
						for (int i = 0; i < 6; i++)
						{
							int dusttype = Main.rand.Next(new int[] { DustID.SolarFlare, DustID.Vortex, DustID.PinkFlame, DustID.BlueCrystalShard });
							var dust = Dust.NewDustDirect(teleportPosition1, npc.width, npc.height, dusttype, 0, 0, 50, default(Color), 1.25f);
							dust.noGravity = true;
						}

						if (timer < 320)
						{
							speed = timer / 20;
						}
						else
						{
							speed = 16;
						}



						if (distanceTo > 300)
						{

							AdjustMagnitude(ref newMove, speed);
							npc.velocity = (6 * npc.velocity + newMove) / 7f;
							AdjustMagnitude(ref npc.velocity, speed);
						}
						else
						{
							EaseToSpeed(ref npc.velocity, speed + 2f);
						}
					}


					else if (attack == 2)
					{

						if (classAttack == 1)
						{

							npc.velocity *= 0.95f;
							meteorTimer--;
							if (meteorTimer == 119)
							{

								Vector2 teleportPosition = playerTarget.Center + Vector2.One.RotatedByRandom(360) * 250;
								npc.position = teleportPosition;
								Main.PlaySound(SoundID.Item101, npc.Center);
								for (int i = 0; i < 500; i++)
								{
									var dust = Dust.NewDustDirect(teleportPosition, npc.width, npc.height, DustID.SolarFlare, Main.rand.NextFloat(-6, 5) * 2f, Main.rand.NextFloat(-6, 5) * 2f, 50, default(Color), 1.25f);
									dust.noGravity = true;
								}

							}
							if (meteorTimer <= 90 && meteorTimer % 10 == 0)
							{
								int amount = 1;
								for (int i = 0; i < amount; i++)
								{

									if (Main.netMode != NetmodeID.MultiplayerClient)
									{
										owner = Main.myPlayer;
									}

									var a = Projectile.NewProjectileDirect(playerTarget.Center + new Vector2(Main.rand.NextFloat(-600, 600), Main.rand.NextFloat(-250, 50)), Vector2.Zero, ModContent.ProjectileType<Target>(), 0, 0, owner);
									a.timeLeft = 60;
									a.netUpdate = true;

								}
							}

							if (meteorTimer == 0 || distanceTo > 1200)
							{
								meteorTimer = 120;
							}

						}
						else if (classAttack == 2)
						{

							dashTimer++;
							if (dashTimer == 1)
							{
								int randomPos = Main.rand.Next(0, 4);
								if (randomPos == 1)
								{
									Vector2 teleportPosition2 = playerTarget.Center + new Vector2(360, 360);
									for (int i = 0; i < 500; i++)
									{
										var dust = Dust.NewDustDirect(teleportPosition2, npc.width, npc.height, DustID.Vortex, Main.rand.NextFloat(-6, 5) * 2f, Main.rand.NextFloat(-6, 5) * 2f, 50, default(Color), 1.25f);
										dust.noGravity = true;
									}
									npc.position = teleportPosition2;
									npc.velocity = new Vector2(0, -20);
								}

								else if (randomPos == 2)
								{
									Vector2 teleportPosition2 = playerTarget.Center + new Vector2(-360, -360);
									for (int i = 0; i < 500; i++)
									{
										var dust = Dust.NewDustDirect(teleportPosition2, npc.width, npc.height, DustID.Vortex, Main.rand.NextFloat(-6, 5) * 2f, Main.rand.NextFloat(-6, 5) * 2f, 50, default(Color), 1.25f);
										dust.noGravity = true;
									}
									npc.position = teleportPosition2;
									npc.velocity = new Vector2(0, 20);
								}
								else if (randomPos == 3)
								{
									Vector2 teleportPosition2 = playerTarget.Center + new Vector2(-360, 360);
									for (int i = 0; i < 500; i++)
									{
										var dust = Dust.NewDustDirect(teleportPosition2, npc.width, npc.height, DustID.Vortex, Main.rand.NextFloat(-6, 5) * 2f, Main.rand.NextFloat(-6, 5) * 2f, 50, default(Color), 1.25f);
										dust.noGravity = true;
									}
									npc.position = teleportPosition2;
									npc.velocity = new Vector2(20, 0);
								}

								else
								{
									Vector2 teleportPosition2 = playerTarget.Center + new Vector2(360, -360);
									for (int i = 0; i < 500; i++)
									{
										var dust = Dust.NewDustDirect(teleportPosition2, npc.width, npc.height, DustID.Vortex, Main.rand.NextFloat(-6, 5) * 2f, Main.rand.NextFloat(-6, 5) * 2f, 50, default(Color), 1.25f);
										dust.noGravity = true;
									}
									npc.position = teleportPosition2;
									npc.velocity = new Vector2(-20, 0);
								}

								Main.PlaySound(SoundID.Item101, npc.Center);


							}

							if (dashTimer <= 36 && dashTimer % 4 == 0)
							{
								if (Main.netMode != NetmodeID.MultiplayerClient)
								{
									owner = Main.myPlayer;
								}

								var a = Projectile.NewProjectileDirect(npc.Center, Vector2.Zero, ModContent.ProjectileType<LightningProjectile>(), 25, 5, owner);
								a.netUpdate = true;
								a.ai[0] = 60;
							}

							if (dashTimer == 36)
							{
								for (int i = 0; i < 500; i++)
								{
									var dust = Dust.NewDustDirect(npc.position, npc.width, npc.height, DustID.Vortex, Main.rand.NextFloat(-6, 5) * 2f, Main.rand.NextFloat(-6, 5) * 2f, 50, default(Color), 1.25f);
									dust.noGravity = true;
								}
							}

							if (dashTimer > 36)
							{
								npc.alpha = 255;
								npc.damage = 0;
								npc.velocity *= 0;
								npc.dontTakeDamage = true;
							}
							else
							{
								npc.alpha = 0;
								npc.damage = 100;
								npc.dontTakeDamage = false;
							}

							if (dashTimer == 110)
							{
								dashTimer = 0;
							}
						}
						else if (classAttack == 3)
						{
							starTimer++;
							npc.velocity *= 0.95f;
							if (starTimer > 90)
							{
								Vector2 teleportPosition2 = playerTarget.Center + Vector2.One.RotatedByRandom(360) * 300;
								npc.position = teleportPosition2;
								Main.PlaySound(SoundID.Item101, npc.Center);
								for (int i = 0; i < 500; i++)
								{
									var dust = Dust.NewDustDirect(teleportPosition2, npc.width, npc.height, DustID.PinkFlame, Main.rand.NextFloat(-6, 5) * 4f, Main.rand.NextFloat(-6, 5) * 4f, 50, default(Color), 2f);
									dust.noGravity = true;
								}

								if (Main.netMode != NetmodeID.MultiplayerClient)
								{
									owner = Main.myPlayer;
								}
								for (int i = 0; i < 8; i++)
								{
									Vector2 direction = new Vector2(0, -2f);
									direction = direction.RotatedBy(MathHelper.ToRadians(45f * i));
									var a = Projectile.NewProjectileDirect(npc.Center, direction, ModContent.ProjectileType<LargeCelestialStar>(), 35, 3, owner);
									a.netUpdate = true;
									a.timeLeft = 300;
									a.hostile = true;
									a.friendly = false;
									a.tileCollide = false;

								}
								starTimer = 0;
							}


						}
						else if (classAttack == 4)
						{
							spinTimer += 1;
							if (spinTimer == 1)
							{
								spinning = 6f;
								spin = Main.rand.NextFloat(0, 360);
							}
							if (spinTimer < 150)
							{
								//playerTarget.Center - Vector2.UnitY.RotatedBy(angle - MathHelper.TwoPi * i / totalIllusions);
								isSpinning = true;
								spinPos = playerTarget.Center;

								npc.ai[0] += 1;
								if (npc.ai[0] >= 1)
								{
									spin += spinning;
									spinning *= 0.985f;
									npc.ai[0] = 0;
								}


								npc.Center = playerTarget.Center - Vector2.UnitY.RotatedBy(MathHelper.ToRadians(spin)) * 400;
							}
							if (spinTimer == 150)
							{
								npc.velocity = newMove;
								AdjustMagnitude(ref npc.velocity, 25);
								Vector2 pos = playerTarget.Center;
								spinPos = pos;
							}
							if (spinTimer > 200)
							{
								spinTimer = 0;
							}


						}

					}


					else if (attack == 3)
					{
						if (classAttack == 1)
						{
							AttackTimer++;
							if (AttackTimer < 90)
								npc.velocity *= 0.95f;

							if (AttackTimer == 1)
							{
								Vector2 teleportPosition2 = playerTarget.Center - new Vector2(0, 300);
								npc.velocity *= 0;
								npc.Center = teleportPosition2;
								Main.PlaySound(SoundID.Item101, npc.Center);
								for (int i = 0; i < 500; i++)
								{
									var dust = Dust.NewDustDirect(teleportPosition2, npc.width, npc.height, DustID.SolarFlare, Main.rand.NextFloat(-6, 5) * 2f, Main.rand.NextFloat(-6, 5) * 2f, 50, default(Color), 1.25f);
									dust.noGravity = true;
								}

								for (int i = 0; i < 11; i++)
								{
									Vector2 position = new Vector2(400, -750 + 150 * i);
									if (Main.netMode != NetmodeID.MultiplayerClient)
									{
										owner = Main.myPlayer;
									}
									var a = Projectile.NewProjectileDirect(playerTarget.Center + position, new Vector2(-10, 0), ModContent.ProjectileType<SolarSwordBeamHostile>(), 20, 4, owner);
									a.ai[0] = 50;


								}

								for (int i = 0; i < 11; i++)
								{
									Vector2 position = new Vector2(-400, -750 + 150 * i);
									if (Main.netMode != NetmodeID.MultiplayerClient)
									{
										owner = Main.myPlayer;
									}
									var a = Projectile.NewProjectileDirect(playerTarget.Center + position, new Vector2(10, 0), ModContent.ProjectileType<SolarSwordBeamHostile>(), 20, 4, owner);
									a.ai[0] = 50;
								}
							}
							if (AttackTimer == 110)
							{
								npc.velocity = newMove * 5f;
								AdjustMagnitude(ref npc.velocity, 25);
							}
							if (AttackTimer == 150)
							{
								AttackTimer = 0;
							}
						}
						else if (classAttack == 2)
						{
							starTimer++;
							npc.velocity *= 0.9f;
							if (starTimer > 70)
							{
								Vector2 teleportPosition2 = playerTarget.Center + Vector2.One.RotatedByRandom(360) * 500;
								npc.position = teleportPosition2;
								Main.PlaySound(SoundID.Item101, npc.Center);
								for (int i = 0; i < 500; i++)
								{
									var dust = Dust.NewDustDirect(teleportPosition2, npc.width, npc.height, DustID.Vortex, Main.rand.NextFloat(-6, 5) * 2f, Main.rand.NextFloat(-6, 5) * 2f, 50, default(Color), 1.25f);
									dust.noGravity = true;
								}

								if (Main.netMode != NetmodeID.MultiplayerClient)
								{
									owner = Main.myPlayer;
								}
								for (int i = 0; i < 13; i++)
								{

									Vector2 position = npc.Center - Vector2.UnitY.RotatedBy(MathHelper.ToRadians(30f * i)) * 200;
									var a = Projectile.NewProjectileDirect(position, Vector2.Zero, ModContent.ProjectileType<LightningProjectile>(), 35, 3, owner);
									a.ai[0] = 60;
									a.netUpdate = true;
									a.timeLeft = 300;
									a.hostile = true;
									a.friendly = false;
									a.tileCollide = false;

								}
								starTimer = 0;

							}

						}
						else if (classAttack == 3)
						{
							npc.velocity *= 0.95f;
							starTimer1 += 1;
							if (starTimer1 == 1)
							{
								shooting = 30f;
								starRotation = 0f;// Main.rand.NextFloat(0, 360);
								npc.ai[3] = 0;
								turning = true;
							}
							if (starTimer1 >= 10 && starTimer1 <= 11)
							{

								for (int i = 0; i < 8; i++)
								{
									var a = Projectile.NewProjectileDirect(npc.Center, Vector2.UnitY.RotatedBy(MathHelper.ToRadians(starRotation + i * 45)) * 2, ModContent.ProjectileType<LargeCelestialStar>(), 20, 3, owner);
									a.ai[0] = 3;
									a.timeLeft = 300;
									a.netUpdate = true;
									a.hostile = true;
									a.friendly = false;
								}
								for (int i = 0; i < 8; i++)
								{
									var a = Projectile.NewProjectileDirect(npc.Center, Vector2.UnitY.RotatedBy(MathHelper.ToRadians(starRotation + i * 45)) * 2, ModContent.ProjectileType<LargeCelestialStar>(), 20, 3, owner);
									a.ai[0] = 4;
									a.timeLeft = 300;
									a.netUpdate = true;
									a.hostile = true;
									a.friendly = false;
								}
								timer2 = 0;


							}
							if (starTimer1 < 200 && starTimer1 > 10)
							{


								npc.ai[3] += 1;
								if (npc.ai[3] >= shooting)
								{
									//starRotation += 20;
									if (shooting > 10)
										shooting -= 4;
									if (Main.netMode != NetmodeID.MultiplayerClient)
									{
										owner = Main.myPlayer;
									}
									if (turning)
									{
										for (int i = 0; i < 8; i++)
										{
											var a = Projectile.NewProjectileDirect(npc.Center, Vector2.UnitY.RotatedBy(MathHelper.ToRadians(starRotation + i * 45)) * 2, ModContent.ProjectileType<LargeCelestialStar>(), 20, 3, owner);
											a.ai[0] = 1;
											a.timeLeft = 300;
											a.netUpdate = true;
											a.hostile = true;
											a.friendly = false;
										}
										turning = false;
									}
									else
									{
										for (int i = 0; i < 8; i++)
										{
											var a = Projectile.NewProjectileDirect(npc.Center, Vector2.UnitY.RotatedBy(MathHelper.ToRadians(starRotation + i * 45)) * 2, ModContent.ProjectileType<LargeCelestialStar>(), 20, 3, owner);
											a.ai[0] = 2;
											a.timeLeft = 300;
											a.netUpdate = true;
											a.hostile = true;
											a.friendly = false;
										}
										turning = true;
									}
									npc.ai[3] = 0;
								}

							}
							if (starTimer1 > 200)
							{
								Vector2 teleportPosition4 = playerTarget.Center + Vector2.One.RotatedByRandom(360) * 250;
								npc.position = teleportPosition4;
								Main.PlaySound(SoundID.Item101, npc.Center);
								for (int i = 0; i < 500; i++)
								{
									var dust = Dust.NewDustDirect(teleportPosition1, npc.width, npc.height, DustID.PinkFlame, Main.rand.NextFloat(-6, 5) * 4f, Main.rand.NextFloat(-6, 5) * 4f, 50, default(Color), 2f);
									dust.noGravity = true;

								}
								starTimer1 = 0;
							}

						}
						else if (classAttack == 4)
						{
							npc.velocity *= 0.95f;
							spinTimer1 += 1;
							if (spinTimer1 == 1)
							{
								spinning1 = 6f;
								spin1 = Main.rand.NextFloat(0, 360);
							}
							if (spinTimer1 < 150)
							{
								//playerTarget.Center - Vector2.UnitY.RotatedBy(angle - MathHelper.TwoPi * i / totalIllusions);
								isSpinning1 = true;

								npc.ai[1] += 1;
								if (npc.ai[1] >= 1)
								{
									spin1 += spinning1;
									spinning1 *= 0.985f;
									npc.ai[1] = 0;
								}

								npc.ai[2] += 1;

								if (npc.ai[2] == 4)
								{

									for (int i = 0; i < 360; i++)
									{
										Vector2 position = new Vector2(1250, 0).RotatedBy(i);
										int dusttype1 = Main.rand.Next(new int[] { DustID.BlueCrystalShard, DustID.SolarFlare, DustID.PinkFlame, DustID.Vortex });
										if (dusttype1 == DustID.PinkFlame)
										{
											int dust = Dust.NewDust(position + npc.Center, 1, 1, dusttype1, 0, 0, 50, default(Color), 2f);
											Dust celestialDust = Main.dust[dust];
											celestialDust.noGravity = true;
										}
										else
										{
											int dust = Dust.NewDust(position + npc.Center, 1, 1, dusttype1, 0, 0, 50, default(Color), 0.75f);
											Dust celestialDust = Main.dust[dust];
											celestialDust.noGravity = true;
										}



										npc.ai[2] = 0;

									}
									npc.ai[2] = 0;
								}

								if (distanceTo > 1250)
								{
									Vector2 teleportPosition3 = playerTarget.Center + Vector2.One.RotatedByRandom(360) * 250;
									npc.position = teleportPosition3;
									Main.PlaySound(SoundID.Item101, npc.Center);
									for (int i = 0; i < 500; i++)
									{
										var dust = Dust.NewDustDirect(teleportPosition1, npc.width, npc.height, DustID.BlueCrystalShard, Main.rand.NextFloat(-6, 5) * 2f, Main.rand.NextFloat(-6, 5) * 2f, 50, default(Color), 1.25f);
										dust.noGravity = true;

									}
								}


							}
							if (spinTimer1 == 150)
							{
								for (int i = 0; i < 8; i++)
								{
									if (Main.netMode != NetmodeID.MultiplayerClient)
									{
										owner = Main.myPlayer;
									}
									float offsetAngle = (i) * 45;
									Projectile.NewProjectile(npc.Center + Vector2.UnitY.RotatedBy(MathHelper.ToRadians(offsetAngle + spin1)) * npc.Distance(playerTarget.Center), Vector2.Zero, ModContent.ProjectileType<LunarExplosion>(), 40, 5, owner);
								}
								isSpinning1 = false;
							}
							if (spinTimer1 > 200)
							{
								Vector2 teleportPosition3 = playerTarget.Center + Vector2.One.RotatedByRandom(360) * 250;
								npc.position = teleportPosition3;
								Main.PlaySound(SoundID.Item101, npc.Center);
								for (int i = 0; i < 500; i++)
								{
									var dust = Dust.NewDustDirect(teleportPosition1, npc.width, npc.height, DustID.BlueCrystalShard, Main.rand.NextFloat(-6, 5) * 2f, Main.rand.NextFloat(-6, 5) * 2f, 50, default(Color), 1.25f);
									dust.noGravity = true;

								}
								spinTimer1 = 0;
							}
						}
					}
				}

				//transition
				if (phase2)
				{

					BeginTimer++;
					if (BeginTimer == 1)
					{
						pause = true;
						for (int i = 0; i < 500; i++)
						{
							int dusttype = Main.rand.Next(new int[] { DustID.SolarFlare, DustID.Vortex, DustID.PinkFlame, DustID.BlueCrystalShard });
							var dust = Dust.NewDustDirect(npc.Center, npc.width, npc.height, dusttype, Main.rand.NextFloat(-6, 5) * 2f, Main.rand.NextFloat(-6, 5) * 2f, 50, default(Color), 1.25f);
							dust.noGravity = true;
						}
						npc.alpha = 255;
						npc.dontTakeDamage = true;
						npc.velocity *= 0;
					}

					if (BeginTimer > 120 && pause)
					{
						npc.TargetClosest();
						Npc1 = NPC.NewNPC((int)playerTarget.Center.X, (int)playerTarget.Center.Y - 400, ModContent.NPCType<SolarShell>());
						Npc2 = NPC.NewNPC((int)playerTarget.Center.X, (int)playerTarget.Center.Y + 400, ModContent.NPCType<NebulaShell>());
						Npc3 = NPC.NewNPC((int)playerTarget.Center.X - 400, (int)playerTarget.Center.Y, ModContent.NPCType<StardustShell>());
						Npc4 = NPC.NewNPC((int)playerTarget.Center.X + 400, (int)playerTarget.Center.Y, ModContent.NPCType<VortexShell>());
						Main.npc[Npc1].target = npc.target;
						Main.npc[Npc2].target = npc.target;
						Main.npc[Npc3].target = npc.target;
						Main.npc[Npc4].target = npc.target;
						for (int i = 0; i < 500; i++)
						{
							int dusttype = Main.rand.Next(new int[] { DustID.SolarFlare, DustID.Vortex, DustID.PinkFlame, DustID.BlueCrystalShard });
							var dust = Dust.NewDustDirect(npc.Center, npc.width, npc.height, dusttype, Main.rand.NextFloat(-6, 5) * 2f, Main.rand.NextFloat(-6, 5) * 2f, 50, default(Color), 1.25f);
							dust.noGravity = true;
						}
						npc.alpha = 0;
						npc.dontTakeDamage = false;
						pause = false;
						timer = 0;
						attack = 1;
						isSpinning = false;
						teleportTimer = 0;
						meteorTimer = 118;
						dashTimer = 0;
						starTimer = 0;
						spinTimer = 0;
						isSpinning1 = false;
						starTimer1 = 0;
						npc.damage = 150;
						AttackTimer = 0;

					}
					if (BeginTimer > 121)
					{
						if (Main.npc[Npc1].active == false)
						{
							dead1 = true;
						}

						if (Main.npc[Npc2].active == false)
						{
							dead2 = true;
						}

						if (Main.npc[Npc3].active == false)
						{
							dead3 = true;
						}

						if (Main.npc[Npc4].active == false)
						{
							dead4 = true;
						}

						if (dead1 && dead2 && dead3 && dead4)
                        {
							phase2true = true;
							npc.dontTakeDamage = false;
						}

						else
						{
							npc.dontTakeDamage = true;
						}
					}
				}

				//phase 2
				if (phase2true)
				{
					Phase2Timer++;
					if (Phase2Timer == 1)
					{
						timer = 0;
						attack = 1;
						isSpinning = false;
						teleportTimer = 0;
						meteorTimer = 118;
						dashTimer = 0;
						starTimer = 0;
						spinTimer = 0;
						isSpinning1 = false;
						starTimer1 = 0;
						npc.alpha = 0;
						npc.damage = 150;
						npc.dontTakeDamage = false;
						AttackTimer = 0;
					}
					if (Phase2Timer % 60 == 0)
                    {
						if (Main.netMode != NetmodeID.MultiplayerClient)
						{
							owner = Main.myPlayer;
						}

						var a = Projectile.NewProjectileDirect(playerTarget.Center + new Vector2(Main.rand.NextFloat(-600, 600), Main.rand.NextFloat(-250, 50)), Vector2.Zero, ModContent.ProjectileType<Target>(), 0, 0, owner);
						a.timeLeft = 60;
						a.netUpdate = true;
					}

					if (attack == 1)
					{

						teleportTimer++;
						if (teleportTimer == 1 || (float)Math.Sqrt(((playerTarget.Center.X - teleportPosition1.X) * (playerTarget.Center.X - teleportPosition1.X)) + ((playerTarget.Center.Y - teleportPosition1.Y) * (playerTarget.Center.Y - teleportPosition1.Y))) > 1500f)
						{
							teleportPosition1 = playerTarget.Center + Vector2.One.RotatedByRandom(360) * 250;
						}
						if (teleportTimer > 150)
						{

							npc.position = teleportPosition1;
							Main.PlaySound(SoundID.Item101, npc.Center);
							for (int i = 0; i < 500; i++)
							{
								int dusttype = Main.rand.Next(new int[] { DustID.SolarFlare, DustID.Vortex, DustID.PinkFlame, DustID.BlueCrystalShard });
								var dust = Dust.NewDustDirect(teleportPosition1, npc.width, npc.height, dusttype, Main.rand.NextFloat(-6, 5) * 2f, Main.rand.NextFloat(-6, 5) * 2f, 50, default(Color), 1.25f);
								dust.noGravity = true;
							}

							if (Main.netMode != NetmodeID.MultiplayerClient)
							{
								owner = Main.myPlayer;
							}
							for (int i = 0; i < 8; i++)
							{
								Vector2 direction = new Vector2(0, -2f);
								direction = direction.RotatedBy(MathHelper.ToRadians(45f * i));
								var a = Projectile.NewProjectileDirect(npc.Center, direction, ModContent.ProjectileType<LargeCelestialStar>(), 35, 3, owner);
								a.netUpdate = true;
								a.timeLeft = 300;
								a.hostile = true;
								a.friendly = false;
								a.tileCollide = false;

							}
							teleportTimer = 0;
						}
						for (int i = 0; i < 6; i++)
						{
							int dusttype = Main.rand.Next(new int[] { DustID.SolarFlare, DustID.Vortex, DustID.PinkFlame, DustID.BlueCrystalShard });
							var dust = Dust.NewDustDirect(teleportPosition1, npc.width, npc.height, dusttype, 0, 0, 50, default(Color), 1.25f);
							dust.noGravity = true;
						}

						if (timer < 320)
						{
							speed = timer / 20;
						}
						else
						{
							speed = 16;
						}



						if (distanceTo > 300)
						{

							AdjustMagnitude(ref newMove, speed);
							npc.velocity = (6 * npc.velocity + newMove) / 7f;
							AdjustMagnitude(ref npc.velocity, speed);
						}
						else
						{
							EaseToSpeed(ref npc.velocity, speed + 2f);
						}
					}
					if (attack == 2)
                    {
					
						npc.velocity *= 0.95f;
						starTimer1 += 1;
						if (starTimer1 == 1)
						{
							shooting = 60;
							starRotation = 0f;// Main.rand.NextFloat(0, 360);
							npc.ai[3] = 0;
							turning = true;
						}
						
						if (starTimer1 < 200 && starTimer1 > 60)
						{


							npc.ai[3] += 1;
							if (npc.ai[3] >= shooting)
							{
								
								if (Main.netMode != NetmodeID.MultiplayerClient)
								{
									owner = Main.myPlayer;
								}
								if (turning)
								{
									for (int i = 0; i < 8; i++)
									{
										var a = Projectile.NewProjectileDirect(npc.Center, Vector2.UnitY.RotatedBy(MathHelper.ToRadians(starRotation + i * 45)) * 1.5f, ModContent.ProjectileType<LargeCelestialStar>(), 20, 3, owner);
										a.ai[0] = 1;
										a.timeLeft = 300;
										a.netUpdate = true;
										a.hostile = true;
										a.friendly = false;
									}
									turning = false;
								}
								else
								{
									for (int i = 0; i < 8; i++)
									{
										var a = Projectile.NewProjectileDirect(npc.Center, Vector2.UnitY.RotatedBy(MathHelper.ToRadians(starRotation + i * 45)) * 1.5f, ModContent.ProjectileType<LargeCelestialStar>(), 20, 3, owner);
										a.ai[0] = 2;
										a.timeLeft = 300;
										a.netUpdate = true;
										a.hostile = true;
										a.friendly = false;
									}
									turning = true;
								}
								npc.ai[3] = 0;
							}

							}
						if (starTimer1 > 200)
						{
							Vector2 teleportPosition4 = playerTarget.Center + Vector2.One.RotatedByRandom(360) * 250;
							npc.position = teleportPosition4;
							Main.PlaySound(SoundID.Item101, npc.Center);
							for (int i = 0; i < 500; i++)
							{
								var dust = Dust.NewDustDirect(teleportPosition1, npc.width, npc.height, DustID.PinkFlame, Main.rand.NextFloat(-6, 5) * 4f, Main.rand.NextFloat(-6, 5) * 4f, 50, default(Color), 2f);
								dust.noGravity = true;

							}
							starTimer1 = 0;
						}

						
					
						
						npc.velocity *= 0.95f;
						spinTimer1 += 1;
						if (spinTimer1 == 1)
						{
							spinning1 = 6f;
							spin1 = Main.rand.NextFloat(0, 360);
						}
						if (spinTimer1 < 150)
						{
							//playerTarget.Center - Vector2.UnitY.RotatedBy(angle - MathHelper.TwoPi * i / totalIllusions);
							isSpinning1 = true;

							npc.ai[1] += 1;
							if (npc.ai[1] >= 1)
							{
								spin1 += spinning1;
								spinning1 *= 0.985f;
								npc.ai[1] = 0;
							}

							



						}	
						if (spinTimer1 == 150)
						{
							for (int i = 0; i < 8; i++)
							{
								if (Main.netMode != NetmodeID.MultiplayerClient)
								{
									owner = Main.myPlayer;
								}
								float offsetAngle = (i) * 45;
								var a = Projectile.NewProjectileDirect(npc.Center + Vector2.UnitY.RotatedBy(MathHelper.ToRadians(offsetAngle + spin1)) * npc.Distance(playerTarget.Center), Vector2.Zero, ModContent.ProjectileType<LunarExplosion>(), 40, 5, owner);
								a.timeLeft = 120;
							}
							isSpinning1 = false;
						}
						if (spinTimer1 > 200)
						{
							
							spinTimer1 = 0;
						}
						
					}
					if (attack == 3)
                    {
						starTimer++;
						npc.velocity *= 0.9f;
						if (starTimer > 70)
						{
							Vector2 teleportPosition2 = playerTarget.Center + Vector2.One.RotatedByRandom(360) * 500;
							npc.position = teleportPosition2;
							Main.PlaySound(SoundID.Item101, npc.Center);
							for (int i = 0; i < 500; i++)
							{
								var dust = Dust.NewDustDirect(teleportPosition2, npc.width, npc.height, DustID.Vortex, Main.rand.NextFloat(-6, 5) * 2f, Main.rand.NextFloat(-6, 5) * 2f, 50, default(Color), 1.25f);
								dust.noGravity = true;
							}

							if (Main.netMode != NetmodeID.MultiplayerClient)
							{
								owner = Main.myPlayer;
							}
							for (int i = 0; i < 13; i++)
							{

								Vector2 position = npc.Center - Vector2.UnitY.RotatedBy(MathHelper.ToRadians(30f * i)) * 200;
								var a = Projectile.NewProjectileDirect(position, Vector2.Zero, ModContent.ProjectileType<LightningProjectile>(), 35, 3, owner);
								a.ai[0] = 60;
								a.netUpdate = true;
								a.timeLeft = 300;
								a.hostile = true;
								a.friendly = false;
								a.tileCollide = false;

							}
							for (int i = 0; i < 6; i++)
							{
								Vector2 direction = new Vector2(0, -2f);
								direction = direction.RotatedBy(MathHelper.ToRadians(60f * i));
								var a = Projectile.NewProjectileDirect(npc.Center, direction, ModContent.ProjectileType<LargeCelestialStar>(), 35, 3, owner);
								a.netUpdate = true;
								a.timeLeft = 300;
								a.hostile = true;
								a.friendly = false;
								a.tileCollide = false;

							}
							starTimer = 0;

						}
					}
					if (attack == 4)
					{
						AttackTimer++;
						if (AttackTimer < 90)
							npc.velocity *= 0.95f;

						if (AttackTimer == 1)
						{
							Vector2 teleportPosition2 = playerTarget.Center - new Vector2(0, 300);
							npc.velocity *= 0;
							npc.Center = teleportPosition2;
							Main.PlaySound(SoundID.Item101, npc.Center);
							for (int i = 0; i < 500; i++)
							{
								var dust = Dust.NewDustDirect(teleportPosition2, npc.width, npc.height, DustID.SolarFlare, Main.rand.NextFloat(-6, 5) * 2f, Main.rand.NextFloat(-6, 5) * 2f, 50, default(Color), 1.25f);
								dust.noGravity = true;
							}

							for (int i = 0; i < 11; i++)
							{
								Vector2 position = new Vector2(400, -750 + 150 * i);
								if (Main.netMode != NetmodeID.MultiplayerClient)
								{
									owner = Main.myPlayer;
								}
								var a = Projectile.NewProjectileDirect(playerTarget.Center + position, new Vector2(-10, 0), ModContent.ProjectileType<SolarSwordBeamHostile>(), 20, 4, owner);
								a.ai[0] = 50;


							}

							for (int i = 0; i < 11; i++)
							{
								Vector2 position = new Vector2(-400, -750 + 150 * i);
								if (Main.netMode != NetmodeID.MultiplayerClient)
								{
									owner = Main.myPlayer;
								}
								var a = Projectile.NewProjectileDirect(playerTarget.Center + position, new Vector2(10, 0), ModContent.ProjectileType<SolarSwordBeamHostile>(), 20, 4, owner);
								a.ai[0] = 50;
							}
						}
						
						if (AttackTimer == 200)
						{
							AttackTimer = 0;
						}

						spinTimer += 1;
						if (spinTimer == 1)
						{
							spinning = 6f;
							spin = Main.rand.NextFloat(0, 360);
						}
						if (spinTimer < 150)
						{
							//playerTarget.Center - Vector2.UnitY.RotatedBy(angle - MathHelper.TwoPi * i / totalIllusions);
							isSpinning = true;
							spinPos = playerTarget.Center;

							npc.ai[0] += 1;
							if (npc.ai[0] >= 1)
							{
								spin += spinning;
								spinning *= 0.985f;
								npc.ai[0] = 0;
							}


							npc.Center = playerTarget.Center - Vector2.UnitY.RotatedBy(MathHelper.ToRadians(spin)) * 400;
						}
						if (spinTimer == 150)
						{
							npc.velocity = newMove;
							AdjustMagnitude(ref npc.velocity, 25);
							Vector2 pos = playerTarget.Center;
							spinPos = pos;
						}
						if (spinTimer > 200)
						{
							spinTimer = 0;
						}
					}
					

				}
			}

			//effects if in phase 1
			if (!phase2)
			{
				float rotationsPerSecond = (npc.velocity.X + npc.velocity.Y);
				bool rotateClockwise = true;
				npc.rotation += (rotateClockwise ? 1 : -1) * MathHelper.ToRadians(rotationsPerSecond * 1f);
			}
			
			
		}

        public override bool CheckDead()
        {
			if (Main.netMode != NetmodeID.MultiplayerClient)
			{
				owner = Main.myPlayer;
			}
			Projectile.NewProjectile(npc.Center, Vector2.Zero, ModContent.ProjectileType<CelestialShockwave>(), 0, 0, owner);
			return true;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			if (isSpinning)
			{

				for (int i = 0; i < 4; i++)
				{
					Vector2 vector11 = npc.Size * 0.5f;
					float offsetAngle = (i) * 90;
					Vector2 drawPos = (spinPos + Vector2.UnitY.RotatedBy(MathHelper.ToRadians(offsetAngle + spin)) * npc.Distance(spinPos)) - Main.screenPosition;
					if ((i == 0 && spinTimer > 166) || (i == 2 && spinTimer < 166))
					{
						color = npc.GetAlpha(Color.White);
					}
					else
                    {
						color = npc.GetAlpha(Color.LightSteelBlue);
					}
					float rotation = npc.rotation;//(Main.player[npc.target].Center - (drawPos + Main.screenPosition)).ToRotation() + MathHelper.PiOver2;
					spriteBatch.Draw(Main.npcTexture[npc.type], drawPos, npc.frame, color, rotation, vector11, npc.scale, SpriteEffects.None, 0f);
				}
				return false;

			}
			
			
			else if (isSpinning1)
			{

				for (int i = 0; i < 8; i++)
				{
					Vector2 vector11 = npc.Size * 0.5f;
					float offsetAngle = (i) * 45;
					Vector2 drawPos = npc.Center + Vector2.UnitY.RotatedBy(MathHelper.ToRadians(offsetAngle + spin1)) * npc.Distance(Main.player[npc.target].Center)- Main.screenPosition;
					
					color = npc.GetAlpha(Color.LightSteelBlue);
					
					float rotation = npc.rotation;//(Main.player[npc.target].Center - (drawPos + Main.screenPosition)).ToRotation() + MathHelper.PiOver2;
					spriteBatch.Draw(Main.npcTexture[npc.type], drawPos, npc.frame, color, rotation, vector11, npc.scale, SpriteEffects.None, 0f);
				}
				return true;

			}
			else
			{
				return true;
			}
		}

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            if (isSpinning)
            {
				return false;
            }
            else
            {
				return base.DrawHealthBar(hbPosition, ref scale, ref position);

			}
			
        }

		public override void FindFrame(int frameHeight)
		{
			if (++npc.frameCounter >= 5)
			{
				npc.frameCounter = 0;
				int startingFrame = phase2 ? 8 : 0;
				int endingFrame = phase2 ? 16 : 7;
				npc.frame.Y = (npc.frame.Y + frameHeight) % (endingFrame * frameHeight);
				if (npc.frame.Y / frameHeight < startingFrame)
					npc.frame.Y = frameHeight * startingFrame;
			}
		}

		public override void NPCLoot()
		{
			
			if (Main.rand.NextBool(10))
			{
				//Item.NewItem(npc.getRect(), ItemType<AbominationTrophy>());
			}
			if (Main.expertMode)
			{
				npc.DropBossBags();
			}
			else
			{
				if (Main.rand.NextBool(7))
				{
					Item.NewItem(npc.getRect(), ItemType<CelestialConstructMask>());
				}
				int itemtype = Main.rand.Next(new int[] { ItemID.SolarEruption, ItemID.DayBreak, ItemID.VortexBeater, ItemID.Phantasm, ItemID.NebulaArcanum, ItemID.NebulaBlaze, ItemID.StardustCellStaff, ItemID.StardustDragonStaff });

				Item.NewItem(npc.getRect(), itemtype);

			}

		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			name = "A Celestial Construct";
			potionType = ItemID.SuperHealingPotion;
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.lifeMax = (int)(npc.lifeMax * 0.625f * bossLifeScale);
			npc.damage = (int)(npc.damage * 0.6f);
		}
		 
		private void AdjustMagnitude(ref Vector2 vector, float speed)
		{
			float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
			if (magnitude > speed)
			{
				vector *= speed / magnitude;
			}
		}

		private void EaseToSpeed(ref Vector2 vector, float speed)
		{
			float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
			if (magnitude > speed)
			{
				vector *= 0.95f;
			}
			else
            {
				vector *= 1.05f;
            }
		}
	}
}