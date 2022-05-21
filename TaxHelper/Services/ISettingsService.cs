namespace TaxHelper.Services
{
    public interface ISettingsService<T>
    {
        T Settings { get; set; }
    }
}
