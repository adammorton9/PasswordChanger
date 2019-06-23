namespace PasswordChanger.Domain
{
    public class Company
    {
        public int Id { get; }
        public string Name { get; }

        public Company(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
