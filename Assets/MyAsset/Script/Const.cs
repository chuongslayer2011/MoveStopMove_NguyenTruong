using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Const
{
    public static string ILDE_ANIM = "idle";
    public static string RUN_ANIM = "run";
    public static string ATTACK_ANIM = "attack";
    public static string DIE_ANIM = "die";
    public static string DANCE_ANIM = "dance";
    public static string WIN_ANIM = "win";
    public static string PLAYER_TAGNAME = "Player";
    public static string ENEMY_TAGNAME = "Enemy";
    public static string SHIELD_TAGNAME = "Shield";
    private static List<string> names = new List<string>()
    {
        "Victor",
        "Kenneth",
        "Parker",
        "Bobby",
        "James",
        "Walter",
        "Clark",
        "Cooper",
        "Perez",
        "Alexander",
        "Rogers",
        "Morris",
        "Campbell",
        "Garcia",
        "Carol",
        "Lawrence",
        "Hughes",
        "Beverly",
        "Anderson",
        "Daniel",
        "Adams",
        "Catherine",
        "Marie",
        "Bennett",
        "Carolyn",
        "Patterson",
        "Harris",
        "Bonnie",
        "Johnny",
        "Moore",
        "Marilyn",
        "Chris",
        "Powell",
        "Gonzales",
        "Wilson",
        "Lopez",
        "Murphy",
        "Gregory",
        "Martinez",
        "Carter",
        "Coleman",
        "Mitchell",
        "Carlos ",
        "Edwards",
        "Kathleen",
        "Howard",
        "Christine",
        "Torres",
        "Jose",
        "Christina",
        "Davis",
        "Virginia",
        "Paula",
        "Louise",
        "Amanda",
        "Judy",
        "Philip",
        "Coleman",
        "Campbell",
        "Hernandez",
        "Mitchell",
        "Lori",
        "Kathy",
        "Diane",
        "Emily",
        "Shirley",
        "Miller",
        "Rogers",
        "Powell",
        "Rivera",
        "Bonnie",
        "Craig",
        "Michelle",
        "Laura",
        "Flores",
        "Brooks",
        "Richardson",
        "Walker",
    };
    public static string GetRandomName()
    {
        return names[Random.Range(0, names.Count)];
    }
}
