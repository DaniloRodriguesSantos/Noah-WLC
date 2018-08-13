using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class conversa_MAE : MonoBehaviour
{

    public RectTransform rTransform;

    public GameObject muzzle_Inicial_NOAH;
    public Button[] btts_Dialogue_NOAH;

    public GameObject muzzle_Inicial_MAE;
    public Button[] btts_Dialogue_MAE;

    public GameObject muzzle_Conversa;


    int current_array_index;
    [Space(10)]
    [TextArea(5, 2)]
    public string[] dialogos_MAE_DIA1_depoisEscola;

    [Space(10)]
    [TextArea(5, 2)]
    public string[] dialogos_MAE_DIA2_depoisEscola;

    [Space(10)]
    [TextArea(5, 2)]
    public string[] dialogos_MAE_DIA3_depoisEscola;

    public Text text3Pontinhos;
    public Button btt_Voltar;

    public bool conversa_MAE_1;
    public bool conversa_MAE_2;
    public bool conversa_MAE_3;

    // Use this for initialization
    void Awake()
    {
        btts_Dialogue_NOAH = muzzle_Inicial_NOAH.GetComponentsInChildren<Button>();
        btts_Dialogue_MAE = muzzle_Inicial_MAE.GetComponentsInChildren<Button>();
    }
    private void Start()
    {
        text3Pontinhos.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (conversa_MAE_1)
        {
            StartCoroutine(conversa_SAM_DIA1_depoisEscola());
            conversa_MAE_1 = false;
        }
        if (conversa_MAE_2)
        {
            StartCoroutine(conversa_SAM_DIA2_depoisEscola());
            conversa_MAE_2 = false;
        }
        if (conversa_MAE_3)
        {
            StartCoroutine(conversa_SAM_DIA3_depoisEscola());
            conversa_MAE_3 = false;
        }
    }

    #region conversa_1
    private void criarDialogo_MAE_DIA1_depoisEscola()
    {
        btts_Dialogue_MAE[0].gameObject.transform.parent = muzzle_Conversa.transform;
        btts_Dialogue_MAE[0].GetComponentInChildren<Text>().text = dialogos_MAE_DIA1_depoisEscola[current_array_index];
        btts_Dialogue_MAE = muzzle_Inicial_MAE.GetComponentsInChildren<Button>();

        rTransform.sizeDelta += new Vector2(0, 40);
        current_array_index++;
    }
    private void criarDialogo_NOAH_DIA1_depoisEscola()
    {
        btts_Dialogue_NOAH[0].gameObject.transform.parent = muzzle_Conversa.transform;
        btts_Dialogue_NOAH[0].GetComponentInChildren<Text>().text = dialogos_MAE_DIA1_depoisEscola[current_array_index];
        btts_Dialogue_NOAH = muzzle_Inicial_NOAH.GetComponentsInChildren<Button>();

        rTransform.sizeDelta += new Vector2(0, 40);
        current_array_index++;
    }
    IEnumerator conversa_SAM_DIA1_depoisEscola()
    {
        btt_Voltar.enabled = false;
        criarDialogo_MAE_DIA1_depoisEscola();

        yield return new WaitForSeconds(2);
        text3Pontinhos.enabled = true;

        yield return new WaitForSeconds(2);
        text3Pontinhos.enabled = false;
        criarDialogo_MAE_DIA1_depoisEscola();

        yield return new WaitForSeconds(2);
        text3Pontinhos.enabled = true;

        yield return new WaitForSeconds(2);
        text3Pontinhos.enabled = false;
        criarDialogo_MAE_DIA1_depoisEscola();

        yield return new WaitForSeconds(2);
        text3Pontinhos.enabled = true;

        yield return new WaitForSeconds(2);
        text3Pontinhos.enabled = false;
        criarDialogo_MAE_DIA1_depoisEscola();

        btt_Voltar.enabled = true;
        text3Pontinhos.enabled = false;
        current_array_index = 0;
    }
    #endregion

    #region conversa_2
    private void criarDialogo_MAE_DIA2_depoisEscola()
    {
        btts_Dialogue_MAE[0].gameObject.transform.parent = muzzle_Conversa.transform;
        btts_Dialogue_MAE[0].GetComponentInChildren<Text>().text = dialogos_MAE_DIA2_depoisEscola[current_array_index];
        btts_Dialogue_MAE = muzzle_Inicial_MAE.GetComponentsInChildren<Button>();

        rTransform.sizeDelta += new Vector2(0, 40);
        current_array_index++;
    }
    private void criarDialogo_NOAH_DIA2_depoisEscola()
    {
        btts_Dialogue_NOAH[0].gameObject.transform.parent = muzzle_Conversa.transform;
        btts_Dialogue_NOAH[0].GetComponentInChildren<Text>().text = dialogos_MAE_DIA2_depoisEscola[current_array_index];
        btts_Dialogue_NOAH = muzzle_Inicial_NOAH.GetComponentsInChildren<Button>();

        rTransform.sizeDelta += new Vector2(0, 40);
        current_array_index++;
    }
    IEnumerator conversa_SAM_DIA2_depoisEscola()
    {
        btt_Voltar.enabled = false;
        criarDialogo_MAE_DIA2_depoisEscola();
        text3Pontinhos.enabled = false;

        yield return new WaitForSeconds(2);
        text3Pontinhos.enabled = true;

        yield return new WaitForSeconds(2);
        criarDialogo_MAE_DIA2_depoisEscola();
        text3Pontinhos.enabled = false;

        yield return new WaitForSeconds(2);
        text3Pontinhos.enabled = true;

        yield return new WaitForSeconds(2);
        criarDialogo_MAE_DIA2_depoisEscola();
        text3Pontinhos.enabled = false;

        yield return new WaitForSeconds(2);
        text3Pontinhos.enabled = true;

        yield return new WaitForSeconds(2);
        criarDialogo_MAE_DIA2_depoisEscola();
        text3Pontinhos.enabled = false;

        yield return new WaitForSeconds(2);
        text3Pontinhos.enabled = true;

        yield return new WaitForSeconds(2);
        criarDialogo_MAE_DIA2_depoisEscola();

        btt_Voltar.enabled = true;
        text3Pontinhos.enabled = false;
        current_array_index = 0;
    }
    #endregion

    #region conversa_3
    private void criarDialogo_MAE_DIA3_depoisEscola()
    {
        btts_Dialogue_MAE[0].gameObject.transform.parent = muzzle_Conversa.transform;
        btts_Dialogue_MAE[0].GetComponentInChildren<Text>().text = dialogos_MAE_DIA3_depoisEscola[current_array_index];
        btts_Dialogue_MAE = muzzle_Inicial_MAE.GetComponentsInChildren<Button>();

        rTransform.sizeDelta += new Vector2(0, 40);
        current_array_index++;
    }
    private void criarDialogo_NOAH_DIA3_depoisEscola()
    {
        btts_Dialogue_NOAH[0].gameObject.transform.parent = muzzle_Conversa.transform;
        btts_Dialogue_NOAH[0].GetComponentInChildren<Text>().text = dialogos_MAE_DIA3_depoisEscola[current_array_index];
        btts_Dialogue_NOAH = muzzle_Inicial_NOAH.GetComponentsInChildren<Button>();

        rTransform.sizeDelta += new Vector2(0, 40);
        current_array_index++;
    }
    IEnumerator conversa_SAM_DIA3_depoisEscola()
    {
        btt_Voltar.enabled = false;
        criarDialogo_MAE_DIA3_depoisEscola();

        yield return new WaitForSeconds(2);
        criarDialogo_NOAH_DIA3_depoisEscola();

        yield return new WaitForSeconds(2);
        criarDialogo_NOAH_DIA3_depoisEscola();

        yield return new WaitForSeconds(2);
        text3Pontinhos.enabled = true;

        yield return new WaitForSeconds(2);
        criarDialogo_MAE_DIA3_depoisEscola();
        text3Pontinhos.enabled = false;

        yield return new WaitForSeconds(2);
        text3Pontinhos.enabled = true;

        yield return new WaitForSeconds(2);
        criarDialogo_MAE_DIA3_depoisEscola();
        btt_Voltar.enabled = true;
        text3Pontinhos.enabled = false;
        current_array_index = 0;
    }
    #endregion

}
