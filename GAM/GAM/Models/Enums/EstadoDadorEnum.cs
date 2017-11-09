﻿using System.ComponentModel.DataAnnotations;

namespace GAM.Models.Enums
{
    public enum EstadoDadorEnum
    {
        [Display(Name = "Pendente de Aprovacao")]
        PendenteAprovacao,
        [Display(Name = "Com processo ativo")]
        ProcessoAtivo,
        [Display(Name = "Aceite")]
        Aceite,
        [Display(Name = "Rejeitado")]
        Rejeitado
    }
}
