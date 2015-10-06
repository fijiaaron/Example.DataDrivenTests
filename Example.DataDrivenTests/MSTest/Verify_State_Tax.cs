using System;
using System.Collections.Generic;
using Example.Automation.Site;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Example.DataDrivenTests.Data;
using NSubstitute;
using OpenQA.Selenium;

namespace Example.DataDrivenTests.MSTest
{
	[TestClass]
	public class Verify_State_Tax
	{
		private InsuranceSite site;
		private Dictionary<String, Double> stateTaxRates;

		[TestInitialize]
		public void Initialize()
		{
			IWebDriver driver = Substitute.For<IWebDriver>();
			site = new InsuranceSite(driver);
			stateTaxRates = StateTaxRates.GetStateTaxRates(@"StateTaxRates.csv");
		}

		[TestMethod]
		[DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "StateTaxRates.csv", "StateTaxRates#csv", DataAccessMethod.Sequential)]
		public void Tax_Should_Be_Calculated_For_Each_State()
		{
			/* Arrange*/
			string state = Convert.ToString(_testContext.DataRow["state"]);
			double taxRate = Convert.ToDouble(_testContext.DataRow["taxRate"]);
			Console.WriteLine("tax rate for {0} is {1:P}", state, taxRate);
			
			/* Act */
			var receipt = site.OrderInsurancePolicy(state, new InsurancePolicyOptions());

			/* Assert */
			var calculatedTax = Math.Round(receipt.Total * taxRate, 2);
			var expectation = String.Format("expected tax amount for {0} should be {1:C} ", state, calculatedTax);
			Console.WriteLine(expectation);

			Assert.AreEqual(calculatedTax, receipt.TaxAmount, expectation);
			receipt.TaxAmount.Should().Be(calculatedTax);
		}

		// MSTest needs a TestContext object in order to run data driven tests
		public TestContext TestContext
		{
			get { return _testContext; }
			set { _testContext = value; }
		}
		private static TestContext _testContext;
	}
}