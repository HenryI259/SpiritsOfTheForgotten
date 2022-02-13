using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritsOfTheForgotten.Items
{
    public class SoftGlobalItem : GlobalItem
    {

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FallenStar, 5);
            recipe.AddIngredient(ItemID.EnchantedSword, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(ItemID.Arkhalis);
            recipe.AddRecipe();
            
        }

    }

}
