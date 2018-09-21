
using CorMon.Application.Posts.Dto;
using CorMon.Application.Taxonomies.Dto;
using CorMon.Application.Users.Dto;
using CorMon.Core.Domain;
using System.Collections.Generic;

namespace CorMon.Application.Mapper
{
    public interface IMapperService
    {
        PostOutput BindToOutputModel(Post post, User user, IEnumerable<Taxonomy> tags, IEnumerable<Taxonomy> categories);
        PostInput BindToInputModel(Post post, string[] tagsPrefill);
        Post BindToDomainModel(PostInput input, string[] categoryIds, string[] tagIds);
        Taxonomy BindToDomainModel(TaxonomyInput input);
        TaxonomyOutput BindToOutputModel(Taxonomy taxonomy);
        TaxonomyInput BindToInputModel(Taxonomy taxonomy);
        UserOutput BindToOutputModel(User user);
        UserInput BindToInputModel(User user);
    }
}
