namespace ShareHistoryQueryApi.Converters
{
    public interface IShareHistoryQueryResponseConverter<in T>
    {
        ShareHistoryQueryResponse ConvertFrom(T queryResponse);
    }
}