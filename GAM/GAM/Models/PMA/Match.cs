using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GAM.Models.PMA
{
    public class MatchStats
    {
        public int MatchStatsId { get; set; }
        public bool CorOlhosHomem { get; set; }
        public bool CorOlhosMulher { get; set; }
        public bool CorCabeloHomem { get; set; }
        public bool CorCabeloMulher { get; set; }
        public bool CorPeleHomem { get; set; }
        public bool CorPeleMulher { get; set; }
        public bool GrupoSanguineoHomem { get; set; }
        public bool GrupoSanguineoMulher { get; set; }
        public bool RacaHomem { get; set; }
        public bool RacaMulher { get; set; }
        public bool TexturaCabeloHomem { get; set; }
        public bool TexturaCabeloMulher { get; set; }
    }

    public class MatchStatsInfo
    {
        // int NrRecords { get; set; }
        public int CorOlhosHomem { get; set; }
        public int CorOlhosMulher { get; set; }
        public int CorCabeloHomem { get; set; }
        public int CorCabeloMulher { get; set; }
        public int CorPeleHomem { get; set; }
        public int CorPeleMulher { get; set; }
        public int GrupoSanguineoHomem { get; set; }
        public int GrupoSanguineoMulher { get; set; }
        public int RacaHomem { get; set; }
        public int RacaMulher { get; set; }
        public int TexturaCabeloHomem { get; set; }
        public int TexturaCabeloMulher { get; set; }
    }

}
