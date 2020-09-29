using System.Collections.Generic;

namespace Lord.Core {

    public class TaskContextNode : Node {
        /* Method signature for the action. */
        public delegate NodeStates ActionNodeDelegate(Context context);

        /* The delegate that is called to evaluate this node */
        private ActionNodeDelegate m_action;
        private Context context;

        /* Because this node contains no logic itself,
         * the logic must be passed in in the form of 
         * a delegate. As the signature states, the action
         * needs to return a NodeStates enum */
        public TaskContextNode(ActionNodeDelegate action, Context context, string nodeName = "<node>") {
            m_action = action;
            this.nodeName = nodeName;
            this.context = context;
        }

        /* Evaluates the node using the passed in delegate and 
         * reports the resulting state as appropriate */
        public override NodeStates Evaluate() {
            switch (m_action(context)) {
                case NodeStates.SUCCESS:
                    nodeState = NodeStates.SUCCESS;
                    return nodeState;
                case NodeStates.FAILURE:
                    nodeState = NodeStates.FAILURE;
                    return nodeState;
                case NodeStates.RUNNING:
                    nodeState = NodeStates.RUNNING;
                    return nodeState;
                default:
                    nodeState = NodeStates.FAILURE;
                    return nodeState;
            }
        }

    }
}