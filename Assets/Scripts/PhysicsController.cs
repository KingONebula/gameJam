using UnityEngine;

[System.Serializable]
public class PhysicsController
{
    Rigidbody2D body;
    Timer timer;

    Vector2 fVelocity, currentV;
    public PhysicsController(Rigidbody2D body)
    {
        timer = new Timer();
        currentV = Vector3.zero;
        this.body = body;
    }

    public void AddForce(Vector3 force)
    {
        timer.setTimer(0.5f);
        cancelLerp();
        body.AddForce(force, ForceMode2D.Impulse);

    }
    public void lerpVelocity(Vector3 velocity, float length)
    {
        fVelocity = velocity;
        timer.setTimer(length);
    }
    public void updateLerp()
    {
        timer.timeUpdate();
        body.velocity = Vector2.Lerp(fVelocity, Vector3.zero, timer.getPercent());
        if (body.velocity == Vector2.zero)
        {
            fVelocity = Vector2.zero;
        }

    }
    public void cancelLerp()
    {
        fVelocity = Vector2.zero;
    }
    public void returnVelocity(Vector2 velocity)
    {
        body.velocity = Vector3.ClampMagnitude(body.velocity, 10);
        fVelocity = Vector2.SmoothDamp(fVelocity, Vector2.zero, ref currentV, 0.5f);
        Vector2 Vfinal = fVelocity + velocity;
        Vfinal.y = body.velocity.y;
        body.velocity = Vfinal;
    }
}