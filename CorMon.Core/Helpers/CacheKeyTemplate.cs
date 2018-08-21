
namespace CorMon.Core.Helpers
{
   public static class CacheKeyTemplate
    {
        #region Posts

        /// <summary>
        /// Gets a key for caching
        /// </summary>
        /// <remarks>
        /// {0} : Post Id
        /// </remarks>
        public static string PostByIdCacheKey => "CorMon.Posts.Id-{0}";


        /// <summary>
        /// Gets a key for caching
        /// </summary>
        /// <remarks>
        /// {0} : page 
        /// {1} : recordsPerPage
        /// {2} : term
        /// {3} : publishStatus
        /// {4} : sortOrder
        /// </remarks>
        public static string PostsSearchCacheKeyCategoryIdCacheKey => "CorMon.Posts.Search-{0}-{1}-{2}-{3}-{4}";

        #endregion

        #region Taxonomies

      
        #endregion
    }
}
