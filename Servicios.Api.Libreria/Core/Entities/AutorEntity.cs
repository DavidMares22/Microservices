namespace Servicios.Api.Libreria.Core.Entities
{
    [BsonCollection("Autor")]

    public class AutorEntity : Document
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string GradoAcademico { get; set; }

    }
}
