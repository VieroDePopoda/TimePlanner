using System;
using System.ComponentModel.DataAnnotations;

namespace TimePlanner.Models
{
    public class BaseModel
    {
        [Key]
        public Guid Id { get; set; }
        public bool IsCompleted { get; set; }
    }
}
