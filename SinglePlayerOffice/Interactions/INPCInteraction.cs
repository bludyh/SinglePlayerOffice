using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;

namespace SinglePlayerOffice.Interactions {
    interface INPCInteraction {

        Ped Ped { get; set; }
        bool IsGreeted { get; }
        int ConversationState { get; set; }

    }
}
