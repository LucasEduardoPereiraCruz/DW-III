namespace VasosInteligentes.Data
{
    public class MongoSettings
    {
        public string ConnectionString { get; set; }
        public string DataBase { get; set; }

        public bool IsSsl { get; set; }
    }
}
