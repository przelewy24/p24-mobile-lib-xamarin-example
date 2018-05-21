using System;
using P24XamarinLib.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using P24Lib;
using Android.Webkit;
using Android.Graphics;
using Android.Runtime;

[assembly: ExportRenderer(typeof(P24WebView), typeof(WebViewRender))]
namespace P24XamarinLib.Droid
{
    public class WebViewRender : WebViewRenderer
    {

        private P24WebViewClient webViewClient;

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

            webViewClient = new P24WebViewClient(element);
            Control?.SetWebViewClient(webViewClient);
        }
    }

    public class P24WebViewClient: WebViewClient {

        private P24WebView p24WebView;

        public P24WebViewClient(P24WebView webView) {
            p24WebView = webView;
        }

        public override void OnPageStarted(Android.Webkit.WebView view, string url, Bitmap favicon)
        {
            p24WebView.OnNavigating(url.ToString());
            base.OnPageStarted(view, url, favicon);
        }

        public override void OnPageFinished(Android.Webkit.WebView view, string url)
        {
            p24WebView.OnNavigated(url.ToString());
            base.OnPageFinished(view, url);
        }

        public override void OnReceivedError(Android.Webkit.WebView view, [GeneratedEnum] ClientError errorCode, string description, string failingUrl)
        {
            p24WebView.OnError(view.Url.ToString());
            base.OnReceivedError(view, errorCode, description, failingUrl);
        }

    }

}