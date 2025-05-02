namespace Ambev.DeveloperEvaluation.WebApi.Common
{
    public static class QueryExtensions
    {
        private static readonly string[] DefaultExcludes = { "_page", "_size", "_order" };

        /// <summary>
        /// Extracts all query-string entries except the paging and ordering keys,
        /// trimming each value’s whitespace.
        /// </summary>
        public static IDictionary<string, string> ToFilters(
            this IQueryCollection query,
            params string[] excludes)
        {
            var blacklist = (excludes?.Length > 0 ? excludes : DefaultExcludes)
                                .Select(e => e.ToLowerInvariant())
                                .ToHashSet();

            return query
                .Where(kvp => !blacklist.Contains(kvp.Key.ToLowerInvariant()))
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.ToString().Trim());
        }
    }
}
