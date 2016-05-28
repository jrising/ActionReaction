/******************************************************************\
 *      Class Name:     Testing
 *      Written By:     James.R
 *      Copyright:      Virsona Inc
 *      -----------------------------------------------------------
 * An interface for actions with unit tests
\******************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using ActionReaction.Actions;

namespace ActionReaction.Interfaces
{
    interface ITestableHandler : IAction
    {
        int TestCount { get; }
        Dictionary<string, object> GetTestArguments(int ii);
        object GetExpectedResult(int ii);
    }
}
