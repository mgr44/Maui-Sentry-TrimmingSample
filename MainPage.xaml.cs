namespace SentryPOC
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnButtonClicked(object sender, EventArgs e)
        {
            SentrySdk.CauseCrash(CrashType.Managed);
        }
    }

}
