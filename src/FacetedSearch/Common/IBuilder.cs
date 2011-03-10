namespace FacetedSearch.Common
{
    public interface IBuilder<out T>
    {
        IBuilder<T> BuildPart(object part);
        T GetResult();
    }
}