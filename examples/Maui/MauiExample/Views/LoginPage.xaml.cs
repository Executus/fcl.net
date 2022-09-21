using MauiExample.Helpers;

namespace MauiExample.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage()
	{
		InitializeComponent();        
	}

    private async void Blocto_Clicked(object sender, EventArgs e)
    {
        var fcl = ServiceHelper.GetService<Fcl.Net.Core.Fcl>();
        fcl.SetWalletProvider(new Fcl.Net.Core.Models.FclWalletDiscovery
        {
            Wallet = new Uri("https://flow-wallet-testnet.blocto.app/api/flow/authn"),
            WalletMethod = Fcl.Net.Core.FclServiceMethod.HttpPost
        });
        await fcl.AuthenticateAsync();

        await Shell.Current.GoToAsync($"//main");
    }
}