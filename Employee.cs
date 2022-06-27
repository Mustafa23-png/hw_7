namespace hw7_mateen
{
    public class Employee
    {
        private int id;
        private string name;
        private string email;
        private decimal salary;
        private string department;

        public Employee(int id, string name, string email, decimal salary, string department)
        {
            this.id = id;
            this.name = name;
            this.email = email;
            this.salary = salary;
            this.department = department;
        }

        public int Id {
            get => id;
        }

        public string Name
        {
            get => name;
            set => name = value;
        }

        public string Email
        {
            get => email;
            set => email = value;
        }

        public decimal Salary
        {
            get => salary;
            set => salary = value;
        }

        public string Department
        {
            get => department;
            set => department = value;
        }

        public override string ToString()
        {
            return "id: "+id+", name: " + name+", email: " + email+", salary: " + salary +", department: "+ department;
        }
    }
}