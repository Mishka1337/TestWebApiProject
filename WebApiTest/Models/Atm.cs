namespace WebApiTest.Models
{
    public static class Atm
    {
        public static int? Count100 { get; set; }

        public static int? Count200 { get; set; }

        public static int? Count1000 { get; set; }

        public static int? Count5000 { get; set; }

        public static bool Is_Valid_Value(int? value)
        {
            if (value / 5000 < Count5000)
            {
                value -=  5000 * (value / 5000);
            }
            else
            {
                value -= 5000 * Count5000;
            }
            if (value / 1000 < Count1000)
            {
                value -=  1000 * (value / 1000);
            }
            else
            {
                value -= 1000 * Count1000;
            }if (value / 200 < Count200)
            {
                value -=  200 * (value / 200);
            }
            else
            {
                value -= 200 * Count200;
            }
            if (value / 100 < Count100)
            {
                value -=  100 * (value / 100);
            }
            else
            {
                value -= 100 * Count100;
            }

            return value == 0;
        }

        public static void Withdraw(int? value, Cardholder cardholder)
        {
            if (value / 5000 < Count5000)
            {
                Count5000 -= value / 5000;
                cardholder.Count5000 += value / 5000;
                value -= 5000 * (value / 5000);
            }
            else
            {
                value -= 5000 * Count5000;
                cardholder.Count5000 += Count5000;
                Count5000 = 0;
            }

            if (value / 1000 < Count1000)
            {
                Count1000 -= value / 1000;
                cardholder.Count1000 += value / 1000;
                value -= 1000 * (value / 1000);
            }
            else
            {
                value -= 1000 * Count1000;
                cardholder.Count1000 += Count1000;
                Count1000 = 0;
            }

            if (value / 200 < Count200)
            {
                Count200 -= value / 200;
                cardholder.Count200 += value / 200;
                value -= 200 * (value / 200);
            }
            else
            {
                value -= 200 * Count200;
                cardholder.Count200 += Count200;
                Count200 = 0;
            }

            if (value / 100 < Count100)
            {
                Count100 -= value / 100;
                cardholder.Count100 += value / 100;
                value -= 100 * (value / 100);
            }
            else
            {
                value -= 100 * Count100;
                cardholder.Count5000 += Count5000;
                Count100 = 0;
            }
        }
    }
}
