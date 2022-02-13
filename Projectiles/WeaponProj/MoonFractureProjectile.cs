using Terraria.ID;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using SpiritsOfTheForgotten.Buffs;

namespace SpiritsOfTheForgotten.Projectiles.WeaponProj
{
    public class MoonFractureProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
             DisplayName.SetDefault("Moon Fracture");
        }
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.damage = 100;
            projectile.knockBack = 0;
            projectile.aiStyle = 1;
            aiType = ProjectileID.SkyFracture;
            projectile.tileCollide = false;
            
            Main.projFrames[projectile.type] = 4;
            projectile.penetrate = 5;
            projectile.timeLeft = 300;


        }

        public override void OnHitNPC(Terraria.NPC target, int damage, float knockback, bool crit)
        {
            Terraria.Main.player[projectile.owner].AddBuff(ModContent.BuffType<LunarPowerBuff>(), 300);
            target.AddBuff(ModContent.BuffType<CelestialFlamesBuff>(), 300);


        }

        public override void AI()
        {
            
            projectile.frameCounter++;
            if (projectile.frameCounter >= 2)
            {
                projectile.frameCounter = 0;
                projectile.frame = (projectile.frame + 1) % 4;
            }
        }
        
    }
}
