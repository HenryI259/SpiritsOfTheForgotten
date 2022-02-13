using Terraria.ID;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using SpiritsOfTheForgotten.Buffs;

namespace SpiritsOfTheForgotten.Projectiles.WeaponProj
{
    public class CelestialSpark : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Celestial Spark");
        }
        public override void SetDefaults()
        {
            projectile.width = 2;
            projectile.height = 2;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.damage = 100;
            projectile.knockBack = 0;
            projectile.rotation = projectile.velocity.ToRotation();
            projectile.tileCollide = true;
            projectile.aiStyle = 2;
            projectile.alpha = 255;
           
        }
        public override void AI()
        {
            for (int i = 0; i < 3; i++)
            {
                
                var a = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.Vortex, 0, 0, 150, default(Color), 0.4f);
                a.velocity *= 0;
            }
        }

        public override void OnHitNPC(Terraria.NPC target, int damage, float knockback, bool crit)
        {
            
            target.AddBuff(ModContent.BuffType<CelestialFlamesBuff>(), 300);


        }

    }
}
