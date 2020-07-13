using System;
using System.ComponentModel.DataAnnotations;

namespace TimePlanner.Models
{
    public class BaseModel     
    {
        // базовая модель для всех будущих моделей
        [Key]
        public Guid Id { get; set; }
    }
}
