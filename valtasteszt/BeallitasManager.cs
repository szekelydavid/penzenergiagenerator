using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterProject
{
    public static class BeallitasManager
    {
        private static readonly Dictionary<string,int> _beallitas = new Dictionary<string, int>();

        public static void InitializeDefaultBeallitas()
        {
            (string, int)[] alap = new (string, int)[]
            {
               
                ("pont",10 ),
                ("ido",45),
                ("kerdes",12),
                ("nyelv", 0),
                ("vmm",0),
          
            };

            foreach (var x in alap)
            {
                _beallitas.Add(x.Item1, x.Item2);
            }

        }

        public static void EditBeallitas(string beallitas, int ujertek)
        {
            if (_beallitas.ContainsKey(beallitas))
                _beallitas[beallitas] = ujertek;
        }

        public static int GetBeallitas(string beallitas)
        {
            if (_beallitas.TryGetValue(beallitas, out int value)) return value;
            throw new System.Exception("Nincs");
        }

    }


 


}

 

