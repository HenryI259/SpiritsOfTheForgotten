using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using SpiritsOfTheForgotten.Buffs;
using System.Security.Cryptography.X509Certificates;
using SpiritsOfTheForgotten.Projectiles.WeaponProj;

namespace SpiritsOfTheForgotten.Projectiles.WeaponProj
{
	public class MiniNebula : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("MiniNebula");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;        //The recording mode
		}



		public override void OnHitNPC(Terraria.NPC target, int damage, float knockback, bool crit)
		{
			

			int itemType = Utils.SelectRandom(Main.rand, ItemID.NebulaPickup1, ItemID.NebulaPickup2, ItemID.NebulaPickup3);
			int itemIndex = Item.NewItem((int)target.position.X, (int)target.position.Y, target.width, target.height, itemType, 1, false, 0, false, false);
			Main.item[itemIndex].velocity.Y = Main.rand.NextFloat(-4f, 0f);
			Main.item[itemIndex].velocity.X = Main.rand.NextFloat(0.5f, 1.5f) * (float)projectile.direction;

			if (Main.netMode == NetmodeID.MultiplayerClient)
				NetMessage.SendData(MessageID.SyncItem, -1, -1, null, itemIndex);

		}


		public override void SetDefaults()
		{
			projectile.width = 50;
			projectile.height = 50;          
			projectile.friendly = true;         
			projectile.hostile = false;        
			projectile.magic = true;           
			projectile.timeLeft = 600;          
			projectile.alpha = 255;          
			projectile.light = 0.5f;            
			projectile.ignoreWater = true;          
			projectile.tileCollide = true; 



		}
		public int timer;
		public override void AI()
		{
			timer += 1;
			if (timer > 500)
			{
				// Fade out
				projectile.alpha += 25;
				if (projectile.alpha > 255)
				{
					projectile.alpha = 255;
				}
			}
			else
			{
				// Fade in
				projectile.alpha -= 25;
				if (projectile.alpha < 100)
				{
					projectile.alpha = 100;
				}
			}
			projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;
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

		public override void Kill(int timeLeft)
		{
			int numberProjectiles = 3 + Main.rand.Next(0, 1);

			for (int i = 0; i < numberProjectiles; i++)
			{

				int a = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.Next(-20, 20) * .4f, Main.rand.Next(-20, 20) * .4f, ModContent.ProjectileType<NebulaCloud>(), (int)(projectile.damage * .5f), 4, projectile.owner);
				Main.projectile[a].tileCollide = true;
				Main.projectile[a].hostile = false;
				Main.projectile[a].friendly = true;
			}




		}

		


	}
}
	
		
