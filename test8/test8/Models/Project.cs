using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using Foolproof;
using System.Web.Mvc;

namespace test8.Models
{
    public class Project
    {
        public int id { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string title { get; set; }

        public string Description { get; set; }

        [Required]
        [Display(Name = "Time")]
        [Range(0, double.MaxValue)]
        [DivisibleByQuarter]
        public double time { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime startDate { get; set; }

        [Required]
        [GreaterThan("startDate")]
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime endDate { get; set; }

        [Required]
        [Display(Name = "Leader")]
        public string leader { get; set; }

        [Required]
        public SelectList _users;

        [Display(Name = "Users")]
        public int SelectedUserId { get; set; }

        public IEnumerable<SelectListItem> users
        {
            get { return new SelectList(_users, "Id", "Name"); }
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ProjetDBContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
    }

    public class DivisibleByQuarterAttribute : ValidationAttribute
    {
        public DivisibleByQuarterAttribute()
            : base("{0} value is not divisible by 0.25")
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            double val = (double)value;

            bool valid = val % 0.25 == 0;

            if (valid)
                return null;

            return new ValidationResult(base.FormatErrorMessage(validationContext.MemberName)
                , new string[] { validationContext.MemberName });
        }
    }
}