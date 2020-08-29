namespace Lord.Core {

    public class TaskNode : Node {
        /* Method signature for the action. */
        public delegate NodeStates ActionNodeDelegate();

        /* The delegate that is called to evaluate this node */
        private ActionNodeDelegate m_action;

        /* Because this node contains no logic itself,
         * the logic must be passed in in the form of 
         * a delegate. As the signature states, the action
         * needs to return a NodeStates enum */
        public TaskNode(ActionNodeDelegate action, string nodeName = "<node>") {
            m_action = action;
            this.nodeName = nodeName;
        }

        /* Evaluates the node using the passed in delegate and 
         * reports the resulting state as appropriate */
        public override NodeStates Evaluate() {
            switch (m_action()) {
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