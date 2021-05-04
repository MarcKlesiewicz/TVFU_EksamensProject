using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class EnumConverter
    {
        public string Convert(string category)
        {

            switch (category)
            {
                case "Beskrivelse":
                    return "Description";

                case "Nummer":
                    return "ProductNumber";

                case "Enhedspris":
                    return "Unitprice";

                case "Vejledende_pris":
                    return "Guidingprice";

                case "Total_lager":
                    return "TotalStock";

                case "Spærret":
                    return "Blocked";

                case "Antal_pr_kolli":
                    return "UnitPerPackage";

                case "Mængderabat":
                    return "QuantityDiscount";

                case "Indkøbskode":
                    return "PurchasingManager";

                case "Bekræftet_modtagelsesdatto":
                    return "ConfirmedDeliveryDate";

                case "Oprindelsesland":
                    return "CountryOfOrigin";

                default:
                    return null;

            }
        }
    }
}
