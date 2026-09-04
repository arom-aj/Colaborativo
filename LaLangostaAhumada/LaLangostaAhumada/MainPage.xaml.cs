

using System.Collections.ObjectModel;
using System.Globalization;
 
namespace LaLangostaAhumada;

public partial class MainPage : ContentPage
{
    // Tarifas del negocio
    private const decimal COSTO_BASE = 95.00m;      // 1 a 200 personas
    private const decimal COSTO_MEDIO = 85.00m;     // 201 a 300 personas
    private const decimal COSTO_MAYOREO = 75.00m;   // más de 300 personas

    // Historial de cotizaciones (en memoria, dura mientras la app esté abierta)
    public ObservableCollection<CotizacionItem> HistorialCotizaciones { get; set; } = new();

    public MainPage()
    {
        InitializeComponent();
        BindingContext = this;
        ActualizarVisibilidadHistorial();
    }

    private void BtnCalcular_Clicked(object sender, EventArgs e)
    {
        // Ocultar mensajes previos
        LblError.IsVisible = false;
        FrameResultado.IsVisible = false;

        string nombreCliente = EntryNombreCliente.Text?.Trim() ?? string.Empty;
        string textoPersonas = EntryNumeroPersonas.Text?.Trim() ?? string.Empty;

        // Validaciones
        if (string.IsNullOrWhiteSpace(nombreCliente))
        {
            MostrarError("Por favor ingresa el nombre del cliente.");
            return;
        }

        if (!int.TryParse(textoPersonas, NumberStyles.Integer, CultureInfo.InvariantCulture, out int numeroPersonas))
        {
            MostrarError("Ingresa un número de personas válido.");
            return;
        }

        if (numeroPersonas <= 0)
        {
            MostrarError("El número de personas debe ser mayor a cero.");
            return;
        }

        // Determinar costo por platillo según la cantidad de personas
        decimal costoPorPlatillo = CalcularCostoPorPersona(numeroPersonas);
        decimal total = numeroPersonas * costoPorPlatillo;

        // Mostrar resultado en pantalla
        LblCliente.Text = nombreCliente;
        LblPersonas.Text = numeroPersonas.ToString();
        LblCostoUnitario.Text = costoPorPlatillo.ToString("C2");
        LblTotal.Text = total.ToString("C2");
        FrameResultado.IsVisible = true;

        // Agregar al historial (se inserta al inicio para ver lo más reciente primero)
        HistorialCotizaciones.Insert(0, new CotizacionItem
        {
            Cliente = nombreCliente,
            Detalle = $"{numeroPersonas} personas · {costoPorPlatillo:C2} c/u · {DateTime.Now:dd/MM/yyyy hh:mm tt}",
            TotalFormateado = total.ToString("C2")
        });

        ActualizarVisibilidadHistorial();
    }

    private void BtnReiniciar_Clicked(object sender, EventArgs e)
    {
        // Limpia los campos de entrada y oculta el resultado, sin tocar el historial
        EntryNombreCliente.Text = string.Empty;
        EntryNumeroPersonas.Text = string.Empty;
        LblError.IsVisible = false;
        FrameResultado.IsVisible = false;
        EntryNombreCliente.Focus();
    }

    private void BtnLimpiarHistorial_Clicked(object sender, EventArgs e)
    {
        HistorialCotizaciones.Clear();
        ActualizarVisibilidadHistorial();
    }

    private decimal CalcularCostoPorPersona(int numeroPersonas)
    {
        if (numeroPersonas > 300)
            return COSTO_MAYOREO;
        else if (numeroPersonas > 200) // 201 a 300
            return COSTO_MEDIO;
        else // 1 a 200
            return COSTO_BASE;
    }

    private void MostrarError(string mensaje)
    {
        LblError.Text = mensaje;
        LblError.IsVisible = true;
    }

    private void ActualizarVisibilidadHistorial()
    {
        // Muestra el mensaje "sin cotizaciones" solo cuando la lista está vacía
        bool hayHistorial = HistorialCotizaciones.Count > 0;
        LblSinHistorial.IsVisible = !hayHistorial;
        ListaHistorial.IsVisible = hayHistorial;
    }
}

// Modelo simple para cada elemento del historial
public class CotizacionItem
{
    public string Cliente { get; set; } = string.Empty;
    public string Detalle { get; set; } = string.Empty;
    public string TotalFormateado { get; set; } = string.Empty;
}