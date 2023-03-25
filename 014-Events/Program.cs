namespace _014_Events
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var stock = new Stock("Amazon");
            stock.Price = 100;

            //Here I told the compiler that I'm intereseted in the event ,,, when the event make ,, do the Method " Stock_OnPricechanged"
            stock.OnPriceChanged += (Stock stock,decimal oldPrice) =>
            {
                string result = "";
                if (stock.Price > oldPrice)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    result = "UP";
                }
                else if (stock.Price < oldPrice)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    result = "DOWN";

                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    result = "No Change";
                }
                Console.WriteLine($"{stock.Name} : {stock.Price} : {result}");
            };

            stock.ChangeStockPriceBy(0.01m);
            stock.ChangeStockPriceBy(-0.02m);
            stock.ChangeStockPriceBy(0.03m);

            #region UN-Subscripe
            ////Here if i want to remove the attention of the event. 
            //stock.OnPriceChanged -= Stock_OnPriceChanged;

            //stock.ChangeStockPriceBy(0.05m);
            //stock.ChangeStockPriceBy(-0.02m);
            //stock.ChangeStockPriceBy(0.00m); 
            #endregion

            Console.ReadKey();
        }


        // Method generated when using event by the subscriper += 
        #region I will use Lamda Expression Instead
        //private static void Stock_OnPriceChanged(Stock stock, decimal oldPrice)
        //{
        //    string result = "";
        //    if (stock.Price > oldPrice)
        //    {
        //        Console.ForegroundColor= ConsoleColor.Green;
        //        result = "UP";
        //    }
        //    else if (stock.Price < oldPrice)
        //    {
        //        Console.ForegroundColor = ConsoleColor.Red;
        //        result = "DOWN";

        //    }
        //    else
        //    {
        //        Console.ForegroundColor = ConsoleColor.Gray;
        //    }
        //    Console.WriteLine($"{stock.Name} : {stock.Price} : {result}");
        //} 
        #endregion
    }



    public delegate void StockPriceChangehandler(Stock stock, decimal oldPrice);

    public class Stock
    {
        private string name;
        private decimal price;


        //Declaration of Event...   datatype of the event must be delegate ( OnPricechanged ----> StockPriceChangedhandler )
        public event StockPriceChangehandler OnPriceChanged;


        public string Name => this.name;                                             //Get the name only
        public decimal Price { get => this.price; set => this.price = value; }      //Get And Set the Price


        //Constructor used to be called to get the name of the stock and assign it to name field "Private" 
        public Stock(string stockname)
        {
            this.name = stockname;
        }

        //Method to change the Stock Price by Percent  
        public void ChangeStockPriceBy(decimal percent)
        {
            decimal oldPrice = this.price;
            this.price += Math.Round(this.Price * percent, 2);
            if (OnPriceChanged != null)
            {
                OnPriceChanged(this, oldPrice);       /// Firing the Event
            }
        }

    }
}