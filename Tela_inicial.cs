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
                    Jogador.GetComponent<Input_player>().enabled = false;
                    GameObject.Find("Tela Inicial").transform.Find("Titulo").gameObject.GetComponent<TMP_Text>().text = "Game over!";
                    GameObject.Find("Tela Inicial").transform.Find("Titulo").gameObject.GetComponent<TMP_Text>().enabled = true;
                    GameObject.Find("Tela Inicial").transform.Find("Press_to_Start").gameObject.GetComponent<TMP_Text>().text = "Touch if you wanna retry";
                    GameObject.Find("Tela Inicial").transform.Find("Press_to_Start").gameObject.SetActive(true);

                    foreach(Touch touch in Input.touches){
                        if (touch.phase == TouchPhase.Began){
                            Jogador.GetComponent<Animator>().SetBool("morreu", false);
                            Jogador.GetComponent<BoxCollider2D>().enabled = true;
                            Jogador.GetComponent<Input_player>().enabled = true;
                            GameObject.Find("Tela Inicial").transform.Find("Titulo").gameObject.GetComponent<TMP_Text>().enabled = false;
                            GameObject.Find("Tela Inicial").transform.Find("Press_to_Start").gameObject.SetActive(false);
                            Score = 0;
                            Jogador.transform.position = new Vector2(Jogador.GetComponent<Input_player>().posicao_inicial.x -3, Jogador.GetComponent<Input_player>().posicao_inicial.y);
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

                    // GameObject.Find("Tela Inicial").transform.Find("Panel").gameObject.GetComponent<TMP_Text>().enabled = false;
                    GameObject.Find("Tela Inicial").transform.Find("Titulo").gameObject.GetComponent<TMP_Text>().enabled = false;
                    GameObject.Find("Tela Inicial").transform.Find("Press_to_Start").gameObject.SetActive(false);

                    comecou_jogo = true;
            	}
            }
        }
    }
}
