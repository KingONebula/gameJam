using UnityEngine;

[System.Serializable]
public class PhysicsController
{
    Rigidbody2D body;
    Timer timer;
    public bool doesRagdoll, lockVelocity;

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
        doesRagdoll = true;
        body.AddForce(force, ForceMode2D.Impulse);

    }
    public void lerpVelocity(Vector3 velocity, float length)
    {
        lockVelocity = true;
        fVelocity = velocity;
        timer.setTimer(length);
    }
    public void updateLerp()
    {
        if (!lockVelocity)
            return;
        timer.timeUpdate();
        body.velocity = Vector2.Lerp(fVelocity, Vector3.zero, timer.getPercent());
        if (body.velocity == Vector2.zero)
        {
            fVelocity = Vector2.zero;
            lockVelocity = false;
        }

    }
    public void cancelLerp()
    {
        fVelocity = Vector2.zero;
        lockVelocity = false;
    }
    public void returnVelocity(Vector2 velocity)
    {
        body.velocity = Vector3.ClampMagnitude(body.velocity, 10);
        if (lockVelocity)
            return;

        if (doesRagdoll)
        {
            fVelocity = body.velocity;
            timer.timeUpdate();
            if (timer.timeEnd)
                doesRagdoll = false;
        }
        if (!doesRagdoll || body.velocity.magnitude < 1)
        {
            fVelocity = Vector2.SmoothDamp(fVelocity, Vector2.zero, ref currentV, 0.25f);
            Vector2 Vfinal = fVelocity + velocity;
            Vfinal.y = body.velocity.y;
            body.velocity = Vfinal;
        }
    }
}