namespace DevourDev.International
{
    public interface IInternational<TItem, TCulture>
    {
        TItem Get(TCulture culture);
        bool TryGet(TCulture culture, out TItem item);
    }
}
