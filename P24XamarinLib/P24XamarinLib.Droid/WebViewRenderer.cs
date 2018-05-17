using System;
using P24XamarinLib.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using P24Lib;

[assembly: ExportRenderer(typeof(P24WebView), typeof(WebViewRender))]
namespace P24XamarinLib.Droid
{
    public class WebViewRender : WebViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.WebView> e)
        {
            base.OnElementChanged(e);

            if (Control != null && e.NewElement != null)
            {
                InitializeCommands((P24WebView)e.NewElement);
            }
        }

        private void InitializeCommands(P24WebView element)
        {
            element.reloadAction = () =>
            {
                Control?.Reload();
            };

        }
    }
}