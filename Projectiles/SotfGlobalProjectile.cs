using SpiritsOfTheForgotten.Buffs;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;


namespace SpiritsOfTheForgotten.Projectiles
{
    public class SotfGlobalProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        
        public bool vortexBoost;
        public bool boosted = false;

        

        public override void AI(Projectile projectile)
        {
            if (vortexBoost && !boosted)
            {
                projectile.damage *= 15;
                projectile.damage /= 10;
                boosted = true;
            }
        }

        public override void OnHitPlayer(Projectile projectile, Player target, int damage, bool crit)
        {
            if (projectile.type == 462)
            {
                target.AddBuff(ModContent.BuffType<CelestialFlamesBuff>(), 90);
            }

            if (projectile.type == 452)
            {
                target.AddBuff(ModContent.BuffType<CelestialFlamesBuff>(), 180);
            }

            if (projectile.type == 454)
            {
                target.AddBuff(ModContent.BuffType<CelestialFlamesBuff>(), 240);
            }

            if (projectile.type == 455)
            {
                target.AddBuff(ModContent.BuffType<CelestialFlamesBuff>(), 480);
            }


        }


        public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.type == 638)
            {
                target.AddBuff(ModContent.BuffType<CelestialFlamesBuff>(), 300);
            }

            if (projectile.type == 639)
            {
                target.AddBuff(ModContent.BuffType<CelestialFlamesBuff>(), 300);
            }

            if (projectile.type == 640)
            {
                target.AddBuff(ModContent.BuffType<CelestialFlamesBuff>(), 300);
            }

            if (projectile.type == 642)
            {
                target.AddBuff(ModContent.BuffType<CelestialFlamesBuff>(), 300);
            }

            if (projectile.type == 645)
            {
                target.AddBuff(ModContent.BuffType<CelestialFlamesBuff>(), 300);
            }
            
            if (Main.player[projectile.owner].GetModPlayer<SotfPlayer>().SolarStrength == true && projectile.type != 612 && projectile.melee && projectile.friendly)
            {
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, 0, ProjectileID.SolarWhipSwordExplosion, (int)(projectile.damage * .5f), projectile.knockBack * 2f, projectile.owner);


            }
            
        }


    }

}