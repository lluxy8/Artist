namespace Web.Models
{

    public class DataTableViewModel
    {
        public string TableId { get; set; }
        public List<string> Columns { get; set; }
        public bool IsOrderable { get; set; }
    }
}
