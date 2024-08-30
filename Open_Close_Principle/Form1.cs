namespace Open_Close_Principle
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<IFood> list = new List<IFood>();
            list.Add(new Sausage(10, 15, "French sausage with salty sauce", 6));
            list.Add(new Rice(10, "German Rice made with love",15));
            list.Add(new Steak(5, 5, "Spanich steak with spicy sauce", 10));
            Better_Invoice bi = new Better_Invoice();
            MessageBox.Show($"Total price: {bi.GetTotal(list)}");


        }
        /// <summary>
        /// BAD IMPLEMENTATION
        /// </summary>

        public class Food
        {
            public string name { get; set; }
            public string type { get; set; }
            public decimal price { get; set; }
        }

        public class Bad_Invoice
        {
            public decimal GetTotal(IEnumerable<Food> foodList)
            {
                decimal total = 0;
                foreach (Food food in foodList)
                {
                    if (food.type == "spicy") total += food.price * 1.1m;
                    else if (food.type == "salty") total += food.price * 1.2m;
                    else if (food.type == "veggie") total += food.price * 1.3m;
                    //Each type of food must be added here... so the class is not "closed to modify"
                }
                return total;
            }
        }
        /// <summary>
        /// END OF BAD IMPLEMENTATION
        /// </summary>
        /// 
        /// 
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// BETTER IMPLEMENTATION
        /// </summary>

        public interface IFood
        {
            public decimal GetPrice();
        }

        public abstract class FoodBase
        {
            public FoodBase(string name, decimal price)
            {
                this.name = name;
                this.price = price;
            }

            public string name { get; set; }
            public decimal price { get; set; }
        }

        public class Steak : FoodBase, IFood
        {
            public Steak(decimal invoice, decimal promo, string name, decimal price):base(name,price)
            {
                this.invoice = invoice;
                this.promo = promo;
            }

            public decimal invoice { get; set; }
            public decimal promo { get; set; }

            public decimal GetPrice()
            {
                return (price * invoice) - promo;
            }
        }


        public class Rice :FoodBase, IFood
        {
            public Rice(decimal invoice, string name, decimal price) : base(name, price)
            {
                this.invoice = invoice;
            }

            public decimal invoice { get; set; }

            public decimal GetPrice()
            {
                return (price * invoice);
            }
        }


        public class Sausage : FoodBase, IFood
        {
            public Sausage(decimal invoice, decimal taxes, string name, decimal price) : base(name, price)
            {
                this.invoice = invoice;
                this.taxes = taxes;
            }
            public decimal invoice { get; set; }

            public decimal taxes { get; set; }

            public decimal GetPrice()
            {
                return (price * invoice)+taxes;
            }
        }

        public class Better_Invoice
            {
                public decimal GetTotal(IEnumerable<IFood> foodList)
                {
                    decimal total = 0;
                    foreach (IFood food in foodList)
                    {
                        total += food.GetPrice();
                    }
                    return total;
                }
            }
            /// <summary>
            /// END OF BETTER IMPLEMENTATION
            /// </summary>

        }
    }
