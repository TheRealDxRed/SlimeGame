using System;
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
        for (int i = 0; i < Math.Max(pinv.GetHealth(), pinv.GetMaxHealth()); i++) {
            int j = i + 1;
            GUI.color = Color.white;

            if (j > pinv.GetMaxHealth())
                GUI.color = Color.green;

            GUI.DrawTexture(new Rect(10 + 64*i, 10, 64, 128), emptyHeart);

            if(j <= pinv.GetHealth())
                GUI.DrawTexture(new Rect(10 + 64*i, 10, 64, 128), fullHeart);
        }
    }
}