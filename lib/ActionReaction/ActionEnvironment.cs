/******************************************************************\
 *      Class Name:     ActionEnvironment
 *      Written By:     James Rising
 *      Copyright:      2009, Virsona, Inc.
 *                      GNU Lesser General Public License, Ver. 3
 *                      (see license.txt and license.lesser.txt)
 *      -----------------------------------------------------------
 * This file is part of Plugger Base and is free software: you can
 * redistribute it and/or modify it under the terms of the GNU
 * Lesser General Public License as published by the Free Software
 * Foundation, either version 3 of the License, or (at your option)
 * any later version.
 *
 * Plugger Base is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public
 * License along with Plugger Base.  If not, see
 * <http://www.gnu.org/licenses/>.
 *      -----------------------------------------------------------
 * The shared state available to all actions
\******************************************************************/
using System.Collections.Generic;
using ActionReaction.Actions;
using ActionReaction.Interfaces;
using ActionReaction.Evaluations;

namespace ActionReaction
{
    // The plugin environment combines a globally accessible environment
    public class ActionEnvironment
    {
        protected IMessageReceiver receiver;

        // ICallables and IHandlers, keyed by named result types (or "" for unnamed)
        protected Dictionary<string, List<IAction>> actions;

        public ActionEnvironment(IMessageReceiver receiver)
        {
            this.receiver = receiver;
            actions = new Dictionary<string, List<IAction>>();
        }

        public IDictionary<string, List<IAction>> Actions
        {
            get
            {
                return actions;
            }
        }

        public void AddAction(IAction action)
        {
            string resultname = action.Output.Name;
            // Add this to the possible actions to produce this
            GetNamedActions(resultname).Add(action);
        }

        public object ImmediateConvertTo(object args, IArgumentType resultType, int maxlevel, int time)
        {
            return QueueArena.CallResult(CallableConvertTo(resultType, maxlevel), args, time, 0.0);
        }

        public ICallable CallableConvertTo(IArgumentType resultType, int maxlevel)
        {
            return new ActionConversion(this, resultType, new Dictionary<IAction,int>(), Aborter.NewAborter(maxlevel));
        }

        public List<IAction> GetNamedActions(string name)
        {
            if (string.IsNullOrEmpty(name))
                return actions[""];
            else
            {
                List<IAction> namedacts = null;
                if (!actions.TryGetValue(name, out namedacts))
                    actions[name] = namedacts = new List<IAction>();

                return namedacts;
            }
        }
    }
}
