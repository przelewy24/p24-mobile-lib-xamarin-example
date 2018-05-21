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
            var content = TransferPageHelper.ContentForTrnDirect(transactionParams);
            Content = content;
        }

        public TransferPage(TrnRequestParams transactionParams)
        {
            var content = TransferPageHelper.ContentForTrnRequest(transactionParams);
            Content = content;
        }

        public TransferPage(ExpressParams transactionParams)
        {
            var content = TransferPageHelper.ContentForExpress(transactionParams);
            Content = content;
        }

        protected override bool OnBackButtonPressed()
        {
            if (TransferPageHelper.CanMoveToBankList())
            {
                DisposeWebView();
                var newContent = TransferPageHelper.GetContentForBack();
                Content = newContent;
                return true;
            }
            else
            {
                DisposeWebView();
                Content = null;
                BanksUrl.Clear();
                Navigation.PopAsync();
                return true;
            }
        }

        public void DisposeWebView() {
            if (Content != null) {
                (Content as WebViewWithProgress).DisposeWebView();
            }
        }

        protected override void OnDisappearing() {
            DisposeWebView();
        }

        public void CustomBackButtonAction()
        {
            OnBackButtonPressed();
        }

    }
}