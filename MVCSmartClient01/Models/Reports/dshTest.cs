//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVCSmartClient01.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public partial class dshTest
    {
        public class RegionSales
        {
            public int Year { get; private set; }
            public string Region { get; private set; }
            public double Sales { get; private set; }
            public RegionSales(int year, string region, double sales)
            {
                Year = year;
                Region = region;
                Sales = sales;
            }
        }
        public static IList<RegionSales> GetSales()
        {
            int prevYear = DateTime.Now.Year - 1;
            return new List<RegionSales>() {
                new RegionSales(prevYear - 2, "Asia", 4.2372),
                new RegionSales(prevYear - 2, "Australia", 1.7871),
                new RegionSales(prevYear - 2, "Europe", 3.0884),
                new RegionSales(prevYear - 2, "North America", 3.4855),
                new RegionSales(prevYear - 2, "South America", 1.6027),
                new RegionSales(prevYear - 1, "Asia", 4.7685),
                new RegionSales(prevYear - 1, "Australia", 1.9576),
                new RegionSales(prevYear - 1, "Europe", 3.3579),
                new RegionSales(prevYear - 1, "North America", 3.7477),
                new RegionSales(prevYear - 1, "South America", 1.8237),
                new RegionSales(prevYear, "Asia", 5.2890),
                new RegionSales(prevYear, "Australia", 2.2727),
                new RegionSales(prevYear, "Europe", 3.7257),
                new RegionSales(prevYear, "North America", 4.1825),
                new RegionSales(prevYear, "South America", 2.1172)
            };
        }
    }
}
