using PersonReader;

namespace GraphQL.API.Queries
{
    [ExtendObjectType(Name = "Query")]
    public class PersonQuery
    {
        public Task<ICollection<Person>> GetPersonAsync(
        [Service] PersonService service,
        CancellationToken cancellationToken)
        {
            return service.GetPersonAsync(cancellationToken);
        }

        public Task<Person> GetPersonByIdAsync(
            [Service] PersonService service,
            int id,
            CancellationToken cancellationToken)
        {
            return service.GetPersonByIdAsync(id);
        }
    }
}