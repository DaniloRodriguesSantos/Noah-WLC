using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPensamento : MonoBehaviour {

	[SerializeField] private bool isPensamento_Tipo1;
	[SerializeField] private bool isPensamento_Tipo2;
	private Transform player_Trans;
	[SerializeField] private Transform StartPoint_P2;
	private PensamentoController pensamentoController_Script;

	private void Awake()
	{
		player_Trans = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
		pensamentoController_Script = GameObject.Find ("PensamentoController").GetComponent<PensamentoController> ();
	}

	private void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag ("Player")) {
			if (this.isPensamento_Tipo2) {
				player_Trans.position = StartPoint_P2.position;
			}
			if (this.isPensamento_Tipo1) {
				pensamentoController_Script.voltaPosicao = true;
			}
		}
	}
}
