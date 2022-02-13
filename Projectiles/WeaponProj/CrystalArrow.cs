using Terraria.ID;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;


namespace SpiritsOfTheForgotten.Projectiles.WeaponProj
{
    class CrystalArrow:ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystal Arrow");
        }

        public override void SetDefaults()
        {
            projectile.height = 18;
            projectile.width = 7;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.damage = 20;
            projectile.knockBack = 5;
            projectile.timeLeft = 100000;
            projectile.aiStyle = 1;
            projectile.rotation = projectile.velocity.ToRotation();
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 3; i++)
            {
                int angle = Main.rand.Next(1, 360);
                int a = Projectile.NewProjectile(projectile.position.X, projectile.position.Y, (float)Math.Cos(angle*Math.PI/180)*2, (float)Math.Sin(angle * Math.PI / 180)*2, ProjectileID.CrystalStorm, projectile.damage / 3, 0, projectile.owner, 0, 0);
                Main.projectile[a].penetrate = -1;
                Main.projectile[a].usesIDStaticNPCImmunity = true;
                Main.projectile[a].idStaticNPCHitCooldown = 10;
            }
            Main.PlaySound(SoundID.Item101, projectile.position);
        }
    }
}
