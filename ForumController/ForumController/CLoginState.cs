using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForumController
{
    public class CLoginState
    {
        bool _stateLog;
        public bool StateLog
        {
            get
            {
                return _stateLog;
            }
            set
            {
                _stateLog = value;
            }
        }
        public CLoginState(bool state)
        {
            StateLog = state;
        }
    }
}
