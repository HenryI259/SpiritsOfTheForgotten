using Terraria.ID;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;


namespace SpiritsOfTheForgotten.Projectiles.WeaponProj
{
    class CrystalShard : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystal Shard");
        }

        public override void SetDefaults()
        {
            projectile.height = 24;
            projectile.width = 10;
            projectile.friendly = true;
            projectile.damage = 20;
            projectile.knockBack = 5;
            projectile.timeLeft = 300;
            projectile.aiStyle = 1;
            projectile.rotation = projectile.velocity.ToRotation();
            projectile.aiStyle = 1;
            Main.projFrames[projectile.type] = 2;

        }

        public bool randombool;
        public override void AI()
        {
            projectile.ai[0]++;
            if (projectile.ai[0] == 1) randombool = Main.rand.NextBool();

            projectile.frame = randombool ? 0 : 1;


        }
    }
}
