using e_commerce_app.Shared;
namespace e_commerce_app.Client.Services.CategoryService;

public interface ICategoryService
{
    List<Category> Categories { get; set; }
    Task GetCategories();
}