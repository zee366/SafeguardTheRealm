using Events;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handle events and display of a single button for a product
/// </summary>
public class MarketSlot : MonoBehaviour {

    public                   MarketSlotEvent onTryBuying;
    [SerializeField] private Color[]         rarityColor;
    private                  MarketProduct   _product;

    private Text  _priceText;
    private Image _image;
    private Text _attack;
    private Text _speed;
    [SerializeField] private GameObject _stats;
    [SerializeField] private GameObject _support;
    

    private void Awake() {
        _support.SetActive(false);
        _stats.SetActive(false);
        _priceText = transform.Find("Price").GetComponent<Text>();
        _image     = transform.Find("Image").GetComponent<Image>();
        _attack = transform.Find("Stats").Find("Attack").Find("Value").GetComponent<Text>();
        _speed = transform.Find("Stats").Find("Speed").Find("Value").GetComponent<Text>();
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


    /// <summary>
    /// Receiving a product data struct to display info in template
    /// </summary>
    /// <param name="product"></param>
    public void SetProduct(MarketProduct product) {
        _product        = product;
        _priceText.text = _product.price.ToString();
        _attack.text = product.product.attack_descriptor.ToString();
        _speed.text = product.product.speed_descriptor.ToString();
        if ( _attack.text == "Support" || _speed.text == "Support" ) {
            _support.SetActive(true);
            _stats.SetActive(false);
        } else {
            _stats.SetActive(true);
            _support.SetActive(false);
        }
        // Tower image
        Tower tower = _product.product;
        if ( tower != null && tower.thumbnail != null ) {
            _image.sprite = tower.thumbnail;
        }

        // Apply tint to self for now
        GetComponent<Image>().color = rarityColor[_product.rarity];
    }

    /// <summary>
    /// Get current displaying product's struct
    /// </summary>
    /// <returns></returns>
    public MarketProduct GetProduct() { return _product; }

}