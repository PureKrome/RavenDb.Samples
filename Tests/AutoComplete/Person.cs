namespace Tests.AutoComplete
{
    public class Person
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return string.Format("{0}{1}",
                                 string.IsNullOrEmpty(FirstName) ? string.Empty : FirstName + " ",
                                 string.IsNullOrEmpty(Surname) ? string.Empty : Surname).Trim();
        }
    }
}