using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using SpiritsOfTheForgotten.Items.Vanity;

namespace SpiritsOfTheForgotten.Npcs.Celestialminiboss
{
	public class CelestialConstructBag : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Treasure Bag");
			Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
		}

		public override void SetDefaults() {
			item.maxStack = 999;
			item.consumable = true;
			item.width = 24;
			item.height = 24;
			item.rare = ItemRarityID.Expert;
			item.expert = true;
		}

		public override bool CanRightClick() {
			return true;
		}

		public override void OpenBossBag(Player player) {
			player.TryGettingDevArmor();
			if (Main.rand.NextBool(7)) {
				player.QuickSpawnItem(ItemType<CelestialConstructMask>());
			}
			int itemtype = Main.rand.Next(new int[] { ItemID.SolarEruption, ItemID.DayBreak, ItemID.VortexBeater, ItemID.Phantasm, ItemID.NebulaArcanum, ItemID.NebulaBlaze, ItemID.StardustCellStaff, ItemID.StardustDragonStaff });

			player.QuickSpawnItem(itemtype);
		}

		public override int BossBagNPC => NPCType<Npcs.Celestialminiboss.CelestialMiniboss>();
	}
}