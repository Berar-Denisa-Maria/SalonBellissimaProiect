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

    // Adaugarea unui angajat
    private async void OnAddAngajatClicked(object sender, EventArgs e)
    {
        string nume = await DisplayPromptAsync("Adauga Angajat", "Introduceti numele:");
        string prenume = await DisplayPromptAsync("Adauga Angajat", "Introduceti prenumele:");
        string functie = await DisplayPromptAsync("Adauga Angajat", "Introduceti functia:");
        var angajat = new Angajat { Nume = nume, Prenume = prenume, Functie = functie };
        await App.Database.SaveAngajatAsync(angajat);
        LoadAngajati();
    }
}