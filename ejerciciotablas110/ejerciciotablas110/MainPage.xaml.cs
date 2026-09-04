using System.Collections.ObjectModel;

namespace ejerciciotablas110
{
    public partial class MainPage : ContentPage
    {
        

        public MainPage()
        {
            InitializeComponent();
            CargarPicker();
        }
        private void CargarPicker()
        {
            for (int i = 1; i <= 10; i++)
            {
                PickerTabla.Items.Add(i.ToString());
            }

            PickerTabla.SelectedIndex = 0; // Selecciona la tabla del 1 por defecto
        }

        private void PickerTabla_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PickerTabla.SelectedIndex == -1)
                return;

            int numero = PickerTabla.SelectedIndex + 1;
            MostrarTabla(numero);
        }

        private void MostrarTabla(int numero)
        {
            var resultados = new ObservableCollection<string>();

            for (int i = 1; i <= 10; i++)
            {
                resultados.Add($"{numero} x {i} = {numero * i}");
            }

            CvResultados.ItemsSource = resultados;
        }

    }
}
