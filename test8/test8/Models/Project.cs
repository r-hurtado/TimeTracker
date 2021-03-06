﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using Foolproof;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Display(Name = "Hours")]
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

        public class timeLog
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int ID { get; set; }
            public string user { get; set; }
            public double time { get; set; }
            public DateTime date { get; set; }

            public timeLog(string u, double t, DateTime d)
            {
                user = u;
                time = t;
                date = d;
            }
        }

        public List<timeLog> inputs { get; set; }

        public class projectAccess
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int ID { get; set; }
            public string user { get; set; }

            //0 = user
            //1 = leader
            public int level { get; set; }

            public projectAccess(string u, int p, int l)
            {
                user = u;
                ID = p;
                level = l;
            }
        }

        public List<projectAccess> access { get; set; }
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