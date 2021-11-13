namespace csf.main.models
{
    class Country
    {
        public string code { get; set; }
        public string name { get; set; }

        public Country(string code, string name)
        {
            this.code = code;
            this.name = name;
        }

        public override string ToString() { 
        
            return $"({code}, {name})";
        }
    }
}
