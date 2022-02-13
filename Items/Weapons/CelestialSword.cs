using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using SpiritsOfTheForgotten.Projectiles.WeaponProj;
using static Terraria.ModLoader.ModContent;
using Terraria.DataStructures;

namespace SpiritsOfTheForgotten.Items.Weapons
{
	public class CelestialSword : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Celestial Slasher"); 
			Tooltip.SetDefault("Rains lunar bolts on hit");
			Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(7, 19));
		}

		public override void SetDefaults() 
		{
			item.damage = 500;
			item.melee = true;
			item.width = 74;
			item.height = 66;
			item.useTime = 7;
			item.useAnimation = 21;
			item.channel = true;
			item.noUseGraphic = true;
			item.noMelee = true;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 10;
			item.value = Item.buyPrice(0, 15, 0, 0);
			item.rare = ItemRarityID.Red;
			item.UseSound = SoundID.Item117;
			item.autoReuse = true;
			item.shoot = ProjectileType<CelestialSlash>();
			item.shootSpeed = 72f;

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
			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemType<CelestialShard>(), 1);
			recipe.AddIngredient(ItemID.Arkhalis, 1);
			recipe.AddIngredient(ItemID.GreenPhasesaber, 1);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemType<CelestialShard>(), 1);
			recipe.AddIngredient(ItemID.Arkhalis, 1);
			recipe.AddIngredient(ItemID.PurplePhasesaber, 1);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemType<CelestialShard>(), 1);
			recipe.AddIngredient(ItemID.Arkhalis, 1);
			recipe.AddIngredient(ItemID.RedPhasesaber, 1);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemType<CelestialShard>(), 1);
			recipe.AddIngredient(ItemID.Arkhalis, 1);
			recipe.AddIngredient(ItemID.WhitePhasesaber, 1);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemType<CelestialShard>(), 1);
			recipe.AddIngredient(ItemID.Arkhalis, 1);
			recipe.AddIngredient(ItemID.YellowPhasesaber, 1);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}