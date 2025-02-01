using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject PrefabBackGround;
    GameObject BackGround;
    [SerializeField] GameObject[] A;
    [SerializeField] Transform Map;
    [SerializeField] Vector2 mapTransform;
    [SerializeField] float jumpvalue;
    [SerializeField] float maxv;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bulletspawner;
    [SerializeField] GameObject DiePanel;
    [SerializeField] GameObject PausePanel;
    [SerializeField] GameObject ScorePanel;
    public TextMeshProUGUI scoresave;
    public TextMeshProUGUI bestsave;
     float scoresaveamount;
     float bestsaveamount=0;
    GameObject bulletPrefab;
    Rigidbody2D rb;
    SpriteRenderer sprite;
    float scoreAmount;
   [SerializeField] float TimeRepeating;
    [SerializeField] float scoreProgress;
    [SerializeField] float shootingCooldown;
    [SerializeField] TextMeshProUGUI txt;
 
    bool canShoot=true;
    // Start is called before the first frame update
    void Start()
    {
        ScorePanel.SetActive(true);
           scoreAmount = 0;
       
        Time.timeScale = 1;
        
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        InvokeRepeating("InstintiateObject", 0.01f, TimeRepeating);
        LoadData();
    }
    public void SaveData()
    {
        scoresaveamount = scoreAmount;
        PlayerPrefs.SetFloat("Score", scoresaveamount);
        if (scoreAmount > bestsaveamount)
        {
            bestsaveamount = scoreAmount;
            PlayerPrefs.SetFloat("Best", bestsaveamount);
        }
        LoadData();
        scoresave.text = scoresaveamount.ToString("F0");
        bestsave.text = bestsaveamount.ToString("F0");
     
    }
    public void LoadData()
    {
        bestsaveamount = PlayerPrefs.GetFloat("Best");
        scoresaveamount = PlayerPrefs.GetFloat("Score");
    }
    public void InstintiateObject()
    {
        if (A[A.Length - 1] == null)
            return;
        else

            BackGround = Instantiate(PrefabBackGround, A[A.Length - 1].transform.position, Quaternion.identity, null);
    }  
    
    // Update is called once per frame
    void Update()
    {
        A = GameObject.FindGameObjectsWithTag("A");
        ChangeValues();
        MoveTransform();
        if (Input.GetKeyDown(KeyCode.L))
            PlayerPrefs.DeleteAll();
        if (Application.isEditor)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Jump();
            else if (Input.GetKeyDown(KeyCode.Mouse0))
                Shoot();
        }
    }
    public void timeChanger(int time)
    {
        Time.timeScale = time;
      
    }
    private void FixedUpdate()
    {
        RigidbodyMove();
    }
    private void MoveTransform()
    {
        Map.Translate(mapTransform * Time.deltaTime);
        transform.Translate(mapTransform * Time.deltaTime);

    }
    private void RigidbodyMove()
    {
        float maxVelocity = maxv;
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocity);
    }
    public void Jump()=>rb.velocity = new Vector2(rb.velocity.x, jumpvalue);
    public void Shoot()
    {
        if (canShoot)
        {
            bulletPrefab = Instantiate(bullet, bulletspawner.transform.position, Quaternion.identity, null);
            StartCoroutine(Shooting());
        }
        

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Die"))
            Die();
            
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Die"))
            Die();
    }
    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }
    public void Die()
    {

        ScorePanel.SetActive(false);
        DiePanel.SetActive(true);
        PausePanel.SetActive(false);
        Time.timeScale = 0;
        sprite.enabled = false;
        SaveData();
       
        
    }

    private void ChangeValues()
    {
        scoreAmount +=scoreProgress*Time.deltaTime;
        txt.text = scoreAmount.ToString("F0");

    }
    IEnumerator Shooting()
        {
        canShoot = false;
        yield return new WaitForSeconds(shootingCooldown);
        canShoot = true;

    }
}
