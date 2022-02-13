using System.Threading;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace SpiritsOfTheForgotten.Npcs.Celestialminiboss
{
    public class SolarDaggerBeam : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Solar Dagger Beam");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 7;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;        //The recording mode
        }

        public override void SetDefaults()
        {
            projectile.width = 52;
            projectile.height = 14;
            projectile.damage = 75;
            projectile.timeLeft = 180;
            projectile.light = 0.5f;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
        }
        
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            //Redraw the projectile with the color not influenced by light
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, Main.projectileTexture[projectile.type].Frame(1, Main.projFrames[projectile.type], 0, projectile.frame), color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
        
        public Vector2 direction;
        public int timer;
        public override void AI()
        {
            timer++;

            if (timer == 2)
                direction = projectile.velocity;
            if (timer == 3)
                projectile.velocity = Vector2.Zero;
           
            if (timer < projectile.ai[0])
            {
                projectile.rotation = (direction).ToRotation();
                projectile.hostile = false;
            }
            else
            {
                if (projectile.ai[1] == 0) projectile.hostile = true;
            }
            
            if (timer == projectile.ai[0])
            {
                projectile.velocity = direction;
                AdjustMagnitude(ref projectile.velocity, 20);
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