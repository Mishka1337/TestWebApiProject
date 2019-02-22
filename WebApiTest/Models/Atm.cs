namespace WebApiTest.Models
{
    public class Atm
    {
        public int Id { get; set; }

        public int Count100 { get; set; }

        public int Count200 { get; set; }

        public int Count1000 { get; set; }

        public int Count5000 { get; set; }

        public bool TryWithdraw(int value, Cardholder cardholder)
        {
            int count100 = Count100;
            int count200 = Count200;
            int count1000 = Count1000;
            int count5000 = Count5000;
            int cardholderCount100 = cardholder.Count100;
            int cardholderCount200 = cardholder.Count200;
            int cardholderCount1000 = cardholder.Count1000;
            int cardholderCount5000 = cardholder.Count5000;

            if (value / 5000 < count5000)
            {
                count5000 -= value / 5000;
                cardholderCount5000 += value / 5000;
                value -= 5000 * (value / 5000);
            }
            else
            {
                value -= 5000 * count5000;
                cardholderCount5000 += Count5000;
                count5000 = 0;
            }

            if (value / 1000 < count1000)
            {
                count1000 -= value / 1000;
                cardholderCount1000 += value / 1000;
                value -= 1000 * (value / 1000);
            }
            else
            {
                value -= 1000 * count1000;
                cardholderCount1000 += count1000;
                count1000 = 0;
            }

            if (value / 200 < count200)
            {
                count200 -= value / 200;
                cardholderCount200 += value / 200;
                value -= 200 * (value / 200);
            }
            else
            {
                value -= 200 * count200;
                cardholderCount200 += count200;
                count200 = 0;
            }

            if (value / 100 < count100)
            {
                count100 -= value / 100;
                cardholderCount100 += value / 100;
                value -= 100 * (value / 100);
            }
            else
            {
                value -= 100 * count100;
                cardholderCount100 += count100;
                count100 = 0;
            }
            if (value != 0)
            {
                return false;
            }
            Count100 = count100;
            Count200 = count200;
            Count1000 = count1000;
            Count5000 = count5000;
            cardholder.Count100 = cardholderCount100;
            cardholder.Count200 = cardholderCount200;
            cardholder.Count1000 = cardholderCount1000;
            cardholder.Count5000 = cardholderCount5000;
            return true;
        }
    }
}
