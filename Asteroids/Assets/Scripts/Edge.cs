using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour
{
    [SerializeField] Type type;

    Dictionary<GameObject, CooldownEntry> cooldownList = new Dictionary<GameObject, CooldownEntry>();
    float buffer = .1f;
    float cooldownTime = .03f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("Ship") || collision.CompareTag("Bullet") || collision.CompareTag("Asteroid")) && !cooldownList.ContainsKey(collision.gameObject))
        {
            if(collision.CompareTag("Bullet"))
            {
                collision.gameObject.GetComponent<Bullet>().HasWrapped();
            }
            if (type == Type.Horizontal)
            {
                float x = collision.transform.position.x;
                collision.transform.position = new Vector2(-x + (x > 0 ? buffer : -buffer), collision.transform.position.y);
                cooldownList.Add(collision.gameObject, new CooldownEntry(cooldownTime));
            }
            else if (type == Type.Vertical)
            {
                float y = collision.transform.position.y;
                collision.transform.position = new Vector2(collision.transform.position.x, -y + (y > 0 ? buffer : -buffer));
                cooldownList.Add(collision.gameObject, new CooldownEntry(cooldownTime));
            }
        }
    }

    private void Update()
    {
        List<GameObject> toRemove = new List<GameObject>();
        foreach(KeyValuePair<GameObject, CooldownEntry> kvp in cooldownList)
        {
            kvp.Value.cooldown -= Time.deltaTime;
            if(kvp.Value.cooldown <= 0)
            {
                toRemove.Add(kvp.Key);
            }
        }
        foreach(GameObject o in toRemove)
        {
            //print(o.name + " was removed");
            cooldownList.Remove(o);
        }
    }
}

class CooldownEntry
{
    public float cooldown;
    public CooldownEntry(float value) { cooldown = value; }
}

public enum Type
{
    Vertical,
    Horizontal
}
