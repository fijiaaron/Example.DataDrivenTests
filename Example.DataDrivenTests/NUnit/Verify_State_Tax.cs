using System;
using System.Collections.Generic;
using System.Linq;
using Example.Automation.Site;
using FluentAssertions;
using Example.DataDrivenTests.Data;
using NSubstitute;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Example.DataDrivenTests.NUnit
{
	[TestFixture]
	public class Verify_State_Tax
	{
		private InsuranceSite site;
		private Dictionary<String, Double> stateTaxRates;

		[TestFixtureSetUp]
		public void Initialize()
		{
			IWebDriver driver = Substitute.For<IWebDriver>();
			site = new InsuranceSite(driver);
			stateTaxRates = StateTaxRates.GetStateTaxRates(@"StateTaxRates.csv");
		}

		[Test]
		[TestCaseSource("GetStateTaxRatesFromCsv")]
		public void Tax_Should_Be_Calculated_For_Each_State(string state, double taxRate)
		{
			/* Arrange*/
			Console.WriteLine("tax rate for {0} is {1:P}", state, taxRate);
			
			/* Act */
			var receipt = site.OrderInsurancePolicy(state, new InsurancePolicyOptions());

			/* Assert */
			var calculatedTax = Math.Round(receipt.Total * taxRate, 2);
			var expectation = String.Format("expected tax amount for {0} should be {1:C} ", state, calculatedTax);
			Console.WriteLine(expectation);

			Assert.AreEqual(calculatedTax, receipt.TaxAmount, expectation); // using plain old Asserts
			receipt.TaxAmount.Should().Be(calculatedTax); //using FluentAssertions
		}

		// NUnit needs a datasource
		public static IEnumerable<TestCaseData> GetStateTaxRatesFromCsv()
		{
			var stateTaxRates = StateTaxRates.GetStateTaxRates(@"StateTaxRates.csv");
			var data = stateTaxRates.Select(x => new TestCaseData(x.Key, x.Value)).ToList();
			return data;
		} 
	}
}