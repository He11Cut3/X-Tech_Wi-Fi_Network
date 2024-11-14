using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X_Tech_TestWork_2_.Model
{
    public class DocumentModel
    {
        public List<Person> Persons { get; set; }

        public DocumentModel()
        {
            Persons = new List<Person>
        {
            new Person { Number = 1, LastName = "Иванов", FirstName = "Иван" },
            new Person { Number = 2, LastName = "Сидоров", FirstName = "Сидор" },
            new Person { Number = 3, LastName = "Петров", FirstName = "Петр" }
        };
        }
    }

    public class Person
    {
        public int? Number { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
    }

}
