using read_write_files.Models;
using read_write_files.Repositories;
using read_write_files.Interfaces;

namespace read_write_files
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Database product = new();

            string option;

            do
            {
                Console.WriteLine("\nEscolha uma das opções abaixo:\n");
                Console.WriteLine("1 - Listar produtos");
                Console.WriteLine("2 - Cadastrar produto");
                Console.WriteLine("3 - Editar produto");
                Console.WriteLine("4 - Excluir produto");
                Console.WriteLine("0 - Sair da aplicação\n");

                option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        if (product.ReadAll().Count == 0)
                        {
                            Console.WriteLine("Não há produtos cadastrados.");
                        }
                        else
                        {
                            var productList = product.ReadAll();

                            Console.WriteLine("\nLista de produtos");
                            foreach (var item in productList)
                            {
                                Console.WriteLine($"{item.IdProduct} - {item.Name} - {item.Description} - {item.Price}");
                            }
                        }
                        break;

                    case "2":
                        Console.WriteLine("Digite o ID do produto:");
                        string id = Console.ReadLine();

                        Console.WriteLine("Digite o nome do produto:");
                        string name = Console.ReadLine();

                        Console.WriteLine("Digite a descrição opcional;");
                        string description = Console.ReadLine();

                        Console.WriteLine("Digite o preço do produto:");
                        string price = Console.ReadLine();

                        Database newProduct = new()
                        {
                            IdProduct = id,
                            Name = name,
                            Description = description,
                            Price = Convert.ToDecimal(price),
                        };

                        newProduct.Create(newProduct);

                        break;

                    case "3":
                        Console.WriteLine("Selecione o ID do roduto a ser atualizado:");
                        string idUpdateProduct = Console.ReadLine();

                        Console.WriteLine("Selecione o novo nome do produto:");
                        string nameUpdateProduct = Console.ReadLine();

                        Console.WriteLine("Selecione a nova descrição do produto:");
                        string descriptionUpdateProduct = Console.ReadLine();

                        Console.WriteLine("Selecione o novo preço do produto:");
                        string priceUpdateProduct = Console.ReadLine();

                        foreach (var item in product.ReadAll())
                        {
                            if(item.IdProduct == idUpdateProduct)
                            {
                                Database updatedProduct = new()
                                {
                                    IdProduct = item.IdProduct,
                                    Name = nameUpdateProduct,
                                    Description = descriptionUpdateProduct,
                                    Price = Convert.ToDecimal(priceUpdateProduct),
                                };

                                updatedProduct.Update(updatedProduct);
                            }
                        }


                        break;

                    case"4":

                        Console.WriteLine("Digite o ID do produto a ser excluido:");
                        string idDelProduct = Console.ReadLine();  
                        product.Delete(idDelProduct);

                        break;
                    default:
                        break;


                }

            } while (option != "0");
        }
    }
}