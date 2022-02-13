using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using SpiritsOfTheForgotten.Projectiles.WeaponProj;

namespace SpiritsOfTheForgotten.Items.Weapons
{
	public class CelestialMagicGun : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Celestial Vaporizer"); 
			Tooltip.SetDefault("Fires lasers everywhere");

		}


		public override void SetDefaults()
		{

			item.damage = 77;
			item.crit = 10;
			item.width = 56;
			item.height = 27;
			item.useTime = 3;
			item.useAnimation = 5;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 5;
			item.value = 150000;
			item.rare = ItemRarityID.Red;
			item.UseSound = SoundID.Item91;
			item.autoReuse = true;
			item.shootSpeed = 12;
			item.magic = true;
			item.noMelee = true;
			item.shoot = ModContent.ProjectileType<CelestialLaser>();	
			item.rare = ItemRarityID.Red;
			item.mana = 1;
			
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-4, 10);
		}

		public int offset;
		public int rotation = 0;
		public int increase = 1;

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 offset = new Vector2(0, 10);


			if (Main.rand.Next(0, 51) == 0)
			{

				Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<CelestialOrb>(), damage, knockBack, player.whoAmI);
				Main.PlaySound(SoundID.Item117);
				
			}
			
		
			
				
			Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.ToRadians(rotation));
			Vector2 perturbedSpeed1 = new Vector2(speedX, speedY).RotatedBy(MathHelper.ToRadians(-rotation));
			//Vector2 perturbedSpeed2 = new Vector2(speedX, speedY).RotatedBy(MathHelper.ToRadians(rotation / 2));
			//Vector2 perturbedSpeed3 = new Vector2(speedX, speedY).RotatedBy(MathHelper.ToRadians(-rotation / 2));

			Projectile.NewProjectile(position.X + offset.X, position.Y + offset.Y, perturbedSpeed.X / 2.5f, perturbedSpeed.Y / 2.5f, type, damage, knockBack, player.whoAmI);
			Projectile.NewProjectile(position.X + offset.X, position.Y + offset.Y, perturbedSpeed1.X / 2.5f, perturbedSpeed1.Y / 2.5f, type, damage, knockBack, player.whoAmI);
			//Projectile.NewProjectile(position.X + offset.X, position.Y + offset.Y, perturbedSpeed2.X, perturbedSpeed2.Y, type, damage, knockBack, player.whoAmI);
			//Projectile.NewProjectile(position.X + offset.X, position.Y + offset.Y, perturbedSpeed3.X, perturbedSpeed3.Y, type, damage, knockBack, player.whoAmI);


			if (rotation == 10)
            {
				increase = -1;
            }

			if (rotation == -10)
			{
				increase = 1;
			}

			rotation += increase;
				


			return false;
			

		}


		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.LaserRifle);
			recipe.AddIngredient(ItemID.LaserMachinegun, 1);
			recipe.AddIngredient(ModContent.ItemType<CelestialShard>(), 1);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();

		}

		








	}
}