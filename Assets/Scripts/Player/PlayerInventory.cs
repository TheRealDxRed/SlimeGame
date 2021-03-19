using System;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {
    [SerializeField] bool hasDoubleJump, hasWallJump;

    int currentHealth, maxHealth, healthUpgrades, maxHealthUpgrades;

    PlayerMold oldMold = PlayerMold.NONE;
    [SerializeField] PlayerMold mold = PlayerMold.NONE;

    //TODO figure out item names

    void Update() {
        if (mold != oldMold) {
            SetMold(mold);
        }
    }

    void SetMold(PlayerMold newMold) {
        mold = newMold;
    }

    PlayerMold GetMold() {
        return mold;
    }
    
    public bool CanDoubleJump() {
        return (hasDoubleJump && ((int)mold & MoldPowers.DOUBLE_JUMP) != 0);
    }

    public bool CanWallJump() {
        return (hasWallJump && ((int)mold & MoldPowers.WALL_JUMP) != 0);
    }

    public bool CanWallStick() {
        return (((int)mold & MoldPowers.WALL_STICK) != 0);
    }

    public bool CanSwim() {
        return (((int)mold & MoldPowers.SWIM) != 0);
    }
}
