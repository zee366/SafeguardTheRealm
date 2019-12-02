using System;
using UnityEngine;
using UnityEngine.Events;

namespace SnapSystem {
    public class SnapManager : MonoBehaviour {

        [SerializeField] private LayerMask  selectableLayerMask;
        [SerializeField] private GameObject inBetweenSelectionVisual;

        public UnityEvent onSelect;
        public UnityEvent onUndefinedSelection;
        public UnityEvent onTransfer;
        public UnityEvent onCancel;

        private Camera        _cam;
        private SnapLocation  _currentTarget;
        private SnapLocation  _lastSelected;
        private Transform     _inBetweenRef;
        private Player        _player;
        private Inventory     _inventory;
        private TowerUpgrader _upgrader;
        private bool          _locked = false;
        private int           _totalTowerCount;


        private void Awake() {
            _cam = Camera.main;

            // Create between selection panel right away
            _inBetweenRef = Instantiate(inBetweenSelectionVisual, transform.position, Quaternion.identity, transform).transform;
            _inBetweenRef.gameObject.SetActive(false);

            // Get a reference to player/inventory to be able to query for level and prevent placement accordingly.
            _player    = FindObjectOfType<Player>();
            _inventory = FindObjectOfType<Inventory>();
            _upgrader  = FindObjectOfType<TowerUpgrader>();

            _inventory.onAdd.AddListener(gObj => { _totalTowerCount++; });
            _upgrader.onUpgrade.AddListener(() => { _totalTowerCount -= 3; });
        }


        private void Start() {
            _totalTowerCount = _inventory.GetSize(); // Initial state of inventory (not counting on grind tho)
        }


        private void Update() {
            // Entire Grid system is locked
            if ( _locked ) return;

            // Check Mouse position for available action on Grid
            CheckCurrentTarget(Input.mousePosition);

            // Left click
            if ( Input.GetMouseButtonDown(0) ) CheckLocationSelection();

            // Player may cancel an action with the Esc. Button or the right mouse button right now
            if ( Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape) ) CancelAction();
        }


        public void LockGrid() { _locked = true; }

        public void UnlockGrid() { _locked = false; }

        public bool IsLocked() { return _locked; }


        /// <summary>
        /// Cancel current movement action
        /// </summary>
        public void CancelAction() {
            if ( _lastSelected != null ) {
                _lastSelected.IsSelected = false;
                _lastSelected            = null;
                onCancel?.Invoke();
            }
        }


        /// <summary>
        /// Mouse movement check for target.
        /// Used for location highlighting and potential tile information gathering
        /// </summary>
        private void CheckCurrentTarget(Vector3 mousePos) {
            RaycastHit hit;
            Ray        ray = _cam.ScreenPointToRay(mousePos);

            if ( Physics.Raycast(ray, out hit, Mathf.Infinity, selectableLayerMask) ) {
                // Make sure the selectable has associated script
                SnapLocation newRef = hit.transform.GetComponent<SnapLocation>();

                // Change state of older one
                if ( _currentTarget != null && _currentTarget != newRef ) _currentTarget.IsLit = false;

                _currentTarget = newRef;

                // Make sure the new reference was indeed a SnapLocation.. maybe not
                if ( _currentTarget == null ) return;

                // Here the current target is a valid SnapLocation
                _currentTarget.IsLit = true;
            } else {
                // We didn't hit this layer
                if ( _currentTarget != null ) {
                    _currentTarget.IsLit = false;
                    _currentTarget       = null;
                }
            }

            UpdateInBetween();
        }


        /// <summary>
        /// Maybe update position, scale & rotation of the inBetween panel
        /// </summary>
        private void UpdateInBetween() {
            if ( !HasOneSelected() || _currentTarget == null ) {
                _inBetweenRef.gameObject.SetActive(false);
                return;
            }

            // Position it in the middle, and as a flat Quad, change orientation.
            Vector3 lastPos = _lastSelected.transform.position;
            Vector3 diff    = _currentTarget.transform.position - lastPos;

            _inBetweenRef.position   = lastPos + (diff / 2) + Vector3.one / 2;
            _inBetweenRef.localScale = Vector3.one * diff.magnitude;

            Vector3 norm = Vector3.Cross(Vector3.up, diff).normalized;

            if ( norm.magnitude > 0 ) {
                // Rotate for height difference
                float   angle     = Mathf.Rad2Deg * -Mathf.Asin(diff.y / diff.magnitude);
                Vector3 rotatedUp = Quaternion.AngleAxis(angle, norm) * Vector3.up;
                _inBetweenRef.rotation = Quaternion.LookRotation(norm, rotatedUp);
            }

            _inBetweenRef.gameObject.SetActive(true);
        }


        /// <summary>
        /// Selectable selection logic
        /// </summary>
        private void CheckLocationSelection() {
            if ( _currentTarget == null ) {
                onUndefinedSelection?.Invoke();
                return;
            }

            // If we already have a selection => MoveAction
            if ( HasOneSelected() && !_lastSelected.IsEmpty && _currentTarget.IsEmpty ) {
                TransferAction(_currentTarget);
                return;
            }

            // Check if we can select it..
            if ( !_currentTarget.IsEmpty ) {
                if ( HasOneSelected() ) _lastSelected.IsSelected = false;

                _lastSelected            = _currentTarget;
                _lastSelected.IsSelected = true;
                onSelect?.Invoke();
            } else {
                onUndefinedSelection?.Invoke();
            }
        }


        /// <summary>
        /// Moving objects from one tile to another
        /// </summary>
        /// <param name="moveToTarget"></param>
        private void TransferAction(SnapLocation moveToTarget) {
            int towersOnGround = _totalTowerCount - _inventory.GetSize();

            // Check if player have already placed all his towers
            // Still allow transfers inside the inventory itself.
            // Player can place level + 2 amount of towers.
            if ( moveToTarget.transform.GetComponentInParent<Inventory>() == null
                 && towersOnGround >= _player.GetPlayerLevel() + 2 ) {
                CancelAction();
                return;
            }

            _currentTarget.ReplaceObject(_lastSelected.GetObject());

            // Simply for good practice... this will trigger some event as well
            _lastSelected.Clear();

            _lastSelected.IsSelected = false;
            _lastSelected            = null;
            onTransfer?.Invoke();
        }


        /// <summary>
        /// Defines if one location is already selected to do an action with it.
        /// </summary>
        /// <returns></returns>
        private bool HasOneSelected() { return _lastSelected != null; }

    }
}