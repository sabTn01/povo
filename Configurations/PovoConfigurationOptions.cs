namespace POVO.Backend.Configurations
{
    public class PovoConfigurationOptions
    {
        public PovoInfrastructureConfigurationOptions Infrastructure { get; set; }
    }

    public class PovoInfrastructureConfigurationOptions
    {
        public string ConnectionString { get; set; }
    }
}
