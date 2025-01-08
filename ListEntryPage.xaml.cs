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
    private async void OnUpdateServiciuClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var serviciu = (Serviciu)button.CommandParameter;

        string denumireNoua = await DisplayPromptAsync("Update Serviciu", "Introdu denumirea noua:");
        if (!string.IsNullOrEmpty(denumireNoua))
        {
            serviciu.DenumireServiciu = denumireNoua;
            await App.Database.SaveServiciuAsync(serviciu);
            LoadServicii(); 
        }
    }

    private async void OnDeleteServiciuClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var serviciu = (Serviciu)button.CommandParameter;

        bool confirmare = await DisplayAlert("Stergere", "Sigur doresti sa stergi acest serviciu?", "Da", "Nu");
        if (confirmare)
        {
            await App.Database.DeleteServiciuAsync(serviciu);
            LoadServicii();
        }
    }


}