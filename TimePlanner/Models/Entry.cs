using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TimePlanner.Models
{
    public class Entry : BaseModel
    {
        [Required]
        [Display(Name = "Тип записи")]
        public string Type { get; set; }

        [Required]
        [Display(Name = "Тема")]
        public string Theme { get; set; }

        [Required]
        [Display(Name = "Дата/Время начала")]
        public DateTime StartTime { get; set; }

        [Display(Name = "Дата/Время окончания")]
        public DateTime EndTime { get; set; }

        [Display(Name = "Место встречи")]
        public string Place { get; set; }

        [DefaultValue(false)]
        public bool IsCompleted { get; set; }
    }
}