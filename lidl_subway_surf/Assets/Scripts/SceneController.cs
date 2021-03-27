using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    float score = 0;
    int coin_count = 0;

    [Header("audio")]
    public AudioSource jump;
    public AudioSource slide;
    public AudioSource die;
    public AudioSource move;
    public AudioSource coin;

    [Header("dependencies")]
    public Animator animator;
    public Text score_text;
    public GameObject pause_ui;
    public GameObject play_ui;
    public GameObject die_ui;
    PlayerController controller;

    private void Start()
    {
        controller = GetComponent<PlayerController>();
        play_ui.SetActive(true);
        pause_ui.SetActive(false);
        die_ui.SetActive(false);

        Time.timeScale = 1;
    }

    private void Update()
    {
        animator.SetFloat("x_speed", controller.move_vector.x * 20);

        score += Mathf.Round(transform.position.z / 2) - score;//¯\_(ツ)_/¯
        score_text.text = $"score: {score + coin_count * 20}";
    }

    public void Jump()
    {
        jump.Play();
        animator.SetTrigger("jump");
    }
    public void Slide()
    {
        slide.Play();
        animator.SetTrigger("slide");
    }
    public void Move()
    {
        move.Play();
    }
    public void Die()
    {
        die.Play();
        Time.timeScale = 0;
        die_ui.SetActive(true);
        play_ui.SetActive(false);
        pause_ui.SetActive(false);
    }

    public void Pick_up_coin()
    {
        coin_count += 1;
        coin.Play();
    }


    #region tlacitka
    public void Pause()
    {
        Time.timeScale = 0;
        play_ui.SetActive(false);
        pause_ui.SetActive(true);
    }

    public void UnPause()
    {
        Time.timeScale = 1;
        play_ui.SetActive(true);
        pause_ui.SetActive(false);
    }

    public void Reset()
    {
        SceneManager.LoadScene(0);
    }
    #endregion

}
