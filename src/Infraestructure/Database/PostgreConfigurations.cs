namespace Infraestructure.Database
{
    public class PostgreConfigurations
    {
        public string Server { get; set; }
        public string User { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }
        public string DatabaseName { get; set; }

        public string GetConnectionString()
        {
            return @$"Host={Server}; Port={Port}; Database={DatabaseName}; Username={User}; Password={Password}";
        }
    }
}