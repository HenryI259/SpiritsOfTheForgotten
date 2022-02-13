using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using SpiritsOfTheForgotten.Projectiles.WeaponProj;
using SpiritsOfTheForgotten;
using System;
using static Terraria.ModLoader.ModContent;

namespace SpiritsOfTheForgotten.Items.Weapons
{
	public class LunarStaff : ModItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("Rains moons and lunar flares from the sky\nRight click to fire a burst of 3 moons");
			Item.staff[item.type] = true;
		}

		

		public override void SetDefaults() 
		{
			item.width = 64;
			item.height = 64;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 10;
			item.value = 150000;
			item.rare = ItemRarityID.Red;
			item.UseSound = SoundID.Item88;
			item.autoReuse = true;
			item.noMelee = true;
			item.magic = true;
			item.mana = 15;
			item.shoot = ModContent.ProjectileType<CelestialMoon>();
			item.shootSpeed = 10; 
		}



		

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			if (player.altFunctionUse != 2)
			{
				float projposX;
				float projposY;

				int numberProjectiles = Main.rand.Next(2, 4);
				for (int i = 0; i < numberProjectiles; i++)
				{
					projposX = Main.rand.Next(-100, 100) + ((position.X + Main.MouseWorld.X) / 2);
					projposY = position.Y - 700;
					Vector2 velo = new Vector2(Main.MouseWorld.X - projposX + Main.rand.Next(-25, 25), Main.MouseWorld.Y - projposY);
					AdjustMagnitude(ref velo, 9);

					type = Main.rand.Next(new int[] { ModContent.ProjectileType<CelestialMoon>(), ModContent.ProjectileType<CelestialMoon>(), ProjectileID.LunarFlare });
					Projectile.NewProjectile(projposX, projposY, velo.X, velo.Y, type, damage, 10, player.whoAmI);
					
				}
				return false;
			}
			else
            {
				return true;
            }
        }





		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemType<CelestialShard>(), 1);

			recipe.AddIngredient(ItemID.MeteorStaff, 1);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();

		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				item.useAnimation = 18;
				item.useTime = 6;
				item.reuseDelay = 14;
				item.damage = 200;
			}
			else
			{
				item.useTime = 9;
				item.useAnimation = 9;
				item.damage = 100;
				item.reuseDelay = 0;
			}
			return base.CanUseItem(player);
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