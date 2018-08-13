using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class conversa_SAM : MonoBehaviour {

    Audio_MainScript script_Audio_MainScript;

    public RectTransform rTransform;

    public GameObject muzzle_Inicial_NOAH;
    public Button[] btts_Dialogue_NOAH;

    public GameObject muzzle_Inicial_SAM;
    public Button[] btts_Dialogue_SAM;

    public GameObject muzzle_Conversa;


    int current_array_index;
    [Space(10)]
    [TextArea(5,2)]
    public string[] dialogos_SAM_DIA1_depoisEscola;

    [Space(10)]
    [TextArea(5, 2)]
    public string[] dialogos_SAM_DIA2_depoisEscola;

    [Space(10)]
    [TextArea(5, 2)]
    public string[] dialogos_SAM_DIA3_depoisEscola;

    public Text text3Pontinhos;
    public Button btt_Voltar;

    public bool conversa_Sam_1;
    public bool conversa_Sam_2;
    public bool conversa_Sam_3;

    // Use this for initialization
    void Awake () {
        script_Audio_MainScript = GameObject.Find("Main Camera").GetComponent<Audio_MainScript>();
        btts_Dialogue_NOAH = muzzle_Inicial_NOAH.GetComponentsInChildren<Button>();
        btts_Dialogue_SAM = muzzle_Inicial_SAM.GetComponentsInChildren<Button>();
    }
    private void Start()
    {
        text3Pontinhos.enabled = false;
    }

    // Update is called once per frame
    void Update () {
        if (conversa_Sam_1)
        {
            StartCoroutine(conversa_SAM_DIA1_depoisEscola());
            conversa_Sam_1 = false;
        }
        if (conversa_Sam_2)
        {
            StartCoroutine(conversa_SAM_DIA2_depoisEscola());
            conversa_Sam_2 = false;
        }
        if (conversa_Sam_3)
        {
            StartCoroutine(conversa_SAM_DIA3_depoisEscola());
            conversa_Sam_3 = false;
        }
        
    }

    #region conversa_1
    private void criarDialogo_SAM_DIA1_depoisEscola()
    {
        btts_Dialogue_SAM[0].gameObject.transform.parent = muzzle_Conversa.transform;
        btts_Dialogue_SAM[0].GetComponentInChildren<Text>().text = dialogos_SAM_DIA1_depoisEscola[current_array_index];
        btts_Dialogue_SAM = muzzle_Inicial_SAM.GetComponentsInChildren<Button>();

        rTransform.sizeDelta += new Vector2(0, 40);
        current_array_index++;
    }
    private void criarDialogo_NOAH_DIA1_depoisEscola()
    {
        btts_Dialogue_NOAH[0].gameObject.transform.parent = muzzle_Conversa.transform;
        btts_Dialogue_NOAH[0].GetComponentInChildren<Text>().text = dialogos_SAM_DIA1_depoisEscola[current_array_index];
        btts_Dialogue_NOAH = muzzle_Inicial_NOAH.GetComponentsInChildren<Button>();

        rTransform.sizeDelta += new Vector2(0, 40);
        current_array_index++;
    }
    IEnumerator conversa_SAM_DIA1_depoisEscola()
    {
        btt_Voltar.enabled = false;
        criarDialogo_SAM_DIA1_depoisEscola();//0
        text3Pontinhos.enabled = false;

        yield return new WaitForSeconds(0.5f);
        script_Audio_MainScript.tocar_Digitando();
        yield return new WaitForSeconds(4);
        criarDialogo_NOAH_DIA1_depoisEscola();//1

        yield return new WaitForSeconds(2);
        text3Pontinhos.enabled = true;

        yield return new WaitForSeconds(2);
        criarDialogo_SAM_DIA1_depoisEscola();//2
        text3Pontinhos.enabled = false;

        yield return new WaitForSeconds(0.5f);
        script_Audio_MainScript.tocar_Digitando();
        yield return new WaitForSeconds(4);
        criarDialogo_NOAH_DIA1_depoisEscola();//3

        yield return new WaitForSeconds(2);
        text3Pontinhos.enabled = true;

        yield return new WaitForSeconds(2);
        criarDialogo_SAM_DIA1_depoisEscola();//4
        text3Pontinhos.enabled = false;

        yield return new WaitForSeconds(2);
        text3Pontinhos.enabled = true;

        yield return new WaitForSeconds(2);
        criarDialogo_SAM_DIA1_depoisEscola();//5
        text3Pontinhos.enabled = false;

        yield return new WaitForSeconds(2);
        text3Pontinhos.enabled = true;

        yield return new WaitForSeconds(2);
        criarDialogo_SAM_DIA1_depoisEscola();//6
        text3Pontinhos.enabled = false;

        /*yield return new WaitForSeconds(2);
        text3Pontinhos.enabled = true;

        yield return new WaitForSeconds(2);
        criarDialogo_SAM_DIA1_depoisEscola();//7*/

        yield return new WaitForSeconds(0.5f);
        script_Audio_MainScript.tocar_Digitando();
        yield return new WaitForSeconds(4);
        criarDialogo_NOAH_DIA1_depoisEscola();//7

        yield return new WaitForSeconds(2);
        text3Pontinhos.enabled = true;

        yield return new WaitForSeconds(2);
        criarDialogo_SAM_DIA1_depoisEscola();//8
        text3Pontinhos.enabled = false;

        yield return new WaitForSeconds(0.5f);
        script_Audio_MainScript.tocar_Digitando();
        yield return new WaitForSeconds(4);
        criarDialogo_NOAH_DIA1_depoisEscola();//9

        yield return new WaitForSeconds(2);
        text3Pontinhos.enabled = true;

        yield return new WaitForSeconds(2);
        criarDialogo_SAM_DIA1_depoisEscola();//10


        btt_Voltar.enabled = true;
        text3Pontinhos.enabled = false;
        current_array_index = 0;

    }
    #endregion

    #region conversa_2
    private void criarDialogo_SAM_DIA2_depoisEscola()
    {
        btts_Dialogue_SAM[0].gameObject.transform.parent = muzzle_Conversa.transform;
        btts_Dialogue_SAM[0].GetComponentInChildren<Text>().text = dialogos_SAM_DIA2_depoisEscola[current_array_index];
        btts_Dialogue_SAM = muzzle_Inicial_SAM.GetComponentsInChildren<Button>();

        rTransform.sizeDelta += new Vector2(0, 40);
        current_array_index++;
    }
    private void criarDialogo_NOAH_DIA2_depoisEscola()
    {
        btts_Dialogue_NOAH[0].gameObject.transform.parent = muzzle_Conversa.transform;
        btts_Dialogue_NOAH[0].GetComponentInChildren<Text>().text = dialogos_SAM_DIA2_depoisEscola[current_array_index];
        btts_Dialogue_NOAH = muzzle_Inicial_NOAH.GetComponentsInChildren<Button>();

        rTransform.sizeDelta += new Vector2(0, 40);
        current_array_index++;
    }
    IEnumerator conversa_SAM_DIA2_depoisEscola()
    {
        btt_Voltar.enabled = false;
        criarDialogo_SAM_DIA2_depoisEscola();

        yield return new WaitForSeconds(0.5f);
        script_Audio_MainScript.tocar_Digitando();
        yield return new WaitForSeconds(4);
        criarDialogo_NOAH_DIA2_depoisEscola();

        yield return new WaitForSeconds(0.5f);
        script_Audio_MainScript.tocar_Digitando();
        yield return new WaitForSeconds(4);
        criarDialogo_NOAH_DIA2_depoisEscola();

        yield return new WaitForSeconds(2);
        text3Pontinhos.enabled = true;

        yield return new WaitForSeconds(2);
        criarDialogo_SAM_DIA2_depoisEscola();
        text3Pontinhos.enabled = false;

        yield return new WaitForSeconds(2);
        text3Pontinhos.enabled = true;

        yield return new WaitForSeconds(2);
        criarDialogo_SAM_DIA2_depoisEscola();
        btt_Voltar.enabled = true;
        text3Pontinhos.enabled = false;
        current_array_index = 0;
    }
    #endregion

    #region conversa_3
    private void criarDialogo_SAM_DIA3_depoisEscola()
    {
        btts_Dialogue_SAM[0].gameObject.transform.parent = muzzle_Conversa.transform;
        btts_Dialogue_SAM[0].GetComponentInChildren<Text>().text = dialogos_SAM_DIA3_depoisEscola[current_array_index];
        btts_Dialogue_SAM = muzzle_Inicial_SAM.GetComponentsInChildren<Button>();

        rTransform.sizeDelta += new Vector2(0, 40);
        current_array_index++;
    }
    private void criarDialogo_NOAH_DIA3_depoisEscola()
    {
        btts_Dialogue_NOAH[0].gameObject.transform.parent = muzzle_Conversa.transform;
        btts_Dialogue_NOAH[0].GetComponentInChildren<Text>().text = dialogos_SAM_DIA3_depoisEscola[current_array_index];
        btts_Dialogue_NOAH = muzzle_Inicial_NOAH.GetComponentsInChildren<Button>();

        rTransform.sizeDelta += new Vector2(0, 40);
        current_array_index++;
    }
    IEnumerator conversa_SAM_DIA3_depoisEscola()
    {
        btt_Voltar.enabled = false;
        criarDialogo_SAM_DIA3_depoisEscola();

        yield return new WaitForSeconds(0.5f);
        script_Audio_MainScript.tocar_Digitando();
        yield return new WaitForSeconds(4);
        criarDialogo_NOAH_DIA3_depoisEscola();

        yield return new WaitForSeconds(0.5f);
        script_Audio_MainScript.tocar_Digitando();
        yield return new WaitForSeconds(4);
        criarDialogo_NOAH_DIA3_depoisEscola();

        yield return new WaitForSeconds(2);
        text3Pontinhos.enabled = true;

        yield return new WaitForSeconds(2);
        criarDialogo_SAM_DIA3_depoisEscola();
        text3Pontinhos.enabled = false;

        yield return new WaitForSeconds(2);
        text3Pontinhos.enabled = true;

        yield return new WaitForSeconds(2);
        criarDialogo_SAM_DIA3_depoisEscola();
        btt_Voltar.enabled = true;
        text3Pontinhos.enabled = false;
        current_array_index = 0;
    }
    #endregion

}
