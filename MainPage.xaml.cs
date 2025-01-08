using SalonBellissima.Models;
namespace SalonBellissima
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            VerificaProgramari();
        }

        private async void OnCategoriiButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListPage());
        }

        private async void VerificaProgramari()
        {
            var programari = await App.Database.GetProgramariAsync();
            DateTime dataMaine = DateTime.Now.Date.AddDays(1);  //stabilim data de maine

           
            var programareMaine = programari.FirstOrDefault(p => p.DataOra.Date == dataMaine);

          
            if (programareMaine != null)
            {
                var serviciu = await App.Database.GetServiciuAsync();
                var serviciuSelectat = serviciu.FirstOrDefault(s => s.Id == programareMaine.ServiciuId);
                notificareLabel.Text = $"Maine aveti programarea: {serviciuSelectat.DenumireServiciu} la ora {programareMaine.DataOra.ToShortTimeString()}";
            }
            else
            {
                notificareLabel.Text = "Nu aveti programari pentru maine.";
            }
        }

        private async void OnNavigareProgramariClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PaginaProgramare());
        }
    }

}

