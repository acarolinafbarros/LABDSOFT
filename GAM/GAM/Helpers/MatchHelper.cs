using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GAM.Models;
using GAM.Models.DadorViewModels;
using GAM.Models.Enums;

namespace GAM.Helpers
{
    public static class MatchHelper
    {
        public static List<GrupoSanguineoMatchEnum> ConvertGrupoSanguineo(this Casal casal)
        {
            return new List<GrupoSanguineoMatchEnum>{
                casal.GrupoSanguineoMulher.ConvertGrupoSanguineo(),
                casal.GrupoSanguineoHomem.ConvertGrupoSanguineo()
            }.OrderBy(x=>(int)x).ToList();
        }

        public static GrupoSanguineoMatchEnum ConvertGrupoSanguineo(this GrupoSanguineoEnum gs)
        {
            if (gs == GrupoSanguineoEnum.APos || gs == GrupoSanguineoEnum.ANeg)
                return GrupoSanguineoMatchEnum.A;
            if (gs == GrupoSanguineoEnum.BPos || gs == GrupoSanguineoEnum.BNeg)
                return GrupoSanguineoMatchEnum.B;
            if (gs == GrupoSanguineoEnum.ABPos || gs == GrupoSanguineoEnum.ABNeg)
                return GrupoSanguineoMatchEnum.AB;

            return GrupoSanguineoMatchEnum.O;
        }

        public static bool VerificaCompatibilidade_Abo(Casal casal, Dador dador)
        {
            var gruposCasal = casal.ConvertGrupoSanguineo();
            if (gruposCasal.Contains(GrupoSanguineoMatchEnum.AB))
                return true;

            //A+B
            if (gruposCasal.Contains(GrupoSanguineoMatchEnum.A) && gruposCasal.Contains(GrupoSanguineoMatchEnum.B))
                return true;

            //Dador == O
            if (dador.GrupoSanguineo.ConvertGrupoSanguineo()==GrupoSanguineoMatchEnum.O)
                return true;

            if (gruposCasal.Contains(dador.GrupoSanguineo.ConvertGrupoSanguineo()))
                return true;


            return false;
        }

        public static bool VerificaCompatibilidade_Rh(Casal casal, Dador dador)
        {
            //Sintonia entre casal e diferença do dador provoca exclusao
            if (((int) casal.GrupoSanguineoMulher) % 2 == ((int) casal.GrupoSanguineoMulher) % 2 &&
                ((int) casal.GrupoSanguineoMulher) % 2 != ((int) dador.GrupoSanguineo) % 2)
            {
                return false;
            }

            return true;
        }

        public static bool VerificaCompatibilidade_Olhos(Casal casal, Dador dador)
        {
            //TODO
            return true;
        }

        public static bool GamMatch(this Casal casal, Dador dador)
        {
            //mecanismo de match
            
            return VerificaCompatibilidade_Rh(casal, dador) 
                && VerificaCompatibilidade_Abo(casal, dador)
                && VerificaCompatibilidade_Olhos(casal, dador);
        }

    }
}
