using System;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {
    int defaultMaxHealth = 5;

    [SerializeField] bool hasDoubleJump, hasWallJump;

    [SerializeField] int currentHealth, maxHealth, healthUpgrades, maxHealthUpgrades;

    [SerializeField] PlayerMold mold = PlayerMold.NONE;

    //TODO figure out item names

    void Start() {
        maxHealth = defaultMaxHealth;
    }

    public void SetMold(PlayerMold newMold) {
        mold = newMold;
    }

    PlayerMold GetMold() {
        return mold;
    }

    public void SetHealth(int newHealth) {
        if (newHealth > maxHealth) {
            currentHealth = maxHealth;
        } else if (newHealth < 0) {
            currentHealth = 0;
        } else
            currentHealth = newHealth;
    }

    public void SetHealthUpgrades(int newUpgrades) {
        healthUpgrades = newUpgrades;
    }
    
    public int GetHealth() => currentHealth;
    public int GetMaxHealth() => maxHealth;
    
    public bool CanDoubleJump() {
        return (hasDoubleJump && MoldContains(mold, MoldPowers.DOUBLE_JUMP));
    }

    public bool CanWallJump() {
        return (hasWallJump && MoldContains(mold, MoldPowers.WALL_JUMP));
    }

    public bool CanWallStick() {
        return (MoldContains(mold, MoldPowers.WALL_STICK));
    }

    public bool CanSwim() {
        return (MoldContains(mold, MoldPowers.SWIM));
    }

    // hacky code go brrr
    bool MoldContains(PlayerMold m, int p) => ((int)m & p) != 0;
}
