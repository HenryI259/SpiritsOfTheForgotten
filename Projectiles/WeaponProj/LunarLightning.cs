using Terraria.ID;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using SpiritsOfTheForgotten.Buffs;

namespace SpiritsOfTheForgotten.Projectiles.WeaponProj
{
    public class LunarLightning : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lunar Lightning");
        }
        public override void SetDefaults()
        {
            projectile.width = 2;
            projectile.height = 2;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.damage = 100;
            projectile.knockBack = 0;            
            projectile.tileCollide = false;
            projectile.timeLeft = 300;
            projectile.alpha = 255;
            projectile.extraUpdates = 10;
        }

        

        public int timer;
        public override void AI()
        {
            timer += 1;
            NPC npc = Main.npc[(int)projectile.ai[0]];
            if (!npc.CanBeChasedBy(projectile))
            {
                projectile.Kill();
            }
            
            if (timer % 3 == 0)
            {
                projectile.velocity = projectile.DirectionTo(npc.Center).RotatedByRandom(MathHelper.ToRadians(90)) * 15f;
            }

            for (int i = 0; i < 8; i++)
            {
                var a = Dust.NewDustDirect(projectile.position - new Vector2(2f, 2f), projectile.width + 4, projectile.height + 4, DustID.Vortex, projectile.velocity.X * 0.01f, projectile.velocity.Y * 0.01f, 100, default(Color), 1f);
                a.noGravity = true;
                a.velocity *= 0.2f;
            }
        }

    }
}