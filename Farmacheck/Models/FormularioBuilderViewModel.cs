namespace Farmacheck.Models
{
    public class FormularioBuilderViewModel
    {
        public int FormularioId { get; set; }
        public string NombreFormulario { get; set; }
        public FormularioViewModel FormularioConfiguracion { get; set; } = new();
        public List<SeccionViewModel> Secciones { get; set; } = new();
    }
}
