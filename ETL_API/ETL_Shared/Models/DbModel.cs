using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL_Shared.Models
{
    public enum EpilepsyType { Yes = 1, No }
    public class Allergy
    {
        public int AllergyId { get; set; }
        [Required, StringLength(50)]
        public string AllergyName { get; set; } = default!;
        public virtual ICollection<Allergy_Detail> Allergy_Details { get; set; } = new List<Allergy_Detail>();
    }

    public class NCD
    {
        public int NCDId { get; set; }
        [Required, StringLength(50)]
        public string NCDName { get; set; } = default!;
        public virtual ICollection<NCD_Detail> NCD_Details { get; set; } = new List<NCD_Detail>();
    }

    public class Patient
    {
        public int PatientId { get; set; }
        [Required, StringLength(50)]
        public string PatientName { get; set; } = default!;
        [EnumDataType(typeof(EpilepsyType))]
        public EpilepsyType? Epilepsy { get; set; } = default!;
        [Required, ForeignKey("DiseaseInformation")]
        public int DiseaseInformationId { get; set; }
        public virtual DiseaseInformation DiseaseInformation { get; set; } = default!;


        public virtual ICollection<NCD_Detail> NCD_Details { get; set; } = new List<NCD_Detail>();
        public virtual ICollection<Allergy_Detail> Allergy_Details { get; set; } = new List<Allergy_Detail>();

    }
    public class DiseaseInformation
    {
        public int DiseaseInformationId { get; set; }
        [Required, StringLength(50)]
        public string DiseaseName { get; set; } = default!;
        public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
    }

    public class NCD_Detail
    {
        public int Id { get; set; }
        [Required, ForeignKey("Patient")]
        public int PatientId { get; set; }
        [Required, ForeignKey("NCD")]
        public int NCDId { get; set; }
        public virtual NCD NCD { get; set; } = default!;
        public virtual Patient Patient { get; set; } = default!;
    }

    public class Allergy_Detail
    {
        public int Id { get; set; }
        [Required, ForeignKey("Patient")]
        public int PatientId { get; set; }
        [Required, ForeignKey("Allergy")]
        public int AllergyId { get; set; }
        public virtual Allergy Allergy { get; set; } = default!;
        public virtual Patient Patient { get; set; } = default!;
    }

    public class PatientDbContext(DbContextOptions<PatientDbContext> options) : DbContext(options)
    {
        public DbSet<Allergy> Allergies { get; set; }
        public DbSet<NCD> NCDs { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<DiseaseInformation> DiseaseInformations { get; set; }
        public DbSet<NCD_Detail> NCD_Details { get; set; }
        public DbSet<Allergy_Detail> Allergy_Details { get; set; }
    }
}
