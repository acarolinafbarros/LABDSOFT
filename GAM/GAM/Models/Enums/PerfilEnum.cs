using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GAM.Models.Enums
{
    public enum PerfilEnum
    {
        [Display(Name = "Administrador")]
        Admin,
        Medico,
        Enfermeiro,
        [Display(Name = "Enfermeiro Coordenador")]
        EnfermeiroCoordenador,
        [Display(Name = "Diretor Geral")]
        DiretorGeral,
        [Display(Name = "Assistente Social")]
        AssistenteSocial,
        Embriologista,
        [Display(Name = "Diretora de Laboratorio")]
        DiretoraLaboratorio,
        [Display(Name = "PMA")]
        PMA,
        [Display(Name = "Diretora do Banco")]
        DiretoraBanco,
        Gestor
    }
}
