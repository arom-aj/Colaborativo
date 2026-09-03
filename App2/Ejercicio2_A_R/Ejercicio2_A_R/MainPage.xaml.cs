namespace Ejercicio2_A_R
{
    public partial class MainPage : ContentPage
    {
        private int contador = 10;
        private IDispatcherTimer timer;

        public MainPage()
        {
            InitializeComponent();
        }

        private void BtnIniciar_Clicked(object sender, EventArgs e)
        {
            contador = 10;
            LblContador.Text = contador.ToString();
            BtnIniciar.IsEnabled = false;

            timer = Dispatcher.CreateTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            contador--;

            if (contador <= 0)
            {
                LblContador.Text = "¡Fin!";
                timer.Stop();
                BtnIniciar.IsEnabled = true;
            }
            else
            {
                LblContador.Text = contador.ToString();
            }
        }
    }
}