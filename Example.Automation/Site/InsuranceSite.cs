using System;
using OpenQA.Selenium;

namespace Example.Automation.Site
{
    public class InsuranceSite
    {
	    private IWebDriver driver;

	    public InsuranceSite(IWebDriver driver)
	    {
		    this.driver = driver;
	    }

		public InsurancePolicyReceipt OrderInsurancePolicy(String state, InsurancePolicyOptions options)
		{
			//TODO: implement steps to order an insurance policy (e.g. with Selenium) and get price info from order summary page

			return new InsurancePolicyReceipt
			{
				State = state,
				Options = options,
				SubTotal = 199.99,
				TaxAmount = 18.50,
				Total = 224.27
			};
		}
	}
}
