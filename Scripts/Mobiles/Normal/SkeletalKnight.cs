using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a skeletal corpse")]
    public class SkeletalKnight : BaseCreature
    {
        [Constructable]
        public SkeletalKnight()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "cavaleiro esqueleto";
            Body = 147;
            BaseSoundID = 451;

            SetStr(196, 250);
            SetDex(76, 95);
            SetInt(36, 60);

            SetHits(118, 150);

            SetDamage(8, 18);

            SetDamageType(ResistanceType.Physical, 40);
            SetDamageType(ResistanceType.Cold, 60);

            SetResistance(ResistanceType.Physical, 35, 45);
            SetResistance(ResistanceType.Fire, 20, 30);
            SetResistance(ResistanceType.Cold, 50, 60);
            SetResistance(ResistanceType.Poison, 20, 30);
            SetResistance(ResistanceType.Energy, 30, 40);

            SetSkill(SkillName.ResistenciaMagica, 65.1, 80.0);
            SetSkill(SkillName.Anatomia, 85.1, 100.0);
            SetSkill(SkillName.Briga, 85.1, 95.0);

            Fame = 3000;
            Karma = -3000;

            VirtualArmor = 40;
            PackItem(new Bone());
            switch ( Utility.Random(6) )
            {
                case 0:
                    PackItem(new PlateArms());
                    break;
                case 1:
                    PackItem(new PlateChest());
                    break;
                case 2:
                    PackItem(new PlateGloves());
                    break;
                case 3:
                    PackItem(new PlateGorget());
                    break;
                case 4:
                    PackItem(new PlateLegs());
                    break;
                case 5:
                    PackItem(new PlateHelm());
                    break;
            }

            PackItem(new Scimitar());
            PackItem(new WoodenShield());
        }

        public SkeletalKnight(Serial serial)
            : base(serial)
        {
        }

        public override bool BleedImmune
        {
            get
            {
                return true;
            }
        }

        public override void OnThink()
        {
            base.OnThink();
            //Console.WriteLine("TICK " + this.Aggressors == null);
            if (this.Combatant != null)
            {
                if (!IsCooldown("bonethrow"))
                {
                    if (this.Combatant is PlayerMobile)
                    {
                        var player = (PlayerMobile)this.Combatant;
                        if (player.GetDistanceToSqrt(this.Location) <= 3 || !this.InLOS(player))
                        {
                            return;
                        }
                        SetCooldown("bonethrow", TimeSpan.FromSeconds(6));
                        this.MovingParticles(player, 0xF7E, 9, 0, false, false, 9502, 4019, 0x160);
                        AOS.Damage(player, 2 + Utility.Random(5), 0, 0, 0, 0, 0);
                        PublicOverheadMessage(Network.MessageType.Emote, 0, false, "* joga um osso *");
                    }


                }
            }
        }

        public override TribeType Tribe { get { return TribeType.Undead; } }

        public override OppositionGroup OppositionGroup
        {
            get
            {
                return OppositionGroup.FeyAndUndead;
            }
        }
        public override void GenerateLoot()
        {
            AddLoot(LootPack.Average);
            AddLoot(LootPack.Meager);
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
