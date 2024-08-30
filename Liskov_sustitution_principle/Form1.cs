namespace Liskov_sustitution_principle
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SaleWithTaxes sale = new LocalSale(100, "Jhon", 0.16m);
            sale.CalculateTaxes();
            sale.Generate();
            sale = new FrontierSale(150, "Paul", 0.15m);
            sale.CalculateTaxes();

            AbstractSale sale2 = new ForeignSale(200, "Peter");
            sale2.Generate();
        }

        public abstract class AbstractSale
        {             
            decimal amount;
            string customer;
            protected AbstractSale(decimal amount, string customer)
            {
                this.amount = amount;
                this.customer = customer;
            }
            public abstract void Generate();
        }

        public class LocalSale : SaleWithTaxes
        {
            public LocalSale(decimal amount, string customer, decimal taxes) : base(amount, customer, taxes) { }

            public override void Generate() {MessageBox.Show("sale completed"); }

            public override void CalculateTaxes() { MessageBox.Show("Taxes calculation complete"); }

        }

        public class FrontierSale : SaleWithTaxes
        {
            public FrontierSale(decimal amount, string customer, decimal taxes) : base(amount, customer, taxes) { }

            public override void Generate() { MessageBox.Show("sale completed"); }

            public override void CalculateTaxes() { MessageBox.Show("Taxes calculation complete"); }

            public void CalculateFrontierTaxes() { MessageBox.Show("The frontier is applying new taxes now!!"); }

        }

        public class ForeignSale : AbstractSale
        {
            public ForeignSale(decimal amount, string customer) : base(amount, customer) { }
            
            public override void Generate() { MessageBox.Show("Sale generated"); }
        }

        public abstract class SaleWithTaxes : AbstractSale
        {
            public SaleWithTaxes(decimal amount, string customer, decimal taxes):base(amount, customer)
            {
                this.taxes = taxes;
            }
            
            protected decimal taxes;
            public abstract void CalculateTaxes();
        }
    }
}
