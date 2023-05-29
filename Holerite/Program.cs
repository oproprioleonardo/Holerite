using System;

namespace Holerite
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string nome, matricula;
            double salarioMensal;
            int nDependentes, diasTrabalhados;
            float horaExtra;
            Console.Write("Insira o nome do funcionário: ");
            nome = Console.ReadLine();
            Console.Write("Insira a matrícula do funcionário: ");
            matricula = Console.ReadLine();
            Console.Write("Insira o salário mensal: ");
            salarioMensal = double.Parse(Console.ReadLine());
            Console.Write("Insira o número de dependentes: ");
            nDependentes = int.Parse(Console.ReadLine());
            Console.Write("Insira os dias de trabalho: ");
            diasTrabalhados = int.Parse(Console.ReadLine());
            Console.Write("Insira as horas extras: ");
            horaExtra = float.Parse(Console.ReadLine());


            var funcionario = new Funcionario(nome, matricula, salarioMensal, nDependentes);
            var folhaPagamento = new FolhaPagamento(funcionario, diasTrabalhados, horaExtra);

            string operation;
            do
            {
                Console.Clear();
                Console.WriteLine($"Funcionário: {nome}");
                Console.WriteLine($"Matrícula: {matricula}");
                Console.WriteLine($"Salário Mensal: {salarioMensal:N2}");
                Console.WriteLine($"Número de dependentes: {nDependentes}");
                Console.WriteLine();
                Console.WriteLine("Qual dado deseja coletar? (Digite 'sair' para finalizar)");
                for (int i = 0; i < 5; i++)
                    Console.WriteLine(
                        $"{folhaPagamento.PegarCodigoPeloIndice(i)} - {folhaPagamento.PegarDescricaoPeloIndice(i)}");
                operation = Console.ReadLine()?.ToLower() ?? "sair";
                Console.WriteLine();
                try
                {
                    var index = folhaPagamento.PegarIndicePeloCodigo(operation);
                    var valor = folhaPagamento.PegarValorPeloIndice(index);
                    var desc = folhaPagamento.PegarDescricaoPeloIndice(index);
                    var referencia = folhaPagamento.PegarReferenciaPeloIndice(index);
                    Console.WriteLine("Código | Descrição | Referência | Valor");
                    Console.WriteLine($"{operation} | {desc} | {referencia:N2} | {valor:N2}");
                    Console.WriteLine();
                    Console.WriteLine("Tecle para continuar... ");
                    Console.ReadKey();
                }
                catch (Exception e)
                {
                    operation = "sair";
                }
            } while (!operation.Equals("sair"));

            /*
            Console.WriteLine("");
            Console.WriteLine("Valor do salário: " + folhaPagamento.PegarValorSalario().ToString("N2"));
            Console.WriteLine("Valor do descanso remunerado: " +
                              folhaPagamento.PegarValorDescansoRemunerado().ToString("N2"));
            Console.WriteLine("Valor das horas extras: " + folhaPagamento.PegarValorHorasExtras().ToString("N2"));
            Console.WriteLine("Valor do INSS: " + folhaPagamento.PegarValorInss().ToString("N2"));
            Console.WriteLine("Valor do IRRF: " + folhaPagamento.PegarValorIrrf().ToString("N2"));
            Console.ReadKey();
            */
        }
    }
}