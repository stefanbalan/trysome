namespace CoL.Service.Infra;

public class MissingConfigurationException(string? message = null) 
    : Exception(message);