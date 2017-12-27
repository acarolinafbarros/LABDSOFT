using System;

namespace iGAMBot.Model
{
    public class ModelDadorMarcarConsultToBot
    {
        public string Nome { get; set; }

        public string DocIdentificacao { get; set; }

        public int DadorId { get; set; }

        public int SlotId { get; set; }

        public DateTime DataConsultaDisponivel { get; set; }
    }
}