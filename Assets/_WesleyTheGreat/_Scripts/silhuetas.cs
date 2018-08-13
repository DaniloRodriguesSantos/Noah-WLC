using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class silhuetas : MonoBehaviour {

    Transform thisT;
    //Vector3 scale;
    public GameObject pontoVirar;
    public GameObject pontoDesaparecer;
    SpriteRenderer thisSpriteRenderer;

    bool estaAndando;
    public bool iniciarVirado;

    public float velocidade;

    // Use this for initialization
    void Awake () {

        thisT = GetComponent<Transform>();
        thisSpriteRenderer = GetComponent<SpriteRenderer>();
        
    }
    private void Start()
    {
        estaAndando = true;
        if (iniciarVirado)
        {
            virar();
        }
    }

    // Update is called once per frame
    void Update () {


        if (estaAndando)
        {
            thisT.Translate(velocidade * Time.deltaTime, 0, 0);
        }
        else
        {
            thisT.Translate(0, 0, 0);
        }
	}



    public void ir_e_voltar()
    {
       
    }
    public void ir_e_desaparecer()
    {

    }
    public void virar()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        velocidade *= -1;
    }
    public void desaparecer()
    {
        thisSpriteRenderer.enabled = false;
        estaAndando = false;
        StartCoroutine(coroutine());
    }
    IEnumerator coroutine()
    {
        yield return new WaitForSeconds(10f);
        thisSpriteRenderer.enabled = true;
        estaAndando = true;
        virar();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == pontoVirar.name)
        {
            virar();
        }
        if (collision.gameObject.name == pontoDesaparecer.name)
        {
            desaparecer();
        }
    }
}
