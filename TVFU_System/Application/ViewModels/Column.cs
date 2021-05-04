namespace Application.ViewModels
{
    public enum ColumnOrder
    {
        Ascending,
        Descending,
        Null
    }
    public class Column
    {
        public string Name { get; set; }

        public ColumnOrder Order { get; set; } = ColumnOrder.Null;

        public Column(string name)
        {
            Name = name;
        }

        public void NextOrder()
        {
            switch (Order)
            {
                case ColumnOrder.Ascending:
                    Order = ColumnOrder.Descending;
                    break;
                case ColumnOrder.Descending:
                    Order = ColumnOrder.Null;
                    break;
                case ColumnOrder.Null:
                    Order = ColumnOrder.Ascending;
                    break;
            }
        }
    }
}
