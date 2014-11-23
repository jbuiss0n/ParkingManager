using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManager
{
	public class ParkingBusiness
	{
		private readonly IDictionary<int, int> m_prices;

		public ParkingBusiness(IDictionary<int, int> prices)
		{
			m_prices = prices;
		}

		public int GetParkingTime(ref int money)
		{
			var time = 0;

			foreach (var price in m_prices.OrderByDescending(price => price.Key))
			{
				time += price.Value * (int)Math.Floor(money / (decimal)price.Key);
				money = money % price.Key;
			}

			return time;
		}
	}
}