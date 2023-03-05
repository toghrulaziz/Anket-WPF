using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AnketWPF
{
    public class Person : INotifyPropertyChanged
    {
		private string? name;
		private string? surname;
		private string? email;
		private string? phone;
		private DateTime? birthday;

		public DateTime? Birthday
		{
			get { return birthday; }
			set { birthday = value; OnPropertyChanged(); }
		}


		public string? Phone
		{
			get { return phone; }
			set { phone = value; OnPropertyChanged(); }
		}


		public string? Email
		{
			get { return email; }
			set { email = value; OnPropertyChanged(); }
		}


		public string? Surname
		{
			get { return surname; }
			set { surname = value; OnPropertyChanged(); }
		}


		public string? Name
		{
			get { return name; }
			set { name = value; OnPropertyChanged(); }
		}




        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


		public Person()
		{

		}

		public Person(string? name,string? surname,string? email,string? phone,DateTime? birthday)
		{
			Name = name;
			Surname = surname;
			Email = email;
			Phone = phone;
			Birthday = birthday;
		}

        public override string ToString()
        {
            return $"{Name}";
        }

    }
}
