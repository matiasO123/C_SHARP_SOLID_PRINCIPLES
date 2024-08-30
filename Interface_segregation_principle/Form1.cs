namespace Interface_segregation_principle
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        //Original code
        //the interface has a lot of method. It is not bad, but maybe some classes need a few methods, not all
        public interface Crud<T>
        {
            public T get(int id);
            public List<T> list();
            public void delete(int id);
            public void update(int id, T value);
            public void add(int id, T value);
        }

        //End of original code


        //Better code below
        //Let´s divide the intefaces!

        public interface BasicOps<T>
        {
            public T get(int id);
            public List<T> list();
            public void Add(int id, T value);
        }

        public interface Editable<T>
        {
            public void update(int id, T value);
        }

        public interface Delete<T>
        {
            public void delete(int id);
        }


        public class Person
        {
            public int id;
            public string name;
            public int age;
        }


        public class BasicPerson() : BasicOps<Person>
        {
            void BasicOps<Person>.Add(int id, Person value)
            {
                Console.WriteLine("Add a person");
            }

            Person BasicOps<Person>.get(int id)
            {
                Console.WriteLine("Get a person");
                return new Person { id = id };
            }

            List<Person> BasicOps<Person>.list()
            {
                Console.WriteLine("Get people");
                return new List<Person>();
            }
        }

        public class CompletePerson() : BasicOps<Person>, Editable<Person>, Delete<Person>
        {
            public void Add(int id, Person value)
            {
                Console.WriteLine("Add a person");
            }

            public void delete(int id)
            {
                throw new NotImplementedException();
            }

            public Person get(int id)
            {
                Console.WriteLine("Get a person");
                return new Person { id = id };
            }

            public List<Person> list()
            {
                Console.WriteLine("Get people");
                return new List<Person>();
            }

            public void update(int id, Person value)
            {
                Console.WriteLine("Delete person");
            }
        }

        //End of better code
    }



}

