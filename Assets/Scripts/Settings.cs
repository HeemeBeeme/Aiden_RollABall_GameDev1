using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Toggle ChromaticToggle;
    public Toggle VignetteToggle;

    private float VignetteStandard = 0.2f;

    public bool LoadSettings = true;

    public Volume GlobalVolume;
    private VolumeProfile profile;

    private void Start()
    {
        profile = GlobalVolume.sharedProfile;

        ChromaticAberration();
        Vignette();

        LoadSettings = false;
    }

    public void ChromaticAberration()
    {
        if (!profile.TryGet<ChromaticAberration>(out var chromaticAbr))
        {
            chromaticAbr = profile.Add<ChromaticAberration>(false);
        }

        if(!LoadSettings)
        {
            chromaticAbr.active = ChromaticToggle.isOn;
        }
        else
        {
            ChromaticToggle.isOn = chromaticAbr.active;
        }
    }

    public void Vignette()
    {
        if (!profile.TryGet<Vignette>(out var vignette))
        {
            vignette = profile.Add<Vignette>(false);
        }

        vignette.color.Override(Color.black);
        vignette.intensity.Override(VignetteStandard);

        if (!LoadSettings)
        {
            vignette.active = VignetteToggle.isOn;
        }
        else
        {
            VignetteToggle.isOn = vignette.active;
        }
    }
}
