using Terraria.ID;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using SpiritsOfTheForgotten.Projectiles.WeaponProj;

namespace SpiritsOfTheForgotten.Items.Weapons
{
    class ShardThrower:ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shard Thrower");
            Tooltip.SetDefault("It shines bright");
        }

        public override void SetDefaults()
        {
            item.crit = 10;
            item.damage = 10;
            item.knockBack = 0;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.useAnimation = 30;
            item.reuseDelay = 2;
            item.width = 64;
            item.height = 20;
            item.ranged = true;
            item.noMelee = true;
            item.useAmmo = AmmoID.Gel;
            item.rare = ItemRarityID.Blue;
            item.UseSound = SoundID.Item34;
            item.shoot = ModContent.ProjectileType<ShardFlame>();
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

        public override bool ConsumeAmmo(Player player)
        {// Because of how the game works, player.itemAnimation will be 11, 7, and finally 3. (UseAmination - 1, then - useTime until less than 0.) 
         // We can get the Clockwork Assault Riffle Effect by not consuming ammo when itemAnimation is lower than the first shot.
            return !(player.itemAnimation < item.useAnimation-2);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-7, 0);
        }
    }
}
