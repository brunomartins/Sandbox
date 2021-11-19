namespace Core
{
    public class Sheet
    {
        public Sheet(string name, string[] headers, object[][] data)
        {
            Name = name;
            Header = headers;
            Data = data;
        }
        public string Name { get; internal set; }

        public string[] Header { get; internal set; }

        public object[][] Data { get; internal set; }
    }
}
