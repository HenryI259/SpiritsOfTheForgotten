using Terraria.ModLoader;
using Terraria.ID;

namespace SpiritsOfTheForgotten.Items.Vanity
{
	[AutoloadEquip(EquipType.Head)]
	public class CelestialConstructMask : ModItem
	{
		public override void SetDefaults() {
			item.width = 30;
			item.height = 30;
			item.rare = ItemRarityID.Cyan;
			item.vanity = true;
		}

		public override bool DrawHead() {
			return false;
		}
	}
}