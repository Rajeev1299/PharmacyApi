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
        private readonly IGeneriRepository<Sale> _saleRepo;
        private readonly IGeneriRepository<Medicine> _medicineRepo;
        public SaleController(IGeneriRepository<Sale> SaleRepo, IGeneriRepository<Medicine> medicineRepo)
        {
            _saleRepo = SaleRepo;
            _medicineRepo = medicineRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<ViewSaleDto>>> GetAllAsync(int medicineId)
        {
            var results = await _saleRepo.GetAllAsync();
            var response = results.Where(x => x.MedicineId == medicineId).Adapt<List<ViewSaleDto>>();
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ViewSaleDto>> RecordSaleAsync(int medicineId, int quantitySold)
        {
            var medicine = await _medicineRepo.GetByIdAsync(medicineId)
            ?? throw new KeyNotFoundException("Medicine not found");

            if (medicine.Quantity < quantitySold)
                throw new InvalidOperationException("Not enough stock");

            medicine.Quantity -= quantitySold;
            await _medicineRepo.UpdateAsync(medicine);

            var sale = new Sale
            {
                MedicineId = medicineId,
                QuantitySold = quantitySold,
                TotalPrice = medicine.Price * quantitySold,
                SaleDate = DateTime.UtcNow
            };
            var result = await _saleRepo.AddAsync(sale);
            return Ok(result.Adapt<ViewSaleDto>());
        }
    }
}
