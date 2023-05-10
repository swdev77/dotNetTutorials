namespace BeautyShopApp.ViewModels;

public class ViewModelBase : BindableObject
{
    public virtual Task InitializeAsync(object navigationData) => Task.FromResult(false);
}
