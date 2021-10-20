using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Security;
using UnityEngine.UI;


public class CardPackUnlock : MonoBehaviour, IStoreListener
{

    private IStoreController controller;
    private IExtensionProvider extensions;

    private IAppleExtensions appleExtension;

    public string currentPack;

    public string currentSet;

    public GamePanel notEnoughPanel;
    public GamePanel areYouSurePanel;

    public GameObject areYouSureText;
    public GameObject notEnoughText;

    public string currenttype;


    public string productId;
    // Start is called before the first frame update
    void Start()
    {

        #if UNITY_IOS
            //GameObject.Find("Store-Restore-Purchases").GetComponent<Image>().enabled = true;
        #endif
        if(controller != null && extensions != null){
            return;
        }else{
            InitializeStore();
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("Coins").GetComponent<Text>().text = PlayerPrefs.GetInt("Coins", 0).ToString();
    }

    public void InitializeStore () {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct("pack_allpacks", ProductType.NonConsumable);
        builder.AddProduct("pack_animals", ProductType.NonConsumable);
        builder.AddProduct("pack_dogs", ProductType.NonConsumable);
        builder.AddProduct("pack_cats", ProductType.NonConsumable);
        builder.AddProduct("pack_sports", ProductType.NonConsumable);
        builder.AddProduct("pack_camping", ProductType.NonConsumable);
        builder.AddProduct("pack_playground", ProductType.NonConsumable);
        builder.AddProduct("pack_kitchen", ProductType.NonConsumable);
        builder.AddProduct("pack_office", ProductType.NonConsumable);
        builder.AddProduct("pack_furniture", ProductType.NonConsumable);
        builder.AddProduct("pack_transport", ProductType.NonConsumable);
        builder.AddProduct("pack_boats", ProductType.NonConsumable);
        builder.AddProduct("pack_cars", ProductType.NonConsumable);
        builder.AddProduct("set_animals", ProductType.NonConsumable);
        builder.AddProduct("set_outdoors", ProductType.NonConsumable);
        builder.AddProduct("set_house", ProductType.NonConsumable);
        builder.AddProduct("set_transportation", ProductType.NonConsumable);
        

        UnityPurchasing.Initialize (this, builder);
    }

     public void restorePurchasesButton(){

        appleExtension.RestoreTransactions (result => {
        if (result) {
            Debug.Log("Transactions Restored!");
        } else {
            Debug.Log("Restoration Failed!");
        }
    });
    }


    public void purchasePackButton() {

        productId = "pack_" + currentPack;
        Debug.Log(productId.Substring(productId.IndexOf("_")+1));

    controller.InitiatePurchase("pack_"+currentPack);


    }

    public void purchaseSetButton() {

        productId = "set_" + currentSet;
        Debug.Log(productId.Substring(productId.IndexOf("_")+1));

    controller.InitiatePurchase("set_"+currentSet);


    }

    // Called when Unity IAP is ready to make purchases.
    public void OnInitialized (IStoreController controller, IExtensionProvider extensions)
    {
        this.controller = controller;
        this.extensions = extensions;

        appleExtension = extensions.GetExtension<IAppleExtensions>();

    }

    /// Called when Unity IAP encounters an unrecoverable initialization error.
    /// Note that this will not be called if Internet is unavailable; Unity IAP
    /// will attempt initialization until it becomes available.
    
    public void OnInitializeFailed (InitializationFailureReason error)
    {
        Debug.Log("Initialization Failed");
    }


    /// Called when a purchase completes.
    ///
    /// May be called at any time after OnInitialized().

    public PurchaseProcessingResult ProcessPurchase (PurchaseEventArgs e)
    {

        bool validPurchase = true; // Presume valid for platforms with no R.V.

    // Unity IAP's validation logic is only included on these platforms.
#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE_OSX
    // Prepare the validator with the secrets we prepared in the Editor
    // obfuscation window.
    var validator = new CrossPlatformValidator(GooglePlayTangle.Data(),
        AppleTangle.Data(), Application.identifier);

    try {
        // On Google Play, result has a single product ID.
        // On Apple stores, receipts contain multiple products.

        var result = validator.Validate(e.purchasedProduct.receipt);
        // For informational purposes, we list the receipt(s)
        Debug.Log("Receipt is valid. Contents:");
        foreach (IPurchaseReceipt productReceipt in result) {
            Debug.Log(productReceipt.productID);
            Debug.Log(productReceipt.purchaseDate);
            Debug.Log(productReceipt.transactionID);

            productId = productReceipt.productID;

            GooglePlayReceipt google = productReceipt as GooglePlayReceipt;
    if (null != google) {
        // This is Google's Order ID.
        // Note that it is null when testing in the sandbox
        // because Google's sandbox does not provide Order IDs.
        Debug.Log(google.orderID);
        Debug.Log(google.purchaseState);
        Debug.Log(google.purchaseToken);
    }

    AppleInAppPurchaseReceipt apple = productReceipt as AppleInAppPurchaseReceipt;
    if (null != apple) {
        Debug.Log(apple.originalTransactionIdentifier);
        Debug.Log(apple.subscriptionExpirationDate);
        Debug.Log(apple.cancellationDate);
        Debug.Log(apple.quantity);
    }
        }
    } catch (IAPSecurityException) {
        Debug.Log("Invalid receipt, not unlocking content");
        validPurchase = false;
    }
#endif

    if (validPurchase) {
        
            PlayerPrefs.SetInt(productId.Substring(productId.IndexOf("_")+1), 1);
    }

        return PurchaseProcessingResult.Complete;
    }

    /// <summary>
    /// Called when a purchase fails.
    /// </summary>
    public void OnPurchaseFailed (Product i, PurchaseFailureReason p)
    {
        Debug.Log("Purchased Failed");
    }


    public void setCurrentType(string type){
        currenttype = type;

        areYouSureText.GetComponent<Text>().text = "ARE YOU SURE YOU WANT TO PURCHASE THIS " + type.ToUpper() + "?"; 
        notEnoughText.GetComponent<Text>().text = "YOU DO NOT HAVE ENOUGH COINS TO PURCHASE THIS " + type.ToUpper() + "!"; 
    }

    public void unlockCardPack(string type){
        if(type == "Pack" && PlayerPrefs.GetInt("Coins", 0) >= 300){
            areYouSurePanel.openPanelCall("");
        }else if(type == "Set" && PlayerPrefs.GetInt("Coins", 0) >= 800){
            areYouSurePanel.openPanelCall("");
        }else{
            notEnoughPanel.openPanelCall("");
        }
    }

    public void purchaseCardPackCoins(){

        if(currenttype == "Pack"){
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins", 0) - 300);
            PlayerPrefs.SetInt(currentPack, 1);
        }else{
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins", 0) - 800);
            PlayerPrefs.SetInt(currentSet, 1);
        }
        
    }
}
