using System.Net;
using System.Collections.Generic;
using Microsoft.Maui.Controls;
using static System.Net.WebRequestMethods;


namespace MauiTarea
{
    public partial class MainPage : ContentPage
    {

        //// Lista de URLs de imágenes
        //private readonly List<string> _imageUrls = new List<string>
        //{
        //    "https://th.bing.com/th/id/OIP.nBTz4kBijuW9us8tdxKScwHaKW?rs=1&pid=ImgDetMain",
        //        "https://preview.redd.it/ronaldo-icon-card-v0-uwxj4tncv32b1.png?width=640&crop=smart&auto=webp&s=4a4e1000d60afe07cf586e1228b2e8c15e427f20",
        //        "https://cardsplus.it/wp-content/uploads/2020/09/MBAPPE-1099x1536.png",
        //        "https://media.tycsports.com/files/2021/08/19/321578/la-mejor-carta-de-messi-en-la-historia-de-fifa_w862.png",
        //        "https://cardsplus.it/wp-content/uploads/2021/01/Bruno-Fernandes-97-TOTY-1099x1536.png",
        //        "https://www.fifaultimateteam.it/en/wp-content/uploads/2021/06/Paredes.jpg",
        //        "https://1.bp.blogspot.com/-QNKGMyXWaTY/WIJd60TyNKI/AAAAAAAAAGY/7v2UO-kddCIpnpMuBnnlew-o-zAxul3kACLcB/s320/BRONCE%2B%25C3%259ANICO%2BDORSCH.png",
        //        "https://th.bing.com/th/id/OIP.nxP-IxMp_i-x89Dn52rQSgHaLH?rs=1&pid=ImgDetMain",
        //        "https://media.contentapi.ea.com/content/dam/ea/easfc/fc-24/ratings/common/full/player-shields/en/237993.png.adapt.265w.png",
        //        "https://media.contentapi.ea.com/content/dam/ea/easfc/fc-24/ratings/common/full/player-shields/fr/274081.png.adapt.250w.png",
        //        "https://media.contentapi.ea.com/content/dam/ea/easfc/fc-24/ratings/common/full/player-shields/en/240964.png.adapt.250w.png",
        //        "https://cardsplus.it/wp-content/uploads/2023/04/FIFA-23-Zidane-Trophy-Titans-Icon.png"

        //};
        private readonly List<string> _imagenesLocales = new List<string>
        {
            "messi.jpg",
            "lewandoski.jpg",
            "dimaria.png",
            "cronaldo.png",
            "neymar.jpg",
            "senesi.png",
            "rossi.png",
            "lemos.png",
            "araujo.jpg",
            "arena.png",
            "compagnucci.png",
            "dimarco.png",
            "scamaca.png"
        };


        private readonly List<int> _imagenesProbabilidades = new List<int>
        {
             5,             //messi
             5,             //lewandoski
             5,             //dimaria
             5,             //cristiano ronaldo
             5,             //neymar
            15,             //sensi
            15,             //rossi
            15,             //lemos
            10,             //araujo
            10,             //arena
            10,             //compagnucci
            10,             //dimarco
            10              //scamaca
        };

        // Lista de valores para cada carta
        private readonly List<decimal> _cartasValores = new List<decimal>
        {
            100, 40, 40, 70, 50, 10, 10, 10, 5, 5, 5, 5, 5
        };

        // Saldo inicial y costo del sobre
        private decimal _saldo = 50;
        private readonly decimal _costeSobre = 10;

        private ImageButton _randomImageButton;
        private Image _displayedImage;
        private Label _balanceLabel;

        public MainPage()
        {
            InitializeComponent();

            // Crear y configurar el saldo del jugador
            _balanceLabel = new Label
            {
                Text = $"Saldo: {_saldo} monedas",
                FontSize = 18,
                HorizontalOptions = LayoutOptions.Center
            };

            // Crear y configurar ImageButton
            _randomImageButton = new ImageButton
            {
                Source = "https://th.bing.com/th/id/OIP.DCR6At-KNr4HhlHctd8hTQAAAA?rs=1&pid=ImgDetMain", // Imagen por defecto
                Aspect = Aspect.AspectFit,
                HeightRequest = 200,
                WidthRequest = 200
            };

            // Crear y configurar Image
            _displayedImage = new Image
            {
                Aspect = Aspect.AspectFit,
                HeightRequest = 200,
                WidthRequest = 200,
                IsVisible = false
            };

            // Configurar evento Clicked
            _randomImageButton.Clicked += OnShowRandomImageButtonClicked;

            // Crear y configurar el layout
            var stackLayout = new VerticalStackLayout
            {
                Padding = 10,
                Children = { _balanceLabel, _randomImageButton, _displayedImage }
            };

            // Asignar el layout a la ContentPage
            Content = stackLayout;
        }

        // Actualizar la visualización del saldo
        private void UpdateBalanceDisplay()
        {
            _balanceLabel.Text = $"Saldo: {_saldo} monedas";
        }

        private string ObtenerCarta()
        {
            // Sumar todas las probabilidades
            int probabilidadTotal = _imagenesProbabilidades.Sum();

            // Seleccionar un número aleatorio entre 0 y la suma de las probabilidades
            var random = new Random();
            int randomNumber = random.Next(0, probabilidadTotal);

            // Seleccionar la imagen basada en el número aleatorio
            int calculoProbabilidad = 0;

            for (int i = 0; i < _imagenesLocales.Count; i++)
            {
                calculoProbabilidad += _imagenesProbabilidades[i];
                if (randomNumber < calculoProbabilidad)
                {
                    return _imagenesLocales[i]; // Retornar la imagen seleccionada
                }
            }

            // En caso de que falle (no debería), retornar la primera imagen por defecto
            return _imagenesLocales[0];
        }


        private void OnShowRandomImageButtonClicked(object sender, EventArgs e)
        {
            if (_saldo < _costeSobre)
            {
                DisplayAlert("Sin saldo", "No tienes suficiente saldo para abrir un sobre.", "OK");
                return;
            }

            _saldo -= _costeSobre;

            string selectedImage = ObtenerCarta();
            int selectedIndex = _imagenesLocales.IndexOf(selectedImage);
            decimal cardValue = _cartasValores[selectedIndex];

            _saldo += cardValue;

            _displayedImage.Source = ImageSource.FromFile(selectedImage);
            _displayedImage.IsVisible = true;

            UpdateBalanceDisplay();
            DisplayAlert("Nueva Carta", $"¡Ganaste {cardValue} monedas! Saldo actual: {_saldo} monedas.", "OK");
        }

    }

}
