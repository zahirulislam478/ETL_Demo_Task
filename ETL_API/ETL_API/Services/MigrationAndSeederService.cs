using ETL_Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace ETL_API.Services
{
    public class MigrationAndSeederService(PatientDbContext db) 
    {
        public async Task MigrateAsync()
        {
            var pendingMigrations = await db.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
            {
                await db.Database.MigrateAsync();
            }
        }
        public async Task SeedAsync()
        {
            if (!(await db.Allergies.AnyAsync()))
            {
                await db.Allergies.AddRangeAsync(new Allergy[]
               {
                    new Allergy{ AllergyName="Drugs - Penicillin"},
                    new Allergy{ AllergyName="Drugs - Others" },
                    new Allergy{ AllergyName="Animals"},
                    new Allergy{ AllergyName="Food"},
                    new Allergy{ AllergyName="Oinments"},
                    new Allergy{ AllergyName="Plant"},
                    new Allergy{ AllergyName="Sprays"},
                    new Allergy{ AllergyName="Others"},
                    new Allergy{ AllergyName="No Allergies"}
               });
            }

            if (!await db.NCDs.AnyAsync())
            {
                await db.NCDs.AddRangeAsync(new NCD[]
                {
                new NCD{ NCDName="Asthma"},
                new NCD{ NCDName="Cancer"},
                new NCD{ NCDName="Disorders of ear"},
                new NCD{ NCDName="Disorder of eye"},
                new NCD{ NCDName="Mental illness"},
                new NCD{ NCDName="Oral health problems"}
                });
            }
            if (!await db.DiseaseInformations.AnyAsync())
            {
                await db.DiseaseInformations.AddRangeAsync(new DiseaseInformation[]
               {
                    new DiseaseInformation{ DiseaseName="Arthritis"},
                    new DiseaseInformation{ DiseaseName="Diabetes"},
                    new DiseaseInformation{ DiseaseName="Parkinson's Disease"}
               });
            }

            await db.SaveChangesAsync();
        }
    }
}

