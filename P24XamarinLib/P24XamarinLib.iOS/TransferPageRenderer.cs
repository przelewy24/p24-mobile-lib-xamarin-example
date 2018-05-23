using System;
using P24XamarinLib.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using P24XamarinLib;
using CoreGraphics;

[assembly: ExportRenderer(typeof(TransferPage), typeof(TransferPageRenderer))]
namespace P24XamarinLib.iOS
{
    public class TransferPageRenderer : PageRenderer
    {
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            SetCustomBackButton();
        }

        private void SetCustomBackButton()
        {
            var backBtnImage = UIImage.FromBundle("backButton.png");

            backBtnImage = backBtnImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);

            var backBtn = new UIButton(UIButtonType.Custom)
            {
                HorizontalAlignment = UIControlContentHorizontalAlignment.Left,
                TitleEdgeInsets = new UIEdgeInsets(11.5f, 0f, 10f, 0f),
                ImageEdgeInsets = new UIEdgeInsets(1f, -9f, 0f, 0f)
            };

            backBtn.SetTitle("Cofnij", UIControlState.Normal);
            backBtn.SetTitleColor(UIColor.White, UIControlState.Normal);
            backBtn.SetTitleColor(UIColor.LightGray, UIControlState.Highlighted);
            backBtn.Font = UIFont.FromName("HelveticaNeue", (nfloat)17);

            backBtn.SetImage(backBtnImage, UIControlState.Normal);

            backBtn.SizeToFit();

            backBtn.TouchDown += (sender, e) =>
            {
                ((TransferPage)Element)?.CustomBackButtonAction();
            };

            //Set the frame of the button
            backBtn.Frame = new CGRect(
                0,
                0,
                UIScreen.MainScreen.Bounds.Width / 4,
                NavigationController.NavigationBar.Frame.Height);

            var btnContainer = new UIView(
                new CGRect(0, 0, backBtn.Frame.Width, backBtn.Frame.Height));
            btnContainer.AddSubview(backBtn);

            var backButtonItem = new UIBarButtonItem("", UIBarButtonItemStyle.Plain, null)
            {
                CustomView = backBtn
            };

            NavigationController.TopViewController.NavigationItem.LeftBarButtonItems = new[] { backButtonItem };
        }
    }
}
