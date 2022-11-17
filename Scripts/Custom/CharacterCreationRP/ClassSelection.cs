using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Commands;
using Server.Custom.Classes;

namespace Server.Gumps.CharacterCreationRP
{
    public class ClassSelectionGump : Gump
    {
        Mobile caller;
        int raceEscolhida;
        ClassePersonagem classeEscolhida;
        List<int> skillsEscolhidas;

        public static void Initialize()
        {
            //CommandSystem.Register("ClassSelection", AccessLevel.Administrator, new CommandEventHandler(ClassGump_OnCommand));
        }

        [Usage("")]
        [Description("Gump de seleção de classe na criação de personagem.")]
        public static void ClassGump_OnCommand(CommandEventArgs e)
        {
            Mobile caller = e.Mobile;

            if (caller.HasGump(typeof(ClassSelectionGump)))
                caller.CloseGump(typeof(ClassSelectionGump));
            caller.SendGump(new ClassSelectionGump(caller));
        }

        public ClassSelectionGump(Mobile from) : this()
        {
            caller = from;
        }


        public ClassSelectionGump(int selectedRace, ClassePersonagem selectedClasse, List<int> selectedSkills) : base(0,0)
        {
            this.Closable = false;
            this.Disposable = false;
            this.Dragable = false;
            this.Resizable = false;

            int classeID = 9999;

            raceEscolhida = selectedRace;

            if (selectedClasse != null)
            {
                classeEscolhida = selectedClasse;
                classeID = classeEscolhida.ID;
            }
            if (selectedSkills != null)
            {
                skillsEscolhidas = selectedSkills;
            }

            string raceLabel = "";

            switch (raceEscolhida)
            {
                case 11: //Aludia
                    raceLabel = "Aludia";
                    break;
                case 22: //Daru
                    raceLabel = "Daru";
                    break;
                default:
                    raceLabel = "Raça inválida";
                    break;
            }

            //Console.WriteLine(String.Format("A raça escolhida foi a ID {0}", raceEscolhida));
            List<ClassePersonagem> classes = ClassDef.GetClasses();

            AddPage(0);
            AddImage(1, 0, 40318);
            AddLabel(315, 128, 1153, @"Classes"); //Titulo da lista de classes

            int classCount = 0; //Contador de classes exibidas
            int totalCount = 0; //Contador de classes total

            int CLbaseX = 338; //Posição 'x' base do label da classe
            int CLbaseY = 158; //Posição 'y' base do label da classe //22 entre cada label
            int CBbaseX = 318; //Posição 'x' base do button da classe
            int CBbaseY = 162; //Posição 'y' base do button da classe //22 entre cada label

            foreach (ClassePersonagem classe in classes)
            {
                switch (raceEscolhida)
                {
                    case 11: //Aludia
                        if (totalCount % 2 == 0)
                        {
                            AddButton(CBbaseX, CBbaseY + (24 * classCount), ((classe.ID == classeID) ? 40310 : 40308), ((classe.ID == classeID) ? 40308 : 40310), classe.ID, GumpButtonType.Reply, 0);
                            AddLabel(CLbaseX, CLbaseY + (24 * classCount), 1153, classe.Nome);
                            classCount++;
                        }
                        break;
                    case 22: //Daru
                        if (totalCount % 2 == 1)
                        {
                            AddButton(CBbaseX, CBbaseY + (24 * classCount), ((classe.ID == classeID) ? 40310 : 40308), ((classe.ID == classeID) ? 40308 : 40310), classe.ID, GumpButtonType.Reply, 0);
                            AddLabel(CLbaseX, CLbaseY + (24 * classCount), 1153, classe.Nome);
                            classCount++;
                        }
                        break;
                }
                totalCount++;
            }

            //Campo de instruções
            AddHtml(73, 298, 173, 271,
                @"À esquerda estão listadas as classes da raça escolhida.<br>Abaixo estão listadas todas as skills que você pode escolher treinar naquela classe.<br>Você pode selecionar até 3 skills para o seu char começar com algum treinamento.<br>Todas as skills permitidas da classe podem ser treinadas e/ou esquecidas livremente durante o jogo.<br>Escolha as 3 skills que seu personagem iniciará com alguma habilidade.<br>Caso queira reconsiderar o povo que escolheu, use o botão abaixo para voltar.",
                (bool)false, (bool)true);

            //Lista de skills da classe. Parte inferior do Gump
            AddLabel(317, 493, 1153, @"Skills da Classe (3)");
            
            if (classeEscolhida != null)
            {
                //AddImage(438, 136, classeEscolhida.Icone); //ainda sem imagem
                //Campo de descrição da classe. Lado direito do Gump
                AddLabel(666, 128, 1153, String.Format("{0} dos {1} (Lore)", classeEscolhida.Nome, raceLabel));
                AddHtml(668, 161, 197, 291, classeEscolhida.Desc, false, true);

                int SLbaseX = 338; //Posição 'x' base do label da classe
                int SLbaseY = 523; //Posição 'y' base do label da classe //22 entre cada label
                int SBbaseX = 318; //Posição 'x' base do button da classe
                int SBbaseY = 525; //Posição 'y' base do button da classe //22 entre cada label

                /*
                HashSet<SkillName> skills = classeEscolhida.getBeneficios(); //Armazena a lista de skills da classe atualmente selecionada
                List <SkillInfo> tabelaSkills = SkillInfo.Table.CastToList<SkillInfo>(); //Armazena a tabela com os nomes de exibição das skills, entre outras informações

                int linhaCount = 0;
                totalCount = 0;
                bool marcado = false;
                foreach(SkillName skill in skills)
                {
                    if ((totalCount > 0) && (totalCount % 5 == 0))
                    {
                        linhaCount++;
                    }
                    if(skillsEscolhidas != null){ marcado = skillsEscolhidas.Contains((int)skill); }
                    else { marcado = false; }

                    AddCheck(SBbaseX + (104 * (totalCount % 5)), SBbaseY + (22 * linhaCount), 40308, 40310, marcado, (int)skill);
                    AddLabel(SLbaseX + (104 * (totalCount % 5)), SLbaseY + (22 * linhaCount), 1153, skill.ToString());
                    totalCount++;
                }
                */
            }

            //Botão para passagem de página (Retorno)
            AddButton(77, 575, 4014, 4016, -999, GumpButtonType.Reply, 0);
            //Botão para passagem de página (Avanço)
            AddButton(843, 575, 4005, 4006, 999, GumpButtonType.Reply, 0);
            
        }

