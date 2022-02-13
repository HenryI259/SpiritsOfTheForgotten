using Terraria.ID;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using SpiritsOfTheForgotten.Buffs;
using SpiritsOfTheForgotten.Items;
using Steamworks;

namespace SpiritsOfTheForgotten.Projectiles.WeaponProj
{
    public class CelestialChunk : ModProjectile
    {
        
        public override void SetDefaults()
        {
            projectile.width = 34;
            projectile.height = 34;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.damage = 100;
            projectile.knockBack = 0;
            
            projectile.tileCollide = true;

            Main.projFrames[projectile.type] = 17;
            projectile.penetrate = -1;
            projectile.timeLeft = 150;


        }

        Vector2 aim;
        Vector2 random;
        public bool kill;

        public override void Kill(int timeLeft)
        {
            bool target = false;
            Vector2 newMove = Vector2.Zero;
            float distance = 800f;
            for (int k = 0; k < 200; k++)
            {
                NPC npc = Main.npc[k];
                if (npc.CanBeChasedBy())
                {
                    Vector2 move = Main.npc[k].Center - projectile.Center;
                    float distanceTo = (float)Math.Sqrt(move.X * move.X + move.Y * move.Y);
                    if (distanceTo < distance)
                    {
                        newMove = move;
                        target = true;
                    }
                    
                }
            }

            int numberProjectiles = 15 + Main.rand.Next(0, 4);
            for (int i = 0; i < numberProjectiles; i++)
            {


                if (target)
                {
                    
                    aim.X = (newMove.X + Main.rand.Next(-7, 7) * newMove.X * 0.05f) * 0.02f;
                    aim.Y = (newMove.Y + Main.rand.Next(-7, 7) * newMove.Y * 0.05f) * 0.02f;
                    
                    AdjustMagnitude(ref aim, Main.rand.NextFloat(11, 16));

                    int a = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, aim.X, aim.Y, ModContent.ProjectileType<CelestialPebble>(), projectile.damage, projectile.knockBack, projectile.owner, 0, 1);
                    Main.projectile[a].tileCollide = true;
                    Main.projectile[a].hostile = false;
                    Main.projectile[a].friendly = true;
                    Main.projectile[a].melee = true;
                }
                else
                {
                    random.X = Main.rand.Next(-5, 6) * 2;
                    random.Y = Main.rand.Next(-5, 6) * 2;
                    AdjustMagnitude(ref random, Main.rand.NextFloat(9, 14));

                    int a = Projectile.NewProjectile(projectile.Center, random, ModContent.ProjectileType<CelestialPebble>(), projectile.damage, projectile.knockBack, projectile.owner, 0, 1);
                    Main.projectile[a].tileCollide = true;
                    Main.projectile[a].hostile = false;
                    Main.projectile[a].friendly = true;
                    Main.projectile[a].melee = true;
                }

            }
            Main.PlaySound(SoundID.Item105, projectile.Center);
            base.Kill(timeLeft);
        }

        public override void OnHitNPC(Terraria.NPC target, int damage, float knockback, bool crit)
        {
            Terraria.Main.player[projectile.owner].AddBuff(ModContent.BuffType<LunarPowerBuff>(), 300);
            target.AddBuff(ModContent.BuffType<CelestialFlamesBuff>(), 300);
            

        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            kill = true;
            projectile.velocity *= 0;
            projectile.timeLeft = 16;
            return false;
        }

        public override void AI()
        {
            
            projectile.velocity = (40 * projectile.velocity) / 41f;
            if (projectile.timeLeft <= 16) kill = true;

            projectile.frameCounter++;
            if (projectile.frameCounter >= 4)
            {
                projectile.frameCounter = 0;
                int startingFrame = kill ? 12 : 0;
                int endingFrame = kill ? 16 : 11;
                projectile.frame = (projectile.frame + 1) % endingFrame;
                if (projectile.frame < startingFrame)
                    projectile.frame = startingFrame;
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
