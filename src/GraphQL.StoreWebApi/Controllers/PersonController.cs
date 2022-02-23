using Microsoft.AspNetCore.Mvc;
using GraphQL.StoreWebApi.Models;
using System.Net;

namespace GraphQL.StoreWebApi.Controllers;


[ApiController]
[Route("[controller]")]
public class PersonController : ControllerBase
{
    private readonly ILogger<PersonController> _logger;

    public PersonController(ILogger<PersonController> logger)
    {
        _logger = logger;
    }



    [HttpGet(Name = "GetPerson")]
    [ProducesResponseType(typeof(IEnumerable<Person>), (int)HttpStatusCode.OK)]
    public Task<ActionResult<IEnumerable<Person>>> Get()
    {
        var result = new List<Person>
        {
             new Person
                    {
                        Id=1,
                        Uri=new Uri("http://dotnetperls.com/"),
                        Name = "Bob",
                        SecondName = "Kante"
                    },
                    new Person
                    {
                        Id=2,
                        Uri=new Uri("/datagridview-tips"),
                        Name = "Mary",
                        SecondName = "Poppins"
                    }

        }
       .ToArray();

        return Task.FromResult<ActionResult<IEnumerable<Person>>>(Ok(result));
    }

    [HttpGet("{id}", Name = "GetPersonById")]

    [ProducesResponseType(typeof(Person), (int)HttpStatusCode.OK)]
    public Task<ActionResult<Person>> GetPersonById(int id)
    {
        var result = new List<Person>
        {
             new Person
                    {
                        Id=1,
                        Uri=new Uri("http://dotnetperls.com/"),
                        Name = "Bob",
                        SecondName = "Kante"
                    },
                    new Person
                    {
                        Id=2,
                        Uri=new Uri("/datagridview-tips"),
                        Name = "Mary",
                        SecondName = "Poppins"
                    }

        }
       .ToArray();

        return Task.FromResult<ActionResult<Person>>(Ok(result.FirstOrDefault(a => a.Id == id)));
    }

}
