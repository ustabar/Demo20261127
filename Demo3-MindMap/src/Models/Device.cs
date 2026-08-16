namespace ContosoDemo.Inventory.Models
{
    public class Device
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; } // "tv" | "whitegoods" | "accessory"
        public bool IsActive { get; set; }
    }
}
