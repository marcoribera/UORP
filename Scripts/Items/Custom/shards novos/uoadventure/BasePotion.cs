using System;
using System.Collections.Generic;
using Server.Engines.Craft;

namespace Server.Items
{
    public enum PotionEffect
    {
        Nightsight,
        CureLesser,
        Cure,
        CureGreater,
        Agility,
        AgilityGreater,
        Strength,
        StrengthGreater,
        PoisonLesser,
        Poison,
        PoisonGreater,
        PoisonDeadly,
        Refresh,
        RefreshTotal,
        HealLesser,
        Heal,
        HealGreater,
        ExplosionLesser,
        Explosion,
        ExplosionGreater,
        Conflagration,
        ConflagrationGreater,
        MaskOfDeath,		// Mask of Death is not available in OSI but does exist in cliloc files
        MaskOfDeathGreater,	// included in enumeration for compatability if later enabled by OSI
        ConfusionBlast,
        ConfusionBlastGreater,
        Invisibility,
        Parasitic,
        Darkglow,
        ExplodingTarPotion,
        #region TOL Publish 93
        Barrab,
        Jukari,
        Kurak,
        Barako,
        Urali,
        Sakkhra,
        #endregion,
        Shatter,
        FearEssence
    }

    public abstract class BasePotion : Item, ICraftable, ICommodity
    {
        private PotionEffect m_PotionEffect;

        public PotionEffect PotionEffect
        {
            get
            {
                return this.m_PotionEffect;
            }
            set
            {
                this.m_PotionEffect = value;
                this.InvalidateProperties();
            }
        }

        private int m_Original_ItemID;
        private bool m_Identified;

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Identified
        {
            get
            {
                return m_Identified;
            }
            set
            {
                m_Identified = value;
                InvalidateProperties();
            }
        }

        TextDefinition ICommodity.Description
        {
            get
            {
                return this.LabelNumber;
            }
        }
        bool ICommodity.IsDeedable
        {
            get
            {
                return (Core.ML);
            }
        }

        public override int LabelNumber
        {
            get
            {
                if (m_Identified)
                {
                    return 1041314 + (int)this.m_PotionEffect;
                }
                else
                {
                    return 1038000; // Não Identificado
                }
            }
        }

        public BasePotion(int itemID, PotionEffect effect)
            : base(3849)
        {
            this.m_Original_ItemID = itemID; //Imagem da White Potion
            this.m_PotionEffect = effect;

            this.Stackable = Core.ML;
            this.Weight = 1.0;
            this.Hue = 2050;
        }

        public BasePotion(Serial serial)
            : base(serial)
        {
        }

        public void Revelar_Original_ItemID()
        {
            this.ItemID = this.m_Original_ItemID;
        }



        public virtual bool RequireFreeHand
        {
            get
            {
                return true;
            }
        }

        public static bool HasFreeHand(Mobile m)
        {
            Item handOne = m.FindItemOnLayer(Layer.OneHanded);
            Item handTwo = m.FindItemOnLayer(Layer.TwoHanded);

            if (handTwo is BaseWeapon)
                handOne = handTwo;
            if (handTwo is BaseWeapon)
            {
                BaseWeapon wep = (BaseWeapon)handTwo;

                if (wep.Attributes.BalancedWeapon > 0)
                    return true;
            }

            return (handOne == null || handTwo == null);
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!this.Movable)
                return;

            if (!from.BeginAction(this.GetType()))
            {
                from.SendLocalizedMessage(500119); // You must wait to perform another action.
                return;
            }

            Timer.DelayCall(TimeSpan.FromMilliseconds(500), () => from.EndAction(this.GetType()));

