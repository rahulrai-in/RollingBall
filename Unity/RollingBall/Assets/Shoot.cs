using UnityEngine;
using UnityEngine.VR.WSA.Input;

public class Shoot : MonoBehaviour
{
    private readonly float ForceMagnitude = 300f;

    private GestureRecognizer gestureRecognizer;

    private AudioSource audioSource;

    private AudioClip shootClip;

    private void ShootBall(InteractionSourceKind source, int tapCount, Ray headRay)
    {
        var ball = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        ball.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        var rigidBody = ball.AddComponent<Rigidbody>();
        rigidBody.mass = 0.5f;
        rigidBody.position = this.transform.position;
        var transformForward = this.transform.forward;
        transformForward = Quaternion.AngleAxis(-10, this.transform.right) * transformForward;
        rigidBody.AddForce(transformForward * this.ForceMagnitude);

        this.audioSource.clip = this.shootClip;
        this.audioSource.Play();
    }

    // Use this for initialization
    private void Start()
    {
        this.gestureRecognizer = new GestureRecognizer();
        this.gestureRecognizer.TappedEvent += this.ShootBall;
        this.gestureRecognizer.StartCapturingGestures();

        this.audioSource = this.gameObject.AddComponent<AudioSource>();
        this.audioSource.playOnAwake = false;
        this.audioSource.spatialize = true;
        this.audioSource.spatialBlend = 1.0f;
        this.audioSource.dopplerLevel = 0.0f;
        this.audioSource.rolloffMode = AudioRolloffMode.Logarithmic;
        this.audioSource.maxDistance = 20f;

        this.shootClip = Resources.Load<AudioClip>("Pitch");
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void OnShoot()
    {
        this.ShootBall(InteractionSourceKind.Voice, 1, new Ray());
    }
}