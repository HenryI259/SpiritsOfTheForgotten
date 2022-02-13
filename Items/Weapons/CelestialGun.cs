using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using SpiritsOfTheForgotten.Projectiles.WeaponProj;

namespace SpiritsOfTheForgotten.Items.Weapons
{
    public class CelestialGun : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Heavenly Blaster");
            Tooltip.SetDefault("Right click to fire a shotgun blast");
        }
        public override void SetDefaults()
        {
            item.damage = 200;
            item.crit = 10;
            item.useTime = 20;
            item.useAnimation = 20;
            item.autoReuse = true;
            item.ranged = true;
            item.noMelee = true;
            item.useAmmo = AmmoID.Bullet;
            item.shootSpeed = 40;
            item.shoot = ProjectileID.Bullet;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.rare = ItemRarityID.Red;
            item.width = 66;
            item.height = 24;
            
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.damage = 500;
                item.useAnimation = 40;
                item.useTime = 40;
                item.UseSound = SoundID.Item115;
                item.shootSpeed = 10;
            }
            else
            {
                item.damage = 230;
                item.useAnimation = 10;
                item.useTime = 10;
                item.UseSound = SoundID.Item41;
                item.shootSpeed = 40;
            }
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            position = ShootOffset(position, 13, 45, speedX, speedY);

            if (player.altFunctionUse == 2)
            {
                type = ModContent.ProjectileType<WhiteBlast>();
                /*
                for (int i = 0; i < 500; i++)
                {
                    Vector2 speed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(180));
                    float scale = 1f - (Main.rand.NextFloat() * .3f);
                    speed *= scale;

                    Projectile.NewProjectile(player.Center, speed, type, damage, knockBack, player.whoAmI);
                }
                */
                
                int numberDusts = 100 + Main.rand.Next(50); 
                for (int i = 0; i < numberDusts; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(15)); // 7 degree spread.

                    float scale = 1f - (Main.rand.NextFloat() * .3f);
                    perturbedSpeed *= scale;
                    var a = Dust.NewDustDirect(position, 1, 1, DustID.PlatinumCoin, perturbedSpeed.X * 2f, perturbedSpeed.Y * 2f, 50, default(Color), 1f);
                    a.noGravity = true;
                }
                
               
            }
            

            return true;
        }


        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<CelestialShard>(), 1);
            recipe.AddIngredient(ItemID.PhoenixBlaster, 1);
            recipe.AddIngredient(ItemID.Uzi, 1);
            recipe.AddIngredient(ItemID.TacticalShotgun, 1);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public static Vector2 ShootOffset(Vector2 origin, float rotation, float distance, float SpeedX, float SpeedY)
        {
            float rotation1 = MathHelper.ToRadians(rotation) * -(Math.Sign(SpeedX));
            Vector2 direction = new Vector2(SpeedX, SpeedY);
            direction.Normalize();
            direction *= distance;
            bool flag = Collision.CanHit(origin, 0, 0, origin + direction, 0, 0);
            Vector2 newPosition = direction.RotatedBy(rotation1);
            if (!flag)
            {
                newPosition -= direction;
            }

            return new Vector2(origin.X + newPosition.X, origin.Y + newPosition.Y);
        }
    }
}
