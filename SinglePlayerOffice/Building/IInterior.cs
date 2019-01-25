using System.Collections.Generic;

namespace SinglePlayerOffice.Buildings {

    internal interface IInterior {

        List<string> ExteriorIpLs { get; set; }

        void LoadInterior();

        void UnloadInterior();

        void LoadExterior();

        void UnloadExterior();

    }

}