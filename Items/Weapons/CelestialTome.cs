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
    public class CelestialTome : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Celestial Tome");
            Tooltip.SetDefault("Summons Moonlords tenticles to swarm the cursor");

        }

        public override void SetDefaults()
        {
			item.width = 32;
			item.height = 38;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 5;
			item.value = 150000;
			item.rare = ItemRarityID.Red;
			item.autoReuse = true;
			item.noMelee = true;
			item.magic = true;
			item.shootSpeed = 10;
			item.useAnimation = 8;
			item.useTime = 8;
			item.shoot = ProjectileID.RocketI;
			item.channel = true;
			item.damage = 300;
			item.mana = 5;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			
			
			position = Main.MouseWorld + new Vector2(60, 0).RotatedByRandom(MathHelper.ToRadians(360));
			Projectile.NewProjectile(position, Main.MouseWorld - position, ModContent.ProjectileType<MoonlordTenticle>(), damage, knockBack, player.whoAmI);
			
			return false;
        }

        public override void AddRecipes()
        {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemType<CelestialShard>(), 1);
			recipe.AddIngredient(ItemID.ShadowFlameHexDoll, 1);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}