using System.Globalization;
namespace panicButton;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

    }
    async void Button_Clicked(System.Object sender, System.EventArgs e)
    {
        var status = await Permissions.CheckStatusAsync<Permissions.Sms>();
        string number = TelephoneNumber.Text;
        var location = await Geolocation.Default.GetLocationAsync();
        var point = new Point(location.Latitude, location.Longitude);
        string[] recipients = new[] { number };
        if (!string.IsNullOrWhiteSpace(number))
        {
            if (Sms.Default.IsComposeSupported)
            {
                var numberFormatInfo = new NumberFormatInfo { NumberDecimalSeparator = "." };
                string text = $"I need help here my location\n https://www.google.com/maps/search/?api=1&query={location.Latitude.ToString(numberFormatInfo)},{location.Longitude.ToString(numberFormatInfo)}";

                var messsage = new SmsMessage(text, recipients);
                await Sms.Default.ComposeAsync(messsage);
                 
                await DisplayAlert("SUCCESS", $"Your location sent to {number}!", "OK");
            }
        }
        else
        {
            await DisplayAlert("ERROR", "You need to enter the number!", "OK");
        }
    }
}


