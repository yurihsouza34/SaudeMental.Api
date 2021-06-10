using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SaudeMental.Api.Model
{
    public class FormInfo
    {
        [Key]
        public string Id { get; set; }
        public string UserId { get; set; }
        public DateTime DataDeCriacao { get; set; }

        public int AnxietyScore { get; set; }
        public int DepressionScore { get; set; }

        public int Question01 { get; set; }
        public int Question02 { get; set; }
        public int Question03 { get; set; }
        public int Question04 { get; set; }
        public int Question05 { get; set; }
        public int Question06 { get; set; }
        public int Question07 { get; set; }
        public int Question08 { get; set; }
        public int Question09 { get; set; }
        public int Question10 { get; set; }
        public int Question11 { get; set; }
        public int Question12 { get; set; }
        public int Question13 { get; set; }
        public int Question14 { get; set; }

    }
}
