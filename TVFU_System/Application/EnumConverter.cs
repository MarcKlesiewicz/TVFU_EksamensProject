using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    /// <summary>
    /// Is used to convert a enum to a string
    /// </summary>
    public class EnumConverter
    {
        /// <summary>
        /// Converts SearchCategory to english, UI is displayed in danish and the data i written in English.
        /// </summary>
        /// <param name="category">has to enum 'SearchCategory'</param>
        /// <returns></returns>
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

                case "Vejledende Pris":
                    return "Guidingprice";

                case "Total Lager":
                    return "TotalStock";

                case "Spærret":
                    return "Blocked";

                case "Antal pr Kolli":
                    return "UnitPerPackage";

                case "Mængderabat":
                    return "QuantityDiscount";

                case "Indkøbskode":
                    return "PurchasingManager";

                case "Bekræftet Modtagelsesdato":
                    return "ConfirmedDeliveryDate";

                case "Oprindelsesland":
                    return "CountryOfOrigin";

                default:
                    return null;

            }
        }
    }
}
