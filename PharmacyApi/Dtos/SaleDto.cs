namespace PharmacyApi.Dtos
{
    public class UpdateSaleDto : SaleDto
    {
        public int Id { get; set; }
    }
    public class ViewSaleDto: SaleDto
    {
        public int Id { get; set; }
    }
    public class SaleDto
    {
        public int MedicineId { get; set; }
        public int QuantitySold { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime SaleDate { get; set; }
    }
}
