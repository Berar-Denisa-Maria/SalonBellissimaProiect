using SalonBellissima.Models;

namespace SalonBellissima;

public partial class PaginaProgramare : ContentPage
{
    private List<Serviciu> _serviciiDisponibile;
    private List<Programare> _programariExistente;

    public PaginaProgramare()
    {
        InitializeComponent();
        LoadData();
    }

    private async void LoadData()
    {
        _serviciiDisponibile = await App.Database.GetServiciuAsync();
        _programariExistente = await App.Database.GetProgramariAsync();

        serviciuPicker.ItemsSource = _serviciiDisponibile;
        serviciuPicker.ItemDisplayBinding = new Binding("DenumireServiciu");

        programariListView.ItemsSource = _programariExistente;
    }

    private async void OnConfirmareProgramareClicked(object sender, EventArgs e)
    {
        if (serviciuPicker.SelectedItem == null)
        {
            await DisplayAlert("Eroare", "Te rog sa selectezi un serviciu.", "OK");
            return;
        }

        var serviciuSelectat = (Serviciu)serviciuPicker.SelectedItem;
        DateTime dataSelectata = dataPicker.Date.Add(oraPicker.Time);

        var programareNoua = new Programare
        {
            ServiciuId = serviciuSelectat.Id,
            DataOra = dataSelectata
        };

        await App.Database.SaveProgramareAsync(programareNoua);

        _programariExistente.Add(programareNoua);
        programariListView.ItemsSource = null;
        programariListView.ItemsSource = _programariExistente;

        await DisplayAlert("Succes", $"Programare adaugata pentru {serviciuSelectat.DenumireServiciu} la {dataSelectata}", "OK");
    }
    private async void OnUpdateProgramareClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var programare = (Programare)button.CommandParameter;

        string dataNouaString = await DisplayPromptAsync(
            "Actualizeaza Programare",
            "Introdu o noua data (YYYY-MM-DD):",
            "OK",
            "Anuleaza",
            initialValue: programare.DataOra.ToString("yyyy-MM-dd"));

        
        if (!string.IsNullOrEmpty(dataNouaString) && DateTime.TryParse(dataNouaString, out DateTime dataSelectata))
        {
            string oraNouaString = await DisplayPromptAsync(
                "Actualizeaza Ora",
                "Introdu o noua ora (HH:mm):",
                "OK",
                "Anuleaza",
                initialValue: programare.DataOra.ToString("HH:mm"));

            
            if (!string.IsNullOrEmpty(oraNouaString) && TimeSpan.TryParse(oraNouaString, out TimeSpan oraSelectata))
            {
                programare.DataOra = dataSelectata.Date + oraSelectata;
                await App.Database.SaveProgramareAsync(programare);
                LoadData();
            }
            else
            {
                await DisplayAlert("Eroare", "Ora introdusa nu este valida.", "OK");
            }
        }
        else
        {
            await DisplayAlert("Eroare", "Data introdusa nu este valida.", "OK");
        }
    }

        private async void OnDeleteProgramareClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var programare = (Programare)button.CommandParameter;

        bool confirmare = await DisplayAlert("Stergere", "Sigur doresti sa stergi aceasta programare?", "Da", "Nu");
        if (confirmare)
        {
            await App.Database.DeleteProgramareAsync(programare);
            LoadData();
        }
    }
}