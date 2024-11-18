using System;
using System.Collections.Generic;
using System.Text;
using DesafioProjetoHospedagem.Models;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        // Lista para armazenar os hóspedes cadastrados
        List<Pessoa> hospedes = new List<Pessoa>();

        // Variáveis para armazenar a suíte e a reserva
        Suite suite = null;
        Reserva reserva = null;

        while (true)
        {
            // Menu de opcões
            Console.Clear();
            Console.WriteLine("Bem-vindo ao sistema de reservas!");
            Console.WriteLine("1. Cadastrar Hóspedes");
            Console.WriteLine("2. Selecionar Suíte");
            Console.WriteLine("3. Fazer Reserva");
            Console.WriteLine("4. Exibir Informações da Reserva");
            Console.WriteLine("5. Sair");
            Console.Write("Escolha uma opção: ");
            
            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    Console.Write("Quantos hóspedes deseja cadastrar? ");
                    if (!int.TryParse(Console.ReadLine(), out int quantidadeHospedes) || quantidadeHospedes <= 0)
                    {
                        Console.WriteLine("Quantidade inválida.");
                        break;
                    }

                    // Loop para adicionar cada hóspede à lista
                    for (int i = 0; i < quantidadeHospedes; i++)
                    {
                        Console.Write($"Digite o nome do hóspede {i + 1}: ");
                        string nome = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(nome))
                        {
                            Console.WriteLine("Nome inválido. Tente novamente.");
                            i--; // Repete a iteração para o hóspede atual
                            continue;
                        }
                        hospedes.Add(new Pessoa(nome: nome));
                    }

                    Console.WriteLine("Hóspedes cadastrados com sucesso!");
                    break;

                case "2":
                    // Obtém as suítes disponíveis da classe Suite
                    List<Suite> suitesDisponiveis = Suite.ObterSuítesDisponíveis();

                    Console.WriteLine("Selecione uma suíte:");
                    for (int i = 0; i < suitesDisponiveis.Count; i++)
                    {
                        Suite s = suitesDisponiveis[i];
                        Console.WriteLine($"{i + 1}. {s.TipoSuite} - Capacidade: {s.Capacidade} - Diária: R$ {s.ValorDiaria}");
                    }
                    Console.Write("Digite o número da opção desejada: ");

                    string escolha = Console.ReadLine();
                    if (int.TryParse(escolha, out int indice) && indice >= 1 && indice <= suitesDisponiveis.Count)
                    {
                        suite = suitesDisponiveis[indice - 1];
                        Console.WriteLine($"Você selecionou a suíte: {suite.TipoSuite}.");
                    }
                    else
                    {
                        Console.WriteLine("Opção inválida! Suíte não selecionada.");
                    }
                    break;

                case "3":
                    // Verifica se os dados necessários para a reserva estão disponíveis
                    if (hospedes.Count == 0 || suite == null)
                    {
                        Console.WriteLine("Você precisa cadastrar hóspedes e selecionar uma suíte antes de fazer uma reserva!");
                        break;
                    }

                    Console.Write("Digite a quantidade de dias para a reserva: ");
                    if (!int.TryParse(Console.ReadLine(), out int diasReservados) || diasReservados <= 0)
                    {
                        Console.WriteLine("Quantidade de dias inválida.");
                        break;
                    }

                    try
                    {
                        // Cria a reserva e associa os hóspedes e a suíte
                        reserva = new Reserva(diasReservados: diasReservados);
                        reserva.CadastrarSuite(suite);
                        reserva.CadastrarHospedes(hospedes);

                        Console.WriteLine("Reserva feita com sucesso!");
                    }
                    catch (Exception ex)
                    {
                        // Trata possíveis exceções ao tentar fazer a reserva
                        Console.WriteLine($"Erro ao fazer a reserva: {ex.Message}");
                    }
                    break;

                case "4":
                    // Exibe as informações da reserva, se houver
                    if (reserva == null)
                    {
                        Console.WriteLine("Nenhuma reserva encontrada.");
                    }
                    else
                    {
                        Console.WriteLine($"Quantidade de Hóspedes: {reserva.ObterQuantidadeHospedes()}");

                        // Lista os nomes dos hóspedes cadastrados
                        Console.WriteLine("Hóspedes:");
                        foreach (Pessoa hospede in reserva.Hospedes)
                        {
                            Console.WriteLine($"- {hospede.Nome}");
                        }

                        // Exibe o valor total da reserva
                        Console.WriteLine($"Valor total da diária: {reserva.CalcularValorDiaria():C}");
                        if (reserva.DiasReservados >= 10)
                        {
                            Console.WriteLine("Você recebeu um desconto de 10% por reservar 10 dias ou mais.");
                        }
                    }
                    break;

                case "5":
                    // Encerra o programa
                    Console.WriteLine("Saindo do sistema...");
                    return;

                default:
                    // Trata opções inválidas no menu
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }
}
