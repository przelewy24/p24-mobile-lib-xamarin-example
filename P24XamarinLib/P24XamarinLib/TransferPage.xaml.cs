using P24Lib.Helpers;
using P24Lib.Models.Params;
using Xamarin.Forms;
using P24Lib;

namespace P24XamarinLib
{
    public partial class TransferPage : ContentPage
    {

        public TransferPage(TrnDirectParams transactionParams)
        {
            var content = TransferPageHelper.ContentForTrnDirect(transactionParams, WebView_Navigating);
            Content = content;
        }

        public TransferPage(TrnRequestParams transactionParams)
        {
            var content = TransferPageHelper.ContentForTrnRequest(transactionParams, WebView_Navigating);
            Content = content;
        }

        public TransferPage(ExpressParams transactionParams)
        {
            var content = TransferPageHelper.ContentForExpress(transactionParams, WebView_Navigating);
            Content = content;
        }

        private void WebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            if (TransferPageHelper.IfTransactionFinished(e))
            {
                Navigation.PopAsync();
            }
        }

        protected override bool OnBackButtonPressed()
        {
            if (TransferPageHelper.CanMoveToBankList())
            {
                DisposeWebView();
                var newContent = TransferPageHelper.GetContentForBack(WebView_Navigating);
                Content = newContent;
                return true;
            }
            else
            {
                DisposeWebView();
                Content = null;
                return base.OnBackButtonPressed();
            }
        }

        public void DisposeWebView()
        {
            if (Content != null)
            {
                (Content as WebViewWithProgress).DisposeWebView();
            }
        }

        protected override void OnDisappearing()
        {
            DisposeWebView();
        }

    }
}