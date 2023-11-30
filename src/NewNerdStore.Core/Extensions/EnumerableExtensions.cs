namespace NewNerdStore.Core.Extensions
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> itemAction) where T : class
        {
            foreach (var item in items)
            {
                itemAction(item);
            }
        }
    }
}
