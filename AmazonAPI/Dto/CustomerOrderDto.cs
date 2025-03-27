namespace AmazonAPI.Dto
{
    public class CustomerOrderDto
    {
        // dto used to control what data is exposed ensure fetch only required columns 
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
