using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ParkingManager.Test
{
	[TestClass]
	public class ParkingBusinessTest
	{
		private ParkingBusiness m_business;

		public ParkingBusinessTest()
		{
			m_business = new ParkingBusiness(new Dictionary<int, int>
				{
					{ 50, 30 },
					{ 100, 120 },
					{ 200, 180 },
				});
		}

		[TestMethod]
		public void ShouldReturn0MinutesFor0Cents()
		{
			var money = 0;
			Assert.AreEqual(m_business.GetParkingTime(ref money), 0);
		}

		[TestMethod]
		public void ShouldReturn30MinutesFor50Cents()
		{
			var money = 50;
			Assert.AreEqual(m_business.GetParkingTime(ref money), 30);
		}

		[TestMethod]
		public void ShouldReturn2HoursFor1Euros()
		{
			var money = 100;
			Assert.AreEqual(m_business.GetParkingTime(ref money), 120);
		}

		[TestMethod]
		public void ShouldReturn3HoursFor2Euros()
		{
			var money = 200;
			Assert.AreEqual(m_business.GetParkingTime(ref money), 180);
		}

		[TestMethod]
		public void ShouldReturn5HoursFor3Euros()
		{
			var money = 300;
			Assert.AreEqual(m_business.GetParkingTime(ref money), 300);
		}

		[TestMethod]
		public void ShouldReturn9HoursFor6Euros()
		{
			var money = 600;
			Assert.AreEqual(m_business.GetParkingTime(ref money), 540);
		}

		[TestMethod]
		public void ShouldReturn9HoursFor620CentsWith20CentsRest()
		{
			var money = 620;
			Assert.AreEqual(m_business.GetParkingTime(ref money), 540);
			Assert.AreEqual(money, 20);
		}
	}
}