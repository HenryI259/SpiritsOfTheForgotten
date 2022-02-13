using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using Terraria.DataStructures;
using Terraria.ModLoader;
using SpiritsOfTheForgotten.Npcs.Celestialminiboss;
using SpiritsOfTheForgotten.Projectiles.WeaponProj;

namespace SpiritsOfTheForgotten.Items.Armor.CelestialArmor
{
    public class CelestialConstructMinion : ModProjectile
    {
        public int baseDamage = 200;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Celestial Construct");
        }
        public override void SetDefaults()
        {
            projectile.width = 46;
            projectile.height = 46;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.alpha = 0;
            projectile.minionSlots = 0f;
            projectile.netImportant = true;
            projectile.minion = true;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 10;
            projectile.damage = 200;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.penetrate = -1;
        }

        public Vector2 home;
        public int teleportTimer;
        public Vector2 teleportPosition1;
        public override void AI()
        {
            Player owner = Main.player[projectile.owner];
            if (owner.GetModPlayer<SotfPlayer>().CelestialSet && !owner.dead && owner.active)
                projectile.timeLeft = 2;
            else
            {
                projectile.Kill();
                return;
            }
            projectile.damage = baseDamage * (int)owner.minionDamage;
            NPC minionAttackTargetNpc = projectile.OwnerMinionAttackTargetNPC;
            if (minionAttackTargetNpc != null && projectile.ai[0] != minionAttackTargetNpc.whoAmI && minionAttackTargetNpc.CanBeChasedBy(projectile))
            {
                projectile.ai[0] = minionAttackTargetNpc.whoAmI;
                projectile.netUpdate = true;
            }
            if (projectile.ai[0] >= 0 && projectile.ai[0] < 200) //has target
            {
                NPC npc = Main.npc[(int)projectile.ai[0]];

                if (npc.CanBeChasedBy(projectile) && owner.Distance(projectile.Center) < 2500 && projectile.Distance(npc.Center) < 2500)
                {
                    float length = owner.Distance(npc.Center) - 300;
                    if (length > 300)
                        length = 300;
                    if (owner.Distance(npc.Center) > 350)
                    {
                        home = owner.Center + owner.DirectionTo(npc.Center) * length;
                    }
                    else
                    {
                        home = owner.Center - new Vector2(0, 70);
                    }


                        if (owner.HeldItem.melee)
                        {
                            projectile.Center = Vector2.Lerp(projectile.Center, home, 0.15f);
                            projectile.velocity *= 0.8f;
                            projectile.rotation = projectile.DirectionTo(npc.Center).ToRotation() + MathHelper.ToRadians(-45);
                            if (++projectile.localAI[0] > 30) //spam close range fists
                            {
                                projectile.localAI[0] = 0;
                                if (Main.myPlayer == projectile.owner)
                                {
                                    float num12 = 90;
                                    int num14 = 4;
                                    Vector2 vector5 = projectile.DirectionTo(npc.Center);
                                    vector5.Normalize();
                                    vector5 *= 50f;
                                    for (int num15 = 0; num15 < num14; num15++)
                                    {
                                        //float num16 = (float)num15 - ((float)num14 - 1f) / 2f;
                                        Vector2 vector6 = vector5.RotatedBy(MathHelper.ToRadians(num12 * num15));
                                        Vector2 velo = npc.Center - projectile.Center - vector6;
                                        velo.Normalize();
                                        int num18 = Projectile.NewProjectile(projectile.Center.X + vector6.X, projectile.Center.Y + vector6.Y, velo.X * 12f, velo.Y * 12f, ModContent.ProjectileType<SolarDaggerBeam>(), projectile.damage, projectile.knockBack, projectile.owner, 30, 1);
                                        var num17 = Main.projectile[num18];
                                        num17.melee = true;
                                        num17.usesLocalNPCImmunity = true;
                                        num17.localNPCHitCooldown = 10;
                                        num17.friendly = true;
                                        num17.hostile = false;
                                        num17.penetrate = -1;
                                        Helper.Syncing.SyncProjectile(num18);
                                    }

                                
                                }
                            }
                        }
                        else if (owner.HeldItem.ranged)
                        {
                            projectile.Center = Vector2.Lerp(projectile.Center, home, 0.15f);
                            projectile.velocity *= 0.8f;
                            projectile.rotation = projectile.DirectionTo(npc.Center).ToRotation() + MathHelper.ToRadians(-135);
                            if (++projectile.localAI[0] > 10) //spam close range fists
                            {
                                projectile.localAI[0] = 0;
                                int zap = 0;
                                if (Main.myPlayer == projectile.owner)
                                {
                                    for (int i = 0; i < 200; i++)
                                    {
                                        if (Main.npc[i].CanBeChasedBy(projectile) && projectile.Distance(Main.npc[i].Center) < 2500 && zap < 5)
                                        {
                                            int type = ModContent.ProjectileType<LunarLightning>();
                                            Projectile.NewProjectile(projectile.Center, projectile.DirectionTo(Main.npc[i].Center) * 20, type, projectile.damage * (int)owner.rangedDamage, 0f, projectile.owner, i);
                                            zap++;
                                        }
                                        
                                    }
                                    
                                }
                            }
                        }
                        else if (owner.HeldItem.magic)
                        {
                            projectile.Center = Vector2.Lerp(projectile.Center, home, 0.15f);
                            projectile.velocity *= 0.8f;
                            ///projectile.rotation = projectile.DirectionTo(npc.Center).ToRotation() + MathHelper.ToRadians(-225);
                            if (++projectile.localAI[0] > 45) //spam close range fists
                            {
                                projectile.localAI[0] = 0;
                                if (Main.myPlayer == projectile.owner)
                                {
                                    for (int i = 0; i < 8; i++)
                                    {
                                        Vector2 direction = new Vector2(0, -1.5f);
                                        direction = direction.RotatedBy(MathHelper.ToRadians(45f * i));
                                        int a = Projectile.NewProjectile(projectile.Center, direction, ModContent.ProjectileType<CelestialYoyoStar>(), projectile.damage, projectile.knockBack, projectile.owner);
                                        Main.projectile[a].tileCollide = false;
                                    }
                                }
                            
                            }
                        }
                        else if (owner.HeldItem.summon)
                        {
                            projectile.Center = Vector2.Lerp(projectile.Center, home, 0.15f);
                            projectile.velocity *= 0.8f;
                            projectile.rotation = projectile.DirectionTo(npc.Center).ToRotation() + MathHelper.ToRadians(-315);
                            if (++projectile.localAI[0] > 5) //spam close range fists
                            {
                                projectile.localAI[0] = 0;
                                if (Main.myPlayer == projectile.owner)
                                {
                                    for (int i = 0; i < 1; i++)
                                    {
                                        int p = Projectile.NewProjectile(projectile.Center, projectile.DirectionTo(npc.Center).RotatedByRandom(MathHelper.ToRadians(20)) * 15, ModContent.ProjectileType<Fragment>(), projectile.damage * (int)owner.minionDamage / 2, 0f, projectile.owner);
                                        Main.projectile[p].friendly = true;
                                        Main.projectile[p].hostile = false;
                                        Main.projectile[p].minion = true;
                                        Helper.Syncing.SyncProjectile(p);
                                    }
                                }
                            }
                        }
                        else
                        {
                            Vector2 newMove = npc.Center - projectile.Center;
                            float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
                            projectile.rotation = projectile.velocity.X + projectile.velocity.Y;
                            teleportTimer++;
                            if (teleportTimer == 1 || (float)Math.Sqrt(((npc.Center.X - teleportPosition1.X) * (npc.Center.X - teleportPosition1.X)) + ((npc.Center.Y - teleportPosition1.Y) * (npc.Center.Y - teleportPosition1.Y))) > 1000f)
                            {
                                teleportPosition1 = npc.Center + Vector2.One.RotatedByRandom(360) * 150;
                            }
                            if (teleportTimer > 180)
                            {

                                projectile.position = teleportPosition1;
                                Main.PlaySound(SoundID.Item101, projectile.Center);
                                for (int i = 0; i < 200; i++)
                                {
                                    int dusttype = Main.rand.Next(new int[] { DustID.SolarFlare, DustID.Vortex, DustID.PinkFlame, DustID.BlueCrystalShard });
                                    var dust = Dust.NewDustDirect(teleportPosition1, (int)(projectile.width * 0.75f), (int)(projectile.height * 0.75f), dusttype, Main.rand.NextFloat(-6, 5), Main.rand.NextFloat(-6, 5), 50, default(Color), 1.25f);
                                    dust.noGravity = true;
                                }
                                teleportTimer = 0;
                            }
                            for (int i = 0; i < 6; i++)
                            {
                                int dusttype = Main.rand.Next(new int[] { DustID.SolarFlare, DustID.Vortex, DustID.PinkFlame, DustID.BlueCrystalShard });
                                var dust = Dust.NewDustDirect(teleportPosition1, projectile.width, projectile.height, dusttype, 0, 0, 50, default(Color), 1.25f);
                                dust.noGravity = true;
                            }

                           



                            if (distanceTo > 100)
                            {

                                AdjustMagnitude(ref newMove, 20);
                                projectile.velocity = (3 * projectile.velocity + newMove) / 4f;
                                AdjustMagnitude(ref projectile.velocity, 20);
                            }
                            else
                            {
                                EaseToSpeed(ref projectile.velocity, 22f);
                            }
                        }
                }
                else //forget target
                {
                    projectile.ai[0] = -1f;
                    projectile.localAI[0] = 0f;
                    projectile.netUpdate = true;
                }
            }
            else //no target
            {
                projectile.rotation = 0;
                projectile.localAI[0] = 0f;

                Vector2 home = owner.Center - new Vector2(0, 70);

                projectile.direction = projectile.spriteDirection = owner.direction;
                
                if (projectile.Distance(home) > 2000f)
                {
                    projectile.Center = owner.Center;
                    projectile.velocity = Vector2.Zero;
                }
                else
                {
                    projectile.Center = Vector2.Lerp(projectile.Center, home, 0.25f);
                    projectile.velocity *= 0.8f;
                }
                
                if (++projectile.localAI[1] > 6f)
                {
                    projectile.localAI[1] = 0f;

                    float maxDistance = 1500f;
                    int possibleTarget = -1;
                    for (int i = 0; i < 200; i++)
                    {
                        NPC npc = Main.npc[i];
                        if (npc.CanBeChasedBy(projectile))// && Collision.CanHitLine(projectile.Center, 0, 0, npc.Center, 0, 0))
                        {
                            float npcDistance = owner.Distance(npc.Center);
                            if (npcDistance < maxDistance)
                            {
                                maxDistance = npcDistance;
                                possibleTarget = i;
                            }
                        }
                    }

                    if (possibleTarget >= 0)
                    {
                        projectile.ai[0] = possibleTarget;
                        projectile.netUpdate = true;
                    }
                }
            }
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
        public void SpectreHeal(Projectile proj)
        {
            if (!Main.player[proj.owner].moonLeech)
            {
                float num = 0.2f;
                num -= proj.numHits * 0.05f;
                if (num <= 0f)
                {
                    return;
                }
                float num2 = proj.damage * num;
                if ((int)num2 <= 0)
                {
                    return;
                }
                if (Main.player[Main.myPlayer].lifeSteal <= 0f)
                {
                    return;
                }
                Main.player[Main.myPlayer].lifeSteal -= num2 * 5; //original damage

                float num3 = 0f;
                int num4 = proj.owner;
                for (int i = 0; i < 255; i++)
                {
                    if (Main.player[i].active && !Main.player[i].dead && ((!Main.player[proj.owner].hostile && !Main.player[i].hostile) || Main.player[proj.owner].team == Main.player[i].team))
                    {
                        float num5 = Math.Abs(Main.player[i].position.X + (Main.player[i].width / 2) - proj.position.X + (proj.width / 2)) + Math.Abs(Main.player[i].position.Y + (Main.player[i].height / 2) - proj.position.Y + (proj.height / 2));
                        if (num5 < 1200f && (Main.player[i].statLifeMax2 - Main.player[i].statLife) > num3)
                        {
                            num3 = (Main.player[i].statLifeMax2 - Main.player[i].statLife);
                            num4 = i;
                        }
                    }
                }
                Projectile.NewProjectile(proj.position.X, proj.position.Y, 0f, 0f, ProjectileID.SpiritHeal, 0, 0f, proj.owner, num4, 5);
            }
        }
    }
}