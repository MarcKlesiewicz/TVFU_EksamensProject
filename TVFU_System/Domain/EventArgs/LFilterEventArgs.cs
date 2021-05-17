using System.Collections.Generic;

namespace Domain.EventArgs
{
    public class LFilterEventArgs : System.EventArgs
    {
        public string SearchCategory { get; set; }

        public string SearchInput { get; set; }

        public List<string> Filters { get; set; } = new List<string>();
    }
}
