using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using SpiritsOfTheForgotten.Projectiles.WeaponProj;
using static Terraria.ModLoader.ModContent;
using Terraria.DataStructures;

namespace SpiritsOfTheForgotten.Items.Weapons
{
	public class CelestialLaserShooter : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Celestial Vaporizer"); 
			Tooltip.SetDefault("Fires lasers beams the get faster and more accuate the longer you hold down");
			Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 3));
		}

		public override void SetDefaults() 
		{
			item.damage = 300;
			item.magic = true;
			item.width = 92;
			item.height = 30;
			item.useTime = 7;
			item.useAnimation = 21;
			item.channel = true;
			item.noUseGraphic = true;
			item.noMelee = true;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 10;
			item.value = Item.buyPrice(0, 15, 0, 0);
			item.rare = ItemRarityID.Red;
			item.autoReuse = true;
			item.shoot = ProjectileType<CelestialLaserShooterProjectile>();
			item.shootSpeed = 30f;

		}
		
		

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemType<CelestialShard>(), 1);
			recipe.AddIngredient(ItemID.Arkhalis, 1);
			recipe.AddIngredient(ItemID.BluePhasesaber, 1);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
			
			
		}
	}
}