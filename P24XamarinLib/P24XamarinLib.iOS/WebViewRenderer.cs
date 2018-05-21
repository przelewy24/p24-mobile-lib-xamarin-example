using P24XamarinLib.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using System.Threading.Tasks;
using P24Lib;
using Foundation;

[assembly: ExportRenderer(typeof(P24WebView), typeof(WebViewRender))]
namespace P24XamarinLib.iOS
{
    public class WebViewRender : WebViewRenderer
    {

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            var webView = e.NewElement as P24WebView;
            if (webView != null)
            {
                webView.EvaluateJavascript = (js) =>
                {
                    return Task.FromResult(this.EvaluateJavascript(js));
                };

                initRefreshCommand(webView);
            }


        }


        private void initRefreshCommand(P24WebView p24WebView)
        {
            if (NativeView != null)
            {
                var uiWebView = (UIWebView)NativeView;
                p24WebView.reloadAction = () =>
                {
                    uiWebView.Reload();
                };

            }

        }

    }

}
