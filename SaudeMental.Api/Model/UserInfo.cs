using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SaudeMental.Api.Model
{
    public class UserInfo
    {
        [Key]
        public int Id { get; set; }
        public string userId { get; set; }
        public bool DoTreatment { get; set; } = false;
        public bool AnxietyDiagnosis { get; set; } = false;
        public bool DepressionDiagnosis { get; set; } = false;
    }
}
