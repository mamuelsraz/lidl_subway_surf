using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("running")]
    public float speed;
    public float speed_multiplier_over_time;
    public float switch_lane_speed;
    public float lane_offset;
    public int lane_count; //na každou stranu
    int current_lane;

    [Header("jumping")]
    public float jump_force;
    float y_velocity;
    public float gravity = 10f;
    public bool grounded;
    public LayerMask ignoreMe;

    [Header("sliding")]
    public float collider_size;
    public float slide_time;
    float real_slide_time;
    bool sliding;
    public float slide_jump_down_force; //kvalitní jméno

    [Header("dependencies")]
    public Animator animator;
    CharacterController cc;
    SwipeManager swipe_input;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        swipe_input = GetComponent<SwipeManager>();
        current_lane = 0;
        y_velocity = 0;
    }

    void Update()
    {
        grounded = Physics.CheckSphere(transform.position, 1f, ~ignoreMe); //fuj
        speed += speed_multiplier_over_time * Time.deltaTime;

        Inputs();
        Slide();

        current_lane = Mathf.Clamp(current_lane, -1 * lane_count, lane_count);
        y_velocity -= gravity * Time.deltaTime;
        if (grounded && y_velocity <= 0) y_velocity = -5f;

        Vector3 move_vector = Vector3.forward * speed * Time.deltaTime; //dopredu
        move_vector += Vector3.up * y_velocity * Time.deltaTime; // nahoru
        move_vector += Vector3.left * (transform.position.x - current_lane*lane_offset) * switch_lane_speed * Time.deltaTime; // do strany
        cc.Move(move_vector);

        animator.SetFloat("x_speed", move_vector.x * 20);
    }

    void Inputs()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || swipe_input.SwipeRight) current_lane += 1;
        if (Input.GetKeyDown(KeyCode.LeftArrow) || swipe_input.SwipeLeft) current_lane -= 1;
        if ((Input.GetKeyDown(KeyCode.UpArrow) || swipe_input.SwipeUp) && grounded) 
        {
            Debug.Log("yes");
            animator.SetTrigger("jump");
            y_velocity = jump_force;
            real_slide_time = 0;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || swipe_input.SwipeDown)
        {
            animator.SetTrigger("slide");
            y_velocity = -1 * slide_jump_down_force;
            cc.height = collider_size;
            cc.center = new Vector3(0, collider_size/2, 0);
            real_slide_time = slide_time;
            sliding = true;
        }

        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(0);
    }

    void Slide()
    {
        if (sliding && real_slide_time <= 0)
        {
            cc.height = 1;
            cc.center = new Vector3(0, 0.5f, 0);
        }
        real_slide_time -= Time.deltaTime;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.DrawRay(hit.point, hit.normal * 5, Color.red);
        if (hit.normal.y < 0.4f) SceneManager.LoadScene(0);
    }
}
