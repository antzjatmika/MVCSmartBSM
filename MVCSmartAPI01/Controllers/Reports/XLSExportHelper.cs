using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Data.Filtering;
using MVCSmartAPI01.Models;

namespace APIService.Controllers
{
    public static class XLSExportHelper
    {
        public static string[] ParseQueryPekerjaan(string strFilterExpre1, string strFilterExpre2)
        {
            string[] arrReturn = new string[] { };
            string strTemp2 = strFilterExpre2.Replace(" ME ", " >= ");
            strTemp2 = strTemp2.Replace(" LE ", " <= ");
            strTemp2 = strTemp2.Replace(" NN ", " && ");
            string strFilterExp1 = CustomCriteriaToLinqWhereParser.Process(CriteriaOperator.Parse(strFilterExpre1) as CriteriaOperator);
            string strFilterExp2 = strTemp2;
            arrReturn = new string[] { strFilterExp1, strFilterExp2 };
            return arrReturn;
        }

    }
}