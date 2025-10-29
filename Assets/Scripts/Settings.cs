using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Toggle ChromaticToggle;
    public Toggle VignetteToggle;
    public Toggle GrainToggle;

    public Volume volume;
    private VolumeProfile profile;

    private void Start()
    {
        profile = volume.sharedProfile;
    }

    public void ChromaticAberration()
    {
        if (!profile.TryGet<ChromaticAberration>(out var chromaticAbr))
        {
            chromaticAbr = profile.Add<ChromaticAberration>(false);
        }

        chromaticAbr.active = ChromaticToggle.isOn;
    }

    public void Vignette()
    {
        if (!profile.TryGet<Vignette>(out var vignette))
        {
            vignette = profile.Add<Vignette>(false);
        }

        vignette.active = VignetteToggle.isOn;
    }

    public void FilmGrain()
    {
        if (!profile.TryGet<FilmGrain>(out var filmGrain))
        {
            filmGrain = profile.Add<FilmGrain>(false);
        }

        filmGrain.active = GrainToggle.isOn;
    }
}
