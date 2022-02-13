using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;
using Terraria.ID;

namespace SpiritsOfTheForgotten.Projectiles.WeaponProj
{
    public class CelestialDeathbeam : ModProjectile
    {
        public float RotationalSpeed
        {
            get => projectile.ai[0];
            set => projectile.ai[0] = value;
        }
        
       

        public override void SetDefaults()
		{
			projectile.width = 10;
			projectile.height = 10;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.tileCollide = false;
		}

        public Color laserColor = Helper.Colors.Celestial();
        public Texture2D LaserBeginTexture => ModContent.GetTexture("SpiritsOfTheForgotten/Projectiles/WeaponProj/CelestialDeathbeamBegin");
        public Texture2D LaserMiddleTexture => ModContent.GetTexture("SpiritsOfTheForgotten/Projectiles/WeaponProj/CelestialDeathbeamMiddle");
        public Texture2D LaserEndTexture => ModContent.GetTexture("SpiritsOfTheForgotten/Projectiles/WeaponProj/CelestialDeathbeamEnd");
        public float MaxLength = 2000;
        public float LaserLength = 1000;
        public float timer = 0;
        public float MaxTime = 25;
        public float MaxSize = 0.75f;

        
      

        public void DrawLaser(SpriteBatch spriteBatch, Color beamColor, float scale)
        {
            float beginTextureHeight = 22;
            float beginTextureWidth = 22;
            float middleTextureHeight = 30;
            float middleTextureWidth = 26;
            float endTextureHeight = 30;
            //float endTextureWidth = 28;
            
            
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone);
            ArmorShaderData shader = GameShaders.Armor.GetShaderFromItemId(ItemID.VoidDye);
            shader.Apply(null);


            spriteBatch.Draw(LaserBeginTexture, projectile.Center - Main.screenPosition, null, beamColor, projectile.rotation, new Vector2(beginTextureWidth, beginTextureHeight) / 2f, scale, SpriteEffects.None, 0f);


            float middleLaserLength = LaserLength;
            middleLaserLength -= (beginTextureHeight / 2 + endTextureHeight) * scale;
            Vector2 centerOnLaser = projectile.Center;
            centerOnLaser += projectile.velocity * scale * beginTextureHeight / 2f;

            if (middleLaserLength > 0f)
            {
                float laserOffset = middleTextureHeight * scale;
                float incrementalBodyLength = 0f;
                while (incrementalBodyLength + 1f < middleLaserLength)
                {
                    spriteBatch.Draw(LaserMiddleTexture, centerOnLaser - Main.screenPosition, null, beamColor, projectile.rotation, middleTextureWidth * 0.5f * Vector2.UnitX, scale, SpriteEffects.None, 0f);
                    incrementalBodyLength += laserOffset;
                    centerOnLaser += projectile.velocity * laserOffset;
                }
            }

            if (Math.Abs(LaserLength - MaxLength) < 30f)
            {
                Vector2 laserEndCenter = centerOnLaser - Main.screenPosition;
                spriteBatch.Draw(LaserEndTexture, laserEndCenter, null, beamColor, projectile.rotation, LaserEndTexture.Frame(1, 1, 0, 0).Top(), scale, SpriteEffects.None, 0f);
            }
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 4;
        }

        public override void AI()
        {
            timer++;
            if (timer >= MaxTime)
            {
                projectile.Kill();
            }

            projectile.velocity = projectile.velocity.SafeNormalize(-Vector2.UnitY);

            float scaleIncrease = (float)Math.Sin(timer / MaxTime * MathHelper.Pi) * 1 * MaxSize;
            if (scaleIncrease > MaxSize) scaleIncrease = MaxSize;
            projectile.scale = scaleIncrease;

            float newDirection = projectile.velocity.ToRotation() + RotationalSpeed;
            projectile.rotation = newDirection - MathHelper.PiOver2; 
            projectile.velocity = newDirection.ToRotationVector2();

            float idealLaserLength = MaxLength;
            LaserLength = MathHelper.Lerp(LaserLength, idealLaserLength, 0.9f);

            CastLights();
        }

        public override void CutTiles()
        {
            DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
            Vector2 unit = projectile.velocity;
            Utils.PlotTileLine(projectile.Center, projectile.Center + unit * LaserLength, projectile.Size.Length() * projectile.scale, DelegateMethods.CutTiles);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
           
            DrawLaser(spriteBatch, laserColor, projectile.scale);
            return false;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (projHitbox.Intersects(targetHitbox))
                return true;
            float _ = 0f;
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), projectile.Center, projectile.Center + projectile.velocity * LaserLength, projectile.Size.Length() * projectile.scale, ref _);
        }

        private void CastLights()
        {
            DelegateMethods.v3_1 = laserColor.ToVector3();
            Utils.PlotTileLine(projectile.Center, projectile.Center + projectile.velocity * LaserLength, projectile.width * projectile.scale, new Utils.PerLinePoint(DelegateMethods.CastLight));
        }

        public override bool ShouldUpdatePosition() => false;
    }
}