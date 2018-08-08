using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinglePlayerOffice {
    interface IInterior {

        List<string> ExteriorIPLs { get; set; }

        void LoadInterior();
        void UnloadInterior();
        void LoadExterior();
        void UnloadExterior();

    }
}
