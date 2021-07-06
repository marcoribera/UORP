using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("Corpo de Orc")]
    public class OrcCampeao : BaseCreature
    {
        [Constructable]
        public OrcCampeao()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Body = 1646;

            Name = "Campeao Orc";
            BaseSoundID = 0x45A;

            SetStr(150, 190);
            SetDex(100, 120);
            SetInt(40, 60);

            //SetHits(476, 552);

            SetDamage(10, 15);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 45, 55);
            SetResistance(ResistanceType.Fire, 40, 50);
            SetResistance(ResistanceType.Cold, 25, 35);
            SetResistance(ResistanceType.Poison, 25, 35);
            SetResistance(ResistanceType.Energy, 25, 35);

            SetSkill(SkillName.Contusivo, 85, 100.0);
            SetSkill(SkillName.ResistenciaMagica, 125.1, 140.0);
            SetSkill(SkillName.Anatomia, 90.1, 100.0);
            SetSkill(SkillName.Briga, 85, 100.0);
            SetSkill(SkillName.DuasMaos, 85, 100.0);
            SetSkill(SkillName.Cortante, 85, 100.0);
            SetSkill(SkillName.Bloqueio, 80.0, 100.0);

            Fame = 15000;
            Karma = -15000;

            VirtualArmor = 50;

            Item ore = new ShadowIronOre(25);
            ore.ItemID = 0x19B9;
            PackItem(ore);
            PackItem(new IronIngot(10));
             this.AddItem(new WarAxe());          
            if (0.05 > Utility.RandomDouble())
                PackItem(new OrcishKinMask());

            if (0.2 > Utility.RandomDouble())
                PackItem(new BolaBall());

            PackItem(new Yeast());
        }

        public OrcCampeao(Serial serial)
            : base(serial)
        {
        }

        public override bool BardImmune
        {
            get
            {
                return !Core.AOS;
            }
        }
        public override Poison PoisonImmune
        {
            get
            {
                return Poison.Lethal;
            }
        }
        public override int Meat
        {
            get
            {
                return 2;
            }
        }

        public override TribeType Tribe { get { return TribeType.Orc; } }

        public override OppositionGroup OppositionGroup
        {
            get
            {
                return OppositionGroup.SavagesAndOrcs;
            }
        }
        public override bool CanRummageCorpses
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
        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich);
            AddLoot(LootPack.Rich);
        }

        public override bool IsEnemy(Mobile m)
        {
            if (m.Player && m.FindItemOnLayer(Layer.Helm) is OrcishKinMask)
                return false;

            return base.IsEnemy(m);
        }

        public override void AggressiveAction(Mobile aggressor, bool criminal)
        {
            base.AggressiveAction(aggressor, criminal);

            Item item = aggressor.FindItemOnLayer(Layer.Helm);

            if (item is OrcishKinMask)
            {
                AOS.Damage(aggressor, 50, 0, 100, 0, 0, 0);
                item.Delete();
                aggressor.FixedParticles(0x36BD, 20, 10, 5044, EffectLayer.Head);
                aggressor.PlaySound(0x307);
            }
        }

        public override int Damage(int amount, Mobile from, bool informMount, bool checkDisrupt)
        {
            var damage = base.Damage(amount, from, informMount, checkDisrupt);

            if (from != null && from != this && !Controlled && !Summoned && Utility.RandomDouble() <= 0.2)
            {
                SpawnOrcLord(from);
            }

            return damage;
        }

        public void SpawnOrcLord(Mobile target)
        {
            Map map = target.Map;

            if (map == null)
                return;

            int orcs = 0;
            IPooledEnumerable eable = GetMobilesInRange(10);

            foreach (Mobile m in eable)
            {
                if (m is OrcishLord)
                    ++orcs;
            }

            eable.Free();

            if (orcs < 10)
            {
                BaseCreature orc = new SpawnedOrcishLord();

                orc.Team = Team;

                Point3D loc = target.Location;
                bool validLocation = false;

                for (int j = 0; !validLocation && j < 10; ++j)
                {
                    int x = target.X + Utility.Random(3) - 1;
                    int y = target.Y + Utility.Random(3) - 1;
                    int z = map.GetAverageZ(x, y);

                    if (validLocation = map.CanFit(x, y, Z, 16, false, false))
                        loc = new Point3D(x, y, Z);
                    else if (validLocation = map.CanFit(x, y, z, 16, false, false))
                        loc = new Point3D(x, y, z);
                }

                orc.MoveToWorld(loc, map);

                orc.Combatant = target;
            }
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
    }
}
