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
}