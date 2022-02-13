using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;
using SpiritsOfTheForgotten;

namespace SpiritsOfTheForgotten.Items.Armor.CelestialArmor
{
    [AutoloadEquip(EquipType.Body)]
    public class CelestialBattleplate : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Celestial Battleplate");
            Tooltip.SetDefault(@"16% increased damage
6% increased crit chance
Grants minor life regeneration
Enemies are more likely to target you
20% chance not to consume ammo
Increases max number of minions by 3
Increases max number of sentries by 2");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.rare = ItemRarityID.Red;
            item.value = Item.sellPrice(0, 20);
            item.defense = 36;
        }

        public override void UpdateEquip(Player player)
        {
            SotfPlayer modPlayer = player.GetModPlayer<SotfPlayer>();

            Helper.Players.AllDamageUp(player, .16f);
            Helper.Players.AllCritUp(player, 6);
            player.lifeRegen += 3;
            player.aggro += 50;
            modPlayer.AmmoConsumeReduction += .20f;
            player.maxMinions += 3;
            player.maxTurrets += 2;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach(TooltipLine line in tooltips)
            {
                if (line.Name == "ItemName")
                {
                    line.overrideColor = Helper.Colors.Celestial();
                }
            }
        }
    }
}