namespace Holerite
{
    public class Funcionario
    {

        public Funcionario(string nome, string matricula, double salarioMensal, int nDependentes)
        {
            Nome = nome;
            Matricula = matricula;
            SalarioMensal = salarioMensal;
            NDependentes = nDependentes;
        }

        public string Nome { get; private set;}

        public string Matricula { get; private set;}

        public double SalarioMensal { get; private set; }

        public int NDependentes { get; private set; }

    }
}