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
	public class CelestialRocketLauncher : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Celestial Bombarder");  
			Tooltip.SetDefault("Fires a burst of 4 celestial rockets\nRight click to fire a burst of normal rockets\nHold down to charge up a mega rocket");
		
		}

		

		public override void SetDefaults() 
		{
			item.width = 74;
			item.height = 28;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 5;
			item.value = 150000;
			item.rare = ItemRarityID.Red;
			item.UseSound = SoundID.Item11;
			item.autoReuse = false;
			item.noMelee = true;
			item.ranged = true;
			item.shootSpeed = 10;
			item.useAnimation = 16;
			item.useTime = 4;
			item.reuseDelay = 14;
			item.useAmmo = AmmoID.Rocket;
			item.shoot = ProjectileID.RocketI;
			item.channel = true;
			item.damage = 300;
		}


		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-42, -8);
		}

        public int rocket = 0;
		

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			if (player.altFunctionUse != 2)
			{
				rocket += 1;
				if (rocket > 4)
					rocket = 1;

				if (rocket == 1)
					type = ModContent.ProjectileType<SolarRocket>();

				if (rocket == 2)
					type = ModContent.ProjectileType<VortexRocket>();

				if (rocket == 3)
					type = ModContent.ProjectileType<NebulaRocket>();

				if (rocket == 4)
					type = ModContent.ProjectileType<StardustRocket>();

				
			}

			return true;
            
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

        public override bool UseItem(Player player)
        {

			player.GetModPlayer<SotfPlayer>().usinglauncher = true;
			return true;
		}

        public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{

				item.shoot = ProjectileID.RocketI;
			}
			else
			{
				item.shoot = ModContent.ProjectileType<SolarRocket>();
			}
			return base.CanUseItem(player);
		}

		public override bool ConsumeAmmo(Player player)
		{
			return Main.rand.NextFloat() >= .25f;
		}

		


	}
}