using UnityEngine;

public class PlayerInventory : MonoBehaviour {
    bool hasDoubleJump, hasWallJump, hasWallStick;
    int currentHealth, maxHealth, healthUpgrades, maxHealthUpgrades;

    PlayerMold mold;

    //TODO figure out item names

    void SetMold(PlayerMold newMold) {
        mold = newMold;

        switch (mold) {
            case PlayerMold.STICK:
                hasWallStick = true;
                break;
            case PlayerMold.ACID:
                hasWallStick = false;
                break;
            default: // case PlayerMold.NONE:
                hasWallStick = false;
        }
    }
}
