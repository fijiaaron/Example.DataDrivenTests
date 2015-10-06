using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Automation.Site
{
	public class InsurancePolicyReceipt
	{
		public String State { get; set; }
		public InsurancePolicyOptions Options { get; set; }
		public Double SubTotal { get; set; }
		public Double TaxAmount { get; set; }
		public Double Total;
	}
}
