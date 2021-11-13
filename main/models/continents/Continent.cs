using System.Collections.Generic;

namespace csf.main.models
{
    class Continent
    {
        public string code { get; set; }
        public string name { get; set; }
        public List<Country> countries { get; set; }

        public Continent(string code, string name, List<Country> countries)
        {
            this.code = code;
            this.name = name;
            this.countries = countries;
        }

        public override string ToString()
        {

            return $"[{code}, {name}, {string.Join(",", countries)}]";
        }
    }
}
