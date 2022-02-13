using Terraria.ID;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using SpiritsOfTheForgotten.Buffs;
using SpiritsOfTheForgotten.Projectiles.WeaponProj;

namespace SpiritsOfTheForgotten.Projectiles.WeaponProj
{
    public class CelestialOrb : ModProjectile
    {
        
        public override void SetDefaults()
        {
            projectile.width = 52;
            projectile.height = 52;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.damage = 100;
            projectile.knockBack = 0;
            
            projectile.tileCollide = true;
            
            Main.projFrames[projectile.type] = 5;
            projectile.penetrate = -1;
            projectile.timeLeft = 150;
        }

        public override void Kill(int timeLeft)
        {
            int numberProjectiles = 15 + Main.rand.Next(1, 8);
            for (int i = 0; i < numberProjectiles; i++)
            {

                
                   
                Vector2 vel = new Vector2(Main.rand.NextFloat(-25, 25) * 0.5f, Main.rand.NextFloat(-25, 25) * 0.5f);
                AdjustMagnitude(ref vel, Main.rand.NextFloat(12, 32));
                projectile.type = ModContent.ProjectileType<LunarLightning>();
                int a = Projectile.NewProjectile(projectile.Center, vel, projectile.type, projectile.damage, projectile.knockBack, projectile.owner, 0, 1);
                Main.projectile[a].tileCollide = false;
                Main.projectile[a].hostile = false;
                Main.projectile[a].friendly = true;
                
                
            }
            Main.PlaySound(SoundID.Item94, projectile.Center);
            base.Kill(timeLeft);   
        }

        public override void OnHitNPC(Terraria.NPC target, int damage, float knockback, bool crit)
        {
            Terraria.Main.player[projectile.owner].AddBuff(ModContent.BuffType<LunarPowerBuff>(), 300);
            target.AddBuff(ModContent.BuffType<CelestialFlamesBuff>(), 300);


        }

        public override void AI()
        {
            
            projectile.velocity = (40 * projectile.velocity) / 41f;

            projectile.frameCounter++;
            if (projectile.frameCounter >= 5)
            {
                projectile.frameCounter = 0;
                projectile.frame = (projectile.frame + 1) % 5;
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
