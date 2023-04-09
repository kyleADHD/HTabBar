
# Xamarin.Forms Custom HTabbar Control

Easily create customizable tabbed pages with options for top or bottom positioning, customizable header height, color, and content, tab page/content change events, and more! Perfect for creating a seamless user experience.




## Installation

The control can be installed via NuGet package manager. Search for HTabBar and install into your project.

## Usage
To use the HTabControl in your XAML pages, add the following namespace to your XAML file:

```xml
 xmlns:htab="clr-namespace:Xam.HTabView.Control;assembly=HTabBar"
```

```xml
<htab:HTabControl
    x:Name="MyHTabControl"
    HeaderColor="Gray"
    SelectedIndex="{Binding Index}">
    <htab:HTabControl.HTabPages>
        <htab:HTabPage>
            <htab:HTabPage.Header>
                <htab:HTabHeader Title="Tab1">
                </htab:HTabHeader>
            </htab:HTabPage.Header>
            <htab:HTabPage.CustomContentPage>
                <local:Tab1ContentPage></local:Tab1ContentPage>
            </htab:HTabPage.CustomContentPage>
        </htab:HTabPage>
        <htab:HTabPage>
            <htab:HTabPage.Header>
                <htab:HTabHeader IsVisible="False">
                    <Label Text="Tab2"></Label>
                </htab:HTabHeader>
            </htab:HTabPage.Header>
            <htab:HTabPage.Content>
                <StackLayout>
                    <Label Text="This Page Displays Tab2 Content"
                            VerticalOptions="CenterAndExpand" 
                            HorizontalOptions="CenterAndExpand" />
                </StackLayout>
            </htab:HTabPage.Content>
        </htab:HTabPage>
        <htab:HTabPage>
            <htab:HTabPage.Header>
                <htab:HTabHeader>
                    <Label Text="Tab3"></Label>
                </htab:HTabHeader>
            </htab:HTabPage.Header>
            <htab:HTabPage.Content>
                <StackLayout>
                    <Label Text="This Page Displays Tab3 Content"
                            VerticalOptions="CenterAndExpand" 
                            HorizontalOptions="CenterAndExpand" />
                </StackLayout>
            </htab:HTabPage.Content>
        </htab:HTabPage>
        <htab:HTabPage>
            <htab:HTabPage.Header>
                <htab:HTabHeader>
                    <Label Text="Tab4"></Label>
                </htab:HTabHeader>
            </htab:HTabPage.Header>
            <htab:HTabPage.Content>
                <StackLayout>
                    <Label Text="This Page Displays Tab4 Content"
                            VerticalOptions="CenterAndExpand" 
                            HorizontalOptions="CenterAndExpand" />
                </StackLayout>
            </htab:HTabPage.Content>
        </htab:HTabPage>
    </htab:HTabControl.HTabPages>
</htab:HTabControl>

```
    
## Properties

The following properties are available for HTabControl:

- HeaderColor (Color): Color of the tab header
- SelectedIndex (int): Index of the currently selected tab
- HTabPages (ObservableCollection<HTabPage>): Collection of tab pages
- HeaderFontFamily: The font family to be used for the header titles.
- HeaderFontSize: The font size to be used for the header titles.
- HeaderTextColor: The color to be used for the header titles.
- TabBarHeight: The height of the tab bar.
- IsPanEnabled: A boolean indicating whether or not swiping between tabs is enabled.


## Future Goals:

- Add support for more header customization options such as font color, background color, and more.
- Implement additional tab transition animations for a more visually dynamic user experience.
- Explore the possibility of adding more tab positioning options beyond top and bottom.
- Continuously update and maintain the package to ensure compatibility with the latest Xamarin.Forms updates and enhancements.
## Contributing

Contributions to this project are welcome! If you find a bug or have an idea for a new feature, please open an issue or submit a pull request.


## License

This project is licensed under the MIT License
[MIT](https://choosealicense.com/licenses/mit/)


## Support

For support, email WebsiterzTech@gmail.com.


## Credit

This control is based on the original idea by Rajesh Angappan. Significant modifications and additions have been made by myself.