        //Não aceita inicialização sem ter informação da raça
        public ClassSelectionGump() : base(0, 0)
        {
            return;
        }


        public override void OnResponse(NetState sender, RelayInfo info)
        {
            caller = sender.Mobile;
            //skillsEscolhidas = info.Switches.CastToList<int>();
            //Console.WriteLine(String.Format("Botão da classe clicado. ID = {0}", info.ButtonID));
            if (info.ButtonID == 999)
            {
                if (skillsEscolhidas.Count == 3)
                {
                    //Console.WriteLine("Atingiu condição de passar de página");
                    caller.SendGump(new CharDetailsAGump(raceEscolhida, classeEscolhida, skillsEscolhidas, null, null));
                    return;
                }
                else
                {
                    //Console.WriteLine("Numero de skills selecionadas está errado");
                    caller.SendGump(new ClassSelectionGump(raceEscolhida, classeEscolhida, skillsEscolhidas));
                }
            }
            else if (info.ButtonID == -999)
            {
                caller.SendGump(new RaceSelectionGump());
                return;
            }
            else
            {
                classeEscolhida = ClassDef.GetClasse(info.ButtonID);
                skillsEscolhidas = null;
                caller.SendGump(new ClassSelectionGump(raceEscolhida, classeEscolhida, skillsEscolhidas));
            }
            return;
        }
    }
}
