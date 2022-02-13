using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SpiritsOfTheForgotten.Items;
using static Terraria.ModLoader.ModContent;


namespace SpiritsOfTheForgotten.Items.Weapons
{
    public class CelestialFlamethrower : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Celestial Blaze");
            Tooltip.SetDefault("Fires a stream of celestial flames");
           
        }

        public override void SetDefaults()
        {
            
            item.crit = 10;
            item.damage = 100;
            item.knockBack = 0;
            item.useTime = 4;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.useAnimation = 20;
            item.reuseDelay = 2;
            item.width = 64;
            item.height = 20;
            item.ranged = true;
            item.noMelee = true;
            item.useAmmo = AmmoID.Gel;
            item.rare = ItemRarityID.Red;
            item.value = 150000;
            item.UseSound = SoundID.Item34;
            item.shoot = mod.ProjectileType("CelestialFlame");
            item.autoReuse = true;
            item.shootSpeed = 20;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<CelestialShard>(), 1);
            recipe.AddIngredient(ItemID.Flamethrower, 1);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool ConsumeAmmo(Player player)
        {// Because of how the game works, player.itemAnimation will be 11, 7, and finally 3. (UseAmination - 1, then - useTime until less than 0.) 
         // We can get the Clockwork Assault Riffle Effect by not consuming ammo when itemAnimation is lower than the first shot.
            return !(player.itemAnimation < item.useAnimation-2);
        }

    }
}
