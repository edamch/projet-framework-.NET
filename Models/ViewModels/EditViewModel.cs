namespace tp2.Models.ViewModels
{
    public class EditViewModel: CreateViewModels
    {
        public int ProductId { get; set; }
        public string ExistingImagePath { get; set; }
    }
}
