using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class TrainingExercise
    {
        public int Id { get; set; }

        [Required]
        public int TrainingSessionId { get; set; }
        public TrainingSession? TrainingSession { get; set; }

        [Required]
        public int ExerciseTypeId { get; set; }
        public ExerciseType? ExerciseType { get; set; }

        [Display(Name = "Obciążenie (kg)")]
        public double? Load { get; set; }

        [Display(Name = "Liczba serii")]
        public int Sets { get; set; }

        [Display(Name = "Powtórzenia w serii")]
        public int Reps { get; set; }
    }
}
