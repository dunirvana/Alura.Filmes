namespace Alura.Filmes.App.Negocio
{
    public class Cliente : Pessoa
    {
        public byte Id { get; set; }
        public string PrimeiroNome { get; set; }
        public string UltimoNome { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }

        public override string ToString()
        {
            return $"Cliente ({Id}): {PrimeiroNome} {UltimoNome} - {Ativo}";
        }
    }
}
