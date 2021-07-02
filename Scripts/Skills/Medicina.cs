using System;
using Server.Gumps;
using Server.Mobiles;
using Server.Targeting;

namespace Server.SkillHandlers
{
    //Marcknight: Feito
    public class Medicina
    {
        public static void Initialize()
        {
            SkillInfo.Table[(int)SkillName.Medicina].Callback = new SkillUseCallback(OnUse);
        }

        public static TimeSpan OnUse(Mobile m)
        {
            if (PetTrainingHelper.Enabled && m.HasGump(typeof(NewAnimalLoreGump)))
            {
                m.SendLocalizedMessage(500118); // You must wait a few moments to use another skill. //Aguarde um pouco para utilizar outra skill.
            }
            else
            {
                m.Target = new InternalTarget();
                m.SendLocalizedMessage(500328); // What animal should I look at? //Que criatura pretende examinar?
            }

            return TimeSpan.FromSeconds(1.0);
        }

        private class InternalTarget : Target
        {
			private static void SendGump(Mobile from, BaseCreature c)
			{
                from.CheckTargetSkill(SkillName.Veterinaria, c, 0.0, 120.0);

                if (PetTrainingHelper.Enabled && from is PlayerMobile)
                {
                    Timer.DelayCall(TimeSpan.FromSeconds(1), () =>
                        {
                            BaseGump.SendGump(new NewAnimalLoreGump((PlayerMobile)from, c)); 
                        });
                }
                else
                {
                    from.CloseGump(typeof(MedicalLoreGump));
                    from.SendGump(new MedicalLoreGump(c));
                }
			}

			private static void Check(Mobile from, BaseCreature c, double min)
			{
				if (from.CheckTargetSkill(SkillName.Veterinaria, c, min, min + 20.0))
					SendGump(from, c);
				else
					from.SendLocalizedMessage(500334); // You can't think of anything you know offhand. //Exame inconclusivo.
            }

            public InternalTarget()
                : base(8, false, TargetFlags.None)
            {
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (!from.Alive)
                {
                    from.SendLocalizedMessage(500331); // The spirits of the dead are not the province of animal lore. //Os espíritos dos mortos não são o da seara da Veterinária
                }
                else if (targeted is BaseCreature)
                {
                    BaseCreature c = (BaseCreature)targeted;

                    if (!c.IsDeadPet)
                    {
                        if (c.Persuadable)
                        {
							double skill = from.Skills[SkillName.Veterinaria].Value;
							if(skill < 60.0)
                            {
								if (c.Controlled)
									SendGump(from, c);
								else
									from.SendLocalizedMessage(1049674); // At your skill level, you can only lore tamed creatures. //Suas habilidades só permitem examinar criaturas domadas.
                            }
                            else if (skill < 80.0)
                            {
								if (c.Controlled)
									SendGump(from, c);
								else if (c.Tamable)
									Check(from, c, 60.0);
								else
									from.SendLocalizedMessage(1049675); // At your skill level, you can only lore tamed or tameable creatures. //Suas habilidades só permitem examinar criaturas domadas ou domáveis.
                            }
                            else
                            {
								if (c.Controlled)
									SendGump(from, c);
								else if (c.Tamable)
									Check(from, c, 80.0);
								else
									Check(from, c, 100.0);
                            }
                        }
                        else
                        {
                            from.SendLocalizedMessage(500329); // That's not an animal! //Veterinária não se aplica a isso!
                        }
                    }
                    else
                    {
                        from.SendLocalizedMessage(500331); // The spirits of the dead are not the province of animal lore. //Os espíritos dos mortos não são o da seara da Veterinária
                    }
                }
                else
                {
                    from.SendLocalizedMessage(500329); // That's not an animal! //Veterinária não se aplica a isso!
                }
            }
        }
    }

    public class MedicalLoreGump : Gump
    {
        public static string FormatSkill(BaseCreature c, SkillName name)
        {
            Skill skill = c.Skills[name];

            if (skill.Base < 10.0)
                return "<div align=right>---</div>";

            return String.Format("<div align=right>{0:F1}</div>", skill.Value);
        }

        public static string FormatAttributes(int cur, int max)
        {
            if (max == 0)
                return "<div align=right>---</div>";

            return String.Format("<div align=right>{0}/{1}</div>", cur, max);
        }

        public static string FormatStat(int val)
        {
            if (val == 0)
                return "<div align=right>---</div>";

            return String.Format("<div align=right>{0}</div>", val);
        }

