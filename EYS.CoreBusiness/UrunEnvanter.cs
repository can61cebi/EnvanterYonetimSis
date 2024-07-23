using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EYS.CoreBusiness
{
    public class UrunEnvanter
    {
        public int UrunId { get; set; }

        [JsonIgnore]
        public Urun? Urun { get; set; }
        public int EnvanterId { get; set; }

        [JsonIgnore]
        public Envanter? Envanter { get; set; }
        public int EnvanterAdeti { get; set; }
    }
}