            if (from.InRange(this.GetWorldLocation(), 1))
            {
                if (!this.RequireFreeHand || HasFreeHand(from))
                {
                    if (this is BaseExplosionPotion && this.Amount > 1)
                    {
                        BasePotion pot = (BasePotion)Activator.CreateInstance(this.GetType());

                        if (pot != null)
                        {
                            this.Amount--;

                            if (from.Backpack != null && !from.Backpack.Deleted)
                            {
                                from.Backpack.DropItem(pot);
                            }
                            else
                            {
                                pot.MoveToWorld(from.Location, from.Map);
                            }
                            pot.Drink(from);
                        }
                    }
                    else
                    {
                        this.Drink(from);
                    }
                }
                else
                {
                    from.SendLocalizedMessage(502172); // You must have a free hand to drink a potion.
                }
            }
            else
            {
                from.SendLocalizedMessage(502138); // That is too far away for you to use
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)3); // version
            writer.Write((int)m_Original_ItemID);
            writer.Write((bool)m_Identified);
            writer.Write((int)this.m_PotionEffect);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 3:
                    this.m_Original_ItemID = reader.ReadInt();
                    goto case 2;
                case 2:
                    {
                        this.m_Identified = reader.ReadBool();
                        goto case 1;
                    }
                case 1:
                case 0:
                    {
                        this.m_PotionEffect = (PotionEffect)reader.ReadInt();
                        break;
                    }
            }

            if (version == 0)
                this.Stackable = Core.ML;
        }

        public abstract void Drink(Mobile from);

        public static void PlayDrinkEffect(Mobile m)
        {
            m.RevealingAction();
            m.PlaySound(0x2D6);
            m.AddToBackpack(new Bottle());

            if (m.Body.IsHuman && !m.Mounted)
            {
                if (Core.SA)
                {
                    m.Animate(AnimationType.Eat, 0);
                }
                else
                {
                    m.Animate(34, 5, 1, true, false, 0);
                }
            }
        }

        public static int EnhancePotions(Mobile m)
        {
            int EP = AosAttributes.GetValue(m, AosAttribute.EnhancePotions);
            int skillBonus = m.Skills.Alquimia.Fixed / 330 * 10;

            if (Core.ML && EP > 50 && m.IsPlayer())
                EP = 50;

            return (EP + skillBonus);
        }

        public static TimeSpan Scale(Mobile m, TimeSpan v)
        {
            if (!Core.AOS)
                return v;

            double scalar = 1.0 + (0.01 * EnhancePotions(m));

            return TimeSpan.FromSeconds(v.TotalSeconds * scalar);
        }

        public static double Scale(Mobile m, double v)
        {
            if (!Core.AOS)
                return v;

            double scalar = 1.0 + (0.01 * EnhancePotions(m));

            return v * scalar;
        }

        public static int Scale(Mobile m, int v)
        {
            if (!Core.AOS)
                return v;

            return AOS.Scale(v, 100 + EnhancePotions(m));
        }

        public override bool WillStack(Mobile from, Item dropped)
        {
            //TODO: Fazer ela só ser stackeavel quando craftada pela pessoa
            return dropped is BasePotion && ((BasePotion)dropped).m_PotionEffect == this.m_PotionEffect && base.WillStack(from, dropped) && (this.Hue == ((BasePotion)dropped).Hue); //&& Identified;
        }

        #region ICraftable Members

        public int OnCraft(int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes, ITool tool, CraftItem craftItem, int resHue)
        {
            if (craftSystem is DefAlchemy)
            {
                Container pack = from.Backpack;

                if (pack != null)
                {
                    if ((int)this.PotionEffect >= (int)PotionEffect.Invisibility)
                        return 1;

                    List<PotionKeg> kegs = pack.FindItemsByType<PotionKeg>();

                    for (int i = 0; i < kegs.Count; ++i)
                    {
                        PotionKeg keg = kegs[i];

                        if (keg == null)
                            continue;

                        if (keg.Held <= 0 || keg.Held >= 100)
                            continue;

                        if (keg.Type != this.PotionEffect)
                            continue;

                        ++keg.Held;

                        this.Consume();
                        from.AddToBackpack(new Bottle());

                        return -1; // signal placed in keg
                    }
                }
            }

            return 1;
        }
        #endregion
    }
}