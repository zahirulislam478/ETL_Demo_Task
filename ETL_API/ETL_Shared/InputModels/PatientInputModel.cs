using ETL_Shared.Models;
using System.ComponentModel.DataAnnotations;

namespace ETL_Shared.InputModels 
{
    public class PatientInputModel
    {
        public int PatientId { get; set; }
        [Required, StringLength(50), Display(Name ="Patient Name *")]
        public string PatientName { get; set; } = default!;
        [EnumDataType(typeof(EpilepsyType)), Display(Name = "Epilepsy *")]
        public EpilepsyType? Epilepsy { get; set; } = default!;
        [Required(ErrorMessage = "Disease information is required"), Display(Name ="Disease Name *")]
        public int DiseaseInformationId { get; set; }
        [Required(ErrorMessage = "Allergies is required"),  Display(Name = "Allergies *")]
        public int[] AllergyIds { get; set; } = default!;
        [Display(Name = "Other NCDs")]
        public int[]? NCDIds { get; set; } = default!;
    }
}
