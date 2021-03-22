using UnityEngine;

public class PlayerHUD : MonoBehaviour {
    [SerializeField] GameObject playerObject;
    [SerializeField] Texture fullHeart;
    [SerializeField] Texture emptyHeart;

    PlayerInventory pinv;

    void Start() {
        pinv = playerObject.GetComponent<PlayerInventory>();
    }

    void OnGUI() {
        for (int i = 0; i < pinv.GetMaxHealth(); i++) {
            // draw empty heart
            GUI.DrawTexture(new Rect(10 + 80*i, 10, 96, 96), emptyHeart);
        }

        for (int i = 0; i< pinv.GetHealth(); i++) {
            // draw full heart
            GUI.DrawTexture(new Rect(10 + 80*i, 10, 96, 96), fullHeart);
        }
    }
}