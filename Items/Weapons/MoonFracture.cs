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
	public class MoonFracture : ModItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("Fires small moons from all directions");
			Item.staff[item.type] = true;
		}

		

		public override void SetDefaults() 
		{
			item.damage = 350;			
			item.width = 52;
			item.height = 52;
			
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 10;
			item.value = 150000;
			item.rare = ItemRarityID.Red;
			item.UseSound = SoundID.Item9;
			item.autoReuse = true;
			item.shoot = ModContent.ProjectileType<MoonFractureProjectile>();
			item.noMelee = true;
			item.shootSpeed = 15;
			item.magic = true;
			item.useAnimation = 12;
			item.useTime = 4;
			item.reuseDelay = 14;
			item.mana = 10;
		}



		

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			float projposX;
			float projposY;

			int numberProjectiles = 5;
			for (int i = 0; i < numberProjectiles; i++)
			{
				projposX = Main.rand.Next(-750, 750) + position.X;
				projposY = Main.rand.Next(-750, 750) + position.Y;
				int a = Projectile.NewProjectile(projposX, projposY, (Main.MouseWorld.X - projposX) * 5, (Main.MouseWorld.Y - projposY) * 5, ModContent.ProjectileType<MoonFractureProjectile>(), damage, 10, player.whoAmI);
				Main.projectile[a].aiStyle = 1;
				Main.projectile[a].tileCollide = false;
				Main.projectile[a].hostile = false;
				Main.projectile[a].friendly = true;
			}
			return false;
        }

		



		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemType<CelestialShard>(), 1);

			recipe.AddIngredient(ItemID.SkyFracture, 1);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();

		}
	}
}