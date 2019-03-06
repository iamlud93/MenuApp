using DinerMax3000.Business.dsDinerMax3000TableAdapters;
using System.Collections.Generic;

namespace DinerMax3000.Business
{
    public class Menu
    {
        public Menu()
        {
            items = new List<MenuItem>();
        }
        private int _databaseId;

        public void SaveNewMenuItem(string Name, string Description, double Price)
        {
            MenuItemTableAdapter tabMenuItem = new MenuItemTableAdapter();
            tabMenuItem.InsertNewMenuItem(Name, Description, Price, _databaseId);
        }
        public static List<Menu> GetAllMenus()
        {
            MenuTableAdapter taMenu = new MenuTableAdapter();
            MenuItemTableAdapter taMenuItem = new MenuItemTableAdapter();

            var dtMenu = taMenu.GetData();
            List<Menu> allMenus = new List<Menu>();
            foreach(dsDinerMax3000.MenuRow menuRow in dtMenu.Rows)
            {
                Menu currentMenu = new Menu();
                currentMenu.Name = menuRow.Name;
                currentMenu._databaseId = menuRow.Id;
                allMenus.Add(currentMenu);

                var dtMenuItems = taMenuItem.GetMenuItemsByMenuId(menuRow.Id);
                foreach(dsDinerMax3000.MenuItemRow menuItemRow in dtMenuItems.Rows)
                {
                    currentMenu.AddMenuItem(menuItemRow.Name, menuItemRow.Description, menuItemRow.Price);
                };
            };
            return allMenus;
        }
        public void AddMenuItem(string Title, string Description, double Price)
        {
            MenuItem item = new MenuItem();
            item.Title = Title;
            item.Description = Description;
            item.Price = Price;
            items.Add(item);
        }
        public string Name { get; set; }
        public List<MenuItem> items { get; set; }
    }
}
