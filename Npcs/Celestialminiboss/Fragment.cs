using System.Threading;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace SpiritsOfTheForgotten.Npcs.Celestialminiboss
{
    public class Fragment : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stardust Fragment");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 7;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;        //The recording mode
        }

        public override void SetDefaults()
        {
            projectile.width = 22;
            projectile.height = 22;
            projectile.friendly = false;
            projectile.damage = 25;
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
        
     
       
    }
}