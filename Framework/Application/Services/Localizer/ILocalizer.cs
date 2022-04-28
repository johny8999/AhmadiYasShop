namespace Framework.Application.Services.Localizer
{
    public interface ILocalizer
    {
        public string this[string name] { get; } // Localizer["Hi"]
        public string this[string name, params object[] arguments] { get; } // Localizer["Hi","Ali","Hassn"]
    }
}