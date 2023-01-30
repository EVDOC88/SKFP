using Microsoft.VisualBasic.FileIO;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using static mod7.Person;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace mod7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Куда доставить, напишите адрес?");
            string adress = Console.ReadLine();
            Order<HomeDelivery> order = new Order<HomeDelivery>(new HomeDelivery(adress));
            Console.WriteLine("Ваш заказ № {0}", order.Number);
            Order<HomeDelivery>.Product product = new Order<HomeDelivery>.Product();
            var desc = product.AddProduct();
            order.Description = "Товар:" + desc.Name + " " + "Цена:" + desc.Cost;

            Console.WriteLine(order.Description);

            order.Delivery.DisplayAddress();






        }
    }
    abstract class Delivery
    {
        public string? Address;
        public Delivery(string address)
        {
            Address = address;
        }
        public abstract void DisplayAddress();
        protected bool CheckAddress()
        {
            if (Address == "")
            {
                Console.WriteLine("Адрес не может быть пустым, скорректируйте адрес");
                return true;
            }
            else
            {
                return false;
            }
        }


    }

    class HomeDelivery : Delivery
    {
        public HomeDelivery(string address) : base(address)
        {

        }
        public override void DisplayAddress()
        {
            if (CheckAddress())
            {

            }
            else
            {
                Console.WriteLine("Ваш заказ будет доставлен по адресу: {0}", Address);
            }
        }
    }

    class PickPointDelivery : Delivery
    {
        public PickPointDelivery() : base("г. Павловский Посад")
        {

        }
        public override void DisplayAddress()
        {
            Console.WriteLine("Заказ можно забрать завтра в нашем пункте выдачи по адресу: {0}", Address);
        }
    }

    class ShopDelivery : Delivery
    {
        public ShopDelivery() : base("Выбор магазина для доставки можно сделать на сайте или по телефону, назовите номер заказа")
        {

        }
        public override void DisplayAddress()
        {
            Console.WriteLine("{0}, Телефон - 89999, Сайт - собака", Address);
        }
    }

    class Order<TDelivery> where TDelivery : Delivery
    {
        public TDelivery Delivery;

        public int Number;

        public string Description;

        public Order(TDelivery delivery)
        {
            Delivery = delivery;
            Number = new Random().Next(0, 1000);
      
        }


        /// Продукты
        public class Product 
        {
            public string? name;
            public string? description;
            public double cost;
            public int id;

            ///Добавляется продукт 
            public (string Name, string Description, double Cost, int Id) AddProduct()
            {
                (string Name, string Description, double Cost, int Id) AddProduct;


                AddProduct.Name = "Товар";
                AddProduct.Description = "Хороший товар";
                AddProduct.Cost = 142.50;
                AddProduct.Id = 123445;

                name = AddProduct.Name;
                description = AddProduct.Description;
                cost = AddProduct.Cost;
                id = AddProduct.Id;


                return (AddProduct.Name, AddProduct.Description, AddProduct.Cost, AddProduct.Id);
            }

        }

    }
  
    /// Контрагенты
    public abstract class  Person
    {
        private string? firstname;
        private string? surname;
        private string? lastname;
        private int id;
        private int age;
        private string? telefon;
        private TypeOfPerson typeofPerson;
        /// <summary>
        ///  Свойство параметра Age
        /// </summary>
        public int Age
        {
            get
            {
                return age;
            }

            set
            {
                if (value < 18)
                {
                    Console.WriteLine("Возраст должен быть не меньше 18");
                    Console.WriteLine("Введите возраст");
                    Age = int.Parse(Console.ReadLine());

                }
                else
                {
                    age = value;
                }
            }
       
        }
        /// <summary>
        /// Метод добавления контрагента
        /// </summary>
        public virtual void AddPerson()
        {
            Console.WriteLine("Введите фамилию");
            surname = Console.ReadLine();
            Console.WriteLine("Введите имя");
            firstname = Console.ReadLine();
            Console.WriteLine("Введите отчество");
            lastname = Console.ReadLine();
            Console.WriteLine("Введите возраст");
            Age = int.Parse(Console.ReadLine());

        }
        /// <summary>
        /// Список возможных типов контрагентов, для которых будет использован общий метод + свои свойства, для кааждого типа
        /// </summary>
        public enum TypeOfPerson : int
        {
            Курьер,
            Сотрудник,
            Клиент

        }

        /// Курьеры - Неследование от Person
        public class Courier : Person
        {
            private TypeOfPerson typeOfPerson = TypeOfPerson.Курьер ;
            private int IdCourier;


            /// <summary>
            ///  Если вызван метод добавления, то добавляется надпись, что мы в форме для курьера
            /// </summary>
            public Courier(TypeOfPerson typeOfPerson)
            {
                this.typeOfPerson = typeOfPerson;
               Console.WriteLine(" Форма для {0}", typeOfPerson);

            }

            public override void AddPerson()
            {
                ///Использования базового метода добавления
                base.AddPerson();
                /// Переопредление метода ( добавление новых вводных)
                Console.WriteLine("Введите уникальный номер курьера!");
                IdCourier = int.Parse(Console.ReadLine());


            }


        }

        /// Композиция, для класса Сотрудник
        class Employee
        {
            public Person employee;
            public Employee (Person employee)
            {
                this.employee = employee;

            }
            
        }

    }



   
}

    


    





