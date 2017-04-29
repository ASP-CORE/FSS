namespace FSS.Data.Models
{
    using FSS.Data.Shared.Models;

    public class Setting : BaseModel<int>
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }
}
