namespace Farmacheck.Models
{
    public class NotificationFormatViewModel
    {
        public long Id { get; set; }

        public string Descripcion { get; set; } = string.Empty;

        public bool IsChecked { get; set; }
    }
}
