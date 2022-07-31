using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePlayerLook : MonoBehaviour
{
    Color originalColor;
    public Text numberText;
    float timer = 0, willDie = 1;
    int number;
    bool destroyObject = true, repeat = true;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            float xScale = collision.gameObject.transform.localScale.x, yScale = collision.gameObject.transform.localScale.y, zScale = collision.gameObject.transform.localScale.z;
            if(numberText != null)
                number = int.Parse(numberText.text);
            if(gameObject.tag == "Thicker")
            {
                xScale += (float) number / 1000;
                zScale += (float) number / 1000;
                willDie++;
            }
            else if(gameObject.tag == "Thinner")
            {
                xScale += (float)number / 1000;
                zScale += (float)number / 1000;
                willDie--;
            }
            else if (gameObject.tag == "Taller")
            {
                yScale += (float)number / 1000;
                willDie++;

            }
            else if (gameObject.tag == "Smaller")
            {
                yScale += (float)number / 1000;
                willDie--;
            }
            else if(gameObject.tag == "Low Obstacle")
            {
                destroyObject = false;
                originalColor = collision.gameObject.GetComponent<MeshRenderer>().material.color;
                willDie -= 0.1f;
                xScale -= 0.01f;
                yScale -= 0.01f;
                zScale -= 0.01f;
                StartCoroutine(ChangeColor(collision.gameObject, originalColor));
                collision.gameObject.transform.Translate(Vector3.forward * (-1));
            }
            if (willDie > 0)
                collision.gameObject.transform.localScale = new Vector3(xScale, yScale, zScale);
            else
            {
                collision.gameObject.transform.localScale = new Vector3(0.5f, 0.246f, 0.5f);
                Debug.Log("Game Over ");
            }
            if(destroyObject)
                Destroy(gameObject);
        }
    }

    IEnumerator ChangeColor(GameObject player, Color color)
    {
        Debug.Log("entered");
        player.GetComponent<MeshRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        player.GetComponent<MeshRenderer>().material.color = color;
        gameObject.SetActive(false);
        gameObject.SetActive(true);
    }
}
