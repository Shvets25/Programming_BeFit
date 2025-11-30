using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class TrainingSession
    {
        public int Id { get; set; }

        [Display(Name = "Data i czas rozpoczęcia")]
        public DateTime StartTime { get; set; }

        [Display(Name = "Data i czas zakończenia")]
        public DateTime EndTime { get; set; }

        public string? Notes { get; set; }

        public ICollection<TrainingExercise> TrainingExercises { get; set; } = new List<TrainingExercise>();
    }
}
