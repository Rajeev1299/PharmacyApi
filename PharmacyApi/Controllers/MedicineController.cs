using Mapster;
using Microsoft.AspNetCore.Mvc;
using PharmacyApi.Dtos;
using PharmacyApi.Entity;
using PharmacyApi.Repositories;

namespace PharmacyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private readonly IGeneriRepository<Medicine> _genericRepo;

        public MedicineController(IGeneriRepository<Medicine> genericRepo)
        {
            _genericRepo = genericRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<ViewMedicineDto>>> GetAllAsync([FromQuery] string? search = null)
        {
            var results = await _genericRepo.GetAllAsync();
            if (!string.IsNullOrEmpty(search))
            {
                results = results.Where(m => m.FullName.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            var response = results.Adapt<List<ViewMedicineDto>>();
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ViewMedicineDto>> GetAsync(int id)
        {
            var results = (await _genericRepo.GetByIdAsync(id)).Adapt<ViewMedicineDto>();
            return Ok(results);
        }

        [HttpPost]
        public async Task<ActionResult<ViewMedicineDto>> AddAsync(MedicineDto entity)
        {
            var results = (await _genericRepo.AddAsync(entity.Adapt<Medicine>())).Adapt<ViewMedicineDto>();
            return Ok(results);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateAsync(int id, UpdateMedicineDto entity)
        {
            await _genericRepo.UpdateAsync(entity.Adapt<Medicine>());
            return Ok("Successfully updated");
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            await _genericRepo.DeleteAsync(id);
            return Ok("Successfully deleted");
        }
    }
}
