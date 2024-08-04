using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class chat_playerlist_switch : MonoBehaviour
{

    public GameObject player_list;
    public GameObject chat;
    public int current_list = 0;

    public TMP_Text button_text;
    public string[] button_strings;

    public void switch_to_playerlist()
    {
        if (current_list == 0)
        {
            player_list.SetActive(false);
            chat.SetActive(true);

            button_text.text = button_strings[1];

            current_list = 1;
        }
        else if (current_list == 1)
        {
            player_list.SetActive(true);
            chat.SetActive(false);

            button_text.text = button_strings[0];

            current_list = 0;
        }
    }
}
