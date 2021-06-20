using System;
using Server.Items;
using System.Collections.Generic;
using Server.Services;
using Server.Network;
using Server.Misc.Custom;
using Server.Scripts.Custom.Items;

namespace Server.Mobiles
{
    [CorpseName("um corpo despedaçado")]
    public class PesadeloElemental : BaseCreature
    {
        public override bool ReduceSpeedWithDamage { get { return false; } }

        public override bool AutoDispel { get { return true; } }

        [Constructable]
        public PesadeloElemental() : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "Elemental Corrompido";
            this.Body = 14;
            this.BaseSoundID = 268;

            Hue = 2500;

            SetStr(700, 700);
            SetDex(100, 100);
            SetInt(300, 350);

            SetHits(1000);
            SetStam(100, 100);

            SetDamage(10, 20);

            SetDamageType(ResistanceType.Physical, 75);
            SetDamageType(ResistanceType.Fire, 25);
            SetDamageType(ResistanceType.Energy, 50);

            SetResistance(ResistanceType.Physical, 75, 90);
            SetResistance(ResistanceType.Fire, 50, 65);
            SetResistance(ResistanceType.Cold, 45, 60);
            SetResistance(ResistanceType.Poison, 45, 60);
            SetResistance(ResistanceType.Energy, 45, 60);

            SetSkill(SkillName.PodeMagico, 25);
            SetSkill(SkillName.ResistenciaMagica, 150);
            SetSkill(SkillName.Briga, 75, 75);
            SetSkill(SkillName.Arcanismo, 70);
            SetSkill(SkillName.Envenenamento, 50);
            SetSkill(SkillName.Bloqueio, 100);
            SetSkill(SkillName.Anatomia, 120.0);
            SetSkill(SkillName.Medicina, 120.0);
            SetSkill(SkillName.Percepcao, 119.7, 128.9);

            Fame = 24000;
            Karma = -24000;

            VirtualArmor = 200;

            SetWeaponAbility(Habilidade.ConcussionBlow);
            AddItem(new Gold(1000));
            AddItem(new Granite());
            AddItem(new Rock1Rand());
            AddItem(new Rock2Rand());
            AddItem(new BronzeIngot(50));
          //  if(Utility.RandomBool())
            //    AddItem(new QuartzoIngot(80));
           // else
             //   AddItem(new BeriloIngot(80));

           // if(Utility.RandomDouble() < 0.15)
           // {
             //   if (Utility.RandomBool())
               //     AddItem(new AdamantiumIngot(30));
               // else
                 //   AddItem(new VibraniumIngot(30));
           // }
            //AddItem(Carnage.GetRandomPS(110));
        }

        public override bool CanBeParagon { get { return false; } }
        public override bool Unprovokable { get { return true; } }
        public virtual double ChangeCombatant { get { return 0.5; } }
        public override bool AlwaysMurderer { get { return true; } }
        public override Poison PoisonImmune { get { return Poison.Greater; } }
        public override int TreasureMapLevel { get { return 3; } }

       // public override bool OnBeforeDeath()
       // {
         //   foreach (var e in GetLootingRights())
           // {
             //   if (e.m_HasRight && e.m_Mobile != null)
               // {
            //        if(Utility.RandomDouble() < 0.1)
              //          e.m_Mobile.AddToBackpack(new ElementalBall());
                //    else
                  //      e.m_Mobile.AddToBackpack(Decos.RandomDeco());

                    //e.m_Mobile.AddToBackpack(Loot.JoiaRaraRandom(this.Map));

                   // e.m_Mobile.AddToBackpack(new Gold(10000));
                   // e.m_Mobile.SendMessage(78, "Voce ganhou recompensas por ajudar a matar Pedroso");
                   // e.m_Mobile.SendMessage(78, "As recompensas foram colocadas em sua mochila");
               // }
           // }

//            GolemMecanico.JorraOuro(this.Location, this.Map, 150);
  //          return base.OnBeforeDeath();
    //    }

      //  public override void OnDeath(Container c)
        //{
          //  base.OnDeath(c);
         //
           // c.DropItem(new DecoRelPor());
            //c.DropItem(new DecoRelPor());

//            var wind = new Kirin();
  //          wind.MoveToWorld(c.Location, c.Map);
    //        wind.OverheadMessage("* se transformou *");
      //      wind.OverheadMessage("[2H Para Domar]");
        //    Timer.DelayCall(TimeSpan.FromHours(2), () =>
          //  {
            //    if (wind.Deleted || !wind.Alive || wind.ControlMaster != null || wind.Map == Map.Internal)
              //  {
                //    return;
               // }
            //    wind.Delete();
          //  });
       // }

        public override void OnThink()
        {
            base.OnThink();
            var pl = Combatant as PlayerMobile;
            if (pl != null)
            {
                if (!IsCooldown("skill"))
                {
                    SetCooldown("skill", TimeSpan.FromSeconds(5));
                    Terremoto(pl);
                }

                if (Hue == 38 && !IsCooldown("nervoso"))
                {
                    SetCooldown("nervoso", TimeSpan.FromSeconds(10));
                    this.MovingParticles(pl, 6008, 5, 0, false, false, 9502, 4019, 0x160);
                    var dmg = 25 + Utility.Random(25);
                    AOS.Damage(pl, dmg, DamageType.Ranged);
                    this.PublicOverheadMessage(Network.MessageType.Emote, 0, false, "* joga uma pedra *");
                    BaseWeapon.AddBlood(pl, dmg);
                }
            }
        }

