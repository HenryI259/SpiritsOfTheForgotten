using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using SpiritsOfTheForgotten.Projectiles.WeaponProj;
using System;
using static Terraria.ModLoader.ModContent;

namespace SpiritsOfTheForgotten.Items.Weapons
{
	public class CelestialBow : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Pleiades"); 
			Tooltip.SetDefault("Replaces wooden arrows with celestial arrows that rain lunar bolts on death");
		}

		public override void SetDefaults() 
		{
			item.damage = 120;
			item.ranged = true;
			item.width = 50;
			item.height = 108;
			item.useTime = 15;
			item.useAnimation = 15;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 4;
			item.value = 150000;
			item.rare = ItemRarityID.Red;
			item.UseSound = SoundID.Item5;
			item.autoReuse = true;
			item.shootSpeed = 15;
			item.useAmmo = AmmoID.Arrow;
			item.noMelee = true;
			item.crit = 8;
			item.shoot = AmmoID.Arrow;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10, 0);
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			//replaces wooden arrows with celestial arrows
			if (type == ProjectileID.WoodenArrowFriendly)
			{
				type = ModContent.ProjectileType<CelestialArrow>();
			}

			
			
			

			
			float rotation = (float)Math.PI / 10;
			int amount = 7;
			Vector2 direction = new Vector2(speedX, speedY);
			direction.Normalize();
			direction *= 40f;
			bool flag2 = Collision.CanHit(position, 0, 0, position + direction, 0, 0);
			for (int i = 0; i < amount; i++)
			{
				float num16 = (float)i - ((float)amount - 1f) / 2f;
				Vector2 newPosition = direction.RotatedBy(rotation * num16);
				if (!flag2)
				{
					newPosition -= direction;
				}
				int num17 = Projectile.NewProjectile(position.X + newPosition.X, position.Y + newPosition.Y, speedX, speedY, type, damage, knockBack, player.whoAmI);
				Main.projectile[num17].noDropItem = true;
			}

			return false;

			
		}

		public override void AddRecipes() 
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemType<CelestialShard>(), 1);
			recipe.AddIngredient(ItemID.Tsunami, 1);
			recipe.AddIngredient(ItemID.Marrow, 1);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}