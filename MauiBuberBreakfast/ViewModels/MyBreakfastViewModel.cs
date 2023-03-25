using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiBuberBreakfast.Models;

namespace MauiBuberBreakfast.ViewModels;

public partial class MyBreakfastViewModel : ObservableObject
{
    [ObservableProperty]
    List<Breakfast> breakfasts;

    [ObservableProperty]
    bool isRefreshing;

    public MyBreakfastViewModel()
    {
        LoadBreakfastsAsync();
    }

    [RelayCommand]
    public async Task LoadBreakfastsAsync()
    {
        IsRefreshing = true;
        try
        {
            await Task.Delay(2000);

            Breakfasts = new()
            {
                new Breakfast(
                        Name: "Vegan Sunshine",
                        Description: "Vegan everthing! Join us for a healthy breakfas full of vegan dishes and Cookies",
                        StartDateTime: DateTime.Now.AddDays(1),
                        EndDateTime: DateTime.Now.AddDays(1).AddHours(2),
                        Image: new Uri("https://images.unsplash.com/photo-1659984716295-c3d4be94dc4f?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=327&q=80"),
                        Savory: new List<string> { "Oatmeal", "Advocado toast", "Omelet", "Salad" },
                        Sweet: new List<string> { "Cookie" }),
                new Breakfast(
                        Name: "Breakfast 0 Tiffany's",
                        Description: "Hi, I'm Tiffany's ",
                        StartDateTime: DateTime.Now,
                        EndDateTime: DateTime.Now.AddHours(2),
                        Image: new Uri("https://images.unsplash.com/photo-1620921575116-fb8902865f81?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=435&q=80"),
                        Savory: new List<string> { "Sandwich", "Salad", "Omelet" },
                        Sweet: new List<string> { "Pancake", "Waffle" }),
            };
        }
        finally
        {
            IsRefreshing = false;
        }

    }
}
