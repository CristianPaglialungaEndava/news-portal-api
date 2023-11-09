using System.Collections.Generic;

namespace newsPortal.Extensions
{
    public static class PaginationExtension
    {
        public static IEnumerable<T> GetPaginatedItems<T>(this IEnumerable<T> collection, int pageNumber, int pageSize)
        {
            return collection.Skip(GetItemsToSkipNumber(pageNumber, pageSize, collection.Count())).Take(GetItemsToTakeNumber(pageNumber, pageSize, collection.Count()));
        }

        public static bool IsLastPage<T>(this IEnumerable<T> collection,int pageNumber, int pageSize)
        {
            var itemsNumberToSkip = GetItemsToSkipNumber(pageNumber, pageSize, collection.Count());
            return (itemsNumberToSkip + pageSize) >= collection.Count();
        }
        private static int GetItemsToSkipNumber(int pageNumber, int pageSize, int totalItems)
        {
            var itemsNumberToSkip = (pageNumber - 1) * pageSize;
            if (itemsNumberToSkip == totalItems)
            {
                throw new ArgumentException("PageNumber is out of list range");
            }
            return itemsNumberToSkip;

        }
        private static int GetItemsToTakeNumber(int pageNumber, int pageSize, int totalItems)
        {
            var itemsNumberToSkip = GetItemsToSkipNumber(pageNumber, pageSize, totalItems);
            var itemsToTake = pageSize;
            if ((itemsNumberToSkip + itemsToTake) >= totalItems)
            {
                itemsToTake = totalItems - itemsNumberToSkip;
            }
            return itemsToTake;
        }
    }
}
