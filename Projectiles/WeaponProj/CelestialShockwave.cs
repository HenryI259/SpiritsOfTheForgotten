using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritsOfTheForgotten.Projectiles.WeaponProj
{
	public class CelestialShockwave : ModProjectile
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("CelestialExplosion");
			ProjectileID.Sets.NeedsUUID[projectile.type] = true;
		}
		/*
		public float Radius
		{
			get => projectile.ai[0];
			set => projectile.ai[0] = value;
		}

		public float MaxRadius
		{
			get => projectile.ai[1];
			set => projectile.ai[1] = value;
		}

		public float Color1
        {
			get => projectile.ai[2];
			set => projectile.ai[2] = value;
        }
		*/

		public float Radius = 2;
		public float MaxRadius = 1000;
		public const int Lifetime = 180;

		
		public override void SetDefaults()
		{

			projectile.width = 2;
			projectile.height = 2;
			projectile.timeLeft = Lifetime;
			projectile.ignoreWater = false;
			projectile.tileCollide = false;
		}

		public Vector2 position;

		public override void AI()
		{

			
			Radius = MathHelper.Lerp(Radius, MaxRadius, 0.25f);
			projectile.scale = MathHelper.Lerp(1.2f, 5f, Utils.InverseLerp(Lifetime, 0f, projectile.timeLeft, true));
			/*projectile.position = projectile.Center;
			projectile.width = (int)(Radius * projectile.scale);
			projectile.height = (int)(Radius * projectile.scale);
			projectile.position -= projectile.Size * 0.5f;
			projectile.position = projectile.Center;
			*/
			position = projectile.Center;
			projectile.width = (int)(Radius * projectile.scale);
			projectile.height = (int)(Radius * projectile.scale);
			projectile.Center = position;
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);

			float pulseCompletionRatio = Utils.InverseLerp(Lifetime, 0f, projectile.timeLeft, true);
			Vector2 scale = new Vector2(1.5f, 1f);
			DrawData drawData = new DrawData(ModContent.GetTexture("Terraria/Misc/Perlin"),
				projectile.Center - Main.screenPosition + projectile.Size * scale * 0.5f,
				new Rectangle(0, 0, projectile.width, projectile.height),
				new Color(new Vector4(1f - (float)Math.Sqrt(pulseCompletionRatio))) * 0.7f * projectile.Opacity,
				projectile.rotation,
				projectile.Size,
				scale,
				SpriteEffects.None, 0);

			Color color = Helper.Colors.lerp4(Color.Orange, Color.LightBlue, Color.Violet, Color.Cyan, Helper.Numbers.ZerotoOne(1, 2));
			Color pulseColor = Color.Lerp(color * 1.6f, Color.White, MathHelper.Clamp(pulseCompletionRatio * 2.2f, 0f, 1f));
			GameShaders.Misc["ForceField"].UseColor(color);
			GameShaders.Misc["ForceField"].Apply(drawData);
			drawData.Draw(spriteBatch);

			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.instance.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);
			return false;
		}
	}


}