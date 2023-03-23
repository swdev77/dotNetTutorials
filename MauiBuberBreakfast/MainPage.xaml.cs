using MauiBuberBreakfast.ViewModels;

namespace MauiBuberBreakfast;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
		BindingContext = new MyBreakfastViewModel();
	}
}

