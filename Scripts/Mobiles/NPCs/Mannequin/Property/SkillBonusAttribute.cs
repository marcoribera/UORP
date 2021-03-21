using System;
using Server;
using Server.Items;
using System.Collections.Generic;

namespace Server.Mobiles.MannequinProperty
{
    public abstract class SkillBonusAttr : ValuedProperty
    {
        public override Catalog Catalog { get { return Catalog.SkillBonusGear; } }
        public override int Description { get { return 1159316; } } // Increases your skill points in a particular skill, up to, but not exceeding your cap in that skill.
        public abstract SkillName Skill { get; }
        public override int Hue { get { return 0x42FF; } }
        public override int SpriteW { get { return 270; } }
        public override int SpriteH { get { return 300; } }

        public double GetPropertyValue(Item item)
        {
            double value = 0;

            AosSkillBonuses skillbonuses = RunicReforging.GetAosSkillBonuses(item);

            if (skillbonuses != null)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (skillbonuses.GetValues(i, out SkillName check, out double bonus) && check == Skill && bonus > 0)
                    {
                        value += bonus;
                    }
                }
            }

            return value;
        }

        public override bool Matches(Item item)
        {
            Value = GetPropertyValue(item);

            if (Value != 0)
            {
                return true;
            }

            return false;
        }

        public override bool Matches(List<Item> items)
        {
            double total = 0;

            foreach (var item in items)
            {
                total += GetPropertyValue(item);
            }

            Value = total;

            if (Value != 0)
            {
                return true;
            }

            return false;
        }
    }    

    public class SwordsBonusProperty : SkillBonusAttr
    {
        public override int LabelNumber { get { return 1002151; } } // Swordsmanship
        public override SkillName Skill { get { return SkillName.Cortante; } }
    }

    public class FencingBonusProperty : SkillBonusAttr
    {
        public override int LabelNumber { get { return 1044102; } } // Fencing
        public override SkillName Skill { get { return SkillName.Perfurante; } }
    }

    public class MacingBonusProperty : SkillBonusAttr
    {
        public override int LabelNumber { get { return 1044101; } } // Mace Fighting
        public override SkillName Skill { get { return SkillName.Contusivo; } }
    }

    public class MageryBonusProperty : SkillBonusAttr
    {
        public override int LabelNumber { get { return 1002106; } } // Magery
        public override SkillName Skill { get { return SkillName.Arcanismo; } }
    }

    public class MusicianshipBonusProperty : SkillBonusAttr
    {
        public override int LabelNumber { get { return 1002116; } } // Musicianship
        public override SkillName Skill { get { return SkillName.Tocar; } }
    }

    public class WrestlingBonusProperty : SkillBonusAttr
    {
        public override int LabelNumber { get { return 1002169; } } // Wrestling
        public override SkillName Skill { get { return SkillName.Briga; } }
    }

    public class TacticsBonusProperty : SkillBonusAttr
    {
        public override int LabelNumber { get { return 1017321; } } // Tactics
        public override SkillName Skill { get { return SkillName.Anatomia; } }
    }

    public class AnimalTamingBonusProperty : SkillBonusAttr
    {
        public override int LabelNumber { get { return 1044095; } } // Animal Taming
        public override SkillName Skill { get { return SkillName.Adestramento; } }
    }

    public class ProvocationBonusProperty : SkillBonusAttr
    {
        public override int LabelNumber { get { return 1002125; } } // Provocation
        public override SkillName Skill { get { return SkillName.Provocacao; } }
    }

    public class SpiritSpeakBonusProperty : SkillBonusAttr
    {
        public override int LabelNumber { get { return 1002140; } } // Spirit Speak
        public override SkillName Skill { get { return SkillName.PoderMagico; } }
    }

    public class StealthBonusProperty : SkillBonusAttr
    {
        public override int LabelNumber { get { return 1044107; } } // Stealth
        public override SkillName Skill { get { return SkillName.Furtividade; } }
    }

    public class ParryBonusProperty : SkillBonusAttr
    {
        public override int LabelNumber { get { return 1002118; } } // Parrying
        public override SkillName Skill { get { return SkillName.Bloqueio; } }
    }

    public class MeditationBonusProperty : SkillBonusAttr
    {
        public override int LabelNumber { get { return 1044106; } } // Meditation
        public override SkillName Skill { get { return SkillName.Arcanismo; } } //Marcknight: Fazer escolher a maior dentre as skills de conhecimento mágico
    }

    public class AnimalLoreBonusProperty : SkillBonusAttr
    {
        public override int LabelNumber { get { return 1002007; } } // Animal Lore
        public override SkillName Skill { get { return SkillName.Adestramento; } }
    }

    public class DiscordanceBonusProperty : SkillBonusAttr
    {
        public override int LabelNumber { get { return 1044075; } } // Discordance
        public override SkillName Skill { get { return SkillName.Caos; } }
    }

    public class FocusBonusProperty : SkillBonusAttr
    {
        public override int LabelNumber { get { return 1044110; } } // Focus
        public override SkillName Skill { get { return SkillName.PreparoFisico; } }
    }

    public class StealingBonusProperty : SkillBonusAttr
    {
        public override int LabelNumber { get { return 1002142; } } // Stealing
        public override SkillName Skill { get { return SkillName.Prestidigitacao; } }
    }

    public class AnatomyBonusProperty : SkillBonusAttr
    {
        public override int LabelNumber { get { return 1002004; } } // Anatomy
        public override SkillName Skill { get { return SkillName.Anatomia; } }
    }

    public class EvalIntBonusProperty : SkillBonusAttr
    {
        public override int LabelNumber { get { return 1044076; } } // Eval Intelligence
        public override SkillName Skill { get { return SkillName.PoderMagico; } }
    }

    public class VeterinaryBonusProperty : SkillBonusAttr
    {
        public override int LabelNumber { get { return 1044099; } } // Veterinary
        public override SkillName Skill { get { return SkillName.Veterinaria; } }
    }

    public class NecromancyBonusProperty : SkillBonusAttr
    {
        public override int LabelNumber { get { return 1044109; } } // Necromancy
        public override SkillName Skill { get { return SkillName.Necromancia; } }
    }

    public class BushidoBonusProperty : SkillBonusAttr
    {
        public override int LabelNumber { get { return 1044112; } } // Bushido
        public override SkillName Skill { get { return SkillName.Bushido; } }
    }

    public class MysticismBonusProperty : SkillBonusAttr
    {
        public override int LabelNumber { get { return 1044115; } } // Mysticism
        public override SkillName Skill { get { return SkillName.Misticismo; } }
    }

    public class HealingBonusProperty : SkillBonusAttr
    {
        public override int LabelNumber { get { return 1002082; } } // Healing
        public override SkillName Skill { get { return SkillName.Medicina; } }
    }

    public class MagicResistBonusProperty : SkillBonusAttr
    {
        public override int LabelNumber { get { return 1049662; } } // Resisting Spells
        public override SkillName Skill { get { return SkillName.ResistenciaMagica; } }
    }

    public class PeacemakingBonusProperty : SkillBonusAttr
    {
        public override int LabelNumber { get { return 1002120; } } // Peacemaking
        public override SkillName Skill { get { return SkillName.Pacificar; } }
    }

    public class ArcheryBonusProperty : SkillBonusAttr
    {
        public override int LabelNumber { get { return 1002029; } } // Archery
        public override SkillName Skill { get { return SkillName.Atirar; } }
    }

    public class ChivalryBonusProperty : SkillBonusAttr
    {
        public override int LabelNumber { get { return 1044111; } } // Chivalry
        public override SkillName Skill { get { return SkillName.Ordem; } }
    }

    public class NinjitsuBonusProperty : SkillBonusAttr
    {
        public override int LabelNumber { get { return 1044113; } } // Ninjitsu
        public override SkillName Skill { get { return SkillName.Ninjitsu; } }
    }

    public class ThrowingBonusProperty : SkillBonusAttr
    {
        public override int LabelNumber { get { return 1044117; } } // Throwing
        public override SkillName Skill { get { return SkillName.Atirar; } }
    }

    public class LumberjackingBonusProperty : SkillBonusAttr
    {
        public override int LabelNumber { get { return 1044104; } } // Lumberjacking
        public override SkillName Skill { get { return SkillName.Extracao; } }
    }

    public class SnoopingBonusProperty : SkillBonusAttr
    {
        public override int LabelNumber { get { return 1044088; } } // Snooping
        public override SkillName Skill { get { return SkillName.Prestidigitacao; } }
    }

    public class MiningBonusProperty : SkillBonusAttr
    {
        public override int LabelNumber { get { return 1044105; } } // Mining
        public override SkillName Skill { get { return SkillName.Extracao; } }
    }
}
