namespace PersonsInfo
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            Team team = new Team("SoftUni");

            List<Person> persons = new();

            foreach (Person person in persons)
            {
                team.AddPlayer(person);
            }

        }
    }
}