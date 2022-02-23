using PersonReader;

namespace GraphQL.API.Types
{
    using GraphQL.API.Resolvers;
    using GraphQL.Core.Entities;
    using HotChocolate.Types;


    public class PersonType : ObjectType<Person>
    {
        protected override void Configure(IObjectTypeDescriptor<Person> descriptor)
        {
            descriptor
                .Field(f => f.Id)
                .Type<LongType>();

            descriptor
                .Field(f => f.Name)
                .Type<StringType>();

            descriptor
                .Field(f => f.SecondName)
                .Type<StringType>();
            descriptor
             .Field(f => f.Uri)
             .Type<NonNullType<UrlType>>();



        }

    }
}