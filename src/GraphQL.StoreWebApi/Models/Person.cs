using System.ComponentModel.DataAnnotations;

namespace GraphQL.StoreWebApi.Models
{
    public class Person
    {
        public int Id { get; set; }

        public Uri? Uri { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string SecondName { get; set; } = string.Empty;


    }
}