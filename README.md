# Panic Button

<img src="https://github.com/erdalkama/SOSMaui/assets/34250103/e20f892b-6dd1-4d41-bc71-5f2a71d4263c" height="600">

Panic Button allows you to quickly make an emergency call for help. With this simple app, you can send an SMS to a contact and share your location instantly.

## Features

- Emergency call for help
- Share location via SMS

## Usage

1. Open the application.
2. Tap the "PANIC" button.
3. Enter a phone number and click "Send."
4. Your location and the emergency call will be sent via SMS to the specified recipient.

## Permissions

The application requires the following permissions to function properly:

- SMS sending permission
- Location permission

## Screenshots

<img src="https://github.com/erdalkama/SOSMaui/assets/34250103/629b3fb6-ce49-4591-a78a-c396e1193733" height="600">

## Notes

This application has been developed using .Net Maui. Please be cautious when entering the contact phone number and use it only for emergencies.

## Setting Permissions

The application requires certain permissions to function correctly:

- `NSMessagesUsageDescription`: Allow permission for sending SMS within the application.
- `NSLocationAlwaysAndWhenInUseUsageDescription`: Can we use your location at all times?
- `NSLocationWhenInUseUsageDescription`: You need to grant permission to use the panic system.

## Development

You can contribute to the development of the application by using the GitHub repository.

## License

This project is licensed under the MIT License.

---
**WARNING**: This application should not be used for real emergencies. It is solely an example application. In real emergencies, please contact the appropriate official emergency services.

Introduction

In this article, you will learn how to develop an emergency button application using .NET MAUI (Multi-platform App UI). Our app allows users to quickly make a help call and share their location.

What is .NET MAUI?

.NET MAUI is a modern, open-source UI (User Interface) framework developed by Microsoft. With .NET MAUI, you can develop applications that run on different platforms such as Android, iOS, macOS, and Windows, all from a single codebase.

Project Setup

The first step is to create a new .NET MAUI project using Visual Studio or Visual Studio for Mac.

Designing the UI (XAML)

After creating the project, we design the application’s user interface using XAML (Extensible Application Markup Language). Here’s our basic XAML code:

<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="PanicButton.MainPage">

    <ScrollView>
        <StackLayout
            Spacing="25"
            Padding="50" >

            <Button Text="PANIC"
                x:Name="SOSButton"
                WidthRequest="300"
                HeightRequest="300"
                BackgroundColor="Red"
                TextColor="White"
                HorizontalOptions="Center"
                VerticalOptions="Center"
                CornerRadius="150"
                Clicked="Button_Clicked" />

            <Label Text="Please add contact number!"/>
            <Entry x:Name="TelephoneNumber" Placeholder="Number"/>
        </StackLayout>
    </ScrollView>

</ContentPage>
            <Button Text="PANIC"
                x:Name="SOSButton"
                WidthRequest="300"
                HeightRequest="300"
                BackgroundColor="Red"
                TextColor="White"
                HorizontalOptions="Center"
                VerticalOptions="Center"
                CornerRadius="150"
                Clicked="Button_Clicked" />
Inside the StackLayout, there's a Button element. This button is labeled "PANIC." Here are its attributes:

Text: Sets the text displayed on the button to "PANIC."
x:Name: Assigns a unique name to the button, allowing it to be referenced in code-behind.
WidthRequest and HeightRequest: Set the width and height of the button to 300 units, making it a square.
BackgroundColor: Sets the background color of the button to red.
TextColor: Sets the text color of the button to white.
HorizontalOptions and VerticalOptions: Specify the alignment of the button within its container. Here, it's centered both horizontally and vertically.
CornerRadius: Sets the corner radius of the button to 150 units, giving it a circular appearance.
Clicked: Specifies the event handler method (Button_Clicked) to be called when the button is clicked.
            <Label Text="Please add contact number!"/>
Following the button, there’s a Label element that displays the text "Please add contact number!" This label provides instructions or information to the user.

            <Entry x:Name="TelephoneNumber" Placeholder="Number"/>
Finally, there’s an Entry element, which is essentially a text input field. It has an x:Name attribute set to "TelephoneNumber," allowing it to be referenced in code-behind. The Placeholder attribute provides a placeholder text that suggests the user should enter a phone number.

Code (C#)

Once the user interface is created, we define the code-behind using C#. Here’s the main code snippet:

using System.Globalization;

namespace PanicButton;

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
User Permissions and Sending SMS

Our app has the capability to send SMS messages in emergency situations. However, to use this functionality, certain permissions are required.

To check for permission to send SMS in the app, we use the following code:

var status = await Permissions.CheckStatusAsync<Permissions.Sms>();
This code checks the SMS permission using the Permissions class. If permission is granted, the app can send SMS messages. If not, we request permission from the user with the following code:

status = await Permissions.RequestAsync<Permissions.Sms>();
If the user denies permission, the SMS sending operation cannot be performed, and the user receives an error message.

Location Information and Sharing

The app can retrieve the user’s location and share it along with a help message. To retrieve the location, we use the following code:

var location = await Geolocation.Default.GetLocationAsync();
This code asynchronously retrieves the device’s current location using the Geolocation class.

Once the location information is obtained, we include it in an SMS message:

string text = $"I need help here my location\n https://www.google.com/maps/search/?api=1&query={location.Latitude.ToString(numberFormatInfo)},{location.Longitude.ToString(numberFormatInfo)}";
This message contains the user’s location information and a link to Google Maps.

Finally, we send this message to the specified recipients:

var messsage = new SmsMessage(text, recipients);
await Sms.Default.ComposeAsync(messsage);
This code uses the Sms.Default.ComposeAsync() method to create an SMS message and initiate the user's SMS sending app.

With these explanations, you should have a better understanding of the app’s core functionality and permissions. As you continue to develop the app, you can expand this basic functionality to enhance the user experience.

Conclusion

In this article, we’ve seen how to develop a simple emergency button application using .NET MAUI. The app allows users to make quick help calls and share their locations. This serves as a fundamental example of app development, and you can build upon it to create more complex applications.

You can access the project code here.

I hope this article has been helpful in understanding .NET MAUI and app development. For further information and to further develop the project, you can refer to the official .NET MAUI documentation.
