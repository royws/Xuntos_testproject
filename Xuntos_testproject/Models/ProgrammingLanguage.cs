using System;
using System.ComponentModel.DataAnnotations;

namespace Xuntos_testproject.Models
{
    public class ProgrammingLanguage
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        [Required]
        [StringLength(200)]
        public string Experience { get; set; }

        public ProgrammingLanguage(string name, string experience)
        {
            Name = name;
            Experience = experience;
        }
        public ProgrammingLanguage()
        {
        }
    }
}