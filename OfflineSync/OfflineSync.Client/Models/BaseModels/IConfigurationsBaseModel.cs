namespace OfflineSync.Client.Models.BaseModels
{
    internal interface IConfigurationsBaseModel
    {
        string Key { get; set; }
        string Value { get; set; }
    }
}