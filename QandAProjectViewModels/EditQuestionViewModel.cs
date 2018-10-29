using System;
using System.ComponentModel.DataAnnotations;

namespace QandAViewModels
{
    public class EditQuestionViewModel
    {
        [Required]
        public int QuestionId { get; set; }

        [Required]
        public string QuestionName { get; set; }

        [Required]
        public DateTime QuestionDateAndTime { get; set; }

        [Required]
        public int CategoryID { get; set; }
    }
}



