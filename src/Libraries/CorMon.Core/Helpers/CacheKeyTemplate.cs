
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
        /// {2} : taxonomyId
        /// {3} : taxonomyType
        public static string PostsSearchCacheKey => "CorMon.Posts.Search-{0}-{1}-{2}-{3}";




        /// <summary>
        /// Gets a key for caching
        /// </summary>
        /// <remarks>
        public static string PostsCacheKey => "CorMon.Posts";


        #endregion

        #region Taxonomies


        #endregion
    }
}
