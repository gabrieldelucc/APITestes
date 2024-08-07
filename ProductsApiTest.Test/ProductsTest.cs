using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductsApiTest.Contexts;
using ProductsApiTest.Controllers;
using ProductsApiTest.Domains;
using ProductsApiTest.Interfaces;
using ProductsApiTest.Repositories;
using Xunit;

namespace ProductsApiTest.Test
{
    public class ProductsTest
    {
      
        [Fact]
        public void GetProduct()
        {
        //Arrange

            var products = new List<Products>
            {
                new Products {IdProduct = Guid.NewGuid(), Nome = "Gato", Price = 20},
                new Products {IdProduct = Guid.NewGuid(), Nome = "Tomate", Price = 2},
                new Products {IdProduct = Guid.NewGuid(), Nome = "Smartphone", Price = 200},
            };

            var mockRepository = new Mock<IProductsRepository>();

       
            mockRepository.Setup(x => x.Get()).Returns(products);

            // Act

            var result = mockRepository.Object.Get();

        
            Assert.Equal(3, result.Count);

        }

        [Fact]
        public void PostProduct()
        {
            Products prod = new Products
            {
                IdProduct = Guid.NewGuid(),
                Nome = "Gato",
                Price = 7,
            };
            List<Products> products = new List<Products>();

            var mockRepository = new Mock<IProductsRepository>();

            mockRepository.Setup(x => x.Post(prod))
                .Callback<Products>(x => products.Add(x));

            mockRepository.Object.Post(prod);

            Assert.Contains(prod, products);
        }

        [Fact]
        public void GetProductId()
        {
            Products product = new Products
            {
                IdProduct = Guid.NewGuid(),
                Nome = "Gato",
                Price = 7,
            };

            var mockRepository = new Mock<IProductsRepository>();

            mockRepository.Setup(x => x.GetById(product.IdProduct)).Returns(product);

            var result = mockRepository.Object.GetById(product.IdProduct);

            Assert.Equal(product, result);
        }

        [Theory]
        [InlineData("e5b178a0-cf6a-459a-b9b9-f7c7fb469bde")]
        public void DeleteId(Guid guid)
        {

            List<Products> products = new List<Products>
            {
                new Products {IdProduct = Guid.Parse("e5b178a0-cf6a-459a-b9b9-f7c7fb469bde"), Nome = "Ossos de dinossauro", Price = 200},
            };

            var busca = products.FirstOrDefault(x => x.IdProduct == guid)!;
            var mockRepository = new Mock<IProductsRepository>();

            mockRepository.Setup(x => x.Delete(guid)).Callback(() => products.Remove(busca));

            mockRepository.Object.Delete(guid);

            Assert.Equal(0, products.Count);
        }

        [Theory]
        [InlineData("e5b178a0-cf6a-459a-b9b9-f7c7fb469bde")]
        public void PutProduct(Guid guid)
        {
            List<Products> products = new List<Products>
            {
                new Products
                {
                    IdProduct = Guid.Parse("e5b178a0-cf6a-459a-b9b9-f7c7fb469bde"),
                    Nome = "Gato",
                    Price = 21
                }
            };

            Products prod = new Products
            {
                Nome = "Gato",
                Price = 12
            };

            Products busca = products.FirstOrDefault(x => x.IdProduct == guid)!;

            var mockRepository = new Mock<IProductsRepository>();

            mockRepository.Setup(x => x.Put(guid, prod))
                .Callback(() =>
                {
                    var item = products.FirstOrDefault(x => x.IdProduct == guid);

                    if (item != null)
                    {
                        item.Nome =  prod.Nome;
                        item.Price = prod.Price;
                    }
                });

            mockRepository.Object.Put(guid, prod);

            var novaBusca = products.FirstOrDefault(x => x.IdProduct == guid);

            Assert.Equal("Gato", novaBusca.Nome);
        }
    }
}