namespace Chulygon.Dtos
{
    public class ComandoLerDto
    {
        public int Id { get; set; }

        public string ComoSeFai { get; set; }

        public string Linea { get; set; }

        //Eliminada propiedade Plataforma, xa que non se vai expoñer ao cliente da API
    }
}