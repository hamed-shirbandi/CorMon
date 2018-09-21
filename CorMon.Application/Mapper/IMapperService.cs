
using CorMon.Application.Posts.Dto;
using CorMon.Application.Taxonomies.Dto;
using CorMon.Application.Users.Dto;
using CorMon.Core.Domain;

namespace CorMon.Application.Mapper
{
    public interface IMapperService
    {
        PostOutput BindToOutputModel(Post Post);
        PostInput BindToInputModel(Post Post);
        TaxonomyOutput BindToOutputModel(Taxonomy Taxonomy);
        TaxonomyInput BindToInputModel(Taxonomy Taxonomy);
        UserOutput BindToOutputModel(User user);
        UserInput BindToInputModel(User User);
    }
}
