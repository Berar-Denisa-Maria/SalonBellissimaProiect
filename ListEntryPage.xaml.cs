using SalonBellissima.Models;
namespace SalonBellissima;

public partial class ListEntryPage : ContentPage
{
    private int _categorieId;
    public ListEntryPage(Categorie categorie)
    {
        InitializeComponent();
        _categorieId = categorie.Id;
        Title = $"Servicii pentru: {categorie.DenumireCategorie}";
        LoadServicii();
    }

    private async void LoadServicii()
    {
        try
        {
            
            var servicii = await App.Database.GetServiciiByCategorieIdAsync(_categorieId);
            serviciiListView.ItemsSource = servicii;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Eroare", $"Nu s-au putut incarca serviciile: {ex.Message}", "OK");
        }
    }

}