        public static string FormatDouble(double val)
        {
            if (val == 0)
                return "<div align=right>---</div>";

            return String.Format("<div align=right>{0:F1}</div>", val);
        }

        public static string FormatElement(int val)
        {
            if (val <= 0)
                return "<div align=right>---</div>";

            return String.Format("<div align=right>{0}%</div>", val);
        }

        #region Mondain's Legacy
        public static string FormatDamage(int min, int max)
        {
            if (min <= 0 || max <= 0)
                return "<div align=right>---</div>";

            return String.Format("<div align=right>{0}-{1}</div>", min, max);
        }

        #endregion

        private const int LabelColor = 0x24E5;

        public MedicalLoreGump(BaseCreature c)
            : base(250, 50)
        {
            AddPage(0);

            AddImage(100, 100, 2080);
            AddImage(118, 137, 2081);
            AddImage(118, 207, 2081);
            AddImage(118, 277, 2081);
            AddImage(118, 347, 2083);

            AddHtml(147, 108, 210, 18, String.Format("<center><i>{0}</i></center>", c.Name), false, false);

            AddButton(240, 77, 2093, 2093, 2, GumpButtonType.Reply, 0);

            AddImage(140, 138, 2091);
            AddImage(140, 335, 2091);

            int pages = (Core.AOS ? 5 : 3);
            int page = 0;

            #region Attributes
            AddPage(++page);

            AddImage(128, 152, 2086);
            AddHtmlLocalized(147, 150, 160, 18, 1049593, 200, false, false); // Attributes //Atributos

            AddHtmlLocalized(153, 168, 160, 18, 1049578, LabelColor, false, false); // Hits
            AddHtml(280, 168, 75, 18, FormatAttributes(c.Hits, c.HitsMax), false, false);

            AddHtmlLocalized(153, 186, 160, 18, 1049579, LabelColor, false, false); // Stamina
            AddHtml(280, 186, 75, 18, FormatAttributes(c.Stam, c.StamMax), false, false);

            AddHtmlLocalized(153, 204, 160, 18, 1049580, LabelColor, false, false); // Mana
            AddHtml(280, 204, 75, 18, FormatAttributes(c.Mana, c.ManaMax), false, false);

            AddHtmlLocalized(153, 222, 160, 18, 3000111, LabelColor, false, false); // Strength //Força
            AddHtml(320, 222, 35, 18, FormatStat(c.Str), false, false);

            AddHtmlLocalized(153, 240, 160, 18, 3000113, LabelColor, false, false); // Dexterity //Destreza
            AddHtml(320, 240, 35, 18, FormatStat(c.Dex), false, false);

            AddHtmlLocalized(153, 258, 160, 18, 3000112, LabelColor, false, false); // Intelligence //Inteligência
            AddHtml(320, 258, 35, 18, FormatStat(c.Int), false, false);

            if (Core.AOS)
            {
                int y = 276;

                if (Core.SE)
                {
                    double bd = Items.BaseInstrument.GetBaseDifficulty(c);
                    if (c.Uncalmable)
                        bd = 0;

                    AddHtmlLocalized(153, 276, 160, 18, 1070793, LabelColor, false, false); // Barding Difficulty //Dificuldade Bárdica
                    AddHtml(320, y, 35, 18, FormatDouble(bd), false, false);

                    y += 18;
                }

                AddImage(128, y + 2, 2086);
                AddHtmlLocalized(147, y, 160, 18, 1049594, 200, false, false); // Loyalty Rating //Fator de Lealdade
                y += 18;

                AddHtmlLocalized(153, y, 160, 18, (!c.Controlled || c.Loyalty == 0) ? 1061643 : 1049595 + (c.Loyalty / 10), LabelColor, false, false);
            }
            else
            {
                AddImage(128, 278, 2086);
                AddHtmlLocalized(147, 276, 160, 18, 3001016, 200, false, false); // Miscellaneous //Diversos

                AddHtmlLocalized(153, 294, 160, 18, 1049581, LabelColor, false, false); // Armor Rating //Armadura
                AddHtml(320, 294, 35, 18, FormatStat(c.VirtualArmor), false, false);
            }

            AddButton(340, 358, 5601, 5605, 0, GumpButtonType.Page, page + 1);
            AddButton(317, 358, 5603, 5607, 0, GumpButtonType.Page, pages);
            #endregion

            #region Resistances
            if (Core.AOS)
            {
                AddPage(++page);

                AddImage(128, 152, 2086);
                AddHtmlLocalized(147, 150, 160, 18, 1061645, 200, false, false); // Resistances //Resistências

                AddHtmlLocalized(153, 168, 160, 18, 1061646, LabelColor, false, false); // Physical //Físico
                AddHtml(320, 168, 35, 18, FormatElement(c.PhysicalResistance), false, false);

                AddHtmlLocalized(153, 186, 160, 18, 1061647, LabelColor, false, false); // Fire //Fogo
                AddHtml(320, 186, 35, 18, FormatElement(c.FireResistance), false, false);

                AddHtmlLocalized(153, 204, 160, 18, 1061648, LabelColor, false, false); // Cold //Frio
                AddHtml(320, 204, 35, 18, FormatElement(c.ColdResistance), false, false);

                AddHtmlLocalized(153, 222, 160, 18, 1061649, LabelColor, false, false); // Poison //Veneno
                AddHtml(320, 222, 35, 18, FormatElement(c.PoisonResistance), false, false);

                AddHtmlLocalized(153, 240, 160, 18, 1061650, LabelColor, false, false); // Energy //Energia
                AddHtml(320, 240, 35, 18, FormatElement(c.EnergyResistance), false, false);

                AddButton(340, 358, 5601, 5605, 0, GumpButtonType.Page, page + 1);
                AddButton(317, 358, 5603, 5607, 0, GumpButtonType.Page, page - 1);
            }
            #endregion

            #region Damage
            if (Core.AOS)
            {
                AddPage(++page);

                AddImage(128, 152, 2086);
                AddHtmlLocalized(147, 150, 160, 18, 1017319, 200, false, false); // Damage //Dano

                AddHtmlLocalized(153, 168, 160, 18, 1061646, LabelColor, false, false); // Physical //Físico
                AddHtml(320, 168, 35, 18, FormatElement(c.PhysicalDamage), false, false);

                AddHtmlLocalized(153, 186, 160, 18, 1061647, LabelColor, false, false); // Fire  //Fogo
                AddHtml(320, 186, 35, 18, FormatElement(c.FireDamage), false, false);

                AddHtmlLocalized(153, 204, 160, 18, 1061648, LabelColor, false, false); // Cold //Frio
                AddHtml(320, 204, 35, 18, FormatElement(c.ColdDamage), false, false);

                AddHtmlLocalized(153, 222, 160, 18, 1061649, LabelColor, false, false); // Poison //Veneno
                AddHtml(320, 222, 35, 18, FormatElement(c.PoisonDamage), false, false);

                AddHtmlLocalized(153, 240, 160, 18, 1061650, LabelColor, false, false); // Energy //Energia
                AddHtml(320, 240, 35, 18, FormatElement(c.EnergyDamage), false, false);

                #region Mondain's Legacy
                if (Core.ML)
                {
                    AddHtmlLocalized(153, 258, 160, 18, 1076750, LabelColor, false, false); // Base Damage //Dano Base
                    AddHtml(300, 258, 55, 18, FormatDamage(c.DamageMin, c.DamageMax), false, false);
                }
                #endregion

                AddButton(340, 358, 5601, 5605, 0, GumpButtonType.Page, page + 1);
                AddButton(317, 358, 5603, 5607, 0, GumpButtonType.Page, page - 1);
            }
            #endregion

            #region Skills
            AddPage(++page);

            AddImage(128, 152, 2086);
            AddHtmlLocalized(147, 150, 160, 18, 3001030, 200, false, false); // Combat Ratings //Skills de Combate

            AddHtmlLocalized(153, 168, 160, 18, 1044103, LabelColor, false, false); // Wrestling //Briga
            AddHtml(320, 168, 35, 18, FormatSkill(c, SkillName.Briga), false, false);

            AddHtmlLocalized(153, 186, 160, 18, 1044065, LabelColor, false, false); // Bloqueio
            AddHtml(320, 186, 35, 18, FormatSkill(c, SkillName.Bloqueio), false, false);

            AddHtmlLocalized(153, 204, 160, 18, 1044086, LabelColor, false, false); // Magic Resistance //Resistência mágica
            AddHtml(320, 204, 35, 18, FormatSkill(c, SkillName.ResistenciaMagica), false, false);

            AddHtmlLocalized(153, 222, 160, 18, 1044061, LabelColor, false, false); // Anatomy //Anatomia
            AddHtml(320, 222, 35, 18, FormatSkill(c, SkillName.Anatomia), false, false);

            #region Mondain's Legacy
            if (c is CuSidhe)
            {
                AddHtmlLocalized(153, 240, 160, 18, 1044077, LabelColor, false, false); // Healing //Medicina
                AddHtml(320, 240, 35, 18, FormatSkill(c, SkillName.Medicina), false, false);
            }
            else
            {
                AddHtmlLocalized(153, 240, 160, 18, 1044090, LabelColor, false, false); // Poisoning //Envenenamento
                AddHtml(320, 240, 35, 18, FormatSkill(c, SkillName.Envenenamento), false, false);
            }
            #endregion

            AddImage(128, 260, 2086);
            AddHtmlLocalized(147, 258, 160, 18, 3001032, 200, false, false); // Lore & Knowledge

            AddHtmlLocalized(153, 276, 160, 18, 1044076, LabelColor, false, false); // Poder Mágico
            AddHtml(320, 276, 35, 18, FormatSkill(c, SkillName.PoderMagico), false, false);

            AddHtmlLocalized(153, 294, 160, 18, 1044085, LabelColor, false, false); // Arcanismo
            AddHtml(320, 294, 35, 18, FormatSkill(c, SkillName.Arcanismo), false, false);

            //AddHtmlLocalized(153, 312, 160, 18, 1044106, LabelColor, false, false); // Meditation
            //AddHtml(320, 312, 35, 18, FormatSkill(c, SkillName.Meditation), false, false);

            AddButton(340, 358, 5601, 5605, 0, GumpButtonType.Page, page + 1);
            AddButton(317, 358, 5603, 5607, 0, GumpButtonType.Page, page - 1);
            #endregion

            #region Misc
            AddPage(++page);

            AddImage(128, 152, 2086);
            AddHtmlLocalized(147, 150, 160, 18, 1049563, 200, false, false); // Preferred Foods //Alimentos Prediletos

            int foodPref = 3000340;

            if ((c.FavoriteFood & FoodType.FruitsAndVegies) != 0)
                foodPref = 1049565; // Fruits and Vegetables //Frutas e Verduras
            else if ((c.FavoriteFood & FoodType.GrainsAndHay) != 0)
                foodPref = 1049566; // Grains and Hay //Grãos e Feno
            else if ((c.FavoriteFood & FoodType.Fish) != 0)
                foodPref = 1049568; // Fish //Peixe
            else if ((c.FavoriteFood & FoodType.Meat) != 0)
                foodPref = 1049564; // Meat //Carne
            else if ((c.FavoriteFood & FoodType.Eggs) != 0)
                foodPref = 1044477; // Eggs //Ovos

            AddHtmlLocalized(153, 168, 160, 18, foodPref, LabelColor, false, false);

            AddImage(128, 188, 2086);
            AddHtmlLocalized(147, 186, 160, 18, 1049569, 200, false, false); // Pack Instincts //Instinto de bando

            int packInstinct = 3000340;

            if ((c.PackInstinct & PackInstinct.Canine) != 0)
                packInstinct = 1049570; // Canine //Canino
            else if ((c.PackInstinct & PackInstinct.Ostard) != 0)
                packInstinct = 1049571; // Ostard
            else if ((c.PackInstinct & PackInstinct.Feline) != 0)
                packInstinct = 1049572; // Feline //Felino
            else if ((c.PackInstinct & PackInstinct.Arachnid) != 0)
                packInstinct = 1049573; // Arachnid //Aracnídeo
            else if ((c.PackInstinct & PackInstinct.Daemon) != 0)
                packInstinct = 1049574; // Daemon //Demônio
            else if ((c.PackInstinct & PackInstinct.Bear) != 0)
                packInstinct = 1049575; // Bear //Urso
            else if ((c.PackInstinct & PackInstinct.Equine) != 0)
                packInstinct = 1049576; // Equine //Equino
            else if ((c.PackInstinct & PackInstinct.Bull) != 0)
                packInstinct = 1049577; // Bull //Bovino

            AddHtmlLocalized(153, 204, 160, 18, packInstinct, LabelColor, false, false);

            if (!Core.AOS)
            {
                AddImage(128, 224, 2086);
                AddHtmlLocalized(147, 222, 160, 18, 1049594, 200, false, false); // Loyalty Rating //Fator de Lealdade

                AddHtmlLocalized(153, 240, 160, 18, (!c.Controlled || c.Loyalty == 0) ? 1061643 : 1049595 + (c.Loyalty / 10), LabelColor, false, false);
            }

            AddButton(340, 358, 5601, 5605, 0, GumpButtonType.Page, 1);
            AddButton(317, 358, 5603, 5607, 0, GumpButtonType.Page, page - 1);
            #endregion
        }
    }
}
