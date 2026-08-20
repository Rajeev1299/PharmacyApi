namespace PharmacyApi.Dtos
{
    public class UpdateMedicineDto :  MedicineDto
    {
        public int Id { get; set; }
    }
    public class ViewMedicineDto: MedicineDto
    {
        public int Id { get; set; }
    }
    public class MedicineDto
    {
        public string FullName { get; set; } = default!;
        public string? Notes { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Brand { get; set; } = default!;
    }
}
