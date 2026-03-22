using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {

        List<Cliente> clientes = CarregarClientes();

        bool sairMenu = false;
        char sair = 'n';

        do
        {
            Console.WriteLine("=-=-=-=- MENU -=-=-=-=");
            Console.WriteLine("1 - Cadastrar Cliente");
            Console.WriteLine("2 - Listar Clientes");
            Console.WriteLine("3 - Buscar Cliente");
            Console.WriteLine("4 - Atualizar Cliente");
            Console.WriteLine("5 - Remover Cliente");
            Console.WriteLine("0 - Sair");
            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=\n");

            Console.WriteLine("Selecione uma opção:");
            int opcao;

            if (!int.TryParse(Console.ReadLine(), out opcao))
            {
                Console.WriteLine("Entrada inválida!");
                continue;
            }

            switch (opcao)
            {
                case 1:
                    do
                    {
                        Cliente c = new Cliente();

                        Console.WriteLine("=-=- Adicione um Cliente -=-=");

                        Console.Write("Nome: ");
                        c.Nome = Console.ReadLine();

                        Console.Write("Email: ");
                        c.Email = Console.ReadLine();

                        c.Id = clientes.Count > 0 ? clientes[^1].Id + 1 : 1;

                        Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");

                        clientes.Add(c);

                      
                        SalvarClientes(clientes);

                        Console.WriteLine("Deseja cadastrar um novo cliente? (s/n)");
                        sairOpcao();

                    } while (sair != 'n');
                    break;

                case 2:
                    Console.WriteLine("=-=- Listar Cliente -=-=");

                    foreach (var cliente in clientes)
                    {
                        cliente.Exibir();
                        Console.WriteLine("=-=-=-=-=-=-=-=-=-=--=-=");
                    }
                    break;

                case 3:
                    do
                    {
                        int idBusca;
                        Cliente clienteEncontrado = null;

                        Console.WriteLine("=-=- Buscar Cliente -=-=");

                        Console.WriteLine("Digite o ID do cliente:");
                        if (!int.TryParse(Console.ReadLine(), out idBusca))
                        {
                            Console.WriteLine("Entrada inválida!");
                            continue;
                        }

                        foreach (var cliente in clientes)
                        {
                            if (idBusca == cliente.Id)
                            {
                                clienteEncontrado = cliente;
                                break;
                            }
                        }

                        if (clienteEncontrado != null)
                        {
                            clienteEncontrado.Exibir();
                        }
                        else
                        {
                            Console.WriteLine("Cliente não encontrado");
                        }

                        Console.WriteLine("Deseja Buscar um novo cliente? (s/n)");
                        sairOpcao();

                    } while (sair != 'n');

                    break;

                case 4:
                    do
                    {
                        int idAtualizar;
                        Cliente clienteAtualizar = null;

                        Console.WriteLine("=-=- Atualizar Cliente -=-=");

                        Console.WriteLine("Digite o ID do cliente:");
                        if (!int.TryParse(Console.ReadLine(), out idAtualizar))
                        {
                            Console.WriteLine("Entrada inválida!");
                            continue;
                        }

                        foreach (var cliente in clientes)
                        {
                            if (idAtualizar == cliente.Id)
                            {
                                clienteAtualizar = cliente;
                                break;
                            }
                        }

                        if (clienteAtualizar != null)
                        {
                            Console.Write("Novo Nome: ");
                            clienteAtualizar.Nome = Console.ReadLine();

                            Console.Write("Novo Email: ");
                            clienteAtualizar.Email = Console.ReadLine();

                            SalvarClientes(clientes);
                        }
                        else
                        {
                            Console.WriteLine("Cliente não encontrado");
                        }

                        Console.WriteLine("Deseja Atualizar um novo cliente? (s/n)");
                        sairOpcao();

                    } while (sair != 'n');

                    break;

                case 5:
                    do
                    {
                        int idRemover;
                        Cliente clienteRemover = null;

                        Console.WriteLine("=-=- Remover Cliente -=-=");

                        Console.WriteLine("Digite o ID do cliente:");
                        if (!int.TryParse(Console.ReadLine(), out idRemover))
                        {
                            Console.WriteLine("Entrada inválida!");
                            continue;
                        }

                        foreach (var cliente in clientes)
                        {
                            if (idRemover == cliente.Id)
                            {
                                clienteRemover = cliente;
                                break;
                            }
                        }

                        if (clienteRemover != null)
                        {
                            clientes.Remove(clienteRemover);

                            SalvarClientes(clientes);

                            Console.WriteLine("Cliente removido com sucesso");
                        }
                        else
                        {
                            Console.WriteLine("Cliente não encontrado");
                        }

                        Console.WriteLine("Deseja Remover um novo cliente? (s/n)");
                        sairOpcao();

                    } while (sair != 'n');

                    break;

                case 0:
                    sairMenu = true;
                    break;

                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }

        } while (sairMenu != true);


        static void SalvarClientes(List<Cliente> clientes)
        {
            using (StreamWriter sw = new StreamWriter("clientes.txt"))
            {
                foreach (var c in clientes)
                {
                    sw.WriteLine($"{c.Id};{c.Nome};{c.Email}");
                }
            }
        }


        static List<Cliente> CarregarClientes()
        {
            List<Cliente> lista = new List<Cliente>();

            if (File.Exists("clientes.txt"))
            {
                string[] linhas = File.ReadAllLines("clientes.txt");

                foreach (var linha in linhas)
                {
                    string[] dados = linha.Split(';');

                    Cliente c = new Cliente
                    {
                        Id = int.Parse(dados[0]),
                        Nome = dados[1],
                        Email = dados[2]
                    };

                    lista.Add(c);
                }
            }

            return lista;
        }

        void sairOpcao()
        {
            bool validarSair = false;

            do
            {
                if (!char.TryParse(Console.ReadLine(), out sair))
                {
                    Console.WriteLine("Entrada inválida!");
                }

                if (sair != 's' && sair != 'n')
                {
                    Console.WriteLine("Opção inválida!!!");
                }
                else
                {
                    validarSair = true;
                }

            } while (!validarSair);
        }
    }
}