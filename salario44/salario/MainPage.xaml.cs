namespace salario
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object? sender, EventArgs e)
        {
            bool horasOk = double.TryParse(EntryHoras.Text, out double horas);
            bool pagoOk = double.TryParse(EntryPagoPorHora.Text, out double pagoPorHora);

            if (!horasOk || !pagoOk)
            {
                LabelResultado.Text = "Por favor ingresá valores numéricos válidos.";
                return;
            }

            double sueldo = horas * pagoPorHora;

            LabelResultado.Text = $"Sueldo semanal: ${sueldo:0.00}";
        }
    }
}
