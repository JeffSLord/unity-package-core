namespace Lord.Core {
    public interface ISelectable {
        void Select(int option = 0);
        void Deselect();
    }
}