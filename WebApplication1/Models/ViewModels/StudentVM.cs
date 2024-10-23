using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;
using WebApplication1.Models.Entities;

namespace WebApplication1.Models.ViewModels
{
    public class StudentVM
    {
        public System.Guid StudentID { get; set; }
        [Required]
        [DisplayName("Фамилия")]
        [StringLength(100, MinimumLength = 2)]
        public string LastName { get; set; }
        [Required]
        [DisplayName("Имя")]
        public string FirstName { get; set; }

        [DisplayName("Отчество")]
        public string MiddleName { get; set; }
        [Required]
        [DisplayName("Пол")]
        public bool Gender { get; set; }

        [DisplayName("Адрес")]
        public string Address { get; set; }
        [Required]
        [Range(typeof(DateTime), "01/01/1900", "01/01/2024")]
        [DisplayName("Дата роождения")]
        public System.DateTime DateOfBirth { get; set; }

        [DisplayName("дата добавления")]
        public DateTime InsertDateTime { get; set; }

        [DisplayName("Уровень владения английским")]
        public string EducationLevel { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentsInGroups> StudentsInGroups { get; set; }
    }
}