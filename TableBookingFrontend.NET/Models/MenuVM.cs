namespace TableBookingFrontend.NET.Models
{
    public class MenuVM
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public ICollection<MenuItemVM> MenuItems { get; set; }
    }
}
