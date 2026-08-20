using Mapster;
using Microsoft.AspNetCore.Mvc;
using PharmacyApi.Dtos;
using PharmacyApi.Entity;
using PharmacyApi.Repositories;

namespace PharmacyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly IGeneriRepository<Sale> _genericRepo;

        public SaleController(IGeneriRepository<Sale> genericRepo)
        {
            _genericRepo = genericRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<ViewSaleDto>>> GetAllAsync(int medicineId)
        {
            var results = await _genericRepo.GetAllAsync();
            var response = results.Where(x => x.MedicineId == medicineId).Adapt<List<ViewSaleDto>>();
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ViewMedicineDto>> GetAsync(int id)
        {
            var results = (await _genericRepo.GetByIdAsync(id)).Adapt<ViewSaleDto>();
            return Ok(results);
        }

        [HttpPost]
        public async Task<ActionResult<ViewMedicineDto>> AddAsync(SaleDto entity)
        {
            var results = (await _genericRepo.AddAsync(entity.Adapt<Sale>())).Adapt<ViewSaleDto>();
            return Ok(results);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateAsync(int id, UpdateSaleDto entity)
        {
            await _genericRepo.UpdateAsync(entity.Adapt<Sale>());
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
