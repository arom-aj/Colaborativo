namespace NumerosPares;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void MostrarPares_Clicked(object sender, EventArgs e)
    {
        string numeros = "";

        for (int i = 0; i <= 100; i++)
        {
            if (i % 2 == 0)
            {
                numeros += i + " ";
            }
        }

        lblNumeros.Text = numeros;
    }
}