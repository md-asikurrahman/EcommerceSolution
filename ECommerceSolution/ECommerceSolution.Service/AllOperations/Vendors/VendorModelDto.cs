namespace ECommerceSolution.Service.AllOperations.Vendors
{
    public class VendorModelDto: BaseProperty
    {
        public string? VendorName { get; set; }
        public string? MobileNumber { get; set; }
        public string? Description { get; set; }
        public string? VendorTypeName { get; set; }
        public int VendorTypeId { get; set; }
    }
}
