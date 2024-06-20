using System.Windows.Input;

namespace App_CF.Model
{
    public class MenuItems
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public ICommand Command { get; set; }
    }
}