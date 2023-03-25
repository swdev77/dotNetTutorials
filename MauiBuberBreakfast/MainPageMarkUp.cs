using CommunityToolkit.Maui.Markup;

namespace MauiBuberBreakfast;

public class MainPageMarkUp : ContentPage
{
    void Build() => Content = new VerticalStackLayout
    {
        Children =
        {
            new Label
            {
                Text = "Hello .Net",
            }
        }
    };
    public MainPageMarkUp()
    {
        Build();
    }
}