namespace Holerite
{
    public class EncargosImpostos
    {
        private const float Deducao = 189.59f;

        private static readonly float[] _faixaINSS =
        {
            1320.00f,
            2571.29f,
            3856.94f,
            7507.49f
        };

        private static readonly float[] _aliquotaINSS =
        {
            7.5f,
            9.0f,
            12.0f,
            14.0f
        };

        private static readonly float[] _faixaIRRF = new float[5]
        {
            2112.0f,
            2826.65f,
            3751.05f,
            4664.28f,
            999999.99f
        };

        private static float[] _aliquotaIRRF = new float[5]
        {
            0.0f,
            7.5f,
            15.0f,
            22.5f,
            27.5f
        };

        private static readonly float[] _deducaoIRRF = new float[5]
        {
            0.0f,
            158.40f,
            370.40f,
            651.73f,
            884.96f
        };

        public static double CalculoINSS(FolhaPagamento folhaPagamento)
        {
            double valorBase;
            var baseCalculo = folhaPagamento.PegarValorSalario() + folhaPagamento.PegarValorHorasExtras() +
                              folhaPagamento.PegarValorDescansoRemunerado();
            if (baseCalculo < _faixaINSS[0]) valorBase = baseCalculo;
            else
            {
                valorBase = _faixaINSS[0];
                baseCalculo = baseCalculo > _faixaINSS[3] ? _faixaINSS[3] : baseCalculo;
            }

            int indFaixa = 0;
            double valorINSS = 0;
            do
            {
                valorINSS += valorBase * _aliquotaINSS[indFaixa] / 100f;
                baseCalculo -= valorBase;
                if (valorBase > baseCalculo) valorBase = baseCalculo;
                else valorBase = _faixaINSS[++indFaixa] - _faixaINSS[indFaixa - 1];
                folhaPagamento.Referencias[4] = _aliquotaINSS[indFaixa];
            } while (baseCalculo > 0.00);

            return valorINSS;
        }

        public static double CalculoIRRF(FolhaPagamento folhaPagamento)
        {
            var baseCalculo = folhaPagamento.PegarValorSalario() + folhaPagamento.PegarValorHorasExtras() +
                              folhaPagamento.PegarValorDescansoRemunerado()
                              - (folhaPagamento.Funcionario.NDependentes * Deducao) - 528.00f;

            baseCalculo = baseCalculo < 0.0f ? 0.0f : baseCalculo;
            int indFaixa = 0;
            do indFaixa++;
            while (indFaixa < 4 && baseCalculo > _faixaIRRF[indFaixa]);

            folhaPagamento.Referencias[4] = _aliquotaIRRF[indFaixa];
            return baseCalculo * (_aliquotaIRRF[indFaixa] / 100) - _deducaoIRRF[indFaixa];
        }
    }
}