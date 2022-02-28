using GraphQL.API.Queries;

namespace GraphQL.API.Types
{

    // [ExtendObjectType(Name = "Query")]
    // [ExtendObjectType("Query")]
    // public class QueryType : ObjectTypeExtension<PersonQuery>
    public class QueryType : ObjectTypeExtension<PersonQuery>
    {
        protected override void Configure(IObjectTypeDescriptor<PersonQuery> descriptor)
        {
            descriptor
                .Field(f => f.GetPersonByIdAsync(default!, default!, default!))
                .Type<PersonType>();

            descriptor
                .Field(f => f.GetPersonAsync(default!, default!))
                .Type<ListType<PersonType>>();
        }
    }
}