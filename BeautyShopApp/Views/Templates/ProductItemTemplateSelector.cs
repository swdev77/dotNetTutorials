using BeautyShopApp.Models;

namespace BeautyShopApp.Views.Templates;

public class ProductItemTemplateSelector : DataTemplateSelector
{
    public DataTemplate ProductItemTemplate { get; set; }
    public DataTemplate ResultItemTemplate { get; set; }
    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        var product = item as Product;

        if (product.IsEmpty())
        {
            return ResultItemTemplate;
        }

        return ProductItemTemplate;

    }
}
