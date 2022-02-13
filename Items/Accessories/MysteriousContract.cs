using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader; 
using static Terraria.ModLoader.ModContent;

namespace SpiritsOfTheForgotten.Items.Accessories
{
	public class MysteriousContract : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mysterious Contract");
			Tooltip.SetDefault("Gives you a chance to shoot a potion at enemies that will make them take 25% more damage.\nMake sure you read the fine print");
			
		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 24;
			item.value = Item.buyPrice(0, 10, 0, 0);
			item.rare = ItemRarityID.Yellow;
			item.accessory = true;

		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<SotfPlayer>().Contract = true;
		}

       

    }
}