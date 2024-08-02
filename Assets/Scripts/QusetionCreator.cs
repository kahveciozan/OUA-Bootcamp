using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;

public class QusetionCreator : MonoBehaviour
{
    [SerializeField] private List<string> readyQuestions = new List<string>();
    [SerializeField] private List<string> readyAnswers = new List<string>();

    private Dictionary<string, string> questions = new Dictionary<string, string>();

    [Header("Wall Texts")]
    [SerializeField] private TextMeshProUGUI questionText1;

    public string currentAnswer;

    [Header("Panels")]
    [SerializeField] private GameObject upPanel;
    [SerializeField] private GameObject downPanel;

    [Header("Effects")]
    [SerializeField] private ParticleSystem confettiPS;
    [SerializeField] private AudioSource PlayerAudioSource;

    public AudioClip winnerSound, looserSound; 

    private void Awake()
    {
        CheckPoint.OnAnswered += CheckAnswer;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Add all questions list to the dictionary with the answer
        foreach (string question in readyQuestions)
        {
            questions.Add(question, readyAnswers[readyQuestions.IndexOf(question)]);
        }


        CountDown();
    }

    private void CheckAnswer(string answer)
    {

        int currentAnswerInt = int.Parse(currentAnswer);
        int playerAnswerInt = int.Parse(answer);

        if (currentAnswerInt > playerAnswerInt)
        {
            Debug.Log("Wrong Answer - Increase your answer");

            StartCoroutine(AnimController(upPanel));
            PlayAudio(looserSound);
        }
        else if (currentAnswerInt < playerAnswerInt)
        {
            // TODO: Wrong Answer animation - Decrease your answer
            Debug.Log("Wrong Answer - Decrease your answer");

            StartCoroutine(AnimController(downPanel));
            PlayAudio(looserSound);
        }
        else
        {
            Debug.Log("Correct Answer");
            confettiPS.Play();
            CountDown();
            PlayAudio(winnerSound);
        }

    }

    IEnumerator AnimController(GameObject g)
    {
        g.SetActive(true);
        yield return new WaitForSeconds(5f);
        g.SetActive(false);
    }

    // Choose a random question from the dictionary
    private void RandomQuestionGenerator()
    {
        int randomIndex = Random.Range(0, questions.Count);
        string randomQuestion = readyQuestions[randomIndex];
        string randomAnswer = questions[randomQuestion];
        currentAnswer = randomAnswer;
        Debug.Log(randomQuestion);
        Debug.Log(randomAnswer);

        questionText1.text = randomQuestion;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Player entered the trigger");
            //Compare anwser with the player's answer
            string playerAnswer = other.GetComponentInChildren<TextMeshProUGUI>().text;
            if(playerAnswer == currentAnswer)
            {
                Debug.Log("DOGRU CEVAP");
               
            }
            else
            {
                Debug.Log("YANLIS CEVAP");
            }

            other.GetComponent<Rigidbody>().AddExplosionForce(1000f, other.transform.position, 5f);
            //RandomQuestionGenerator();
        }
    }

    // Count Down 3 ,2,1 and start the game
    private void CountDown()
    {
        Vector3 halScale = new Vector3(0.5f, 0.5f, 0.5f);

        questionText1.transform.DOScale(halScale, 0.5f).SetEase(Ease.OutBounce).OnComplete(() =>
        {
            questionText1.transform.DOScale(Vector3.one, 0.5f).OnComplete(() => 
            { 
                questionText1.text = "2";
                questionText1.transform.DOScale(halScale, 0.5f).SetEase(Ease.OutBounce).OnComplete(() =>
                {
                    questionText1.transform.DOScale(Vector3.one, 0.5f).OnComplete(() =>
                    {
                        questionText1.text = "1";
                        questionText1.transform.DOScale(halScale, 0.5f).SetEase(Ease.OutBounce).OnComplete(() =>
                        {
                            questionText1.transform.DOScale(Vector3.one, 0.5f).OnComplete(() =>
                            {
                                questionText1.text = "Start";
                                questionText1.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.OutBounce).OnComplete(() =>
                                {
                                    questionText1.transform.DOScale(Vector3.one, 0.5f);
                                    RandomQuestionGenerator();
                                });
                            });
                        });
                    });
                });

            });

        });

    }

    // Assign audiclip to the audio source and play it
    private void PlayAudio(AudioClip clip)
    {
        PlayerAudioSource.clip = clip;
        PlayerAudioSource.Play();
    }
}
