using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using SpiritsOfTheForgotten.Projectiles;
using System;
using System.Collections.Generic;

namespace SpiritsOfTheForgotten.Items.Weapons
{
	public class StormShotgun: ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Storm Shotgun"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault(Helper.Text.ColorText("Ancient Artifact", Color.SteelBlue) + "\n" +
			   "The Vortexians were once the most intelligent creatures in the galaxy.\n" +
			   "However when disater struck and their world was practically distroyed by otherworldly\n" +
			   "invasive hornets they had to leave their civilization and retreat underground.\n" +
			   "They then formed a group of soldiers called storm divers to fend off the hornets\n" +
			   "wielding their powerful storm shotguns");


		}


		public override void SetDefaults()
		{

			item.damage = 145;
			item.width = 58;
			item.height = 14;
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 2;
			item.value = 70000;
			item.rare = ItemRarityID.Cyan;
			item.autoReuse = true;
			item.shootSpeed = 10;
			item.ranged = true;
			item.noMelee = true;
			item.shoot = ProjectileID.Bullet;
			item.useAmmo = AmmoID.Bullet;
		}

		public override bool ConsumeAmmo(Player player)
		{
			return Main.rand.NextFloat() >= .25f;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10, 0);
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public float speedChange1;
		public float velocity;
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (player.altFunctionUse == 2)
			{
				int numberDusts = 300 + Main.rand.Next(100); // 4 or 5 shots
				for (int i = 0; i < numberDusts; i++)
				{
					Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(30)); // 7 degree spread.

					float scale = 1f - (Main.rand.NextFloat() * .3f);
					perturbedSpeed *= scale;
					var a = Dust.NewDustDirect(player.Center, 1, 1, DustID.Vortex, perturbedSpeed.X * 1.5f, perturbedSpeed.Y * 1.5f, 50, default(Color), 1f);
					a.noGravity = true;
					Main.PlaySound(SoundID.Item36, player.Center);
				}

				Vector2 speedChange = (player.velocity - (5 * (new Vector2(speedX, speedY).SafeNormalize(Vector2.Zero) * 20))) / 5;
				if (speedChange == Vector2.Zero)
				{
					speedChange1 = 0;
				}
				else
                {
					speedChange1 = (float)Math.Sqrt(speedChange.X * speedChange.X + speedChange.Y * speedChange.Y);

				}

				if (speedChange1 > 15)
                {
					speedChange *= 15 / speedChange1;
					float speedChange2 = (float)Math.Sqrt(speedChange.X * speedChange.X + speedChange.Y * speedChange.Y);
					if (player.velocity == Vector2.Zero)
                    {
						velocity = 0;
                    }
					else
                    {
						velocity = (float)Math.Sqrt(player.velocity.X * player.velocity.X + player.velocity.Y * player.velocity.Y);

					}

					if (speedChange2 > velocity)
                    {
						player.velocity = speedChange;
                    }
				}
				else
				{
					player.velocity = speedChange;
				}
			}
			else
			{
				int numberProjectiles = 4 + Main.rand.Next(2); // 4 or 5 shots
				for (int i = 0; i < numberProjectiles; i++)
				{
					Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(13)); // 7 degree spread.

					float scale = 1f - (Main.rand.NextFloat() * .3f);
					perturbedSpeed *= scale;
					var a = Projectile.NewProjectileDirect(player.Center, perturbedSpeed, ProjectileID.VortexLaser, damage, knockBack, player.whoAmI);
					a.friendly = true;
					a.hostile = false;
					a.usesLocalNPCImmunity = true;
					a.localNPCHitCooldown = 10;
				}
			}
			return false;

		}



		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			foreach (TooltipLine line in tooltips)
			{
				if (line.Name == "ItemName")
				{
					line.overrideColor = Helper.Colors.LoreItem();
				}
			}
		}









	}
}