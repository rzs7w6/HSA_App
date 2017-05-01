using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HSA_App
{
    class ImagePage : ContentPage
    {
        public ImagePage(ImageSource source, string total, string date) {
            Button button = new Button
            {
                Text = "Return"
            };
            button.Clicked += OnButtonClicked;
            Content = new StackLayout()
            {
                Padding = new Thickness(5, 0),
                Children =
                {
                    button,
                    //new Image
                    //{
                    //    Source = source
                    //}
                    new PinchToZoomContainer {
                        Content = new Image {
                            Source = source,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            VerticalOptions = LayoutOptions.FillAndExpand
                        },
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.CenterAndExpand

                    },
                    new Label
                    {
                        Text = "Total: " + total,
                    },
                    new Label
                    {
                        Text = "Date: " + date
                    }
                }
            };
        }

        void OnButtonClicked(object sender, EventArgs e)
        {
            //App.Current.MainPage = new NavigationPage(new NavigationLocal(me));
            App.Current.MainPage.Navigation.PopModalAsync();
        }


        public class PinchToZoomContainer : ContentView
        {
            double startScale { get; set; }
            double currentScale { get; set; }
            double xOffset = 0;
            double yOffset = 0;
            public PinchToZoomContainer()
            {
                var pinchGesture = new PinchGestureRecognizer();
                pinchGesture.PinchUpdated += OnPinchUpdated;
                GestureRecognizers.Add(pinchGesture);
            }

            void OnPinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
            {
                if (e.Status == GestureStatus.Started)
                {
                    // Store the current scale factor applied to the wrapped user interface element,
                    // and zero the components for the center point of the translate transform.
                    startScale = Content.Scale;
                    Content.AnchorX = 0;
                    Content.AnchorY = 0;
                }
                if (e.Status == GestureStatus.Running)
                {
                    // Calculate the scale factor to be applied.
                    currentScale += (e.Scale - 1) * startScale;
                    currentScale = Math.Max(1, currentScale);

                    // The ScaleOrigin is in relative coordinates to the wrapped user interface element,
                    // so get the X pixel coordinate.
                    double renderedX = Content.X + xOffset;
                    double deltaX = renderedX / Width;
                    double deltaWidth = Width / (Content.Width * startScale);
                    double originX = (e.ScaleOrigin.X - deltaX) * deltaWidth;

                    // The ScaleOrigin is in relative coordinates to the wrapped user interface element,
                    // so get the Y pixel coordinate.
                    double renderedY = Content.Y + yOffset;
                    double deltaY = renderedY / Height;
                    double deltaHeight = Height / (Content.Height * startScale);
                    double originY = (e.ScaleOrigin.Y - deltaY) * deltaHeight;

                    // Calculate the transformed element pixel coordinates.
                    double targetX = xOffset - (originX * Content.Width) * (currentScale - startScale);
                    double targetY = yOffset - (originY * Content.Height) * (currentScale - startScale);

                    // Apply translation based on the change in origin.
                    if (targetX > -Content.Width * (currentScale - 1))
                    {

                        Content.TranslationX = -Content.Width * (currentScale - 1);
                    }
                    else if (targetX < 0)
                    {
                        Content.TranslationX = 0;
                    }
                    else
                        Content.TranslationX = targetX;

                    if (targetY > -Content.Height * (currentScale - 1))
                    {

                        Content.TranslationY = -Content.Height * (currentScale - 1);
                    }
                    else if (targetY < 0)
                    {
                        Content.TranslationY = 0;
                    }
                    else
                        Content.TranslationY = targetY;

                    // Apply scale factor.
                    Content.Scale = currentScale;
                }
                if (e.Status == GestureStatus.Completed)
                {
                    // Store the translation delta's of the wrapped user interface element.
                    xOffset = Content.TranslationX;
                    yOffset = Content.TranslationY;
                }
            }
        }
    }
}
