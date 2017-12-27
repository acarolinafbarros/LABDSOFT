using System;

namespace iGAMBot.Model
{
    public class ModelDadorCancelarConsultToBot
    {
        public string Nome { get; set; }

        public string DocIdentificacao { get; set; }

        public int ConsultaId { get; set; }

        public int DadorId { get; set; }

        public DateTime DataConsulta { get; set; }
    }
}