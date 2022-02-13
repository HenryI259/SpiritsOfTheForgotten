using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using SpiritsOfTheForgotten.Projectiles.WeaponProj;
using static Terraria.ModLoader.ModContent;

namespace SpiritsOfTheForgotten.Items.Weapons
{
	public class CelestialShortsword : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Celestial Stabber");
			Tooltip.SetDefault("Releases an unstable energy orb that explodes into energy darts");
		}

		public override void SetDefaults()
		{
			item.damage = 200;
			item.melee = true;
			item.width = 48;
			item.height = 52;
			item.useTime = 15;
			item.useAnimation = 15;
			item.useStyle = ItemUseStyleID.Stabbing;
			item.knockBack = 10;
			item.value = 150000;
			item.rare = ItemRarityID.Red;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = ModContent.ProjectileType<CelestialChunk>();
			item.shootSpeed = 10;
			

		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.PlatinumShortsword);
			recipe.AddIngredient(ItemType<CelestialShard>(), 1);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();

		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			speedX = new Vector2(speedX, speedY).Length() * (speedX > 0 ? 1 : -1);
			speedY = 0;
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<CelestialChunk>(), damage, knockBack, player.whoAmI);

			return false;
        }



    }

}