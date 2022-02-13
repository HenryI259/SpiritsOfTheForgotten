using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritsOfTheForgotten.Items.Armor.CelestialArmor
{
    [AutoloadEquip(EquipType.Head)]
    public class CelestialHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Celestial Helmet");
            Tooltip.SetDefault(@"15% increased damage
10% increased critical strike chance
Grants minor life regeneration
Enemies are more likely to target you
Increases maximum mana by 40 and reduces mana usage by 10%
Increases your max number of minions by 2
Increases your max number of sentries by 1");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.rare = ItemRarityID.Red;
            item.value = Item.sellPrice(0, 14);
            item.defense = 30;
        }

        public override void UpdateEquip(Player player)
        {
            Helper.Players.AllDamageUp(player, .15f);
            Helper.Players.AllCritUp(player, 10);
            player.lifeRegen += 3;
            player.manaCost -= .10f;
            player.statManaMax2 += 40;
            player.maxTurrets += 2;
            player.maxMinions += 1;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<CelestialBattleplate>() && legs.type == ModContent.ItemType<CelestialLeggings>();
        }

        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawOutlines = true;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = @"
10% increased damage
Summons a mini Celestial Construct that has different attacks based on what weapon you are holding
Melee - Summons 4 solar daggers that pause and charge at enemies
Ranged - Fires lightning to zap up to 5 nearby enemies
Magic - Summons a spread of stars when enemies are nearby
Summoner - Quickly fires stardust at enemies";
            Helper.Players.AllDamageUp(player, .10f);
            player.GetModPlayer<SotfPlayer>().CelestialSet = true;
            if (player.ownedProjectileCounts[ModContent.ProjectileType<Items.Armor.CelestialArmor.CelestialConstructMinion>()] < 1)
            {
                Projectile.NewProjectile(player.Center, Vector2.Zero, ModContent.ProjectileType<Items.Armor.CelestialArmor.CelestialConstructMinion >(), 220, 0f, player.whoAmI);
            }
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