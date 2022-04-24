using CorMon.Resource;
using System.ComponentModel.DataAnnotations;

namespace CorMon.Core.Enums
{
    public enum PostLevel
    {
        [Display(Name = "Enum_PostLevel_Intro", ResourceType =typeof(Metadata))]
        Intro = 0,
        [Display(Name = "Enum_PostLevel_Typical", ResourceType = typeof(Metadata))]
        Typical = 1,
        [Display(Name = "Enum_PostLevel_Advance", ResourceType = typeof(Metadata))]
        Advance = 2,
    }

     
    public enum SortOrder
    {
        [Display(Name = "Enum_SortOrder_Desc", ResourceType = typeof(Metadata))]
        Desc,
        [Display(Name = "Enum_SortOrder_Asc", ResourceType = typeof(Metadata))]
        Asc,
    }




    public enum PublishStatus
    {
        [Display(Name = "Enum_PublishStatus_Draft", ResourceType = typeof(Metadata))]
        Draft,
        [Display(Name = "Enum_PublishStatus_Publish", ResourceType = typeof(Metadata))]
        Publish
    }



    public enum RobotsState
    {
        [Display(Name = "Enum_RobotsState_Global", ResourceType = typeof(Metadata))]
        Global,
        [Display(Name = "Enum_RobotsState_NoFollow", ResourceType = typeof(Metadata))]
        NoFollow,
        [Display(Name = "Enum_RobotsState_NoIndex", ResourceType = typeof(Metadata))]
        NoIndex,
        [Display(Name = "Enum_RobotsState_NoFollowNoIndex", ResourceType = typeof(Metadata))]
        NoFollowNoIndex
    }






    public enum TaxonomyType
    {
        Category = 0,
        Tag = 1
    }
}
