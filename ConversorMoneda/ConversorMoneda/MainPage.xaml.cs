namespace ConversorMoneda
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnCalcularClicked(object sender, EventArgs e)
        {
            // 1. Validar que los campos no estén vacíos
            if (string.IsNullOrWhiteSpace(TxtPesos.Text) || string.IsNullOrWhiteSpace(TxtTipoCambio.Text))
            {
                await DisplayAlert("Campos Vacíos", "Por favor ingresa tanto el monto como el tipo de cambio.", "OK");
                return;
            }

            // 2. Intentar convertir el texto a números decimales
            bool esPesosValido = double.TryParse(TxtPesos.Text, out double pesos);
            bool esTipoCambioValido = double.TryParse(TxtTipoCambio.Text, out double tipoCambio);

            // 3. Validar valores numéricos correctos
            if (!esPesosValido || !esTipoCambioValido || pesos <= 0 || tipoCambio <= 0)
            {
                await DisplayAlert("Datos Inválidos", "Por favor ingresa valores numéricos mayores a cero.", "OK");
                return;
            }

            // 4. Realizar la operación matemática
            double dolares = pesos / tipoCambio;

            // 5. Mostrar el resultado en la etiqueta correspondiente
            LblResultado.Text = $"${dolares:N2} USD";
        }
    }
}