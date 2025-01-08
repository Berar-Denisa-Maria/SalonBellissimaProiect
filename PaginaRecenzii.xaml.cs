using SalonBellissima.Models;

namespace SalonBellissima;

public partial class PaginaRecenzii : ContentPage
{
    private List<Recenzie> _recenzii;

    public PaginaRecenzii()
    {
        InitializeComponent();
        LoadRecenzii();
    }

    private async void LoadRecenzii()
    {
        _recenzii = await App.Database.GetRecenziiAsync();
        recenziiListView.ItemsSource = _recenzii.ToList(); 
    }

    private async void OnAdaugaRecenzieClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(recenzieEntry.Text))
        {
            await DisplayAlert("Eroare", "Te rog sa introduci o recenzie valida.", "OK");
            return;
        }

        var recenzieNoua = new Recenzie
        {
            TextRecenzie = recenzieEntry.Text,
            DataRecenzie = DateTime.Now
        };

        await App.Database.SaveRecenzieAsync(recenzieNoua);

        _recenzii.Add(recenzieNoua);
        recenziiListView.ItemsSource = null;
        recenziiListView.ItemsSource = _recenzii;

        recenzieEntry.Text = string.Empty;
    }


    private async void OnActualizeazaRecenzieClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var recenzie = (Recenzie)button.CommandParameter;

        string recenzieNoua = await DisplayPromptAsync("Actualizeaza Recenzie",
                                                       "Modifica recenzia:",
                                                       initialValue: recenzie.TextRecenzie);

        if (!string.IsNullOrWhiteSpace(recenzieNoua))
        {
       
            recenzie.TextRecenzie = recenzieNoua;
            recenzie.DataRecenzie = DateTime.Now;

            await App.Database.SaveRecenzieAsync(recenzie);

           
            var index = _recenzii.FindIndex(r => r.Id == recenzie.Id);
            if (index != -1)
            {
                _recenzii[index] = recenzie;
            }

            recenziiListView.ItemsSource = null;
            recenziiListView.ItemsSource = _recenzii;
        }
        else
        {
            await DisplayAlert("Eroare", "Textul nu poate fi gol.", "OK");
        }
    }

    private async void OnStergeRecenzieClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var recenzie = (Recenzie)button.CommandParameter;

        bool confirmare = await DisplayAlert("Stergere",
                                             "Sigur doriti sa stergeti aceasta recenzie?",
                                             "Da", "Nu");
        if (confirmare)
        {
            await App.Database.DeleteRecenzieAsync(recenzie);

            _recenzii.Remove(recenzie);
            recenziiListView.ItemsSource = null;
            recenziiListView.ItemsSource = _recenzii;
        }
    }
}

