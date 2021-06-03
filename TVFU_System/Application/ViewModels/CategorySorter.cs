using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Application.ViewModels
{
    public class CategorySorter
    {
        public IEnumerable<ProductViewModel> Sort(List<ProductViewModel> list, string category, ColumnOrder order)
        {
            var temp = list;

            if (order == ColumnOrder.Null)
            {
                return temp.OrderBy(s => s.ProductNumber);
            }
            else
            {
                switch (category)
                {
                    case "productnumber":
                        if (order == ColumnOrder.Ascending)
                        {
                            return temp.OrderBy(s => s.ProductNumber);
                        }
                        else
                        {
                            return temp.OrderByDescending(s => s.ProductNumber);
                        }
                    case "description":
                        if (order == ColumnOrder.Ascending)
                        {
                            return temp.OrderBy(s => s.Description);
                        }
                        else
                        {
                            return temp.OrderByDescending(s => s.Description);
                        }
                    case "unitprice":
                        if (order == ColumnOrder.Ascending)
                        {
                            return temp.OrderBy(s => s.UnitPrice);
                        }
                        else
                        {
                            return temp.OrderByDescending(s => s.UnitPrice);
                        }
                    case "guidingprice":
                        if (order == ColumnOrder.Ascending)
                        {
                            return temp.OrderBy(s => s.GuidingPrice);
                        }
                        else
                        {
                            return temp.OrderByDescending(s => s.GuidingPrice);
                        }
                    case "totalstock":
                        if (order == ColumnOrder.Ascending)
                        {
                            return temp.OrderBy(s => s.TotalStock);
                        }
                        else
                        {
                            return temp.OrderByDescending(s => s.TotalStock);
                        }
                    case "blocked":
                        if (order == ColumnOrder.Ascending)
                        {
                            return temp.OrderBy(s => s.Blocked);
                        }
                        else
                        {
                            return temp.OrderByDescending(s => s.Blocked);
                        }
                    case "unitperpackage":
                        if (order == ColumnOrder.Ascending)
                        {
                            return temp.OrderBy(s => s.UnitPerPackage);
                        }
                        else
                        {
                            return temp.OrderByDescending(s => s.UnitPerPackage);
                        }
                    case "quantitydiscount":
                        if (order == ColumnOrder.Ascending)
                        {
                            return temp.OrderBy(s => s.QuantityDiscount);
                        }
                        else
                        {
                            return temp.OrderByDescending(s => s.QuantityDiscount);
                        }
                    case "purchasingmanager":
                        if (order == ColumnOrder.Ascending)
                        {
                            return temp.OrderBy(s => s.PurchasingManager);
                        }
                        else
                        {
                            return temp.OrderByDescending(s => s.PurchasingManager);
                        }
                    case "confirmeddeliverydate":
                        if (order == ColumnOrder.Ascending)
                        {
                            return temp.OrderBy(s => s.ConfirmedDeliveryDate);
                        }
                        else
                        {
                            return temp.OrderByDescending(s => s.ConfirmedDeliveryDate);
                        }
                    case "countryoforigin":
                        if (order == ColumnOrder.Ascending)
                        {
                            return temp.OrderBy(s => s.CountryOfOrigin);
                        }
                        else
                        {
                            return temp.OrderByDescending(s => s.CountryOfOrigin);
                        }
                    default:
                        throw new ArgumentNullException("Could not find the given category");
                }
            }
        }
    }
}