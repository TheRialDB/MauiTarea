﻿using System.Net;
using System.Collections.Generic;
using Microsoft.Maui.Controls;
using static System.Net.WebRequestMethods;


namespace MauiTarea
{
    public partial class MainPage : ContentPage
    {
       
        // Lista de URLs de imágenes
        private readonly List<string> _imageUrls = new List<string>
        {
            "https://th.bing.com/th/id/OIP.nBTz4kBijuW9us8tdxKScwHaKW?rs=1&pid=ImgDetMain",
                "https://preview.redd.it/ronaldo-icon-card-v0-uwxj4tncv32b1.png?width=640&crop=smart&auto=webp&s=4a4e1000d60afe07cf586e1228b2e8c15e427f20",
                "https://cardsplus.it/wp-content/uploads/2020/09/MBAPPE-1099x1536.png",
                "https://media.tycsports.com/files/2021/08/19/321578/la-mejor-carta-de-messi-en-la-historia-de-fifa_w862.png",
                "https://cardsplus.it/wp-content/uploads/2021/01/Bruno-Fernandes-97-TOTY-1099x1536.png",
                "https://www.fifaultimateteam.it/en/wp-content/uploads/2021/06/Paredes.jpg",
                "https://1.bp.blogspot.com/-QNKGMyXWaTY/WIJd60TyNKI/AAAAAAAAAGY/7v2UO-kddCIpnpMuBnnlew-o-zAxul3kACLcB/s320/BRONCE%2B%25C3%259ANICO%2BDORSCH.png",
                "https://th.bing.com/th/id/OIP.nxP-IxMp_i-x89Dn52rQSgHaLH?rs=1&pid=ImgDetMain",
                "https://media.contentapi.ea.com/content/dam/ea/easfc/fc-24/ratings/common/full/player-shields/en/237993.png.adapt.265w.png",
                "https://media.contentapi.ea.com/content/dam/ea/easfc/fc-24/ratings/common/full/player-shields/fr/274081.png.adapt.250w.png",
                "https://media.contentapi.ea.com/content/dam/ea/easfc/fc-24/ratings/common/full/player-shields/en/240964.png.adapt.250w.png",
                "https://cardsplus.it/wp-content/uploads/2023/04/FIFA-23-Zidane-Trophy-Titans-Icon.png"

        };

        private ImageButton _randomImageButton;
        private Image _displayedImage;

        public MainPage()
        {
            InitializeComponent();

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
                Children = { _randomImageButton, _displayedImage }
            };

            // Asignar el layout a la ContentPage
            Content = stackLayout;
        }

        private void OnShowRandomImageButtonClicked(object sender, EventArgs e)
        {
            // Seleccionar una imagen aleatoria
            var random = new Random();
            int index = random.Next(_imageUrls.Count);

            //// Mostrar la imagen seleccionada en el ImageButton
            //_randomImageButton.Source = _imageUrls[index];

            // También puedes mostrar la imagen en el control Image, si lo deseas
            _displayedImage.Source = _imageUrls[index];
            _displayedImage.IsVisible = true;
        }

    }

}
