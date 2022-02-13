using System.Threading;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace SpiritsOfTheForgotten.Npcs.Celestialminiboss
{
    public class LightningProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 2;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 7;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;        //The recording mode
            DisplayName.SetDefault("Lightning");
        }

        public override void SetDefaults()
        {
            projectile.width = 18;
            projectile.height = 18;
            projectile.friendly = false;
            projectile.damage = 75;
            projectile.timeLeft = 150;
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

        public int timer;
        public override void AI()
        {
            timer++;

           
            Vector2 targetCenter = projectile.position;
            bool foundTarget = false;

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                Player player = Main.player[i];
               
                float between = Vector2.Distance(player.Center, projectile.Center);
                bool closest = Vector2.Distance(projectile.Center, targetCenter) > between;
                if ((closest) || !foundTarget)
                {
                    targetCenter = player.Center;
                    foundTarget = true;
                }
                
            }

            if (timer < projectile.ai[0])
            {
                projectile.rotation = (targetCenter - projectile.Center).ToRotation() + MathHelper.PiOver2;
                projectile.hostile = false;
            }
            
            if (timer == projectile.ai[0])
            {
                projectile.velocity = targetCenter - projectile.Center;
                AdjustMagnitude(ref projectile.velocity, 20);
                projectile.hostile = true;
            }

            projectile.frameCounter++;
            if (projectile.frameCounter >= 10)
            {
                projectile.frameCounter = 0;
                projectile.frame = (projectile.frame + 1) % 2;
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