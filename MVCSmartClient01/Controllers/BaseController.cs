namespace MVCSmartClient01.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using ApiHelper.Response;
    using MVCSmartClient01.Models;
    using System.Text;

    public abstract class BaseController : Controller
    {
        public string GenerateKataKunci()
        {
            String myImageBase = Guid.NewGuid().ToString();
            string[] arrMyImageBase = myImageBase.Split('-');
            return arrMyImageBase[1 + (DateTime.Now.Minute % 3)];
        }
        public string MaskPasskey(string strInput, bool IsMasked)
        {
            var strBuilder = new StringBuilder();
            int intCharNew = 0;
            foreach (char c in strInput)
            {
                if (IsMasked)
                {
                    intCharNew = ((int)c) + 5;
                }
                else
                {
                    intCharNew = ((int)c) - 5;
                }
                strBuilder.Append((char)intCharNew);
            }
            return strBuilder.ToString();
        }
        protected void AddResponseErrorsToModelState(ApiResponse response)
        {
            var errors = response.ErrorState.ModelState;
            if (errors == null)
            {
                return;
            }

            foreach (var error in errors)
            {
                foreach (var entry in 
                    from entry in ModelState
                    let matchSuffix = string.Concat(".", entry.Key)
                    where error.Key.EndsWith(matchSuffix)
                    select entry)
                {
                    ModelState.AddModelError(entry.Key, error.Value[0]);
                }
            }
        }
    }
}