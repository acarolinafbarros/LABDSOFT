using Microsoft.AspNetCore.Mvc;
using GAM.Data;
using GAM.Security;
using System.Linq;
using GAM.Models.Enums;
using Microsoft.AspNetCore.DataProtection;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;

namespace GAM.Controllers.Chatbot
{
    //[Produces("application/json")]
    
    public class GamToBotController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private EncryptorDador _encryptor;

        public GamToBotController(ApplicationDbContext context, IDataProtectionProvider provider)
        {
            _context = context;
            _encryptor = new EncryptorDador(provider); 
        }

        /// <summary>
        /// Testar no Postman: 
        /// GET - http://localhost:51008/api/GamToBot
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [Route("api/[controller]")]
        [HttpGet]
        public string Get()
        {
            return "iGAM - GamToBotController API";
        }

        /// <summary>
        /// Testar no Postman: 
        /// POST - http://localhost:51008/api/GamToBot/CheckIfDador
        /// Body - { "numeroIdentificacao" : "11223344"}
        /// </summary>
        /// <param name="context"></param>
        /// <returns>ModelDadorToBot</returns>
        [Route("api/[controller]/CheckIfDador")]
        [HttpPost]
        public IActionResult CheckIfDador([FromBody]JsonDTO context)
        {
            string docId = context.DocIdentificacao;

            // 1 - Verificar se o dador existe no sistema
            var dadorAlvo = _encryptor.DecryptData(_context.Dador.SingleOrDefault(d => d.DocIdentificacao == docId));
            
            if (dadorAlvo == null)
            {
                return null;
            }

            // 2 - Get Amostra do tipo Espermograma do Dador
            var amostraAlvo = _context.Amostra.SingleOrDefault(a => a.DadorId == dadorAlvo.DadorId && a.TipoAmostra == TipoAmostraEnum.Espermatozoide);
            if (amostraAlvo == null)
            {
                return null;
            }

            // 3 - Get Espermograma do Dador
            var espermogramaAlvo = _context.Espermograma.SingleOrDefault(e => e.AmostraId == amostraAlvo.AmostraId);
            if (espermogramaAlvo == null)
            {
                return null;
            }

            // 4 - Preencher o objeto personalizado do dador para devolver ao Bot
            var dadorToReturn = new ModelDadorResEspermToBot
            {
                Nome = dadorAlvo.Nome,
                DocIdentificacao = dadorAlvo.DocIdentificacao,
                AmostraId = amostraAlvo.AmostraId,
                GrauA = espermogramaAlvo.GrauA,
                GrauB = espermogramaAlvo.GrauB,
                GrauC = espermogramaAlvo.GrauC,
                GrauD = espermogramaAlvo.GrauD
            };

            return Ok(dadorToReturn);
        }
    }

    public class ModelDadorResEspermToBot
    {
        public string Nome { get; set; }

        public string DocIdentificacao { get; set; }

        public int AmostraId { get; set; }

        public int GrauA { get; set; }

        public int GrauB { get; set; }

        public int GrauC { get; set; }

        public int GrauD { get; set; }
    }

    public class JsonDTO
    {
        public string DocIdentificacao { get; set; }
    }
}
