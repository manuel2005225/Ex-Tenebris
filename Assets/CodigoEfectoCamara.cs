using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DistorsionJugador : MonoBehaviour
{
    public Volume postVolume;
    private LensDistortion distortion;

    void Start()
    {
        if (postVolume.profile.TryGet(out distortion))
        {
            distortion.intensity.Override(0f);
            distortion.active = false;
        }
    }

    public void ActivarDistorsion(float intensidad = -0.5f)
    {
        if (distortion != null)
        {
            distortion.active = true;
            distortion.intensity.Override(intensidad);
        }
    }

    public void DesactivarDistorsion()
    {
        if (distortion != null)
        {
            distortion.active = false;
        }
    }
}
