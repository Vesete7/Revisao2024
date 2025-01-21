using System;
using System.IO;
using System.Text;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n=== Gerador de Senhas ===");
            Console.ResetColor();

            Console.WriteLine("1. Gerar nova senha");
            Console.WriteLine("2. Listar senhas salvas");
            Console.WriteLine("3. Sair");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Escolha uma opção: ");
            Console.ResetColor();
            string escolha = Console.ReadLine();

            switch (escolha)
            {
                case "1":
                    GerarSenha();
                    break;
                case "2":
                    ListarSenhas();
                    break;
                case "3":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Saindo...");
                    Console.ResetColor();
                    return;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Opção inválida! Tente novamente.");
                    Console.ResetColor();
                    break;
            }
        }
    }

    static void GerarSenha()
    {
        try
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Digite o tamanho da senha: ");
            Console.ResetColor();
            int tamanho = int.Parse(Console.ReadLine());

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Incluir letras? (s/n): ");
            Console.ResetColor();
            bool incluirLetras = Console.ReadLine().ToLower() == "s";

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Incluir símbolos (@, !, #, -)? (s/n): ");
            Console.ResetColor();
            bool incluirSimbolos = Console.ReadLine().ToLower() == "s";

            string senha = GerarSenhaAleatoria(tamanho, incluirLetras, incluirSimbolos);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Senha gerada: {senha}");
            Console.ResetColor();

            SalvarSenha(senha);
        }
        catch (FormatException)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Entrada inválida! Tente novamente.");
            Console.ResetColor();
        }
    }

    static string GerarSenhaAleatoria(int tamanho, bool incluirLetras, bool incluirSimbolos)
    {
        const string numeros = "0123456789";
        const string letras = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string simbolos = "@!#-";

        StringBuilder caracteres = new StringBuilder(numeros);

        if (incluirLetras)
        {
            caracteres.Append(letras);
        }

        if (incluirSimbolos)
        {
            caracteres.Append(simbolos);
        }

        if (caracteres.Length == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Erro: Nenhum conjunto de caracteres foi selecionado!");
            Console.ResetColor();
            return null;
        }

        Random random = new Random();
        StringBuilder senha = new StringBuilder();

        for (int i = 0; i < tamanho; i++)
        {
            senha.Append(caracteres[random.Next(caracteres.Length)]);
        }

        return senha.ToString();
    }

    static void SalvarSenha(string senha)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter("bkp.TXT", true))
            {
                writer.WriteLine(senha);
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Senha salva com sucesso!");
            Console.ResetColor();
        }
        catch (Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Erro ao salvar a senha: {e.Message}");
            Console.ResetColor();
        }
    }

    static void ListarSenhas()
    {
        try
        {
            if (File.Exists("bkp.TXT"))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Senhas salvas:");
                Console.ResetColor();
                Console.WriteLine(File.ReadAllText("bkp.TXT"));
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Nenhuma senha encontrada!");
                Console.ResetColor();
            }
        }
        catch (Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Erro ao ler o arquivo: {e.Message}");
            Console.ResetColor();
        }
    }
}
