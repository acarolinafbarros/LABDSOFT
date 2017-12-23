using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GAM.Models;
using GAM.Models.DadorViewModels;
using GAM.Models.Enums;
using GAM.Models.PMA;

namespace GAM.Helpers
{
    public static class MatchHelper
    {
        /// <summary>
        /// Converte Grupo sanguineo do casal (homem + mulher)
        /// </summary>
        /// <param name="casal">Casal</param>
        /// <returns>Lista de grupos sanguineos ordenada</returns>
        public static List<GrupoSanguineoMatchEnum> ConvertGrupoSanguineo(this Casal casal)
        {
            return new List<GrupoSanguineoMatchEnum>{
                casal.GrupoSanguineoMulher.ConvertGrupoSanguineo(),
                casal.GrupoSanguineoHomem.ConvertGrupoSanguineo()
            }.OrderBy(x=>(int)x).ToList();
        }

        /// <summary>
        /// Converte grupo sanguineo para grupo sanguineo Base (utilizado para a tabela de exclusao por grupos sanguineos)
        /// </summary>
        /// <param name="gs">Grupo sanguineo a converter</param>
        /// <returns>Grupo sanguineo apenas A, B, AB e O</returns>
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

        /// <summary>
        /// Verifica compatibilidade segundo tabela ABO
        /// </summary>
        /// <param name="casal">Casal a verificar</param>
        /// <param name="dador">Dador a verificar</param>
        /// <returns>Compatibilidade possivel entre dador e casal</returns>
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

        /// <summary>
        /// Verifica compatibilidade de RH
        /// </summary>
        /// <param name="casal">Casal a verificar</param>
        /// <param name="dador">Dador a verificar</param>
        /// <returns>Compatibilidade possivel entre dador e casal</returns>
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

        /// <summary>
        /// Verifica compatibilidade de olhos
        /// </summary>
        /// <param name="casal">Casal a verificar</param>
        /// <param name="dador">Dador a verificar</param>
        /// <returns>Compatibilidade possivel entre dador e casal</returns>
        public static bool VerificaCompatibilidade_Olhos(Casal casal, Dador dador)
        {
            //TODO faltam enums
            return true;
        }

        /// <summary>
        /// Valida se existe compatibilidade entre casal e dador, 
        /// utilizando o factor de exclusao (ABO, RH e Olhos)
        /// </summary>
        /// <param name="casal">Casal a verificar</param>
        /// <param name="dador">Dador a verificar</param>
        /// <returns>Compatibilidade possivel entre dador e casal</returns>
        public static bool GamMatch(this Casal casal, Dador dador)
        {
            //mecanismo de match
            
            return VerificaCompatibilidade_Rh(casal, dador) 
                && VerificaCompatibilidade_Abo(casal, dador)
                && VerificaCompatibilidade_Olhos(casal, dador);
        }

        /// <summary>
        /// Ordena lista segundo uma preferencia MatchStatsInfo
        /// </summary>
        /// <param name="dadors">lista de dadores (compativeis)</param>
        /// <param name="casal">casal selecionado</param>
        /// <param name="stats">dados preferenciais</param>
        /// <returns>Lista ordenada de compatibilidades</returns>
        public static List<Dador> GetOrdedList(List<Dador> dadors, Casal casal, MatchStatsInfo stats)
        {
            var listaComparacao = dadors.Select(x => new
            {
                x.DadorId,
                GrupoSanguineoMulher = casal.GrupoSanguineoMulher == x.GrupoSanguineo,
                GrupoSanguineoHomem = casal.GrupoSanguineoHomem == x.GrupoSanguineo,
                CorCabeloHomem = casal.CorCabeloHomem == x.CorCabelo,
                CorCabeloMulher = casal.CorCabeloMulher == x.CorCabelo,
                CorOlhosHomem = casal.CorOlhosHomem == x.CorOlhos,
                CorOlhosMulher = casal.CorOlhosMulher == x.CorOlhos,
                CorPeleHomem = casal.CorPeleHomem == x.CorPele,
                CorPeleMulher = casal.CorPeleMulher == x.CorPele,
                RacaHomem = casal.RacaHomem == x.Etnia,
                RacaMulher = casal.RacaHomem == x.Etnia,
                TexturaCabeloHomem = casal.TexturaCabeloHomem == x.TexturaCabelo,
                TexturaCabeloMulher = casal.TexturaCabeloMulher == x.TexturaCabelo

                //DUMMY
                //CorCabeloHomem =false,
                //CorCabeloMulher = false,
                //CorOlhosHomem = false,
                //CorOlhosMulher = false,
                //CorPeleHomem = false,
                //CorPeleMulher = false,
                //RacaHomem = false,
                //RacaMulher = false,
                //TexturaCabeloHomem = false,
                //TexturaCabeloMulher = false

            });

            var ordemStats = stats.GetType().GetProperties().Select(x => new
            {
                Field = x.Name,
                Value = x.GetValue(stats, null)
            }).OrderBy(x=>x.Value).Select(x=>x.Field).ToList().Aggregate((x,y)=> $"{x},{y}" );
            var comparacaoOrdenada = System.Linq.Dynamic.Core.DynamicQueryableExtensions.OrderBy(listaComparacao.AsQueryable(), ordemStats);

            //var comparacaoOrdenada = listaComparacao.OrderBy(ordemStats);
            var sortedList = comparacaoOrdenada.Select(x => dadors.FirstOrDefault(y => x.DadorId == y.DadorId)).ToList();

            return sortedList;
        }

        /// <summary>
        /// Devolve um MatchStats, obtido atravez da comparacao entre dador e casal,
        /// detalhando as semelhancas entre ambos
        /// </summary>
        /// <param name="casal">casal selecionado</param>
        /// <param name="dador">dador a comparar</param>
        /// <returns>MatchStats, com informação sobre match entre dador e casal</returns>
        public static MatchStats GetMatchStats(Casal casal, Dador dador)
        {
            var matchStats = new MatchStats
            {
                GrupoSanguineoMulher = casal.GrupoSanguineoMulher==dador.GrupoSanguineo,
                GrupoSanguineoHomem = casal.GrupoSanguineoHomem == dador.GrupoSanguineo,
                CorCabeloHomem = casal.CorCabeloHomem == dador.CorCabelo,
                CorCabeloMulher = casal.CorCabeloMulher == dador.CorCabelo,
                CorOlhosHomem = casal.CorOlhosHomem == dador.CorOlhos,
                CorOlhosMulher = casal.CorOlhosMulher == dador.CorOlhos,
                CorPeleHomem = casal.CorPeleHomem == dador.CorPele,
                CorPeleMulher = casal.CorPeleMulher == dador.CorPele,
                RacaHomem = casal.RacaHomem == dador.Etnia,
                RacaMulher = casal.RacaHomem == dador.Etnia,
                TexturaCabeloHomem  = casal.TexturaCabeloHomem == dador.TexturaCabelo,
                TexturaCabeloMulher = casal.TexturaCabeloMulher == dador.TexturaCabelo,
                CasalId = casal.CasalID,
                DadorId = dador.DadorId,
                

            };

            return matchStats;
        }

    }
}
