using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tela_inicial : MonoBehaviour
{
	public GameObject Jogador;
    public GameObject Gravidade;
	public GameObject Spawn_plataformas;
    public int        Score = 0;

    private bool comecou_jogo = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(comecou_jogo == false){
            foreach(Touch touch in Input.touches){
            	if (touch.phase == TouchPhase.Began){

            		Jogador.GetComponent<Input_player>().enabled = true;
            		Jogador.GetComponent<Animator>().SetBool("comecou", true);
                    Gravidade.GetComponent<Inverte_gravidade>().enabled = true;

            		Spawn_plataformas.transform.GetChild(0).GetComponent<Movimento_plataformas>().enabled = true;

                    GameObject.Find("Tela Inicial").SetActive(false);
                    
                    comecou_jogo = true;
            	}
            }
        }else{
            Score += (int)Time.time;
            GameObject.Find("Score_display").GetComponent<TMP_Text>().text = Score.ToString();
        }
    }
}
