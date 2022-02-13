using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;
using SpiritsOfTheForgotten;

namespace SpiritsOfTheForgotten.Items.Armor.CelestialArmor
{
    [AutoloadEquip(EquipType.Legs)]
    public class CelestialLeggings : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Celestial Leggings");
            Tooltip.SetDefault(@"10% increased damage
10% increased movement speed and melee speed
Increases ax number of minions by 1
Increases max number of sentries by 1");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.rare = ItemRarityID.Red;
            item.value = Item.sellPrice(0, 14);
            item.defense = 24;
        }

        public override void UpdateEquip(Player player)
        {
            Helper.Players.AllDamageUp(player, .1f);
            player.meleeSpeed += .1f;
            player.maxMinions += 1;
            player.maxTurrets += 1;
            player.moveSpeed += 0.1f;
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