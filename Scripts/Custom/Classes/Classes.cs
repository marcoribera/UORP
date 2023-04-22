using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Custom.Classes
{
    public static class ClassDef
    {
        private static Dictionary<int, ClassePersonagem> ListaClasses;

        public static void AddClasse(ClassePersonagem classe)
        {
            if (!ListaClasses.ContainsKey(classe.ID))
            {
                //Console.WriteLine(String.Format("Tentou inserir classe {0} com ID {1}", classe.Nome, classe.ID));
                ListaClasses.Add(classe.ID, classe);
            }
            else
            {
                Console.WriteLine(String.Format("Tentou inserir classe {0} no lugar da classe {1}, ambas com ID {2}", classe.Nome, ListaClasses[classe.ID].Nome, classe.ID));
            }
        }

        public static List<ClassePersonagem> GetClasses()
        {
            return ListaClasses.Values.ToList();
        }
        public static List<ClassePersonagem> GetClassesByTier(TierClasse tier)
        {
            List<ClassePersonagem> ClassesByTier = new List<ClassePersonagem>();
            foreach (KeyValuePair<int, ClassePersonagem> objeto in ListaClasses)
            {
                if (objeto.Value.Tier == tier)
                {
                    //Console.WriteLine(String.Format("Inseriu {0}:  {1}", objeto.Key, objeto.Value.Nome));
                    ClassesByTier.Add(objeto.Value);
                }
            }
            return ClassesByTier;
        }

        public static ClassePersonagem GetClasse(int id)
        {
            if (!ListaClasses.ContainsKey(id))
            {
                return null;
            }
            else
            {
                return ListaClasses[id];
            }
        }

        static ClassDef()
        {
            if (ListaClasses == null)
            {
                ListaClasses = new Dictionary<int, ClassePersonagem>();
            }
            if (ListaClasses.Count != 0)
                return;


            AddClasse(new ClassePersonagem(1, "Guerreiro Aprendiz", "Combatente Generalista", TierClasse.ClasseBasica,
                    new Dictionary<SkillName, double>()
                    { //Requisitos dos quais são necessários pelo menos 4 para pegar a classe (Funcionamento para pré-alfa)
                    // no beta dia 21/04/2023 mudamos de 50 para 25
                    {SkillName.Cortante, 25},
                    {SkillName.Perfurante, 25},
                    {SkillName.Contusivo, 25},
                    {SkillName.DuasMaos, 25},
                    {SkillName.UmaMao, 25},
                    {SkillName.Atirar, 25},
                    {SkillName.Bloqueio, 25},
                    {SkillName.Briga, 25},
                    {SkillName.PreparoFisico, 25},
                  //  {SkillName.Sobrevivencia, 25},    //  {SkillName.Sobrevivencia, 25}, sobrevivencia foi retirado no beta dia 21/04/2023
                    {SkillName.Bushido, 25}

                    }, //Aumentos de Cap conseguidos
                    new Dictionary<SkillName, double>()
                    {
                    {SkillName.Cortante, 25},
                    {SkillName.Perfurante, 25},
                    {SkillName.Contusivo, 25},
                    {SkillName.DuasMaos, 25},
                    {SkillName.UmaMao, 25},
                    {SkillName.Atirar, 25},
                    {SkillName.Medicina, 5},
                    {SkillName.ConhecimentoArmas, 5},
                    {SkillName.ConhecimentoArmaduras, 5},
                    {SkillName.Bloqueio, 25},
                    {SkillName.Briga, 25},
                    {SkillName.Anatomia, 10},
                    {SkillName.PreparoFisico, 10},
                 //   {SkillName.Sobrevivencia, 20},//  {SkillName.Sobrevivencia, 25}, sobrevivencia foi retirado no beta dia 21/04/2023
                    {SkillName.Bushido, 25}




                    }
                    )
                    );

            AddClasse(new ClassePersonagem(10, "Guerreiro", "Combatente ", TierClasse.ClasseIntermediaria,
                    new Dictionary<SkillName, double>()
                    { 
                    // no beta dia 21/04/2023 4 skills alcançar 50
                    {SkillName.Cortante, 50},
                    {SkillName.Perfurante, 50},
                    {SkillName.Contusivo, 50},
                    {SkillName.DuasMaos, 50},
                    {SkillName.UmaMao, 50},
                    {SkillName.Atirar, 50},
                    {SkillName.Bloqueio, 50},
                    {SkillName.Briga, 50},
                    //{SkillName.PreparoFisico, 25},    // foi retirado no beta dia 21/04/2023
                  //  {SkillName.Sobrevivencia, 25},    //  {SkillName.Sobrevivencia, 25}, sobrevivencia foi retirado no beta dia 21/04/2023
                    {SkillName.Bushido, 50}

                    }, //Aumentos de Cap conseguidos
                    new Dictionary<SkillName, double>()
                    {
                    {SkillName.Cortante, 15},
                    {SkillName.Perfurante, 15},
                    {SkillName.Contusivo, 15},
                    {SkillName.DuasMaos, 15},
                    {SkillName.UmaMao, 15},
                    {SkillName.Atirar, 15},
                    {SkillName.Medicina, 5},
                    {SkillName.ConhecimentoArmas, 5},
                    {SkillName.ConhecimentoArmaduras, 5},
                    {SkillName.Bloqueio, 15},
                    {SkillName.Briga, 15},
                    {SkillName.Anatomia, 10},
                    {SkillName.PreparoFisico, 10},
                 //   {SkillName.Sobrevivencia, 20},//  {SkillName.Sobrevivencia, 25}, sobrevivencia foi retirado no beta dia 21/04/2023
                    {SkillName.Bushido, 15}




                    }
                    )
                    );


            AddClasse(new ClassePersonagem(100, "Guerreiro Experiente", "Combatente Experiente ", TierClasse.ClasseAvancada,
                             new Dictionary<SkillName, double>()
                             { 
                    // no beta dia 21/04/2023 4 skills alcançar 65
                    {SkillName.Cortante, 65},
                    {SkillName.Perfurante, 65},
                    {SkillName.Contusivo, 65},
                    {SkillName.DuasMaos, 65},
                    {SkillName.UmaMao, 65},
                    {SkillName.Atirar, 65},
                    {SkillName.Bloqueio, 65},
                    {SkillName.Briga, 65},
                    //{SkillName.PreparoFisico, 25},    // foi retirado no beta dia 21/04/2023
                  //  {SkillName.Sobrevivencia, 25},    //  {SkillName.Sobrevivencia, 25}, sobrevivencia foi retirado no beta dia 21/04/2023
                    {SkillName.Bushido, 65}

                             }, //Aumentos de Cap conseguidos
                             new Dictionary<SkillName, double>()
                             {
                    {SkillName.Cortante, 15},
                    {SkillName.Perfurante, 15},
                    {SkillName.Contusivo, 15},
                    {SkillName.DuasMaos, 15},
                    {SkillName.UmaMao, 15},
                    {SkillName.Atirar, 15},
                    {SkillName.Medicina, 5},
                    {SkillName.ConhecimentoArmas, 5},
                    {SkillName.ConhecimentoArmaduras, 5},
                    {SkillName.Bloqueio, 15},
                    {SkillName.Briga, 15},
                    {SkillName.Anatomia, 10},
                    {SkillName.PreparoFisico, 10},
                 //   {SkillName.Sobrevivencia, 20},//  {SkillName.Sobrevivencia, 25}, sobrevivencia foi retirado no beta dia 21/04/2023
                    {SkillName.Bushido, 15}




                             }
                             )
                             );


            AddClasse(new ClassePersonagem(1000, "Guerreiro Lendario", "Combatente Lendario ", TierClasse.ClasseLendaria,
           new Dictionary<SkillName, double>()
           { 
                    // no beta dia 21/04/2023 4 skills alcançar 80
                    {SkillName.Cortante, 80},
                    {SkillName.Perfurante, 80},
                    {SkillName.Contusivo, 80},
                    {SkillName.DuasMaos, 80},
                    {SkillName.UmaMao, 80},
                    {SkillName.Atirar, 80},
                    {SkillName.Bloqueio, 80},
                    {SkillName.Briga, 80},
                    //{SkillName.PreparoFisico, 25},    // foi retirado no beta dia 21/04/2023
                  //  {SkillName.Sobrevivencia, 25},    //   sobrevivencia foi retirado no beta dia 21/04/2023
                    {SkillName.Bushido, 80}

           }, //Aumentos de Cap conseguidos
           new Dictionary<SkillName, double>()
           {
                    {SkillName.Cortante, 15},
                    {SkillName.Perfurante, 15},
                    {SkillName.Contusivo, 15},
                    {SkillName.DuasMaos, 15},
                    {SkillName.UmaMao, 15},
                    {SkillName.Atirar, 15},
                    {SkillName.Medicina, 5},
                    {SkillName.ConhecimentoArmas, 5},
                    {SkillName.ConhecimentoArmaduras, 5},
                    {SkillName.Bloqueio, 15},
                    {SkillName.Briga, 15},
                    {SkillName.Anatomia, 10},
                    {SkillName.PreparoFisico, 10},
                 //   {SkillName.Sobrevivencia, 20},//  , sobrevivencia foi retirado no beta dia 21/04/2023
                    {SkillName.Bushido, 15}




           }
           )
           );


            AddClasse(new ClassePersonagem(2, "Ladino Aprendiz", "Treinado nas artes do subterfúgio", TierClasse.ClasseBasica,
                new Dictionary<SkillName, double>()
                { //Requisitos dos quais são necessários pelo menos 4 para pegar a classe (Funcionamento para pré-alfa)
                // no beta dia 21/04/2023 mudamos de 50 para 25
                    {SkillName.Carisma, 25},
                    {SkillName.Furtividade, 25},
                    {SkillName.Mecanica, 25},
                    {SkillName.Percepcao, 25},
                    {SkillName.Prestidigitacao, 25},
                    {SkillName.Tocar, 25},
                    {SkillName.Provocacao, 25},
                    {SkillName.Pacificar, 25},
                  //  {SkillName.Caos, 50}, foi retirado no beta dia 21/04/2023
                    {SkillName.Perfurante, 25},
                   // {SkillName.UmaMao, 50}, foi retirado no beta dia 21/04/2023
                    {SkillName.Anatomia, 25},
                    {SkillName.Atirar, 25},
                    {SkillName.Envenenamento, 25},
                    {SkillName.Ninjitsu, 25}

                }, //Aumentos de Cap conseguidos
                new Dictionary<SkillName, double>()
                {
                    {SkillName.Carisma, 25},
                    {SkillName.Furtividade, 25},
                    {SkillName.Mecanica, 25},
                    {SkillName.Percepcao, 25},
                    {SkillName.Prestidigitacao, 25},
                    {SkillName.Tocar, 25},
                    {SkillName.Provocacao, 25},
                    {SkillName.Pacificar, 25},
                    {SkillName.Caos, 10},
                    {SkillName.Perfurante, 25},
                    {SkillName.Medicina, 5},
                    {SkillName.Contusivo, 10},
                    {SkillName.Cortante, 10},
                    {SkillName.Bloqueio, 10},
                    {SkillName.Erudicao, 10},
                   //{SkillName.UmaMao, 20}, foi retirado no beta dia 21/04/2023
                    {SkillName.Anatomia, 25},
                    {SkillName.Atirar, 25},
                    {SkillName.Envenenamento, 25},
                    {SkillName.Ninjitsu, 25}
                }));

            AddClasse(new ClassePersonagem(20, "Ladino", "Treinado nas artes do subterfúgio", TierClasse.ClasseIntermediaria,
                           new Dictionary<SkillName, double>()
                           { 
                // no beta dia 21/04/2023  4 skills alcançar 50
                    {SkillName.Carisma, 50},
                    {SkillName.Furtividade, 50},
                    {SkillName.Mecanica, 50},
                    {SkillName.Percepcao, 50},
                    {SkillName.Prestidigitacao, 50},
                    {SkillName.Tocar, 50},
                    {SkillName.Provocacao, 50},
                    {SkillName.Pacificar, 50},
                  //  {SkillName.Caos, 50}, foi retirado no beta dia 21/04/2023
                    {SkillName.Perfurante, 50},
                   // {SkillName.UmaMao, 50}, foi retirado no beta dia 21/04/2023
                    {SkillName.Anatomia, 50},
                  {SkillName.Atirar, 50},
                    {SkillName.Envenenamento, 50},
                    {SkillName.Ninjitsu, 50}

                           }, //Aumentos de Cap conseguidos
                           new Dictionary<SkillName, double>()
                           {
                    {SkillName.Carisma, 15},
                    {SkillName.Furtividade, 15},
                    {SkillName.Mecanica, 15},
                    {SkillName.Percepcao, 15},
                    {SkillName.Prestidigitacao, 15},
                    {SkillName.Tocar, 15},
                    {SkillName.Provocacao, 15},
                    {SkillName.Pacificar, 15},
                    {SkillName.Caos, 10},
                    {SkillName.Perfurante, 15},
                    {SkillName.Medicina, 5},
                    {SkillName.Contusivo, 10},
                    {SkillName.Cortante, 10},
                    {SkillName.Bloqueio, 10},
                    {SkillName.Erudicao, 10},
                   //{SkillName.UmaMao, 20}, foi retirado no beta dia 21/04/2023
                    {SkillName.Anatomia, 15},
                    {SkillName.Atirar, 15},
                    {SkillName.Envenenamento, 15},
                    {SkillName.Ninjitsu, 15}
                           }));

            AddClasse(new ClassePersonagem(200, "Ladino Experiente", "Treinado nas artes do subterfúgio", TierClasse.ClasseAvancada,
                           new Dictionary<SkillName, double>()
                           { 
                // no beta dia 21/04/2023  4 skills alcançar 65
                    {SkillName.Carisma, 65},
                    {SkillName.Furtividade, 65},
                    {SkillName.Mecanica, 65},
                    {SkillName.Percepcao, 65},
                    {SkillName.Prestidigitacao, 65},
                    {SkillName.Tocar, 65},
                    {SkillName.Provocacao, 65},
                    {SkillName.Pacificar, 65},
                  //  {SkillName.Caos, 50}, foi retirado no beta dia 21/04/2023
                    {SkillName.Perfurante, 65},
                   // {SkillName.UmaMao, 50}, foi retirado no beta dia 21/04/2023
                    {SkillName.Anatomia, 65},
                  {SkillName.Atirar, 65},
                    {SkillName.Envenenamento, 65},
                    {SkillName.Ninjitsu, 65}

                           }, //Aumentos de Cap conseguidos
                           new Dictionary<SkillName, double>()
                           {
                    {SkillName.Carisma, 15},
                    {SkillName.Furtividade, 15},
                    {SkillName.Mecanica, 15},
                    {SkillName.Percepcao, 15},
                    {SkillName.Prestidigitacao, 15},
                    {SkillName.Tocar, 15},
                    {SkillName.Provocacao, 15},
                    {SkillName.Pacificar, 15},
                    {SkillName.Caos, 10},
                    {SkillName.Perfurante, 15},
                    {SkillName.Medicina, 5},
                    {SkillName.Contusivo, 10},
                    {SkillName.Cortante, 10},
                    {SkillName.Bloqueio, 10},
                    {SkillName.Erudicao, 10},
                   //{SkillName.UmaMao, 20}, foi retirado no beta dia 21/04/2023
                    {SkillName.Anatomia, 15},
                    {SkillName.Atirar, 15},
                    {SkillName.Envenenamento, 15},
                    {SkillName.Ninjitsu, 15}
                           }));

            AddClasse(new ClassePersonagem(2000, "Ladino Lendario", "Treinado nas artes do subterfúgio", TierClasse.ClasseLendaria,
                           new Dictionary<SkillName, double>()
                           { 
                // no beta dia 21/04/2023  4 skills alcançar 80
                    {SkillName.Carisma, 80},
                    {SkillName.Furtividade, 80},
                    {SkillName.Mecanica, 80},
                    {SkillName.Percepcao, 80},
                    {SkillName.Prestidigitacao, 80},
                    {SkillName.Tocar, 80},
                    {SkillName.Provocacao, 80},
                    {SkillName.Pacificar, 80},
                  //  {SkillName.Caos, 50}, foi retirado no beta dia 21/04/2023
                    {SkillName.Perfurante, 80},
                   // {SkillName.UmaMao, 50}, foi retirado no beta dia 21/04/2023
                    {SkillName.Anatomia, 80},
                  {SkillName.Atirar, 80},
                    {SkillName.Envenenamento, 80},
                    {SkillName.Ninjitsu, 80}

                           }, //Aumentos de Cap conseguidos
                           new Dictionary<SkillName, double>()
                           {
                    {SkillName.Carisma, 15},
                    {SkillName.Furtividade, 15},
                    {SkillName.Mecanica, 15},
                    {SkillName.Percepcao, 15},
                    {SkillName.Prestidigitacao, 15},
                    {SkillName.Tocar, 15},
                    {SkillName.Provocacao, 15},
                    {SkillName.Pacificar, 15},
                    {SkillName.Caos, 10},
                    {SkillName.Perfurante, 15},
                    {SkillName.Medicina, 5},
                    {SkillName.Contusivo, 10},
                    {SkillName.Cortante, 10},
                    {SkillName.Bloqueio, 10},
                    {SkillName.Erudicao, 10},
                   //{SkillName.UmaMao, 20}, foi retirado no beta dia 21/04/2023
                    {SkillName.Anatomia, 15},
                    {SkillName.Atirar, 15},
                    {SkillName.Envenenamento, 15},
                    {SkillName.Ninjitsu, 15}
                           }));


            AddClasse(new ClassePersonagem(3, "Mago Aprendiz", "Mago Generalista", TierClasse.ClasseBasica,
                new Dictionary<SkillName, double>()
                { //Requisitos dos quais são necessários pelo menos 4 para pegar a classe (Funcionamento para pré-alfa)
                // no beta dia 21/04/2023 mudamos de 50 para 25

                    {SkillName.Arcanismo, 25},
                    {SkillName.Caos, 25},
                    {SkillName.Feiticaria, 25},
                    {SkillName.ImbuirMagica, 25},
                    {SkillName.Misticismo, 25},
                    {SkillName.Ordem, 25},
                    {SkillName.Necromancia, 25},
                    {SkillName.PoderMagico, 25},
                    {SkillName.ResistenciaMagica, 25},
                    {SkillName.Erudicao, 25}
                   // {SkillName.PreparoFisico, 50}, foi retirado no beta dia 21/04/2023
                  //  {SkillName.Contusivo, 50}, foi retirado no beta dia 21/04/2023
                   // {SkillName.Alquimia, 50} foi retirado no beta dia 21/04/2023

                }, //Aumentos de Cap conseguidos
                new Dictionary<SkillName, double>()
                {
                    {SkillName.Arcanismo, 25},
                    {SkillName.Caos, 25},
                    {SkillName.Feiticaria, 25},
                    {SkillName.ImbuirMagica, 25},
                    {SkillName.Misticismo, 25},
                    {SkillName.Ordem, 25},
                    {SkillName.Necromancia, 25},
                    {SkillName.PoderMagico, 25},
                    {SkillName.ResistenciaMagica, 25},
                    {SkillName.Erudicao, 25},
                 //   {SkillName.PreparoFisico, 20}, foi retirado no beta dia 21/04/2023
                    {SkillName.Contusivo, 10},
                    {SkillName.Alquimia, 10},
                    {SkillName.Atirar, 5},
                    {SkillName.Perfurante, 5},
                    {SkillName.Medicina, 10}



                }));

            AddClasse(new ClassePersonagem(30, "Mago", "Mago Generalista", TierClasse.ClasseIntermediaria,
           new Dictionary<SkillName, double>()
           { 
                // no beta dia 21/04/2023  4 skills alcançar 65

                    {SkillName.Arcanismo, 50},
                    {SkillName.Caos, 50},
                    {SkillName.Feiticaria, 50},
                    {SkillName.ImbuirMagica, 50},
                    {SkillName.Misticismo, 50},
                    {SkillName.Ordem, 50},
                    {SkillName.Necromancia, 50},
                    {SkillName.PoderMagico, 50},
                    {SkillName.ResistenciaMagica, 50},
                    {SkillName.Erudicao, 50}
               // {SkillName.PreparoFisico, 50}, foi retirado no beta dia 21/04/2023
               //  {SkillName.Contusivo, 50}, foi retirado no beta dia 21/04/2023
               // {SkillName.Alquimia, 50} foi retirado no beta dia 21/04/2023

           }, //Aumentos de Cap conseguidos
           new Dictionary<SkillName, double>()
           {
                    {SkillName.Arcanismo, 15},
                    {SkillName.Caos, 15},
                    {SkillName.Feiticaria, 15},
                    {SkillName.ImbuirMagica, 15},
                    {SkillName.Misticismo, 15},
                    {SkillName.Ordem, 15},
                    {SkillName.Necromancia, 15},
                    {SkillName.PoderMagico, 15},
                    {SkillName.ResistenciaMagica, 15},
                    {SkillName.Erudicao, 15},
                 //   {SkillName.PreparoFisico, 20}, foi retirado no beta dia 21/04/2023
                    {SkillName.Contusivo, 10},
                    {SkillName.Alquimia, 10},
                    {SkillName.Atirar, 5},
                    {SkillName.Perfurante, 5},
                    {SkillName.Medicina, 10}



           }));

            AddClasse(new ClassePersonagem(300, "Mago Experiente", "Mago Generalista", TierClasse.ClasseAvancada,
 new Dictionary<SkillName, double>()
 { 
                // no beta dia 21/04/2023  4 skills alcançar 65

                    {SkillName.Arcanismo, 65},
                    {SkillName.Caos, 65},
                    {SkillName.Feiticaria, 65},
                    {SkillName.ImbuirMagica, 65},
                    {SkillName.Misticismo, 65},
                    {SkillName.Ordem, 65},
                    {SkillName.Necromancia, 65},
                    {SkillName.PoderMagico, 65},
                    {SkillName.ResistenciaMagica, 65},
                    {SkillName.Erudicao, 65}
     // {SkillName.PreparoFisico, 50}, foi retirado no beta dia 21/04/2023
     //  {SkillName.Contusivo, 50}, foi retirado no beta dia 21/04/2023
     // {SkillName.Alquimia, 50} foi retirado no beta dia 21/04/2023

 }, //Aumentos de Cap conseguidos
 new Dictionary<SkillName, double>()
 {
                    {SkillName.Arcanismo, 15},
                    {SkillName.Caos, 15},
                    {SkillName.Feiticaria, 15},
                    {SkillName.ImbuirMagica, 15},
                    {SkillName.Misticismo, 15},
                    {SkillName.Ordem, 15},
                    {SkillName.Necromancia, 15},
                    {SkillName.PoderMagico, 15},
                    {SkillName.ResistenciaMagica, 15},
                    {SkillName.Erudicao, 15},
                 //   {SkillName.PreparoFisico, 20}, foi retirado no beta dia 21/04/2023
                    {SkillName.Contusivo, 10},
                    {SkillName.Alquimia, 10},
                    {SkillName.Atirar, 5},
                    {SkillName.Perfurante, 5},
                    {SkillName.Medicina, 10}



 }));


            AddClasse(new ClassePersonagem(3000, "Mago Lendario", "Mago Generalista", TierClasse.ClasseLendaria,
 new Dictionary<SkillName, double>()
 { 
                // no beta dia 21/04/2023  4 skills alcançar 80

                    {SkillName.Arcanismo, 80},
                    {SkillName.Caos, 80},
                    {SkillName.Feiticaria, 80},
                    {SkillName.ImbuirMagica, 80},
                    {SkillName.Misticismo, 80},
                    {SkillName.Ordem, 80},
                    {SkillName.Necromancia, 80},
                    {SkillName.PoderMagico, 80},
                    {SkillName.ResistenciaMagica, 80},
                    {SkillName.Erudicao, 80}
     // {SkillName.PreparoFisico, 50}, foi retirado no beta dia 21/04/2023
     //  {SkillName.Contusivo, 50}, foi retirado no beta dia 21/04/2023
     // {SkillName.Alquimia, 50} foi retirado no beta dia 21/04/2023

 }, //Aumentos de Cap conseguidos
 new Dictionary<SkillName, double>()
 {
                    {SkillName.Arcanismo, 15},
                    {SkillName.Caos, 15},
                    {SkillName.Feiticaria, 15},
                    {SkillName.ImbuirMagica, 15},
                    {SkillName.Misticismo, 15},
                    {SkillName.Ordem, 15},
                    {SkillName.Necromancia, 15},
                    {SkillName.PoderMagico, 15},
                    {SkillName.ResistenciaMagica, 15},
                    {SkillName.Erudicao, 15},
                 //   {SkillName.PreparoFisico, 20},foi retirado no beta dia 21/04/2023
                    {SkillName.Contusivo, 10},
                    {SkillName.Alquimia, 10},
                    {SkillName.Atirar, 5},
                    {SkillName.Perfurante, 5},
                    {SkillName.Medicina, 10}



 }));


            AddClasse(new ClassePersonagem(4, "Trabalhador Aprendiz", "Trabalhador Faz-Tudo", TierClasse.ClasseBasica,
                new Dictionary<SkillName, double>()
                { //Requisitos dos quais são necessários pelo menos 4 para pegar a classe (Funcionamento para pré-alfa)
                // no beta dia 21/04/2023 mudamos de 50 para 25
                    {SkillName.Adestramento, 25},
                    {SkillName.Agricultura, 25},
                    {SkillName.Alquimia, 25},
                    {SkillName.Carpintaria, 25},
                    {SkillName.ConhecimentoArmaduras, 25},
                    {SkillName.ConhecimentoArmas, 25},
                    {SkillName.Costura, 25},
                    {SkillName.Culinaria, 25},
                    {SkillName.Erudicao, 25},
                    {SkillName.Extracao, 25},
                    {SkillName.Ferraria, 25},
                    {SkillName.Medicina, 25},
                    {SkillName.Veterinaria, 25}
                  //  {SkillName.Carisma, 50}, foi retirado no beta dia 21/04/2023
                   // {SkillName.Percepcao, 50} foi retirado no beta dia 21/04/2023


                }, //Aumentos de Cap conseguidos
                new Dictionary<SkillName, double>()
                {
                    {SkillName.Adestramento, 25},
                    {SkillName.Agricultura, 25},
                    {SkillName.Alquimia, 25},
                    {SkillName.Carpintaria, 25},
                    {SkillName.ConhecimentoArmaduras, 25},
                    {SkillName.ConhecimentoArmas, 25},
                    {SkillName.Costura, 25},
                    {SkillName.Extracao, 25},
                    {SkillName.Erudicao, 25},
                    {SkillName.Ferraria, 25},
                    {SkillName.Medicina, 25},
                    {SkillName.Veterinaria, 25},
                    {SkillName.PreparoFisico, 5},
                    {SkillName.UmaMao, 10},
                    {SkillName.DuasMaos, 10}
                    // {SkillName.Carisma, 20}, foi retirado no beta dia 21/04/2023
                    // {SkillName.Percepcao, 20} foi retirado no beta dia 21/04/2023
                }));

            AddClasse(new ClassePersonagem(40, "Trabalhador", "Trabalhador Faz-Tudo", TierClasse.ClasseIntermediaria,
            new Dictionary<SkillName, double>()
            { 
                //no beta dia 21/04/2023  4 skills alcançar 50
                    {SkillName.Adestramento, 50},
                    {SkillName.Agricultura, 50},
                    {SkillName.Alquimia, 50},
                    {SkillName.Carpintaria, 50},
                    {SkillName.ConhecimentoArmaduras, 50},
                    {SkillName.ConhecimentoArmas, 50},
                    {SkillName.Costura, 50},
                    {SkillName.Culinaria, 50},
                    {SkillName.Erudicao, 50},
                    {SkillName.Extracao, 50},
                    {SkillName.Ferraria, 50},
                    {SkillName.Medicina, 50},
                    {SkillName.Veterinaria, 50}
                //  {SkillName.Carisma, 50}, foi retirado no beta dia 21/04/2023
                // {SkillName.Percepcao, 50} foi retirado no beta dia 21/04/2023


            }, //Aumentos de Cap conseguidos
            new Dictionary<SkillName, double>()
            {
                     {SkillName.Adestramento, 15},
                    {SkillName.Agricultura, 15},
                    {SkillName.Alquimia, 15},
                    {SkillName.Carpintaria, 15},
                    {SkillName.ConhecimentoArmaduras, 15},
                    {SkillName.ConhecimentoArmas, 15},
                    {SkillName.Costura, 15},
                    {SkillName.Extracao, 15},
                    {SkillName.Erudicao, 15},
                    {SkillName.Ferraria, 15},
                    {SkillName.Medicina, 15},
                    {SkillName.Veterinaria, 15},
                    {SkillName.PreparoFisico, 5},
                    {SkillName.UmaMao, 10},
                    {SkillName.DuasMaos, 10}
                // {SkillName.Carisma, 20}, foi retirado no beta dia 21/04/2023
                // {SkillName.Percepcao, 20} foi retirado no beta dia 21/04/2023
            }));

            AddClasse(new ClassePersonagem(400, "Trabalhador Experiente", "Trabalhador Faz-Tudo", TierClasse.ClasseAvancada,
           new Dictionary<SkillName, double>()
           { 
                //no beta dia 21/04/2023  4 skills alcançar 65
                    {SkillName.Adestramento, 65},
                    {SkillName.Agricultura, 65},
                    {SkillName.Alquimia, 65},
                    {SkillName.Carpintaria, 65},
                    {SkillName.ConhecimentoArmaduras, 65},
                    {SkillName.ConhecimentoArmas, 65},
                    {SkillName.Costura, 65},
                    {SkillName.Culinaria, 65},
                    {SkillName.Erudicao, 65},
                    {SkillName.Extracao, 65},
                    {SkillName.Ferraria, 65},
                    {SkillName.Medicina, 65},
                    {SkillName.Veterinaria, 65}
               //  {SkillName.Carisma, 50}, foi retirado no beta dia 21/04/2023
               // {SkillName.Percepcao, 50} foi retirado no beta dia 21/04/2023


           }, //Aumentos de Cap conseguidos
           new Dictionary<SkillName, double>()
           {
                    {SkillName.Adestramento, 15},
                    {SkillName.Agricultura, 15},
                    {SkillName.Alquimia, 15},
                    {SkillName.Carpintaria, 15},
                    {SkillName.ConhecimentoArmaduras, 15},
                    {SkillName.ConhecimentoArmas, 15},
                    {SkillName.Costura, 15},
                    {SkillName.Extracao, 15},
                    {SkillName.Erudicao, 15},
                    {SkillName.Ferraria, 15},
                    {SkillName.Medicina, 15},
                    {SkillName.Veterinaria, 15},
                    {SkillName.PreparoFisico, 5},
                    {SkillName.UmaMao, 10},
                    {SkillName.DuasMaos, 10}
               // {SkillName.Carisma, 20}, foi retirado no beta dia 21/04/2023
               // {SkillName.Percepcao, 20} foi retirado no beta dia 21/04/2023
           }));

            AddClasse(new ClassePersonagem(4000, "Trabalhador Lendario", "Trabalhador Faz-Tudo", TierClasse.ClasseLendaria,
          new Dictionary<SkillName, double>()
          { 
                //no beta dia 21/04/2023  4 skills alcançar 80
                    {SkillName.Adestramento, 80},
                    {SkillName.Agricultura, 80},
                    {SkillName.Alquimia, 80},
                    {SkillName.Carpintaria, 80},
                    {SkillName.ConhecimentoArmaduras, 80},
                    {SkillName.ConhecimentoArmas, 80},
                    {SkillName.Costura, 80},
                    {SkillName.Culinaria, 80},
                    {SkillName.Erudicao, 80},
                    {SkillName.Extracao, 80},
                    {SkillName.Ferraria, 80},
                    {SkillName.Medicina, 80},
                    {SkillName.Veterinaria, 80}
              //  {SkillName.Carisma, 50}, foi retirado no beta dia 21/04/2023
              // {SkillName.Percepcao, 50} foi retirado no beta dia 21/04/2023


          }, //Aumentos de Cap conseguidos
          new Dictionary<SkillName, double>()
          {
                    {SkillName.Adestramento, 15},
                    {SkillName.Agricultura, 15},
                    {SkillName.Alquimia, 15},
                    {SkillName.Carpintaria, 15},
                    {SkillName.ConhecimentoArmaduras, 15},
                    {SkillName.ConhecimentoArmas, 15},
                    {SkillName.Costura, 15},
                    {SkillName.Extracao, 15},
                    {SkillName.Erudicao, 15},
                    {SkillName.Ferraria, 15},
                    {SkillName.Medicina, 15},
                    {SkillName.Veterinaria, 15},
                    {SkillName.PreparoFisico, 5},
                    {SkillName.UmaMao, 10},
                    {SkillName.DuasMaos, 10 }
              // {SkillName.Carisma, 20}, foi retirado no beta dia 21/04/2023
              // {SkillName.Percepcao, 20} foi retirado no beta dia 21/04/2023
          }));
        }
    }
}
