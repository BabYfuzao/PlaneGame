using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : MonoBehaviour
{
    public GameObject weaponPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject weapon = Instantiate(weaponPrefab, collision.gameObject.transform);
            Player player = collision.gameObject.GetComponent<Player>();
            Destroy(player.weapon.gameObject);
            player.weapon = weapon.GetComponent<WeaponBase>();
            //GameController.instance.WeaponIconDisplay(player.weapon.weaponID);
            Destroy(gameObject);
        }
    }
}
