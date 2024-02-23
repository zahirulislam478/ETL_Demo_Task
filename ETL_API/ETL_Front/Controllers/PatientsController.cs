using ETL_Shared.InputModels;
using ETL_Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ETL_Front.Controllers
{
    public class PatientsController : Controller
    {
        private readonly HttpClient client;
        public PatientsController()
        {
            this.client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5015");
        }

        public async Task<IActionResult> Index()
        {
            string x = await client.GetStringAsync("api/Patients/All/Include");
            var patients = JsonConvert.DeserializeObject<List<Patient>>(x);
            return View(patients);
        }
        public async Task<IActionResult> Create(string msg = "")
        {
            ViewBag.Allergies = await client.GetFromJsonAsync<List<Allergy>>("api/Allergies");
            var x = await client.GetStringAsync("api/DiseaseInformations");
            ViewBag.DiseaseInformations = JsonConvert.DeserializeObject<List<DiseaseInformation>>(x);
            x = await client.GetStringAsync("api/NCDs");
            ViewBag.NCDs = JsonConvert.DeserializeObject<List<NCD>>(x);
            ViewBag.Msg = msg;
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(PatientInputModel model)
        {
            if (ModelState.IsValid)
            {
                var r = await client.PostAsJsonAsync<PatientInputModel>("api/Patients", model);

                if (r.IsSuccessStatusCode)
                {
                    return RedirectToAction("Create", routeValues: new { msg = "done" });
                }
            }
            ViewBag.Allergies = await client.GetFromJsonAsync<List<Allergy>>("api/Allergies");
            var x = await client.GetStringAsync("api/DiseaseInformations");
            ViewBag.DiseaseInformations = JsonConvert.DeserializeObject<List<DiseaseInformation>>(x);
            x = await client.GetStringAsync("api/NCDs");
            ViewBag.NCDs = JsonConvert.DeserializeObject<List<NCD>>(x);
            ViewBag.Msg = "";
            return RedirectToAction("Create", model);
        }
    }
}
