using Events;
using SnapSystem;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Simple container of snap locations to keep track of different inventory slots and capacity
/// </summary>
public class Inventory : MonoBehaviour {

    private SnapLocation[] _locations;
    private int            _capacity; // The number of available slots
    private int            _size;     // The number of taken slots

    public UnityEvent onSizeChanged;
    public GUnityEvent onAdd;

    private void Awake() {
        // References are taken, and never changed at runtime in our case
        _locations = transform.GetComponentsInChildren<SnapLocation>();
        _capacity  = _locations.Length;

        // Register events to all of the child Locations
        foreach ( SnapLocation location in _locations ) {
            if ( !location.IsEmpty )
                _size++;// Starting size while testing
            location.onContentChange.AddListener(OnLocationContentChanged);
        }
    }


    /// <summary>
    /// Try to add to the inventory
    /// </summary>
    /// <param name="toAdd"></param>
    public bool Add(GameObject toAdd) {
        if ( IsFull() ) return false;

        // Find first empty, and add to it
        foreach ( SnapLocation location in _locations ) {
            if ( location.IsEmpty ) {
                location.ReplaceObject(toAdd);
                onAdd.Invoke(toAdd);
                return true;
            }
        }

        return false;
    }


    public int GetCapacity() { return _capacity; }

    public bool IsFull() { return _size >= _capacity; }


    /// <summary>
    /// Change state based on child event to manipulate
    /// </summary>
    /// <param name="location">The SnapLocation that triggered the event, and has changed</param>
    private void OnLocationContentChanged(SnapLocation location) {
        if ( location.IsEmpty )
            _size--;
        else
            _size++;

        onSizeChanged?.Invoke();
    }


    public int GetSize() { return _size; }
}