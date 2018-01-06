using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GAM.Models.Enums
{

    public enum RegistoDadorEnum
    {
        [Display(Name = "Normal")]
        Normal = 0,
        [Display(Name = "HappyHour")]
        HappyHour = 1,
        [Display(Name = "Extra")]
        Extra = 2,

    }

}
