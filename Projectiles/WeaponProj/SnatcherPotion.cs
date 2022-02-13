using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using SpiritsOfTheForgotten.Buffs;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework.Graphics;


namespace SpiritsOfTheForgotten.Projectiles.WeaponProj
{
	public class SnatcherPotion : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mysterious Potion");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;        //The recording mode
		}



		public override void OnHitNPC(Terraria.NPC target, int damage, float knockback, bool crit)
		{
			Terraria.Main.player[projectile.owner].AddBuff(ModContent.BuffType<TheFinePrint>(), 900);

			target.AddBuff(ModContent.BuffType<MysteriousCurse>(), 900);
			target.AddBuff(BuffID.ShadowFlame, 900);

		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			//Redraw the projectile with the color not influenced by light
			Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}



		public override void SetDefaults()
		{

			projectile.width = 28;               //The width of projectile hitbox
			projectile.height = 28;              //The height of projectile hitbox
			
			projectile.friendly = true;         //Can the projectile deal damage to enemies?
			projectile.hostile = false;         //Can the projectile deal damage to the player?
			
			
			projectile.timeLeft = 600;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			
			
			projectile.ignoreWater = false;          //Does the projectile's speed be influenced by water?
			projectile.tileCollide = true;          //Can the projectile collide with tiles?
			projectile.extraUpdates = 1;            //Set to above 0 if you want the projectile to update multiple time in a frame
			


		}
	
		public override void AI()
        { 
			int s = Main.rand.Next(1, 3);
			Dust.NewDust(projectile.position - new Vector2(2f, 2f), projectile.width + 4, projectile.height + 4, DustID.Shadowflame, projectile.velocity.X * 0.4f, projectile.velocity.Y * 0.4f, 100, default(Color), s);
			
			float rotationsPerSecond = 4.5f;
			bool rotateClockwise = true;
			
			projectile.rotation += (rotateClockwise ? 1 : -1) * MathHelper.ToRadians(rotationsPerSecond * 6f);
		}

		public override void Kill(int timeLeft)
		{

			Main.PlaySound(SoundID.Item107, projectile.Center);
			int s = Main.rand.Next(1, 3);
			Dust.NewDust(projectile.position - new Vector2(2f, 2f), projectile.width + 4, projectile.height + 4, DustID.Shadowflame, projectile.velocity.X * 0.4f, projectile.velocity.Y * 0.4f, 100, default(Color), s);

			Dust.NewDust(projectile.position - new Vector2(2f, 2f), projectile.width + 4, projectile.height + 4, DustID.Shadowflame, projectile.velocity.X * 0.4f, projectile.velocity.Y * 0.4f, 100, default(Color), s);


		}



	}
}