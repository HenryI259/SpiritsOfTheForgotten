using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;
using Microsoft.Xna.Framework;
using SpiritsOfTheForgotten.Items;

namespace SpiritsOfTheForgotten.Minions.CelestialSlime
{
	public class CelestialSlimeStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Heavenly Slime Staff");
			Tooltip.SetDefault("The heavens were disgusted by your first attempt of summoning a slime, let's hope they find this more usful");
			ItemID.Sets.GamepadWholeScreenUseRange[item.type] = true; // This lets the player target anywhere on the whole screen while using a controller.
			ItemID.Sets.LockOnIgnoresCollision[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.damage = 300;
			item.knockBack = 3f;
			item.mana = 13;
			item.width = 64;
			item.height = 64;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.value = Item.buyPrice(0, 15, 0, 0);
			item.rare = ItemRarityID.Red;
			item.UseSound = SoundID.Item44;
			item.autoReuse = true;

			// Minion things
			item.noMelee = true;
			item.summon = true;
			item.buffType = BuffType<CelestialSlimeBuff>();
			item.shoot = ProjectileType<CelestialSlimeMinion>();
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			player.AddBuff(item.buffType, 2);
			position = Main.MouseWorld;
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SlimeStaff, 1);
			recipe.AddIngredient(ModContent.ItemType<CelestialShard>(), 5);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}