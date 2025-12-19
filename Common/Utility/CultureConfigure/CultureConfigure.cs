using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LuxShop.CommonUtility.CultureConfigure
{
    public class CultureConfigure
    {
        public static void ConfigureService(IServiceCollection services, IConfiguration configuration)

        {
            // setting the application culture to fa-IR with yyyy/MM/dd short date pattern.
            

            //var persianCulture = new PersianCulture();
            //persianCulture.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd";
            //persianCulture.DateTimeFormat.LongDatePattern = "dddd d MMMM yyyy";
            //persianCulture.DateTimeFormat.AMDesignator = "صبح";
            //persianCulture.DateTimeFormat.PMDesignator = "عصر";
            //Thread.CurrentThread.CurrentCulture = persianCulture;
            //Thread.CurrentThread.CurrentUICulture = persianCulture;

            //CultureInfo.DefaultThreadCurrentCulture = CultureInfo.DefaultThreadCurrentUICulture
            //= PersianDateExtensionMethods.GetPersianCulture();

        }
    }
}
