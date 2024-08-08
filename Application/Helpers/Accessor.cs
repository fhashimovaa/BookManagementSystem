using Microsoft.Extensions.Configuration;

namespace Application.Helpers;

public class Accessor
{
    public static IConfiguration AppConfiguration { get; set; }
}