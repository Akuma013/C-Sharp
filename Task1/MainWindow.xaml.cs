using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        private Person[] peopleArray = new Person[0]; 
        private int idCounter = 1;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text.Trim();
            string address = txtAddress.Text.Trim();
            if (!IsValidName(name))
            {
                MessageBox.Show("Please enter a valid name (letters only).");
                return;
            }

            if (string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(txtAge.Text) ||
                string.IsNullOrWhiteSpace(address))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            if (!int.TryParse(txtAge.Text, out int age))
            {
                MessageBox.Show("Age must be a valid number.");
                return;
            }

           
            Array.Resize(ref peopleArray, peopleArray.Length + 1);
            peopleArray[^1] = new Person(idCounter++, name, age, address);
            UpdateListBox();
        }

        private void btnSearchByAge_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtSearchAge.Text, out int age))
            {
                MessageBox.Show("Enter a valid age.");
                return;
            }

            var results = peopleArray.Where(p => p.Age == age).ToArray();
            DisplaySearchResults(results);
        }

        private void btnSearchByName_Click(object sender, RoutedEventArgs e)
        {
            string name = txtSearchName.Text.Trim();
            if (!IsValidName(name))
            {
                MessageBox.Show("Please enter a valid name (letters only).");
                return;
            }

            var results = peopleArray.Where(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).ToArray();
            DisplaySearchResults(results);
        }

        private void btnRemoveByAge_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtRemoveAge.Text, out int age))
            {
                MessageBox.Show("Enter a valid age.");
                return;
            }

            int index = Array.IndexOf(peopleArray, peopleArray.FirstOrDefault(p => p.Age == age));

            if (index == -1)
            {
                MessageBox.Show("No person found with this age.");
                return;
            }

            peopleArray = peopleArray.Where(p => p.Age != age).ToArray();
            UpdateListBox();
        }

        private void btnRemoveByName_Click(object sender, RoutedEventArgs e)
        {
            string name = txtRemoveName.Text.Trim();
            if (!IsValidName(name))
            {
                MessageBox.Show("Please enter a valid name (letters only).");
                return;
            }

            int index = Array.IndexOf(peopleArray, peopleArray.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase)));

            if (index == -1)
            {
                MessageBox.Show("No person found with this name.");
                return;
            }

            peopleArray = peopleArray.Where(p => !p.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).ToArray();
            UpdateListBox();
        }

        private void btnSortByAge_Click(object sender, RoutedEventArgs e)
        {
            Array.Sort(peopleArray, (p1, p2) => p1.Age.CompareTo(p2.Age));
            UpdateListBox();
        }

        private void btnSortByName_Click(object sender, RoutedEventArgs e)
        {
            Array.Sort(peopleArray, (p1, p2) => string.Compare(p1.Name, p2.Name, StringComparison.OrdinalIgnoreCase));
            UpdateListBox();
        }

        private void DisplaySearchResults(Person[] results)
        {
            lstPeople.Items.Clear();

            if (results.Length == 0)
            {
                MessageBox.Show("No matching records found.");
                return;
            }

            foreach (var person in results)
            {
                lstPeople.Items.Add(person.ToString());
            }
        }

        private void UpdateListBox()
        {
            lstPeople.Items.Clear();
            foreach (var person in peopleArray)
            {
                lstPeople.Items.Add(person.ToString());
            }
        }

        private bool IsValidName(string name)
        {
            return Regex.IsMatch(name, @"^[A-Za-z\s]+$");
        }
    }

    public struct Person
    {
        public int ID { get; }
        public string Name { get; }
        public int Age { get; }
        public string Address { get; }

        public Person(int id, string name, int age, string address)
        {
            ID = id;
            Name = name;
            Age = age;
            Address = address;
        }

        public override string ToString()
        {
            return $"{ID}: {Name}, {Age} years, {Address}";
        }
    }
}
