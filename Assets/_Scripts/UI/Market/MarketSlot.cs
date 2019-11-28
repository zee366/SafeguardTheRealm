using Events;
using UnityEngine;
using UnityEngine.UI;

public class MarketSlot : MonoBehaviour {

    public                   MarketSlotEvent onTryBuying;
    [SerializeField] private Color[]         rarityColor;
    private                  MarketProduct   _product;

    private Text  _priceText;
    private Image _image;


    private void Awake() {
        _priceText = transform.Find("Price").GetComponent<Text>();
        _image     = transform.Find("Image").GetComponent<Image>();

        // Register self click event
        GetComponent<Button>().onClick.AddListener(SelfClicked);
    }


    /// <summary>
    /// Wraps button event to give reference to entire market slot
    /// </summary>
    private void SelfClicked() {
        Inventory inv = FindObjectOfType<Inventory>();
        if ( inv != null && !inv.IsFull() )
            onTryBuying?.Invoke(this);
    }


    public void Disable() { GetComponent<Button>().interactable = false; }

    public void Enable() { GetComponent<Button>().interactable = true; }


    public void SetProduct(MarketProduct product) {
        _product        = product;
        _priceText.text = _product.price.ToString();

        // Tower image
        Tower tower = _product.product.GetComponent<Tower>();
        if ( tower != null && tower.thumbnail != null ) {
            _image.sprite = tower.thumbnail;
        }

        // Apply tint to self for now
        GetComponent<Image>().color = rarityColor[_product.rarity];
    }


    public MarketProduct GetProduct() { return _product; }

}