using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SportsStore.Test
{
    public class HomeControllerTest
    {
        [Fact]
        public void Can_Use_Repository()
        {
            //arrange
            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product { ProductId = 1 , Name = "P1"},
                new Product { ProductId = 2, Name = "P2"} }).AsQueryable<Product>());
            HomeController homeController = new HomeController(mock.Object);
            //act
            ProductsListViewModel result = homeController.Index(null).ViewData.Model as ProductsListViewModel;
            //assert
            Product[] products = result.Products.ToArray();

            Assert.Equal("P1", products[0].Name);
            Assert.Equal("P2", products[1].Name);
            Assert.True(products.Length == 2);
        }

        [Fact]
        public void Can_Use_Paginate()
        {
            //arrange
            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product { ProductId = 1 , Name = "P1"},
                new Product { ProductId = 2 , Name = "P2"},
                new Product { ProductId = 3 , Name = "P3"},
                new Product { ProductId = 4 , Name = "P4"},
                new Product { ProductId = 5 , Name = "P5"},
                new Product { ProductId = 6 , Name = "P6"},
            }).AsQueryable());

            HomeController controller = new HomeController(mock.Object);
            controller.PageSize = 3;
            //act
            ProductsListViewModel result = controller.Index(null,2).ViewData.Model as ProductsListViewModel;

            //assert

            Product[] products = result.Products.ToArray();
            Assert.True(products.Length == 3);
            Assert.Equal("P4", products[0].Name);
            Assert.Equal("P5", products[1].Name);

        }
        [Fact]
        public void Can_Send_Pagination_View_Model()
        {
            //arrange
            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product { ProductId = 1 ,Name ="P1"},
                new Product { ProductId = 2 ,Name ="P2"},
                new Product { ProductId = 3 ,Name ="P3"},
                new Product { ProductId = 4 ,Name ="P4"},
                new Product { ProductId = 5 ,Name ="P5"},
            }).AsQueryable());

            HomeController controller = new HomeController(mock.Object) { PageSize=3};

            //act
            ProductsListViewModel result = controller.Index(null,2).ViewData.Model as ProductsListViewModel;
            
            //assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.Equal(2, pageInfo.CurrentPage);
            Assert.Equal(3, pageInfo.ItemsPerPage);
            Assert.Equal(5, pageInfo.TotalItems);
            Assert.Equal(2, pageInfo.TotalPages);
        }

        [Fact]
        public void Can_Filter_Products()
        {
            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product { ProductId = 1 , Name = "P1", Category="Cat1"},
                new Product { ProductId = 2 , Name = "P2", Category="Cat3"},
                new Product { ProductId = 3 , Name = "P3", Category="Cat2"},
                new Product { ProductId = 4 , Name = "P4", Category="Cat3"},
                new Product { ProductId = 5 , Name = "P5", Category="Cat2"},
                new Product { ProductId = 6 , Name = "P6", Category="Cat1"},
            }).AsQueryable());

            HomeController controller = new HomeController(mock.Object);
            controller.PageSize = 3;
            //act
            ProductsListViewModel result = controller.Index("Cat2", 1).ViewData.Model as ProductsListViewModel;

            //assert

            Product[] products = result.Products.ToArray();
            Assert.True(products.Length == 2);
            Assert.True("P3" == products[0].Name && products[0].Category == "Cat2");
            Assert.True("P5" == products[1].Name && products[1].Category == "Cat2");
        }
    }
}
