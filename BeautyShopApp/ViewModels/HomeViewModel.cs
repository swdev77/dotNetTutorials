using BeautyShopApp.Models;
using BeautyShopApp.Services;
using System.Collections.ObjectModel;

namespace BeautyShopApp.ViewModels;

public class HomeViewModel : ViewModelBase
{
    ObservableCollection<Product> _products;
    public HomeViewModel()
    {
        LoadData();
    }

    public ObservableCollection<Product> Products
    {
        get => _products;
        set
        {
            _products = value;
            OnPropertyChanged();
        }
    }

    private void LoadData()
    {
        Products = new ObservableCollection<Product>();
        Products.Add(new());

        foreach (Product product in ProductService.Instance.GetProducts())
        {
            Products.Add(product);
        }
    }
}
