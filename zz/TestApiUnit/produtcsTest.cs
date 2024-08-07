using Moq;
using Xunit;
using System;
using System.Collections.Generic;

namespace TestApiUnit
{
    public class ProductsTest
    {
        /// <summary>
        /// Teste para a funcionalidade de listar todos os produtos
        /// </summary>
        [Fact]
        public void Test1()
        {
            // Arrange

            // Lista de produtos
            var productList = new List<Products>
            {
                new Products { IdProduct = Guid.NewGuid(), Name = "Produto 1", Price = 78 },
                new Products { IdProduct = Guid.NewGuid(), Name = "Produto 2", Price = 90 },
                new Products { IdProduct = Guid.NewGuid(), Name = "Produto 3", Price = 787 },
            };

            // Cria um objeto de simulação do tipo ProductsRepository
            var mockRepository = new Mock<ProductsRepository>();

            // Configura o método "GetProducts" para que quando for acionado retorne a lista "mockada"
            mockRepository.Setup(x => x.GetProducts()).Returns(productList);

            // Act

            // Executando o método "GetProducts" e atribui a resposta em result
            var result = mockRepository.Object.GetProducts();

            // Assert

            Assert.Equal(3, result.Count);
        }

        [Fact]
        public void Post()
        {
            // Arrange

            // Criar o objeto
            var product = new Products { IdProduct = Guid.NewGuid(), Name = "Rolex", Price = 99 };

            // Criar a lista
            var productList = new List<Products>();

            var mockRepository = new Mock<ProductsRepository>();
            mockRepository.Setup(x => x.AddProduct(product)).Callback(() => productList.Add(product));

            // Act
            mockRepository.Object.AddProduct(product);

            // Assert
            Assert.Contains(product, productList);
        }

        [Fact]
        public void Delete()
        {
            // Arrange

            // Criar o objeto
            var product = new Products { IdProduct = Guid.NewGuid(), Name = "Produto 1", Price = 78 };

            // Criar a lista
            var productList = new List<Products> { product };

            var mockRepository = new Mock<ProductsRepository>();
            mockRepository.Setup(x => x.DeleteProduct(product.IdProduct)).Callback(() => productList.Remove(product));

            // Act
            mockRepository.Object.DeleteProduct(product.IdProduct);

            // Assert
            Assert.DoesNotContain(product, productList);
        }

        [Fact]
        public void GetById()
        {
            // Arrange

            // Criar o objeto
            var product = new Products { IdProduct = Guid.NewGuid(), Name = "Produto 1", Price = 78 };

            // Criar a lista
            var productList = new List<Products> { product };

            var mockRepository = new Mock<ProductsRepository>();
            mockRepository.Setup(x => x.GetProductById(product.IdProduct)).Returns(product);

            // Act
            var result = mockRepository.Object.GetProductById(product.IdProduct);

            // Assert
            Assert.Equal(product, result);
        }

        // Update
    }

    public class Products
    {
        public Guid IdProduct { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public interface ProductsRepository
    {
        List<Products> GetProducts();
        void AddProduct(Products product);
        void DeleteProduct(Guid id);
        Products GetProductById(Guid id);
    }
}
