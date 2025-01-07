namespace SalonBellissima
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnCategoriiButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListPage());
        }


    }

}
