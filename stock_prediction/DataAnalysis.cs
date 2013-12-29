using System;
using System.Text;
using System.Collections.Generic;

namespace stock_prediction
{
    public class DataAnalysis
	{

		public void GenerateStockQuoteCSV(string code, List<HistoricalStockRecord> records){

			foreach (var record in records)
			{
				StringBuilder sb = new StringBuilder(); 

				sb.AppendLine(string.Format("{0},{1}", "DayInYear", "Close")); 

                for (int i = 0; i < record.Quotes.Length; i++)
				{
                    sb.AppendLine(string.Format("{0},{1}", i+1, record.Quotes[i])); 
				}

				System.IO.File.WriteAllText(string.Format("{0} - {1}.csv", code, record.Year), sb.ToString());
			}
		}

        public void GenerateStockDerivativeCSV(string code, List<HistoricalStockRecord> records, int daysInterval)
        { 
            foreach (var record in records)
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine(string.Format("{0},{1}", "DayInYear", "Derivative"));

                //calculation start on the forth date of the year
                for (int i = 2; i < record.Quotes.Length - 1; )
                {
                    double date1 = record.Quotes[i];
                    double date2 = record.Quotes[i + daysInterval];

                    double derivative = Math.Derivative(0, (double)daysInterval, date1, date2);
                    sb.AppendLine(string.Format("{0},{1}", i + 2, derivative));

                    i = i + daysInterval;
                }

                System.IO.File.WriteAllText(string.Format("{0} - {1} - Derivative.csv", code, record.Year), sb.ToString());
            }
        }
    }
}

