using static MoldPowers;

/*
    value of each PlayerMold is a bitfield of what abilities they enable

    in order: (least significant first)
    bit 0: double jump
    bit 1: wall jump
    bit 2: wall stick
    bit 3: swim
    bit 4-31: unassigned
 */

public enum PlayerMold {
    ALL     = -1,
    NONE    = DOUBLE_JUMP | WALL_JUMP,
    STICK   = WALL_JUMP | WALL_STICK,
    ACID    = SWIM,
}
