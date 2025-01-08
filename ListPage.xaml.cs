using SalonBellissima.Models;
namespace SalonBellissima;

public partial class ListPage : ContentPage
{
	public ListPage()
	{
		InitializeComponent();
        LoadCategorii();
    }

    private async void LoadCategorii()
    {
        var categorii = await App.Database.GetCategorieAsync(); 
        categorieListView.ItemsSource = categorii;
    }

    private async void OnAddCategorieClicked(object sender, EventArgs e)
    {
        string result = await DisplayPromptAsync("Adauga Categorie", "Introduceti denumirea categoriei:");
        if (!string.IsNullOrWhiteSpace(result))
        {
            var categorie = new Categorie
            {
                DenumireCategorie = result
            };
            App.Database.SaveCategorieAsync(categorie);
            LoadCategorii(); 
        }
    }
    private async void OnCategorieDelete(object sender, EventArgs e)
    {
        var button = sender as Button;
        var categorie = button?.CommandParameter as Categorie;

        if (categorie != null)
        {
            bool confirm = await DisplayAlert("Confirmare",
                                              $"Sigur doriti sa stergeti categoria {categorie.DenumireCategorie}?",
                                              "Da",
                                              "Nu");

            if (confirm)
            {
                await App.Database.DeleteCategorieAsync(categorie);

                LoadCategorii();
            }
        }
    }
    private async void OnAddServiciuClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var categorie = button?.CommandParameter as Categorie;

        if (categorie != null)
        {
            string denumireServiciu = await DisplayPromptAsync("Adauga Serviciu", "Introduceti denumirea serviciului:");
            if (!string.IsNullOrWhiteSpace(denumireServiciu))
            {
                string pretServiciu = await DisplayPromptAsync("Adauga Serviciu", "Introduceti pretul serviciului:");
                if (decimal.TryParse(pretServiciu, out decimal pret) && pret > 0)
                {
                    string durataServiciu = await DisplayPromptAsync("Adauga Serviciu", "Introduceti durata serviciului (minute):");
                    if (int.TryParse(durataServiciu, out int durata) && durata > 0)
                    {
        
                        var serviciu = new Serviciu
                        {
                            DenumireServiciu = denumireServiciu,
                            Pret = pret,
                            DurataMinute = durata,
                            CategorieId = categorie.Id 
                        };

                        await App.Database.SaveServiciuAsync(serviciu);

                        await DisplayAlert("Succes", $"Serviciul {serviciu.DenumireServiciu} a fost adaugat!", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Eroare", "Durata serviciului nu este valida!", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Eroare", "Pretul serviciului nu este valid!", "OK");
                }
            }
        }
    }
    private async void OnCategorieSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var categorie = e.SelectedItem as Categorie;
        if (categorie != null)
        {
            await Navigation.PushAsync(new ListEntryPage(categorie));
        }
    }
}
