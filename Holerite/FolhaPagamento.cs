using System;

namespace Holerite
{
    public class FolhaPagamento
    {
        private readonly int _diastrabalhados;

        private readonly float _horaextra;

        private readonly string[] _codigos = { "01", "10", "11", "91", "95" };

        private readonly string[] _descricoes =
        {
            "Salário",
            "Horas extras",
            "DSR sobre HE",
            "INSS",
            "IRRF"
        };

        private readonly double[] _valores = new double[5];

        public FolhaPagamento(Funcionario funcionario, int diastrabalhados, float horaextra)
        {
            Funcionario = funcionario;
            _diastrabalhados = diastrabalhados;
            _horaextra = horaextra;
            CalcularReferencias();
            CalcularValores();
        }

        private void CalcularReferencias()
        {
            Referencias[0] = _diastrabalhados;
            Referencias[1] = _horaextra;
        }

        private void CalcularValores()
        {
            _valores[0] = Funcionario.SalarioMensal * _diastrabalhados / 30;
            _valores[1] = Funcionario.SalarioMensal * _horaextra * 1.5 / 220;
            _valores[2] = _valores[1] * 5 / 26;
            _valores[3] = EncargosImpostos.CalculoINSS(this);
            _valores[4] = EncargosImpostos.CalculoIRRF(this);
        }

        public string PegarCodigoPeloIndice(int index)
        {
            return _codigos[index];
        }

        public int PegarIndicePeloCodigo(string code)
        {
            return Array.IndexOf(_codigos, code);
        }

        public string PegarDescricaoPeloIndice(int index)
        {
            return _descricoes[index];
        }

        public double PegarValorPeloIndice(int index)
        {
            return _valores[index];
        }

        public float PegarReferenciaPeloIndice(int index)
        {
            return Referencias[index];
        }

        public double PegarValorSalario()
        {
            return _valores[0];
        }

        public double PegarValorHorasExtras()
        {
            return _valores[1];
        }

        public double PegarValorDescansoRemunerado()
        {
            return _valores[2];
        }

        public double PegarValorInss()
        {
            return _valores[3];
        }

        public double PegarValorIrrf()
        {
            return _valores[4];
        }

        public Funcionario Funcionario { get; }

        public float[] Referencias { get; } = new float[5];
    }
}