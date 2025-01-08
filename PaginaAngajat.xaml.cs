using SalonBellissima.Models;

namespace SalonBellissima;

public partial class PaginaAngajat : ContentPage
{
	public PaginaAngajat()
	{
		InitializeComponent();
        LoadAngajati();
    }
    private async void LoadAngajati()
    {
        var angajati = await App.Database.GetAngajatiAsync();
        angajatiListView.ItemsSource = angajati;
    }

    private async void OnAddAngajatClicked(object sender, EventArgs e)
    {
        string nume = await DisplayPromptAsync("Adauga Angajat", "Introduceti numele:");
        string functie = await DisplayPromptAsync("Adauga Angajat", "Introduceti functia:");
        var angajat = new Angajat { Nume = nume, Functie = functie };
        await App.Database.SaveAngajatAsync(angajat);
        LoadAngajati();
    }
    private async void OnDeleteAngajatClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var angajat = (Angajat)button.CommandParameter;

        bool confirm = await DisplayAlert("Stergere", $"Sigur doriti sa stergeti pe {angajat.Nume}?", "Da", "Nu");
        if (confirm)
        {
            await App.Database.DeleteAngajatAsync(angajat);
            LoadAngajati();
        }
    }

    private async void OnUpdateAngajatClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var angajat = (Angajat)button.CommandParameter;

        string numeNou = await DisplayPromptAsync("Actualizeaza Angajat", $"Actualizeaza numele pentru {angajat.Nume}:", initialValue: angajat.Nume);
        string functieNoua = await DisplayPromptAsync("Actualizeaza Angajat", $"Actualizeaza functia pentru {angajat.Nume}:", initialValue: angajat.Functie);

        if (!string.IsNullOrWhiteSpace(numeNou) && !string.IsNullOrWhiteSpace(functieNoua))
        {
            angajat.Nume = numeNou;
            angajat.Functie = functieNoua;

            await App.Database.SaveAngajatAsync(angajat);
            LoadAngajati();
        }
        else
        {
            await DisplayAlert("Eroare", "Toate campurile trebuie completate!", "OK");
        }
    }
}