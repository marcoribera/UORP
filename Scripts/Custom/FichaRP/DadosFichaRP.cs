using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Custom.FichaRP
{
    public class DadosFichaRP
    {
        public string Background;
        public string BackgroundHistorico;
        public string MemoriasMarcantes;
        public string MemoriasMarcantesHistorico;
        public string AparenciaRosto;
        public string AparenciaCorpo;
        public string AparenciaMarcas;
        public string PersonalidadePositivo;
        public string PersonalidadeNegativo;
        public string PersonalidadeOutros;
        public string ObjetivosAtual;
        public string ObjetivosHistorico;
        public string FeedbackStaff;
        public string FeedbackStaffHistorico;
        
        public DadosFichaRP()
        {
            Background = "";
            BackgroundHistorico = "";
            MemoriasMarcantes = "";
            MemoriasMarcantesHistorico = "";
            AparenciaRosto = "";
            AparenciaCorpo = "";
            AparenciaMarcas = "";
            PersonalidadePositivo = "";
            PersonalidadeNegativo = "";
            PersonalidadeOutros = "";
            ObjetivosAtual = "";
            ObjetivosHistorico = "";
            FeedbackStaff = "";
            FeedbackStaffHistorico = "";
        }

        public void Serialize(GenericWriter writer)
        {
            writer.Write(1);
            writer.Write(Background);
            writer.Write(BackgroundHistorico);
            writer.Write(MemoriasMarcantes);
            writer.Write(MemoriasMarcantesHistorico);
            writer.Write(AparenciaRosto);
            writer.Write(AparenciaCorpo);
            writer.Write(AparenciaMarcas);
            writer.Write(PersonalidadePositivo);
            writer.Write(PersonalidadeNegativo);
            writer.Write(PersonalidadeOutros);
            writer.Write(ObjetivosAtual);
            writer.Write(ObjetivosHistorico);
            writer.Write(FeedbackStaff);
            writer.Write(FeedbackStaffHistorico);
            return;
            
        }
        public void Deserialize(GenericReader reader)
        {
            int version = reader.ReadInt();
            switch (version)
            {
                case 1:
                    Background = reader.ReadString();
                    BackgroundHistorico = reader.ReadString();
                    MemoriasMarcantes = reader.ReadString();
                    MemoriasMarcantesHistorico = reader.ReadString();
                    goto case 0;
                case 0:
                    AparenciaRosto = reader.ReadString();
                    AparenciaCorpo = reader.ReadString();
                    AparenciaMarcas = reader.ReadString();
                    PersonalidadePositivo = reader.ReadString();
                    PersonalidadeNegativo = reader.ReadString();
                    PersonalidadeOutros = reader.ReadString();
                    ObjetivosAtual = reader.ReadString();
                    ObjetivosHistorico = reader.ReadString();
                    FeedbackStaff = reader.ReadString();
                    FeedbackStaffHistorico = reader.ReadString();
                    break;
            }
            return;
        }
    }
}
