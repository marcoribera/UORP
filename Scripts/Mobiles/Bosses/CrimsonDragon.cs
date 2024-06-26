using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a crimson dragon corpse")]
    public class CrimsonDragon : BasePeerless
    {
        public override bool GiveMLSpecial { get { return false; } }

        private DateTime m_NextTerror;
        [Constructable]
        public CrimsonDragon()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a crimson dragon";
            Body = 197;

            BaseSoundID = 362;
            SetStr(2034, 2140);
            SetDex(215, 256);
            SetInt(1025, 1116);

            SetHits(25000);

            SetDamage(8, 10);

            SetDamageType(ResistanceType.Physical, 50);
            SetDamageType(ResistanceType.Fire, 50);

            SetResistance(ResistanceType.Physical, 80, 85);
            SetResistance(ResistanceType.Fire, 100);
            SetResistance(ResistanceType.Cold, 50, 60);
            SetResistance(ResistanceType.Poison, 80, 85);
            SetResistance(ResistanceType.Energy, 80, 85);

            SetSkill(SkillName.PoderMagico, 110.2, 125.3);
            SetSkill(SkillName.Arcanismo, 110.9, 125.5);
            SetSkill(SkillName.ResistenciaMagica, 116.3, 125.0);
            SetSkill(SkillName.Anatomia, 111.7, 126.3);
            SetSkill(SkillName.Briga, 120.5, 128.0);
            SetSkill(SkillName.Anatomia, 118.7, 125.0);
            SetSkill(SkillName.Percepcao, 120.0);

            // ingredients
            PackResources(8);

            Fame = 20000;
            Karma = -20000;

            VirtualArmor = 70;

            SetSpecialAbility(SpecialAbility.DragonBreath);
        }

        public CrimsonDragon(Serial serial)
            : base(serial)
        {
        }

        public override bool AlwaysMurderer
        {
            get
            {
                return true;
            }
        }
        public override bool Unprovokable
        {
            get
            {
                return true;
            }
        }
        public override bool BardImmune
        {
            get
            {
                return true;
            }
        }

        public override bool ReacquireOnMovement
        {
            get
            {
                return true;
            }
        }
        public override bool AutoDispel
        {
            get
            {
                return true;
            }
        }
        public override bool Uncalmable
        {
            get
            {
                return true;
            }
        }
        public override int Meat
        {
            get
            {
                return 19;
            }
        }
        public override int Hides
        {
            get
            {
                return 40;
            }
        }
        public override HideType HideType
        {
            get
            {
                return HideType.Barbed;
            }
        }
        public override int Scales
        {
            get
            {
                return 12;
            }
        }
        public override ScaleType ScaleType
        {
            get
            {
                return (ScaleType)Utility.Random(4);
            }
        }
        public override FoodType FavoriteFood
        {
            get
            {
                return FoodType.Meat;
            }
        }
        public override Poison PoisonImmune
        {
            get
            {
                return Poison.Lethal;
            }
        }
        public override Poison HitPoison
        {
            get
            {
                return Utility.RandomBool() ? Poison.Deadly : Poison.Lethal;
            }
        }
        public override int TreasureMapLevel
        {
            get
            {
                return 5;
            }
        }
        public override void GenerateLoot()
        {
            AddLoot(LootPack.AosSuperBoss, 8);
            AddLoot(LootPack.Gems, 12);
        }

        public override int GetIdleSound()
        {
            return 0x2D3;
        }

        public override int GetHurtSound()
        {
            return 0x2D1;
        }

        public override void OnDamagedBySpell(Mobile caster)
        {
            if (Map != null && caster != this && 0.50 > Utility.RandomDouble())
            {
                Map = caster.Map;
                Location = caster.Location;
                Combatant = caster;
                Effects.PlaySound(Location, Map, 0x1FE);
            }

            base.OnDamagedBySpell(caster);
        }

        public override void OnMovement(Mobile m, Point3D oldLocation)
        {
            base.OnMovement(m, oldLocation);

            if (m_NextTerror < DateTime.UtcNow && m != null && InRange(m.Location, 3) && m.IsPlayer())
            {
                m.Frozen = true;
                m.SendLocalizedMessage(1080342, Name, 33); // Terror slices into your very being, destroying any chance of resisting ~1_name~ you might have had

                Timer.DelayCall(TimeSpan.FromSeconds(5), new TimerStateCallback(Terrorize), m);
            }
        }

        public override void OnGotMeleeAttack(Mobile attacker)
        {
            if (Map != null && attacker != this && 0.1 > Utility.RandomDouble())
            {
                if (attacker is BaseCreature)
                {
                    BaseCreature pet = (BaseCreature)attacker;
                    if (pet.ControlMaster != null && (attacker is Dragon || attacker is GreaterDragon || attacker is SkeletalDragon || attacker is WhiteWyrm || attacker is Drake))
                    {
                        Combatant = null;
                        pet.Combatant = null;
                        Combatant = null;
                        pet.ControlMaster = null;
                        pet.Controlled = false;
                        attacker.Emote(String.Format("* {0} decided to go wild *", attacker.Name));
                    }

                    if (pet.ControlMaster != null && 0.1 > Utility.RandomDouble())
                    {
                        Combatant = null;
                        pet.Combatant = pet.ControlMaster;
                        Combatant = null;
                        attacker.Emote(String.Format("* {0} is being angered *", attacker.Name));
                    }
                }
            }

            base.OnGotMeleeAttack(attacker);
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
        }

        public override bool OnBeforeDeath()
        {
            Hue = 16385;

            if (!NoKillAwards)
            {
                Map map = Map;

                if (map != null)
                {
                    for (int x = -7; x <= 7; ++x)
                    {
                        for (int y = -7; y <= 3; ++y)
                        {
                            double dist = Math.Sqrt(x * x + y * y);

                            if (dist <= 12)
                                new GoodiesTimer(map, X + x, Y + y).Start();
                        }
                    }
                }
            }

            return base.OnBeforeDeath();
        }

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            if (Utility.RandomDouble() < 0.6)
                c.DropItem(new ParrotItem());

            if (Utility.RandomDouble() < 0.025)
                c.DropItem(new CrimsonCincture());
        }

        private void Terrorize(object o)
        {
            if (o is Mobile)
            {
                Mobile m = (Mobile)o;

                m.Frozen = false;
                m.SendLocalizedMessage(1005603); // You can move again!

                m_NextTerror = DateTime.UtcNow + TimeSpan.FromMinutes(5);
            }
        }

        private class GoodiesTimer : Timer
        {
            private readonly Map m_Map;
            private readonly int m_X;
            private readonly int m_Y;
            public GoodiesTimer(Map map, int x, int y)
                : base(TimeSpan.FromSeconds(Utility.RandomDouble() * 10.0))
            {
                m_Map = map;
                m_X = x;
                m_Y = y;
            }

            protected override void OnTick()
            {
                int z = m_Map.GetAverageZ(m_X, m_Y);
                bool canFit = m_Map.CanFit(m_X, m_Y, z, 6, false, false);

                for (int i = -3; !canFit && i <= 3; ++i)
                {
                    canFit = m_Map.CanFit(m_X, m_Y, z + i, 6, false, false);

                    if (canFit)
                        z += i;
                }

                if (!canFit)
                    return;

                Gold g = new Gold(300, 500);

                g.MoveToWorld(new Point3D(m_X, m_Y, z), m_Map);

                if (0.5 >= Utility.RandomDouble())
                {
                    switch (Utility.Random(3))
                    {
                        case 0: // Fire column
                            {
                                Effects.SendLocationParticles(EffectItem.Create(g.Location, g.Map, EffectItem.DefaultDuration), 0x3709, 10, 30, 5052);
                                Effects.PlaySound(g, g.Map, 0x208);

                                break;
                            }
                        case 1: // Explosion
                            {
                                Effects.SendLocationParticles(EffectItem.Create(g.Location, g.Map, EffectItem.DefaultDuration), 0x36BD, 20, 10, 5044);
                                Effects.PlaySound(g, g.Map, 0x307);

                                break;
                            }
                        case 2: // Ball of fire
                            {
                                Effects.SendLocationParticles(EffectItem.Create(g.Location, g.Map, EffectItem.DefaultDuration), 0x36FE, 10, 10, 5052);

                                break;
                            }
                    }
                }
            }
        }
    }
}
