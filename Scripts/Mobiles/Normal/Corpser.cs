using System;
using Server.Items;
using Server.Spells;

namespace Server.Mobiles
{
    [CorpseName("a corpser corpse")]
    public class Corpser : BaseCreature
    {
        [Constructable]
        public Corpser()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 4, 0.2, 0.4)
        {
            this.Name = "planta carnivora";
            this.Body = 8;
            this.BaseSoundID = 684;

            this.SetStr(156, 180);
            this.SetDex(120, 180);
            this.SetInt(26, 40);

            this.SetHits(94, 108);
            this.SetMana(0);

            this.SetDamage(3, 15);
            this.SetDamageType(ResistanceType.Physical, 60);
            this.SetDamageType(ResistanceType.Poison, 40);

            this.SetResistance(ResistanceType.Physical, 15, 20);
            this.SetResistance(ResistanceType.Fire, 15, 25);
            this.SetResistance(ResistanceType.Cold, 10, 20);
            this.SetResistance(ResistanceType.Poison, 20, 30);

            this.SetSkill(SkillName.ResistenciaMagica, 15.1, 20.0);
            this.SetSkill(SkillName.Anatomia, 45.1, 60.0);
            this.SetSkill(SkillName.Briga, 75.1, 90.0);

            this.Fame = 1000;
            this.Karma = -1000;

            this.VirtualArmor = 8;

            if (0.25 > Utility.RandomDouble())
                this.PackItem(new Board(10));
            else
                this.PackItem(new Log(5));

            this.PackItem(new MandrakeRoot(5));
            this.PackItem(new Ginseng(5));
        }



        public Corpser(Serial serial)
            : base(serial)
        {
        }

        public override Poison PoisonImmune
        {
            get
            {
                return Poison.Lesser;
            }
        }
        public override bool DisallowAllMoves
        {
            get
            {
                return true;
            }
        }

        public override void OnThink()
        {
            var dist = ((Mobile)this.Combatant).GetDistanceToSqrt(this.Location);
            if (this.Combatant != null && this.Combatant is Mobile && dist < 9)
            {
                if (dist <= 2)
                    return;

                if (!this.IsCooldown("omnoma"))
                {
                    this.SetCooldown("omnoma", TimeSpan.FromSeconds(1.5));
                }
                else
                {
                    return;
                }

                if (!this.InLOS(this.Combatant))
                {
                    return;
                }

                var defender = (Mobile)this.Combatant;
                if (defender == null || defender.Map == null || !defender.Alive)
                    return;

                SpellHelper.Turn(this, defender);
                var locPlayerGo = GetPoint(defender, this.Direction);
                if (defender.Map.CanFit(locPlayerGo, locPlayerGo.Z))
                {
                   // this.PlayAttackAnimation();
                    this.MovingParticles(defender, 0x0D3B, 11, 0, false, false, 9502, 4019, 0x160);
                    Timer.DelayCall(TimeSpan.FromMilliseconds(400), () =>
                    {
                        defender.Freeze(TimeSpan.FromMilliseconds(600));
                        Timer.DelayCall(TimeSpan.FromMilliseconds(400), () =>
                        {
                            defender.MovingParticles(this, 0x0D3B, 15, 0, false, false, 9502, 4019, 0x160);
                            defender.SendMessage("A planta carnivora te puxa");
                            defender.MoveToWorld(locPlayerGo, defender.Map);
                            if (!this.IsCooldown("omnom"))
                            {
                                this.SetCooldown("omnom", TimeSpan.FromSeconds(10));
                                //this.OverheadMessage("* Nhom nom nom *");
                            }
                        });
                    });

                }
            }
            base.OnThink();
        }

        public override void OnGaveMeleeAttack(Mobile defender)
        {

        }

        public static Point3D GetPoint(Mobile m, Direction d)
        {
            var loc = new Point3D(m.Location);
            var x = 0;
            var y = 0;
            switch (d & Direction.Mask)
            {
                case Direction.North:
                    --y;
                    break;
                case Direction.Right:
                    ++x;
                    --y;
                    break;
                case Direction.East:
                    ++x;
                    break;
                case Direction.Down:
                    ++x;
                    ++y;
                    break;
                case Direction.South:
                    ++y;
                    break;
                case Direction.Left:
                    --x;
                    ++y;
                    break;
                case Direction.West:
                    --x;
                    break;
                case Direction.Up:
                    --x;
                    --y;
                    break;
            }
            loc.X -= x * 2;
            loc.Y -= y * 2;
            return loc;
        }

        public override void GenerateLoot()
        {
            this.AddLoot(LootPack.Meager, 2);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            if (this.BaseSoundID == 352)
                this.BaseSoundID = 684;
        }
    }


}