        private int casulos = 0;

        public void Nervoso(bool n)
        {
            if (n)
            {
                if (Hue == 2500)
                {
                    Hue = 38;
                    this.DamageMax += 10;
                    this.DamageMin += 10;
                    OverheadMessage("* nervoso *");
                }

            }
            else
            {
                if (Hue == 38)
                {
                    Hue = 2500;
                    this.DamageMax -= 10;
                    this.DamageMin -= 10;
                }
            }
        }

        public override void OnDamage(int amount, Mobile from, bool willKill)
        {
            if (from is PlayerMobile && Utility.RandomDouble() < 0.1)
            {
                var elem = new EarthElemental();
                elem.MoveToWorld(from.Location, from.Map);
                elem.OverheadMessage("* se levanta do chao *");
                elem.Combatant = from;
            }

            if (this.Hits < this.HitsMax / 3)
            {
                Nervoso(true);
            }
            if (casulos <= 1 && this.Hits < this.HitsMax / 10)
            {
                Casulo();
            }
            base.OnDamage(amount, from, willKill);
        }

        public void Casulo()
        {
            casulos++;
            Nervoso(false);
            this.Hidden = true;
            this.Blessed = true;
            this.CanMove = false;
            var casulo = new Item(0x1772);
            casulo.Movable = false;
            casulo.Name = "pedra suspeita";
            casulo.MoveToWorld(this.Location, this.Map);
            casulo.Hue = 2500;
            Effects.SendLocationParticles(EffectItem.Create(this.Location, this.Map, EffectItem.DefaultDuration), 0x3728, 10, 10, 2023);
            new CasuloTimer(this, Point3D.Zero, null, casulo).Start();
        }

        public class CasuloTimer : Timer
        {
            private BaseCreature bixo;
            private Point3D local;
            private Map map;
            private Item i;

            public CasuloTimer(BaseCreature bixo, Point3D local, Map map, Item item) : base(TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(2))
            {
                this.map = map;
                this.local = local;
                this.bixo = bixo;
                i = item;
            }

            protected override void OnTick()
            {
                if (bixo.Hits < bixo.HitsMax)
                {
                    var heal = bixo.HitsMax / 10;
                    bixo.Hits += heal;
                    i.PublicOverheadMessage(MessageType.Regular, 1145, false, "+" + heal);
                }
                else
                {
                    Effects.SendLocationParticles(EffectItem.Create(i.Location, i.Map, EffectItem.DefaultDuration), 0x3728, 10, 10, 2023);
                    i.Delete();
                    bixo.Hidden = false;
                    bixo.Blessed = false;
                    bixo.CanMove = true;
                    bixo.PlayAngerSound();
                    bixo.PlayAttackAnimation();
                    bixo.OverheadMessage( "Ataque de forma descontrolada" );
                    this.Stop();
                }
            }
        }

        public void Terremoto(Mobile alvo)
        {
            Effects.SendMovingParticles(this, new Entity(Serial.Zero, new Point3D(this.X, this.Y, this.Z + 20), this.Map), 0x11B6, 5, 20, true, true, 0, 0, 9502, 1, 0, (EffectLayer)255, 0x100);
            var pentagrama = new BloodyPentagramAddon();
            var loc = new Point3D(alvo.Location.X - 2, alvo.Location.Y - 2, alvo.Location.Z);
            pentagrama.MoveToWorld(loc, alvo.Map);

            //pentagrama.PublicOverheadMessage(Network.MessageType.Regular, 0, true, "* Tremidao *");
            new TerremotoTimer(this, alvo.Location, alvo.Map, pentagrama).Start();
        }

        public class TerremotoTimer : Timer
        {
            private BaseCreature bixo;
            private Point3D local;
            private Map map;
            private Item i;

            public TerremotoTimer(BaseCreature bixo, Point3D local, Map map, Item item) : base(TimeSpan.FromSeconds(2))
            {
                this.map = map;
                this.local = local;
                this.bixo = bixo;
                i = item;
            }

            protected override void OnTick()
            {
                var glr = map.GetClientsInRange(local, 3);
                foreach (var netstate in glr)
                {
                    var m = netstate.Mobile;
                    m.SendMessage("Voce sente o chão tremer...");
                    bixo.DoHarmful(m);
                    m.PlaySound(0x20D);
                    Effects.SendMovingParticles(new Entity(Serial.Zero, new Point3D(m.X, m.Y, m.Z + 20), m.Map), m, 0x11B6, 5, 20, true, true, 0, 0, 9502, 1, 0, (EffectLayer)255, 0x100);
                    Timer.DelayCall(TimeSpan.FromSeconds(0.6), () =>
                    {
                        AOS.Damage(m, 20 + Utility.Random(40), DamageType.SpellAOE);
                        m.Freeze(TimeSpan.FromSeconds(1));
                        m.OverheadMessage("* atordoado *");
                    });
                }
                glr.Free();
                i.Delete();
            }
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.OldRich, 2);
        }

        public override void OnDamagedBySpell(Mobile attacker)
        {
            base.OnDamagedBySpell(attacker);

        }

        public override void OnGaveMeleeAttack(Mobile defender)
        {
            base.OnGaveMeleeAttack(defender);  //if it hits you it spawns vortices

        }

        public override void OnGotMeleeAttack(Mobile attacker)
        {
            base.OnGotMeleeAttack(attacker); //if you hit creature it spawns vortices
        }

        public PesadeloElemental(Serial serial) : base(serial)
        {
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
