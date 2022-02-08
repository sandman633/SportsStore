using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System.Linq;

namespace SportsStore.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private readonly IStoreRepository storeRepository;

        public NavigationMenuViewComponent(IStoreRepository storeRepository)
        {
            this.storeRepository = storeRepository;
        }
        public IViewComponentResult Invoke()
        {
            return View(storeRepository.Products
                            .Select(x => x.Category)
                            .Distinct()
                            .OrderBy(x => x)
                            );
        }
    }
}
