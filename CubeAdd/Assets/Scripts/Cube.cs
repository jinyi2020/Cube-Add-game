using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Cube : MonoBehaviour
{
    public int cubeValue { get; private set; }
    public int id = 0;
    public int valueMaxPower = 5;
    private float moveRange = 6;
    private int forwardForce = 75;
    private int upForce = 10;
    private float mouseToScreenZ = 8;

    public List<TextMeshProUGUI> valueText;


    private bool Pushed = false;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        id = GameObject.Find("Game Manager").GetComponent<GameManager>().cubeCount;
        GameObject.Find("Game Manager").GetComponent<GameManager>().cubeCount++;

        int maxPower = Mathf.Min(valueMaxPower, id);
        cubeValue = (int)Mathf.Pow(2, Random.Range(0, maxPower + 1));

        Debug.Log("Creat cube");
        Debug.Log("cube id: " + id);
        Debug.Log("cube value: " + cubeValue);
        // Debug.Log("cube pos: " + transform.position.x + " " + transform.position.y + " " + transform.position.z );
    }

    // Update is called once per frame
    void Update()
    {
        if (!Pushed)
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 mouseTrans = Camera.main.ScreenToWorldPoint( new Vector3 (mousePos.x, mousePos.y, mouseToScreenZ));
            // Debug.Log("Input mouse: " + Input.mousePosition);
            // Debug.Log("mouse: " +  mouseTrans);
            float mouseZ = mouseTrans.z;
            float posZ = Mathf.Min(Mathf.Abs(mouseZ), moveRange);
            if (mouseZ < 0) { posZ *= -1; }
            transform.position= new Vector3(transform.position.x, transform.position.y, posZ);

        }

        foreach (TextMeshProUGUI valueT in valueText) { valueT.text = cubeValue.ToString(); }
        // Debug.Log("cube pos: " + transform.position.x + " " + transform.position.y + " " + transform.position.z);

    }

    private void OnMouseDown()
    {
        if (!Pushed)
        {
            // Debug.Log("Forward direction: " + transform.forward);
            rb.AddForce(transform.forward * forwardForce, ForceMode.Impulse);
            Pushed = true;
            GameObject.Find("Game Manager").GetComponent<GameManager>().timerStart = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collideCube = collision.gameObject;
        if (collideCube.tag == "Cube" && collideCube.GetComponent<Cube>().cubeValue == cubeValue)
        {
            GameObject.Find("Game Manager").GetComponent<GameManager>().score += cubeValue;
            cubeValue *= 2;
            Destroy(collideCube);
            // rb.AddForce(Vector3.up * upForce, ForceMode.Impulse);
            rb.velocity = new Vector3(- upForce/2, upForce, rb.velocity.z);
        }
    }

   




}
