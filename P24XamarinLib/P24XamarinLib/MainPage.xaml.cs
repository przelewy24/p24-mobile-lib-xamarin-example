using P24Lib.Helpers;
using P24Lib.Models.Params;
using P24Lib.Models.Passage;
using System;
using Xamarin.Forms;

namespace P24XamarinLib
{
    public partial class MainPage : ContentPage
    {
        private bool setSandbox = false;

        public MainPage()
        {
            InitializeComponent();
        }

        async void TransferTrnRequest(object sender, EventArgs e)
        {
            var token = "XXXX-XXXX-XXXX-XXXX"; // transaction token
            var transferPage = new TransferPage(new TrnRequestParams(token).SetSandbox(setSandbox));
            transferPage.Disappearing += (sender2, e2) => { TransferPageResult(); };
            await Navigation.PushAsync(transferPage);
        }

        async void TransferTrnDirect(object sender, EventArgs e)
        {
            var payment = GetTransferParams();
            var transferPage = new TransferPage(payment.SetSandbox(setSandbox));
            transferPage.Disappearing += (sender2, e2) => { TransferPageResult(); };
            await Navigation.PushAsync(transferPage);
        }

        async void TransferExpress(object sender, EventArgs e)
        {
            var url = "https://XXX"; // url to express transaction
            var transferPage = new TransferPage(new ExpressParams(url));
            transferPage.Disappearing += (sender2, e2) => { TransferPageResult(); };
            await Navigation.PushAsync(transferPage);
        }

        async void TransferPassage(object sender, EventArgs e)
        {
            var payment = GetTransferParams();
            AddPassageCart(payment);
            var transferPage = new TransferPage(payment.SetSandbox(setSandbox));
            transferPage.Disappearing += (sender2, e2) => { TransferPageResult(); };
            await Navigation.PushAsync(transferPage);
        }

        private void TransferPageResult()
        {
            var result = Result.Get();

            if (result != null)
            {
                if (result.IsSuccess)
                {
                    // success
                    ResultLabel.Text = "Success";
                }
                else
                {
                    // error
                    var errorCode = result.ErrorCode;
                    ResultLabel.Text = $"Error code: {result.ErrorCode}";
                }
            }
        }

        private TrnDirectParams GetTransferParams()
        {
            return new TrnDirectParams()
            {
                SessionId = Guid.NewGuid().ToString(),
                MerchantId = 64195,
                Crc = setSandbox ? "d27e4cb580e9bbfe" : "b36147eeac447028",
                Amount = 1,
                Language = "pl",
                Currency = "PLN",
                Address = "Ulica testowa",
                City = "Poznan",
                Zip = "61-600",
                Client = "Jan Kowalski",
                Country = "PL",
                Email = "info@przelewy24.pl",
                Phone = "23566345"
            };
        }

        private void AddPassageCart(TrnDirectParams payment)
        {
            payment.PassageCart = new PassageCart();

            var item = new PassageItem()
            {
                Name = "Product 1",
                Description = "description 1",
                Quantity = 1,
                Price = 100,
                Number = 1,
                TargetAmount = 100,
                TargetPosId = 51986
            };

            payment.PassageCart.AddItem(item);

            item = new PassageItem()
            {
                Name = "Product 2",
                Description = "description 2",
                Quantity = 1,
                Price = 100,
                TargetAmount = 100,
                TargetPosId = 51987
            };

            payment.PassageCart.AddItem(item);
        }

        private void ToggledEventHandler(object sender, ToggledEventArgs e)
        {
            setSandbox = e.Value;
        }
    }
}