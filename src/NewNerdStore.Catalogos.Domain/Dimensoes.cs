using NewNerdStore.Core.DomainObjects;

namespace NewNerdStore.Catalogos.Domain
{
    public class Dimensoes
    {

        #region Constructors
        public Dimensoes(decimal altura, decimal largura, decimal profundidade)
        {
            Validations.ValidarSeMenorQue(altura, 1, "O campo Altura não pode ser menor ou igual a 0");
            Validations.ValidarSeMenorQue(largura, 1, "O campo Largura não pode ser menor ou igual a 0");
            Validations.ValidarSeMenorQue(profundidade, 1, "O campo Profundidade não pode ser menor ou igual a 0");

            Altura = altura;
            Largura = largura;
            Profundidade = profundidade;
        }
        #endregion

        #region Properties
        public decimal Altura { get; private set; }

        public decimal Largura { get; private set; }

        public decimal Profundidade { private get; set; }
        #endregion

        #region Ad-hoc Getters
        public string DescricaoFormatada() 
            => $"LxAxP: {Largura} x {Altura} x {Profundidade}";

        public override string ToString() 
            => DescricaoFormatada();
        #endregion

    }
}
