using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManager
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var manager = new ParkingManagerOld();

			manager.ParkingForTime();

			Console.ReadLine();
		}
	}

	public class ParkingManagerOld
	{
		private readonly IDictionary<int, int> openCloseHours = new Dictionary<int, int>
			{
				{ 8, 12 },
				{ 14, 18 },
			};

		private readonly IDictionary<short, short> refPrices = new Dictionary<short, short>
			{
				{ 50, 30 },
				{ 100, 120 },
				{ 200, 180 },
			};

		public void ParkingForTime()
		{
			Console.WriteLine("Combien d'heures voulez vous stationner ?");

			var time = 0.0;

			if (!TryGetInputDouble(out time))
				Environment.Exit(0);

			var moneyCents = 0;
			var rest = time = time * 60;

			for (int i = refPrices.Count - 1; i >= 0 && rest > 0; i--)
			{
				var refValue = refPrices.Values.ElementAt(i);
				var amount = (short)Math.Floor((double)rest / refValue);

				rest = rest % refValue;
				moneyCents += amount * refPrices.First(kv => kv.Value == refValue).Key;
			}

			if (rest > 0)
			{
				var min = refPrices.OrderBy(kv => kv.Key).First();
				moneyCents += min.Key;
				time += min.Value;
			}
			Console.WriteLine(@"Tarif: {0} euros pour {1}.", moneyCents / 100, GetFormatedTime((int)time));
		}

		public void ParkingWithMoney()
		{
			Console.WriteLine("Combien de monnaie avez vous ?");

			var money = 0.0;

			if (!TryGetInputDouble(out money))
				Environment.Exit(0);

			var rest = money * 100;

			var time = GetParkingTime(ref rest);

			Console.WriteLine(@"Vous pouvez stationner pendant {0} pour {1} euros.", GetFormatedTime(time), money - rest / 100);
			if (rest > 0)
			{
				Console.WriteLine("Il vous reste {0} euros.", rest / 100);
			}
		}

		private int GetParkingTime(ref double moneyCents)
		{
			var time = 0;

			for (int i = refPrices.Count - 1; i >= 0 && moneyCents != 0; i--)
			{
				var refValue = refPrices.Keys.ElementAt(i);
				var amount = (short)Math.Floor((double)moneyCents / refValue);

				moneyCents = moneyCents % refValue;
				time += amount * refPrices[refValue];
			}

			return time;
		}

		private bool TryGetInputDouble(out double value)
		{
			value = 0.0;

			while (true)
			{
				var inputDouble = Console.ReadLine();

				if (inputDouble == "exit")
				{
					return false;
				}
				else if (Double.TryParse(inputDouble, NumberStyles.AllowDecimalPoint, new CultureInfo("en-US"), out value))
				{
					break;
				}
				else
				{
					Console.WriteLine("Format invalid (ex: 1 ou 1.5)");
					Console.WriteLine("Tapper exit pour quitter l'application");
				}
			}

			return true;
		}

		private string GetFormatedTime(int minutes)
		{
			var timeStringBuilder = new StringBuilder();
			var minutesInHour = 60;
			var minutesInDay = openCloseHours.Sum(o => o.Value - o.Key) * minutesInHour;

			if (minutes >= minutesInDay)
			{
				var days = minutes / minutesInDay;
				timeStringBuilder.Append(days);
				timeStringBuilder.Append(" jours ");
				minutes -= days * minutesInDay;
			}

			if (minutes >= minutesInHour)
			{
				var hours = minutes / minutesInHour;
				timeStringBuilder.Append(hours);
				timeStringBuilder.Append(" heures ");
				minutes -= hours * minutesInHour;
			}

			if (minutes > 0)
			{
				timeStringBuilder.Append(minutes);
				timeStringBuilder.Append(" minutes");
			}

			return timeStringBuilder.ToString().Trim();
		}
	}
}