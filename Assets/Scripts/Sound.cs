using UnityEngine;
using System.Collections;

public class Sound : MonoBehaviour
{
    private int m_qSamples = 1024;        // array size - number of samples
    private float m_rmsValue;             // sound level - RMS (root mean squared)
    [SerializeField]
    private float m_scaleSample = 2.0f;   // set how much the scale will vary

    private AudioSource m_source;         //audio source

    private float[] m_samples;            //the array - samples we're getting

    private float m_scaleY = 0;           //original scale on y

    public ParticleSystem particleEffect;
    
    // Use this for initialization
    void Start()
    {
        m_source = GetComponent<AudioSource>(); //refernce to audio source
        m_samples = new float[m_qSamples];      //initialise the array of samples
        m_scaleY = transform.localScale.y;      //get the original scale of the object
    }

    // Update is called once per frame
    void Update()
    {
        AnalyzeSound(); //we analyse the sound

        //we scale the object on y based on m_rmsValue
        Vector3 scale = transform.localScale;
        scale.y = m_scaleY + m_scaleSample * m_rmsValue;
        transform.localScale = scale;

        Vector3 pos = new Vector3(0, transform.localScale.y * 0.5f, 0);

        transform.position = pos;

        transform.Rotate(0, m_rmsValue*10, 0 * Time.deltaTime);

        if (m_rmsValue < 0.1f)
        {
            particleEffect.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
        if (m_rmsValue > 0.2f && m_rmsValue < 0.25f)
        {
            particleEffect.startColor = new Color(m_rmsValue * 100, 0, 0, 0.5f);
            particleEffect.Play(true);
        }
        if (m_rmsValue > 0.25f && m_rmsValue < 0.3f)
        {
            particleEffect.startColor = new Color(0, m_rmsValue * 100, 0, 0.5f);
            particleEffect.Play(true);
        }
        if (m_rmsValue > 0.3f && m_rmsValue < 0.4f)
        {
            particleEffect.startColor = new Color(0, 0, m_rmsValue*100, 0.5f);
            particleEffect.Play(true);
        }

        GetComponent<Renderer>().material.color = new Color(0, 0, m_rmsValue * 10);
        

        Debug.Log(m_rmsValue);
    }

    /// <summary>
    /// we analyze the sound and we assign a value to m_rmsValue
    /// </summary>
    private void AnalyzeSound()
    {
        m_source.GetOutputData(m_samples, 0); //we get some samples 

        float sum = 0;

        for (int i = 0; i < m_samples.Length; i++)
        {
            sum += m_samples[i] * m_samples[i]; //sum squared samples
        }

        //formula for sound level
        m_rmsValue = Mathf.Sqrt(sum / m_qSamples);
    }
}
