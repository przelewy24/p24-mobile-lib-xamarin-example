﻿using P24Lib.Helpers;
using P24Lib.Models.Params;
using Xamarin.Forms;

namespace P24XamarinLib
{
    public partial class TransferPage : ContentPage
    {

        public TransferPage(TrnDirectParams transactionParams)
        {
            
            var content = TransferPageHelper.ContentForTrnDirect(transactionParams);
            content.AddNavigating(WebView_Navigating);

            Content = content;
        }

        public TransferPage(TrnRequestParams transactionParams)
        {
            var content = TransferPageHelper.ContentForTrnRequest(transactionParams);
            content.AddNavigating(WebView_Navigating);
            Content = content;
        }

        public TransferPage(ExpressParams transactionParams)
        {
            var content = TransferPageHelper.ContentForExpress(transactionParams);
            content.AddNavigating(WebView_Navigating);
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
                var newContent = TransferPageHelper.GetContentForBack();
                newContent.Navigating += WebView_Navigating;
                Content = newContent;
                return true;
            }
            else
            {
                return base.OnBackButtonPressed();
            }
        }
    }
}