﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Core;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using System;
using System.Collections.Generic;
using System.Linq;

namespace StarlightRiver.Content.Items.SteampunkSet
{
    public class JetwelderGatler : ModProjectile
    {
        public override string Texture => AssetDirectory.SteampunkItem + Name;

        private readonly int ATTACKRANGE = 500;
        private readonly int MINATTACKRANGE = 150;
        private readonly float SPEED = 15f;
        private readonly float IDLESPEED = 8f;

        private NPC target;

        private Vector2 posToBe = Vector2.Zero;

        private float rotationGoal;
        private float currentRotation;

        private int bulletCounter = 0;

        private Player player => Main.player[projectile.owner];

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lunazoa");
            ProjectileID.Sets.Homing[projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
        }

        public override void SetDefaults()
        {
            projectile.aiStyle = -1;
            projectile.width = 60;
            projectile.height = 56;
            projectile.friendly = false;
            projectile.tileCollide = false;
            projectile.hostile = false;
            projectile.minion = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 1200;
        }

        public override void AI()
        {
            NPC testtarget = Main.npc.Where(n => n.CanBeChasedBy(projectile, false) && Vector2.Distance(n.Center, projectile.Center) < 800 && findPosToBe(n).Length() >= 60).OrderBy(n => Vector2.Distance(n.Center, Main.MouseWorld)).FirstOrDefault();

            if (testtarget != default)
            {
                target = testtarget;
                AttackMovement();
            }
            else
                IdleMovement();
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D tex = ModContent.GetTexture(Texture);
            SpriteEffects spriteEffects = projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(tex, projectile.Center - Main.screenPosition, null, lightColor, projectile.rotation, tex.Size() / 2, projectile.scale, spriteEffects, 0f);
            return false;
        }

        private void IdleMovement()
        {
            posToBe = Vector2.Zero;

            Vector2 direction = Vector2.Normalize(projectile.DirectionTo(player.Center)) * IDLESPEED;
            projectile.velocity = Vector2.Lerp(projectile.velocity, direction, 0.05f);
        }

        private void AttackMovement()
        {
            if (posToBe == Vector2.Zero || !ClearPath(target.Center + posToBe, target.Center))
            {
                posToBe = findPosToBe(target);
            }
            if (posToBe.Length() < 60)
            {
                IdleMovement();
                return;
            }

            Vector2 direction = (posToBe + target.Center) - projectile.Center;

            Vector2 towardsTarget = target.Center - projectile.Center;

            rotationGoal = towardsTarget.ToRotation();
            float rotDifference = ((((rotationGoal - currentRotation) % 6.28f) + 9.42f) % 6.28f) - 3.14f;
            currentRotation += Math.Sign(rotDifference) * 0.1f;

            projectile.rotation = currentRotation;

            if (Math.Abs(rotDifference) < 0.15f)
            {
                projectile.rotation = rotationGoal;
            }

            if (projectile.rotation.ToRotationVector2().X < 0)
            {
                projectile.spriteDirection = -1;
                projectile.rotation += 3.14f;
            }
            else
                projectile.spriteDirection = 1;


            if (direction.Length() < 250 || Math.Abs(currentRotation - direction.ToRotation()) < 0.3f)
            {
                FireBullets();
            }
            float speed = (float)Math.Min(SPEED, Math.Sqrt(direction.Length() * 0.1f));
            projectile.velocity = Vector2.Lerp(projectile.velocity, Vector2.Normalize(direction) * speed, 0.2f);
        }

        private void FireBullets()
        {
            bulletCounter++;
            if (bulletCounter % 5 == 0)
            {
                Vector2 bulletOffset = new Vector2(20, 9 * projectile.spriteDirection);

                Vector2 dir = currentRotation.ToRotationVector2();
                dir.Normalize();
                bulletOffset = bulletOffset.RotatedBy(currentRotation);

                Projectile.NewProjectile(projectile.Center + bulletOffset, dir.RotatedByRandom(0.13f) * 15, ProjectileID.Bullet, projectile.damage, projectile.knockBack, player.whoAmI);
            }
        }

        private bool ClearPath(Vector2 point1, Vector2 point2)
        {
            Vector2 direction = point2 - point1;
            for (int i = 0; i < direction.Length(); i += 8)
            {
                Vector2 toLookAt = point1 + (Vector2.Normalize(direction) * i);
                if ((Framing.GetTileSafely((int)(toLookAt.X / 16), (int)(toLookAt.Y / 16)).active() && Main.tileSolid[Framing.GetTileSafely((int)(toLookAt.X / 16), (int)(toLookAt.Y / 16)).type]))
                {
                    return false;
                }
            }
            return true;
        }


        private Vector2 findPosToBe(NPC tempTarget)
        {
            Vector2 ret = Vector2.Zero;
            int tries = 0;
            while (tries < 99)
            {
                tries++;
                float angle = Main.rand.NextFloat(-3.14f, 0f);
                if (angle > -2.355f && angle < -0.785)
                    continue;
                for (int i = 0; i < ATTACKRANGE; i += 8)
                {
                    Vector2 toLookAt = tempTarget.Center + (angle.ToRotationVector2() * i);
                    if (i > ATTACKRANGE - 16 || (Framing.GetTileSafely((int)(toLookAt.X / 16), (int)(toLookAt.Y / 16)).active() && Main.tileSolid[Framing.GetTileSafely((int)(toLookAt.X / 16), (int)(toLookAt.Y / 16)).type]))
                    {
                        ret = (angle.ToRotationVector2() * i * 0.75f);

                        if (i > MINATTACKRANGE)
                            tries = 100;
                        break;
                    }
                }
            }
            return ret;
        }
    }
}