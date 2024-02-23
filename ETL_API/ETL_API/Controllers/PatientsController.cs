using ETL_API.Repositories.Interfaces;
using ETL_Shared.InputModels;
using ETL_Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ETL_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IGenericRepository<Patient> repo;
        public PatientsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.repo = this.unitOfWork.GetRepository<Patient>();
        }
        [HttpGet]
        public async Task<IEnumerable<Patient>> GetPatents()
        {
            var data = await repo.GetAllAsync();
            return data.ToList();
        }
        [HttpGet("All/Include")]
        public async Task<IEnumerable<Patient>> GetPatentsWithAllInclude()
        {
            var data = await repo.GetAllAsync(x => x
            .Include(p => p.Allergy_Details).ThenInclude(a => a.Allergy)
            .Include(p => p.NCD_Details).ThenInclude(n => n.NCD)
            .Include(x => x.DiseaseInformation));
            return data.ToList();
        }
        [HttpPost]
        public async Task<ActionResult<Patient>> PostPatient(PatientInputModel model)
        {

            var patient = new Patient { PatientName = model.PatientName, Epilepsy = model.Epilepsy, DiseaseInformationId = model.DiseaseInformationId };

            // await unitOfWork.SaveAsync();
            foreach (var id in model.AllergyIds)
            {
                patient.Allergy_Details.Add(new Allergy_Detail { AllergyId = id });
            }
            if (model.NCDIds != null)
            {
                foreach (var id in model.NCDIds)
                {
                    patient.NCD_Details.Add(new NCD_Detail { NCDId = id });
                }
            }
            try
            {
                await repo.InsertAsync(patient);
                await unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return NoContent();

        }
    }
}
