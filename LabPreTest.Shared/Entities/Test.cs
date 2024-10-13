﻿using LabPreTest.Shared.Interfaces;
using LabPreTest.Shared.Messages;
using System.ComponentModel.DataAnnotations;

namespace LabPreTest.Shared.Entities
{
    public class Test : IEntityWithId, ITestEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = EntityMessages.RequiredErrorMessage)]
        public int TestID { get; set; }

        [Display(Name = EntityMessages.TestDisplayName)]
        [MaxLength(100, ErrorMessage = EntityMessages.MaxLengthErrorMessage)]
        [Required(ErrorMessage = EntityMessages.RequiredErrorMessage)]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = EntityMessages.RequiredErrorMessage)]
        public Section Section { get; set; } = null!;
        public int SectionId { get; set; }

        [Required(ErrorMessage = EntityMessages.RequiredErrorMessage)]
        public TestTube TestTube { get; set; } = null!;
        public int TestTubeId { get; set; }

        public ICollection<PreanalyticCondition>? Conditions { get; set; }
        [Display(Name = EntityMessages.TestConditionsDisplayMessage)]
        public int ConditionNumber => Conditions == null || Conditions.Count == 0 ? 0 : Conditions.Count;
        
        public ICollection<TemporalOrder>? TemporalOrders { get; set; }

        public ICollection<OrderDetail>? Details { get; set; }
    }
}