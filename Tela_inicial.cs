using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Tela_inicial : MonoBehaviour
{
	public GameObject Jogador;
    public GameObject Gravidade;
	public GameObject Spawn_plataformas;
    public int        Score = 0;

    private bool comecou_jogo = false;
    private bool pega_tempo_morte = true;
    private float tempo_morte;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(comecou_jogo){
            if(Jogador.GetComponent<Input_player>().morreu()){
                if(pega_tempo_morte){
                    tempo_morte = Time.time;
                    pega_tempo_morte = false;
                }

                if(Time.time > tempo_morte + 2){
                    GameObject.Find("Game_Over").transform.Find("Panel").gameObject.SetActive(true);
                    GameObject.Find("Game_Over").transform.Find("Game over").gameObject.SetActive(true);
                    GameObject.Find("Game_Over").transform.Find("Wanna_retry").gameObject.SetActive(true);
                    foreach(Touch touch in Input.touches){
                        if (touch.phase == TouchPhase.Began){
                            SceneManager.LoadScene("Cena00");
                            comecou_jogo = true;
                        }
                    }
                }
            }else{
                Score += 1;
                GameObject.Find("Score_display").GetComponent<TMP_Text>().text = Score.ToString();     
            }
        }else{
            foreach(Touch touch in Input.touches){
            	if (touch.phase == TouchPhase.Began){

            		Jogador.GetComponent<Input_player>().enabled = true;
            		Jogador.GetComponent<Animator>().SetBool("comecou", true);
                    Gravidade.GetComponent<Inverte_gravidade>().enabled = true;

            		Spawn_plataformas.transform.GetChild(0).GetComponent<Movimento_plataformas>().enabled = true;

                    GameObject.Find("Tela Inicial").transform.Find("Panel").gameObject.SetActive(false);
                    GameObject.Find("Tela Inicial").transform.Find("Titulo").gameObject.SetActive(false);
                    GameObject.Find("Tela Inicial").transform.Find("Press_to_Start").gameObject.SetActive(false);

                    comecou_jogo = true;
            	}
            }
        }
    }
}
