using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic.FileIO;
using System.IO;

namespace Example.DataDrivenTests.Data
{
	public class StateTaxRates
	{
		public static Dictionary<string, double> GetStateTaxRates(String filename)
		{
			if (! File.Exists(filename))
			{
				throw new Exception("file not found: " + filename, new FileNotFoundException());
			}

			var stateTaxRates = new Dictionary<string, double>();

			using (var parser = new TextFieldParser(filename))
			{
				parser.TextFieldType = FieldType.Delimited;
				parser.SetDelimiters(",");

				bool firstRow = true;
				while (! parser.EndOfData)
				{
					if (firstRow) // skip header
					{
						parser.ReadLine();
						firstRow = false;
						continue;
					} 
					
					String[] fields = parser.ReadFields();
					if (fields.Length >= 2) // (i.e. state, taxRate)
					{
						string state = fields[0].Trim();
						double taxRate = Convert.ToDouble(fields[1].Trim());
						stateTaxRates.Add(state, taxRate);
					}
					else
					{
						throw new Exception("unexpected field length" + fields.Length + " in " + filename);
					}
				}
			}

			return stateTaxRates;
		}
	}
}
