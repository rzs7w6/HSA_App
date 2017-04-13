using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace HSA_App
{
    public class ViewReceiptsPage : ContentPage
    {

        StackLayout parent = null;

        public ViewReceiptsPage(List<ImageSource> sources)
        {
            parent = new StackLayout();

            foreach (ImageSource source in sources)
            {
                Image image = new Image
                {
                    Source = source
                };
                parent.Children.Add(image);
            }

            Content = parent;
        }
    }
}
